using UnityEngine;

namespace EventSystem
{
    public class SkillHitEventData
    {
        public GameObject caster;
        public GameObject target;
        public float damage;
        public string skillName;
    }
}