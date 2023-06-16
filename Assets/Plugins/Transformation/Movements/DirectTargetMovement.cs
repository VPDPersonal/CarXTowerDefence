using UnityEngine;
using Transformation.Targets;

namespace Transformation.Movements
{
    public sealed class DirectTargetMovement : TargetMovement
    {
        public DirectTargetMovement(ITarget target) : base(target) { }

        public override Vector3 Move(Vector3 currentPosition, float speed)
        {
            if (!CanMove) return currentPosition;
            
            currentPosition += Direction(currentPosition) * speed;
            return currentPosition;
        }
        
        public override Vector3 Direction(Vector3 currentPosition) => (Target.Position - currentPosition).normalized;
    }
}
