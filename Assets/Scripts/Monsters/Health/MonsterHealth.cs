using System;
using Healths;

namespace Monsters.Health
{
    public class MonsterHealth : IDamageable
    {
        #region Events
        public event Action Died;
        public event Action HpChanged;
        public event Action MaxHpChanged;
        #endregion

        #region Fields
        private int _hp;
        private int _maxHp;
        #endregion
        
        #region Properties
        public bool IsDead { get; private set; }

        public int MaxHp
        {
            get => _maxHp;
            protected set
            {
                _maxHp = value < 0 ? 0 : value;
                MaxHpChanged?.Invoke();
            }
        }
        
        public int Hp
        {
            get => _hp;
            protected set
            {
                _hp = value < 0 ? 0 : value;
                HpChanged?.Invoke();
            }
        }
        #endregion

        public MonsterHealth(int hp)
        {
            if (hp < 0) throw new ArgumentException($"Hp can't be less than 0. Hp = {hp}");
            MaxHp = hp;
            _hp = MaxHp;
        }

        #region Take Damage Methods
        public void TakeDamage(int damage)
        {
            if (IsDead) return;
            if (damage < 0) throw new ArgumentException($"Damage can't be less than 0. Damage = {damage}");
            
            Hp -= AffectDamage(damage);
            if (Hp == 0) Die();
        }

        protected virtual int AffectDamage(int damage) => damage;
        #endregion

        private void Die()
        {
            IsDead = true;
            Died?.Invoke();
        }
    }
}
