using Monsters;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Towers.Detectors
{
    [DisallowMultipleComponent]
    public sealed class MonsterDetectorView : MonoBehaviour
    {
        public UnityAction<IReadOnlyList<MonsterView>> Detected;

        private float _radius;
        // private LayerMask _layerMask;
        
        private readonly List<MonsterView> _monsters = new();

        public void SetRadius(float radius) => _radius = radius;

        private void Update()
        {
            _monsters.Clear();
            var colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (var collider in colliders)
                if (collider.TryGetComponent<MonsterView>(out var monster))
                    _monsters.Add(monster);
            
            Detected?.Invoke(_monsters);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
