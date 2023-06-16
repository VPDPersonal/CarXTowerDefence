using UnityEngine;

namespace Transformation.Targets
{
    public abstract class MonoTarget : MonoBehaviour, ITarget
    {
        public virtual bool IsActive => gameObject.activeSelf;
        
        public abstract Vector3 Position { get; }
        public abstract Quaternion Rotation { get; }
    }
}
