using Healths;
using UnityEngine;
using UnityEngine.Events;

namespace Monsters.Health
{
    [DisallowMultipleComponent]
    public abstract class MonsterHealthView : MonoBehaviour, IDamageable
    {
        public event UnityAction<int> DamageTaking;

        public void TakeDamage(int damage) =>
            DamageTaking?.Invoke(damage);
        
        #region Set Hp Methods
        public abstract void SetHp(int hp);

        public abstract void SetMaxHp(int maxHp);
        #endregion

        public abstract void Die();
    }
}
