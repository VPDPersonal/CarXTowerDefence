using System;
using Healths;

namespace Projectiles
{
    public abstract class Projectile
    {
        #region Events
        public event Action DamageMade;
        public event Action Destroyed;
        #endregion

        #region Properties
        public virtual int Damage { get; }
        
        public virtual float Speed { get; }
        
        public bool IsDestroy { get; private set; }
        #endregion

        protected Projectile(IProjectileData data)
        {
            if (data.Speed < 0) throw new ArgumentException($"Speed can't be less than 0. Speed = {data.Speed}");
            if (data.Damage < 0) throw new ArgumentException($"Damage can't be less than 0. Damage = {data.Damage}");
            
            Speed = data.Speed;
            Damage = data.Damage;
        }

        #region Damage Methods
        public void MakeDamage(IDamageable damageable)
        {
            if (IsDestroy) return;
            
            damageable.TakeDamage(Damage);
            AfterMakeDamage();
            DamageMade?.Invoke();

            DestroyProjectile();
        }
        
        protected virtual void AfterMakeDamage() { }
        #endregion

        protected void DestroyProjectile()
        {
            IsDestroy = true;
            Destroyed?.Invoke();
        }
    }
}
