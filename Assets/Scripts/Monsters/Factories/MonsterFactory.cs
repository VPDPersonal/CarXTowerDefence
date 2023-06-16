using MVP;
using MVP.Factory;
using UnityEngine;
using Monsters.Health;
using Transformation.Movements;

namespace Monsters.Factories
{
    public sealed class MonsterFactory : PoolFactory<Monster, MonsterView, MonsterPresenter, FactoryParam<Vector3>>
    {
        #region Fields
        private readonly TargetMovement _movement;
        private readonly MonsterView _prefab;
        private readonly IMonsterData _monsterData;
        #endregion

        public MonsterFactory(
            TargetMovement movement,
            MonsterView prefab,
            IMonsterData monsterData)
        {
            _movement = movement;
            _prefab = prefab;
            _monsterData = monsterData;
        }

        #region Create MVP methods
        protected override MonsterView CreateView(FactoryParam<Vector3> param) =>
            Object.Instantiate(_prefab, (Vector3)param, Quaternion.identity);
        
        protected override Monster CreateModel(MonsterView view, FactoryParam<Vector3> param)
        {
            var health = new MonsterHealth(_monsterData.Hp);
            return new Monster(_monsterData.Speed, _movement, health, view.SelfTransform);
        }

        protected override MonsterPresenter CreatePresenter(Monster model, MonsterView view, FactoryParam<Vector3> param) =>
            new(model, view);

        protected override void AfterOnCreate(MvpPack<Monster, MonsterView, MonsterPresenter> mvp, FactoryParam<Vector3> param)
        {
            var health = GetHealth(mvp.Model, mvp.View);
            World.Bind(health.Model, health.View, health.Presenter);
        }
        #endregion

        #region Configure View
        protected override void ConfigureView(in MonsterView view, FactoryParam<Vector3> param) =>
            view.SelfTransform.position = (Vector3)param;

        protected override void ConfigureModel(ref Monster model, in MonsterView view, FactoryParam<Vector3> param) =>
            model = CreateModel(view, param);

        protected override void AfterOnGet(Monster model, MonsterView view, MonsterPresenter presenter, FactoryParam<Vector3> param)
        {
            var health = GetHealth(model, view);
            World.RebindView(health.Model, health.View, health.Presenter);
        }
        #endregion

        private static MvpPack<MonsterHealth, MonsterHealthView, MonsterHealthPresenter> GetHealth(Monster model, MonsterView view)
        {
            var health = model.Health;
            var healthView = view.Health;
            var healthPresenter = new MonsterHealthPresenter(health, healthView);
            return (health, healthView, healthPresenter);
        }
    }
}