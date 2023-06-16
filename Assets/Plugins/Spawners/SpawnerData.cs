using UnityEngine;

namespace Spawners
{
    [CreateAssetMenu(fileName = "New Simple Spawner Data", menuName = "Spawners/Simple Spawner", order = 0)]
    public class SpawnerData : ScriptableObject
    {
        #region Inspector Fields
        [Header("Start Time")]
        [SerializeField] [Min(0)] private float _minStartTime;
        [SerializeField] [Min(0)] private float _maxStartTime;
        
        [Header("Interval Time")]
        [SerializeField] [Min(0)] private float _minIntervalTime;
        [SerializeField] [Min(0)] private float _maxIntervalTime;
        #endregion

        private void OnValidate()
        {
            if (_maxStartTime < _minStartTime)
                _maxStartTime = _minStartTime;
            
            if (_maxIntervalTime < _minIntervalTime)
                _maxIntervalTime = _minIntervalTime;
        }

        #region Properties
        public float MinStartTime => _minStartTime;

        public float MaxStartTime => _maxStartTime;

        public float MinIntervalTime => _minIntervalTime;

        public float MaxIntervalTime => _maxIntervalTime;
        
        public float RandomStartTime => Random.Range(_minStartTime, _maxStartTime);

        public float RandomIntervalTime => Random.Range(_minIntervalTime, _maxIntervalTime);
        #endregion
    }
}
