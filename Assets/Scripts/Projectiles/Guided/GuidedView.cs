using UnityEngine;
using UnityEngine.Events;

namespace Projectiles.Guided
{
    public class GuidedView : ProjectileView
    {
        public event UnityAction Moving;
        
        private Transform _transform;

        #region Properties
        public Vector3 Position => SelfTransform.position;
        
        public Transform SelfTransform => 
            _transform ? _transform : _transform = transform;
        #endregion
        
        private void Update() => Moving?.Invoke();

        public override void DestroyProjectile() => gameObject.SetActive(false);
    }
}
