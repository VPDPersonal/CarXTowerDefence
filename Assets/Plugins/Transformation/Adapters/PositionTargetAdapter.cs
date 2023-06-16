using UnityEngine;
using Transformation.Targets;

namespace Transformation.Adapters
{
    public class PositionTargetAdapter : ITarget
    {
        private readonly IPosition _position;
			
        public PositionTargetAdapter(IPosition position) => _position = position;

        #region Properties
        public bool IsActive => _position != null;

        public Vector3 Position => _position.Position;
        
        public Quaternion Rotation => Quaternion.identity;
        #endregion
    }
}
