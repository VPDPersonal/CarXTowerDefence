using UnityEngine;
using System.Collections;

namespace Spawners
{
    public abstract class MonoSpawner : MonoBehaviour
    {
        [SerializeField] private bool _playOnStart;

        #region Fields
        private float _startTime;
        private float _minIntervalTime;
        private float _maxIntervalTime;
        private IEnumerator _spawning;
        private ISpawnable _spawner;
        #endregion
        
        public bool IsSpawning => _spawning != null;
        
        public void Constructor(
            float startTime,
            float minIntervalTime,
            float maxIntervalTime,
            ISpawnable spawner)
        {
            _startTime = startTime;
            _minIntervalTime = minIntervalTime;
            _maxIntervalTime = maxIntervalTime;
            _spawner = spawner;
        }

        private void Start()
        {
            if (_playOnStart) StartSpawn();
        }

        #region Spawn Methods
        public void StartSpawn()
        {
            if (IsSpawning)
            {
                Debug.LogWarning("Spawn hsa already started");
                return;
            }

            _spawning = Spawning();
            StartCoroutine(_spawning);
        }

        public void StopSpawn()
        {
            if (!IsSpawning)
            {
                Debug.LogWarning("Spawn hsa already stopped");
                return;
            }
		
            StopCoroutine(_spawning);
            _spawning = null;
        }

        private IEnumerator Spawning()
        {
            yield return new WaitForSeconds(_startTime);
		
            while (IsSpawning)
            {
                _spawner.Spawn();
                var time = Random.Range(_minIntervalTime, _maxIntervalTime);
                yield return new WaitForSeconds(time);
            }
        }
        #endregion
    }
}
