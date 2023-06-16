using System;
using Monsters;
using System.Collections.Generic;

namespace Towers.Detectors
{
    public sealed class MonsterDetector
    {
        public event Action RadiusChanged;
        
        #region Fields
        private float _radius;
        private readonly List<Monster> _monsters;
        #endregion

        #region Properties
        public float Radius
        {
            get => _radius;
            private set
            { 
                _radius = value >= 0 ? value : 0;
                RadiusChanged?.Invoke();
            }
        }
        
        public ICollection<Monster> Monsters => _monsters;
        #endregion

        public MonsterDetector(float radius)
        {
            ThrowIfInvalidData(nameof(radius), radius);
            _radius = radius;
            _monsters = new List<Monster>();
        }

        #region Radius Methods
        public void IncreaseRadius(float value)
        {
            ThrowIfInvalidData(nameof(value), value);
            Radius += value;
        }

        public void DecreaseRadius(float value)
        {
            ThrowIfInvalidData(nameof(value), value);
            Radius -= value;
        }
        #endregion

        public void UpdateList(IEnumerable<Monster> monsters)
        {
            _monsters.Clear();
            if (monsters != null) _monsters.AddRange(monsters);
        }

        private static void ThrowIfInvalidData(string valueName, float value)
        {
            if (value < 0) throw new ArgumentException($"{valueName} can't be less than 0. {valueName} = {value}");
        }
    }
}
