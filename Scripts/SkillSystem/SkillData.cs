using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Skill System/Skill Data")]
    public class SkillData : ScriptableObject
    {
        [Header("ª˘¥°≈‰÷√")]
        public int skillId;
        public string skillName;
        public float damage;
        public float cooldown;
        public float castTime;

        [Header("–ßπ˚≈‰÷√")]
        public SkillEffectType effectType;
        public GameObject effectPrefab;
    }

    public enum SkillEffectType
    {
        Damage,
        Heal,
        Buff,
        KnockBack
    }
}