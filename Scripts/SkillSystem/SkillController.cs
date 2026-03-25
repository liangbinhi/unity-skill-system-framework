using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using Common;

namespace SkillSystem
{
    public class SkillController : MonoBehaviour
    {
        [Header("技能配置")]
        [SerializeField] private List<SkillData> skills = new();

        private readonly Dictionary<int, float> cooldownMap = new();
        private SkillState currentState = SkillState.Idle;

        public SkillState CurrentState => currentState;

        public void CastSkill(int skillId, GameObject target)
        {
            if (currentState != SkillState.Idle) return;

            SkillData skillData = skills.Find(s => s.skillId == skillId);
            if (skillData == null)
            {
                Debug.LogWarning($"未找到技能，skillId={skillId}");
                return;
            }

            if (cooldownMap.TryGetValue(skillId, out float nextReadyTime) && Time.time < nextReadyTime)
            {
                Debug.Log($"技能 {skillData.skillName} 还在冷却中");
                return;
            }

            StartCoroutine(CastSkillRoutine(skillData, target));
        }

        private IEnumerator CastSkillRoutine(SkillData skillData, GameObject target)
        {
            currentState = SkillState.Cast;

            GameEventCenter.Publish(GameEvent.SkillCast, skillData.skillName);

            yield return new WaitForSeconds(skillData.castTime);

            currentState = SkillState.Effect;

            ISkillEffect effect = CreateEffect(skillData.effectType);
            effect?.Execute(gameObject, target, skillData);

            SpawnEffect(skillData);

            currentState = SkillState.Cooldown;
            cooldownMap[skillData.skillId] = Time.time + skillData.cooldown;

            yield return new WaitForSeconds(skillData.cooldown);

            currentState = SkillState.Idle;
        }

        private ISkillEffect CreateEffect(SkillEffectType effectType)
        {
            return effectType switch
            {
                SkillEffectType.Damage => new DamageEffect(),
                _ => null
            };
        }

        private void SpawnEffect(SkillData skillData)
        {
            if (skillData.effectPrefab == null) return;

            GameObject effectObj = ObjectPoolManager.Instance.Get(skillData.effectPrefab.name, skillData.effectPrefab);
            effectObj.transform.position = transform.position + transform.forward * 1.5f;
            effectObj.SetActive(true);

            StartCoroutine(ReleaseEffect(effectObj, 1.0f));
        }

        private IEnumerator ReleaseEffect(GameObject effectObj, float delay)
        {
            yield return new WaitForSeconds(delay);
            ObjectPoolManager.Instance.Release(effectObj.name.Replace("(Clone)", "").Trim(), effectObj);
        }
    }
}