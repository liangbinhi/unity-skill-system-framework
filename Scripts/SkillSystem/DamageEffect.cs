using UnityEngine;
using CombatSystem;
using EventSystem;

namespace SkillSystem
{
    public class DamageEffect : ISkillEffect
    {
        public void Execute(GameObject caster, GameObject target, SkillData skillData)
        {
            if (target == null || skillData == null) return;

            HealthComponent health = target.GetComponent<HealthComponent>();
            if (health == null) return;

            health.TakeDamage(skillData.damage);

            GameEventCenter.Publish(GameEvent.SkillHit, new SkillHitEventData
            {
                caster = caster,
                target = target,
                damage = skillData.damage,
                skillName = skillData.skillName
            });
        }
    }
}