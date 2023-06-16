using UnityEngine;
using Towers.Detectors;
using UnityEngine.Events;
using Transformation.Targets;

namespace Towers
{
    [RequireComponent(typeof(MonsterDetectorView))]
    public class TowerView : MonoBehaviour
    {
        public event UnityAction Updating;

        [SerializeField] private Transform _weapon;
        [SerializeField] private SimpleTarget _shootPosition;

        #region Fields
        private Transform _transform;
        private MonsterDetectorView _detector;
        #endregion

        #region Properties
        public SimpleTarget ShootPosition => _shootPosition;
        
        public MonsterDetectorView Detector => _detector ? _detector : _detector = GetComponent<MonsterDetectorView>();
        
        public Transform SelfTransform => _transform ? _transform : _transform = transform;
        #endregion

        protected virtual void Update() => Updating?.Invoke();

        public void SetRotationWeapon(Quaternion rotation) => _weapon.rotation = rotation;
    }
}
