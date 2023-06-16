using System;
using UnityEngine;

namespace Towers
{
    public abstract class TowerData : ScriptableObject, ITowerData
    {
        #region Fields
        [Header("Presenter")]
        [SerializeField] [Min(0)] private float _radius;
        [SerializeField] [Min(0)] private float _attackRadius;
        [SerializeField] [Min(0)] private float _sootInterval;
        #endregion

        #region Properties
        public float Radius => _radius;

        public float AttackRadius => _attackRadius;

        public float ShootInterval => _sootInterval;
        #endregion

        private void OnValidate()
        {
            if (_attackRadius > _radius)
                _attackRadius = _radius;
        }
    }
}
