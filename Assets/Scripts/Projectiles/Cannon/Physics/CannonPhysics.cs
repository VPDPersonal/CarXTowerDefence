using System;
using UnityEngine;

namespace Projectiles.Cannon.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonPhysics : MonoBehaviour, ICannonPhysics
    {
        public event Action Collide;
        
        private Rigidbody _rigidbody;
        
        private Rigidbody SelfRigidbody => 
            _rigidbody ? _rigidbody : _rigidbody = GetComponent<Rigidbody>();

        public Vector3 Velocity
        {
            get => SelfRigidbody.velocity;
            set => SelfRigidbody.velocity = value;
        }

        public Vector3 AngularVelocity
        {
            get => SelfRigidbody.angularVelocity;
            set => SelfRigidbody.angularVelocity = value;
        }

        public void AddForce(Vector3 force, ForceMode forceMode)
        {
            // SelfRigidbody.velocity = Vector3.zero;
            SelfRigidbody.velocity = force;
            // SelfRigidbody.AddForce(force, forceMode);
        }

        private void OnCollisionEnter(Collision other) =>
            Collide?.Invoke();
    }
}
