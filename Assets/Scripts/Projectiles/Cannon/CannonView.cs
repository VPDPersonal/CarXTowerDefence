using UnityEngine;
using Projectiles.Cannon.Physics;
using UnityEngine.Events;

namespace Projectiles.Cannon
{
    [RequireComponent(typeof(CannonPhysics))]
    public class CannonView : ProjectileView
    {
        private CannonPhysics _cannonPhysics;

        public CannonPhysics Physics => 
            _cannonPhysics ? _cannonPhysics : _cannonPhysics = GetComponent<CannonPhysics>();

        public override void DestroyProjectile() => gameObject.SetActive(false);
    }
}
