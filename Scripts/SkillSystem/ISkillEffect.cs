using UnityEngine;

namespace SkillSystem
{
    public interface ISkillEffect
    {
        void Execute(GameObject caster, GameObject target, SkillData skillData);
    }
}