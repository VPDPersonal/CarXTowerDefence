using UnityEngine;

namespace Transformation.Movements
{
    public interface IMovement
    {
        public bool CanMove { get; }
        
        public Vector3 Move(Vector3 currentPosition, float speed);
    }
}
