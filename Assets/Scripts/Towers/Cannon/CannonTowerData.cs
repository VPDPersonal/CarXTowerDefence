using UnityEngine;

namespace Towers.Cannon
{
    [CreateAssetMenu(fileName = "New Cannon Tower Data", menuName = "Towers/Cannon Tower", order = 0)]
    public class CannonTowerData : TowerData, ICannonTowerData
    {
        [SerializeField] [Min(0)] private float _rotationSpeed;

        public float RotationSpeed => _rotationSpeed;
    }
}
