using System;
using UnityEngine;

namespace Projectiles.Cannon.Physics
{
    public interface ICannonPhysics
    {
        public event Action Collide;
        
        public void AddForce(Vector3 force, ForceMode forceMode);
    }
}
