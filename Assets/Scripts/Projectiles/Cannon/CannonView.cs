using UnityEngine;
using Projectiles.Cannon.Physics;
using UnityEngine.Events;

namespace Projectiles.Cannon
{
    [RequireComponent(typeof(CannonPhysics))]
    public class CannonView : ProjectileView
    {
        public event UnityAction<Vector3> Launched;
        private CannonPhysics _cannonPhysics;

        public CannonPhysics Physics => 
            _cannonPhysics ? _cannonPhysics : _cannonPhysics = GetComponent<CannonPhysics>();

        public void Launch(Vector3 direction)
        {
            Launched?.Invoke(direction);
        }
        
        public override void DestroyProjectile() => gameObject.SetActive(false);
    }
}
