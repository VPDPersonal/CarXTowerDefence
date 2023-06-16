using System;
using UnityEngine;
using Transformation;
using Monsters.Health;
using Transformation.Movements;

namespace Monsters
{
	public class Monster
	{
		#region Fields
		private float _speed;
		
		public readonly MonsterHealth Health;
		private readonly TargetMovement _movement;
		private readonly TransformModel _transform;
		#endregion

		#region Properties
		public virtual float Speed
		{
			get => _speed;
			protected set => _speed = value > 0 ? _speed : 0;
		}

		public IReadOnlyTransform Transform => _transform;

		public Vector3 Direction => _movement.Direction(Transform.Position);
		#endregion

		public Monster(
			float speed,
			TargetMovement movement,
			MonsterHealth health,
			TransformModel transform)
		{
			if (speed < 0) throw new ArgumentException($"Speed can't be less than 0. Speed = {speed}");
		
			_speed = speed;
			Health = health;
			_movement = movement;
			_transform = transform;
		}

		public void Move(float deltaTime)
		{
			if (Health.IsDead) return;
			_transform.Position = _movement.Move(_transform.Position, Speed * deltaTime);
		}
	}
}
