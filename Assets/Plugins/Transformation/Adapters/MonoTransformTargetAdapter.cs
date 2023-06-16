using UnityEngine;
using Transformation.Targets;

namespace Transformation.Adapters
{
    public class MonoTransformTargetAdapter : ITarget
    {
        private readonly Transform _transform;
			
        public MonoTransformTargetAdapter(Transform transform) => _transform = transform;

        public bool IsActive => _transform && _transform.gameObject.activeSelf;

        public Vector3 Position => _transform.position;
        
        public Quaternion Rotation => _transform.rotation;
    }
}
