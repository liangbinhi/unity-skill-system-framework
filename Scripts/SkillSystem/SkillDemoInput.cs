using UnityEngine;

namespace SkillSystem
{
    public class SkillDemoInput : MonoBehaviour
    {
        [SerializeField] private SkillController skillController;
        [SerializeField] private GameObject target;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                skillController.CastSkill(1, target);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                skillController.CastSkill(2, target);
            }
        }
    }
}