using UnityEngine;

namespace Projectiles
{
    public abstract class ProjectileData : ScriptableObject
    {
        #region Inspector Fields
        [Header("Parameters")]
        [SerializeField] [Min(0)] private int _damage;
        [SerializeField] [Min(0)] private float _speed;
        #endregion

        #region Properties
        public int Damage => _damage;

        public float Speed => _speed;
        #endregion
    }
}
