using Transformation;
using Transformation.Movements;

namespace Projectiles.Guided
{
    public class Guided : Projectile
    {
        #region Fields
        private readonly TargetMovement _movement;
        private readonly TransformModel _transform;
        #endregion
        
        public IReadOnlyTransform Transform => _transform;

        public Guided(
            IGuidedData data,
            TargetMovement movement,
            TransformModel transform) : base(data)
        {
            _movement = movement;
            _transform = transform;
        }

        public void Move(float deltaTime)
        {
            if (IsDestroy) return;
            
            _transform.Position = _movement.Move(_transform.Position, Speed * deltaTime);
            if (!_movement.CanMove) DestroyProjectile();
        }
    }
}
