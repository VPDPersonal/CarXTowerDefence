using UnityEngine;
using Projectiles.Cannon.Physics;

namespace Projectiles.Cannon
{
    public class Cannon : Projectile
    {
        protected readonly ICannonPhysics Physics;

        #region Constructor And Destructor
        public Cannon(
            ICannonData data, 
            ICannonPhysics physics) :
            base(data)
        {
            Physics = physics;
            Physics.Collide += OnCollide;
        }

        ~Cannon()
        {
            if (Physics != null) Physics.Collide -= OnCollide;
        }
        #endregion

        public void Launch(Vector3 direction) => Physics.AddForce(direction, ForceMode.VelocityChange);

        private void OnCollide() => DestroyProjectile();
    }
}
