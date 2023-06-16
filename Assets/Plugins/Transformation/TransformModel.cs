using System;
using UnityEngine;

namespace Transformation
{
    public sealed class TransformModel : IReadOnlyTransform
    {
        #region Events
        public event Action PositionChanged;
        public event Action RotationChanged;
        #endregion

        #region Fields
        private Vector3 _position;
        private Vector3 _localPosition;
        private Quaternion _rotation;
        #endregion
        
        #region Properties
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                PositionChanged?.Invoke();
            }
        }

        public Vector3 LocalPosition => Parent != null ? InversePoint(Parent, Position) : Position;

        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                RotationChanged?.Invoke();
            }
        }

        public Quaternion LocalRotation => Parent != null ? Quaternion.Inverse(Parent.Rotation) * Rotation : Rotation;

        public Vector3 Right
        {
            get => Rotation * Vector3.right;
            set => Rotation = Quaternion.FromToRotation(Vector3.right, value);
        }
        
        public Vector3 Up
        {
            get => Rotation * Vector3.up;
            set => Rotation = Quaternion.FromToRotation(Vector3.up, value);
        }
        
        public Vector3 Forward
        {
            get => Rotation * Vector3.forward;
            set => Rotation = Quaternion.LookRotation(value);
        }

        public TransformModel Parent { get; private set; }
        #endregion

        public TransformModel(Vector3 position, Quaternion rotation, TransformModel parent = null)
        {
            Position = position;
            Rotation = rotation;
            Parent = parent;
        }

        #region Transformation Methods
        public void Rotate(Vector3 eulers) => Rotation *= Quaternion.Euler(eulers);
        
        public void Translate(Vector3 translation) => Position += translation;
        #endregion

        public static Vector3 InversePoint(TransformModel relative, Vector3 position)
        {
            var offset = position - relative.Position;

            var inverseRotation = Quaternion.Inverse(relative.Rotation);
            return inverseRotation * offset;
        }

        public static implicit operator TransformModel(Transform transform) =>
            new(transform.position, transform.rotation);
    }
}
