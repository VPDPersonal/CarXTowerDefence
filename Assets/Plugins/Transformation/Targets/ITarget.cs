namespace Transformation.Targets
{
    public interface ITarget : IPosition, IRotation
    {
        public bool IsActive { get; }
    }
}
