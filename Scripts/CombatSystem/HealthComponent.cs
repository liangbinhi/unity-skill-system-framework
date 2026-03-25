using UnityEngine;
using EventSystem;

namespace CombatSystem
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        private float currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"{gameObject.name} 受到 {damage} 点伤害，剩余血量：{currentHealth}");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            currentHealth = 0;
            Debug.Log($"{gameObject.name} 已死亡");

            GameEventCenter.Publish(GameEvent.CharacterDead, gameObject);
        }
    }
}