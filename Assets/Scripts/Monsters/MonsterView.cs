using UnityEngine;
using Monsters.Health;
using UnityEngine.Events;

namespace Monsters
{
    [RequireComponent(typeof(MonsterHealthView))]
    public class MonsterView : MonoBehaviour
    {
        public event UnityAction Moving;

        #region Fields
        private Transform _transform;
        private MonsterHealthView _health;
        #endregion

        #region Properties
        public MonsterHealthView Health => 
            _health ? _health : _health = GetComponent<MonsterHealthView>();
        
        public Transform SelfTransform => 
            _transform ? _transform : _transform = transform;
        #endregion
        
        private void Update() => Moving?.Invoke();
    }
}
