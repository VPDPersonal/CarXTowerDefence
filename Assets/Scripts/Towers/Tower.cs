using System;
using Monsters;
using UnityEngine;
using System.Linq;
using Transformation;
using Towers.Detectors;
using Transformation.Targets;

namespace Towers
{
    public abstract class Tower
    {
        public event Action Shot;

        #region Fields
        private float _timer;
        protected readonly ITarget shootPoint;
        protected readonly TransformModel transform;
        private readonly MonsterDetector _detector;
        #endregion

        #region Properties
        public IReadOnlyTransform Transform => transform;

        public virtual float AttackRadius { get; }
        
        public virtual float ShootInterval { get; }
        #endregion

        protected Tower(
            TransformModel transform,
            MonsterDetector detector,
            ITarget shootPoint,
            ITowerData data)
        {
            _timer = 0;
            this.transform = transform;
            this.shootPoint = shootPoint;
            this._detector = detector;

            AttackRadius = data.AttackRadius;
            ShootInterval = data.ShootInterval;
        }

        public void Update(float deltaTime)
        {
            _timer -= deltaTime;
            if (_detector.Monsters.Count <= 0) return;
            
            var monster = GetNearestMonster();
            UpdateBeforeAttack(deltaTime, monster);
            
            if (_timer > 0) return;
            if (GetDirectionToMonster(monster).magnitude > AttackRadius) 
                return;

            Shoot(monster);
            Shot?.Invoke();
            ResetTimer();
        }

        protected Vector3 GetDirectionToMonster(Monster monster)
        {
            var direction = monster.Transform.Position - shootPoint.Position;
            direction.y = 0;
            return direction;
        }

        protected virtual void UpdateBeforeAttack(float deltaTime, Monster monster) { }
        
        protected abstract void Shoot(Monster monster);

        private Monster GetNearestMonster() =>
            _detector.Monsters.OrderBy(monster => Vector3.Distance(monster.Transform.Position, shootPoint.Position)).First();

        private void ResetTimer() => _timer = ShootInterval;
    }
}
