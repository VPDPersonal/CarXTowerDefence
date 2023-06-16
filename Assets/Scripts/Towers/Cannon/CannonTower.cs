using System;
using Monsters;
using MVP.Factory;
using UnityEngine;
using Transformation;
using Towers.Detectors;
using Transformation.Targets;

namespace Towers.Cannon
{
    public class CannonTower : Tower
    {
        public event Action TowerRotated;

        #region Fields
        private readonly float _rotationSpeed;
        private readonly IFactoryModel<Projectiles.Cannon.Cannon, FactoryParam<Vector3>> _cannonFactory;
        #endregion
        
        public CannonTower(
            TransformModel transform, 
            MonsterDetector detector, 
            ITarget shootPoint, 
            ICannonTowerData data,
            IFactoryModel<Projectiles.Cannon.Cannon, FactoryParam<Vector3>> cannonFactory) : base(transform, detector, shootPoint, data)
        {
            _cannonFactory = cannonFactory;
            _rotationSpeed = data.RotationSpeed;
        }

        protected override void UpdateBeforeAttack(float deltaTime, Monster monster)
        {
            RotateTower(deltaTime, monster);
        }

        private void RotateTower(float deltaTime, Monster monster)
        {
            var direction = GetDirectionToMonster(monster);
            transform.Rotation = Quaternion.Lerp(transform.Rotation, Quaternion.LookRotation(direction),
                _rotationSpeed * deltaTime);
            TowerRotated?.Invoke();
        }

        protected override void Shoot(Monster monster)
        {
            var cannon = _cannonFactory.Create(shootPoint.Position);

            var monsterSpeed = monster.Direction * monster.Speed;
            monsterSpeed.y = 0;

            var cannonSpeed = GetCannonSpeed(cannon.Speed, monster.Speed);
            var hitPoint = GetHitPoint(monster, monsterSpeed, cannonSpeed, out var time);
            var direction = GetProjectileSpeed(hitPoint, cannonSpeed, time);

            cannon.Launch(direction);
        }

        private static float GetCannonSpeed(float cannonSpeed, float monsterSpeed)
        {
            if (cannonSpeed == monsterSpeed) return cannonSpeed + 0.1f;
            return cannonSpeed;
        }

        private Vector3 GetHitPoint(Monster monster, Vector3 monsterSpeed, float cannonSpeed, out float time)
        {
            var direction = monster.Transform.Position - shootPoint.Position;
            direction.y = 0;
            monsterSpeed.y = 0;

            var a = Vector3.SqrMagnitude(monsterSpeed) - cannonSpeed * cannonSpeed;
            var b = 2 * Vector3.Dot(monsterSpeed, direction);
            var c = Vector3.SqrMagnitude(direction);
            var d = Mathf.Sqrt((b * b) - 4 * a * c);

            var time1 = (-b + d) / (2 * a);
            var time2 = (-b - d) / (2 * a);
            time = Mathf.Max(time1, time2);  

            return monster.Transform.Position + monsterSpeed * time;
        }

        private Vector3 GetProjectileSpeed(Vector3 hitPoint, float speed, float time)
        {
            var direction = hitPoint - shootPoint.Position;
            direction.y = 0;

            var antiGravity = -Physics.gravity.y * time / 2;
            var deltaY = (hitPoint.y - shootPoint.Position.y) / time;

            var arrowSpeed = direction.normalized * speed;
            arrowSpeed.y = antiGravity + deltaY;
            return arrowSpeed;
        }
    }
}
