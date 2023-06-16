using System;
using UnityEngine;

namespace Transformation
{
    public interface IReadOnlyTransform : IPosition, IRotation
    {
        #region Events
        public event Action PositionChanged;
        public event Action RotationChanged;
        #endregion

        #region Properties
        public Vector3 Right { get; }
        
        public Vector3 Up { get; }
        
        public Vector3 Forward { get; }
        #endregion
    }
}
