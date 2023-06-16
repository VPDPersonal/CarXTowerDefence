using System;
using UnityEngine;

namespace Projectiles.Cannon.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonPhysics : MonoBehaviour, ICannonPhysics
    {
        public event Action Collide;
        
        private Rigidbody _rigidbody;

        #region Properties
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
        #endregion

        public void AddForce(Vector3 force, ForceMode forceMode) =>
            SelfRigidbody.AddForce(force, forceMode);

        private void OnCollisionEnter(Collision other) =>
            Collide?.Invoke();
    }
}
