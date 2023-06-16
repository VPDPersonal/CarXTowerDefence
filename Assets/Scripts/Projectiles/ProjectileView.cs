using Healths;
using UnityEngine;
using UnityEngine.Events;

namespace Projectiles
{
    public abstract class ProjectileView : MonoBehaviour
    {
        public event UnityAction<IDamageable> DamageMaking; 
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out var monster)) return;
            DamageMaking?.Invoke(monster);
        }

        public abstract void DestroyProjectile();
    }
}
