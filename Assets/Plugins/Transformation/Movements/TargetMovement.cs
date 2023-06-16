using UnityEngine;
using Transformation.Targets;

namespace Transformation.Movements
{
    public abstract class TargetMovement : IMovement
    {
        protected readonly ITarget Target;
        
        public virtual bool CanMove => Target is { IsActive: true };

        protected TargetMovement(ITarget target) => Target = target;

        public abstract Vector3 Move(Vector3 currentPosition, float speed);

        public abstract Vector3 Direction(Vector3 currentPosition);
    }
}
