using UnityEngine;

namespace Monsters
{
    [CreateAssetMenu(fileName = "New Monster Data", menuName = "Monster", order = 0)]
    public class MonsterData : ScriptableObject, IMonsterData
    {
        #region Inspector Fields
        [SerializeField] [Min(0)] private int _minHp;
        [SerializeField] [Min(0)] private int _maxHp;
        [SerializeField] [Min(0)] private float _speed;
        #endregion

        protected virtual void OnValidate()
        {
            if (_maxHp < _minHp)
                _maxHp = _minHp;
        }

        #region Properties
        public int Hp => Random.Range(_minHp, _maxHp + 1);

        public float Speed => _speed;
        #endregion
    }
}
