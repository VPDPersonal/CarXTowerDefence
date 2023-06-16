using UnityEngine;

namespace Transformation.Targets
{
    public class SimpleTarget : MonoTarget
    {
        private Transform _transform;

        #region Properties
        public override Vector3 Position => SelfTransform.position;
        
        public override Quaternion Rotation => SelfTransform.rotation;

        private Transform SelfTransform => _transform ? _transform : _transform = transform;
        #endregion
    }
}
