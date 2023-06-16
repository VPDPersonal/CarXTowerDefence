using Spawners;
using UnityEngine;
using MVP.Factory;

namespace Monsters.Spawners
{
    public sealed class MonsterSpawner : ISpawnable
    {
        #region Fields
        private readonly Vector3 _position;
        private readonly IFactoryModel<Monster, FactoryParam<Vector3>> _monsterFactory;
        #endregion

        public MonsterSpawner(
            Vector3 position,
            IFactoryModel<Monster, FactoryParam<Vector3>> monsterFactory)
        {
            _position = position;
            _monsterFactory = monsterFactory;
        }

        public void Spawn() => _monsterFactory.Create(_position);
    }
}
