using MVP.Factory;
using UnityEngine;

namespace Projectiles.Cannon.Factories
{
    public sealed class CannonFactory : PoolFactory<Cannon, CannonView, CannonPresenter, FactoryParam<Vector3>>
    {
        #region Fields
        private readonly CannonData _data;
        private readonly CannonView _prefab;
        #endregion

        public CannonFactory(
            CannonData data,
            CannonView prefab)
        {
            _data = data;
            _prefab = prefab;
        }

        #region Create MVP Methods
        protected override CannonView CreateView(FactoryParam<Vector3> param) =>
            Object.Instantiate(_prefab, (Vector3)param, Quaternion.identity);

        protected override Cannon CreateModel(CannonView view, FactoryParam<Vector3> param) =>
            new(_data, view.Physics);

        protected override CannonPresenter CreatePresenter(Cannon model, CannonView view, FactoryParam<Vector3> param) =>
            new(model, view);
        #endregion

        #region Configure
        protected override void ConfigureView(in CannonView view, FactoryParam<Vector3> param)
        {
            view.GetComponent<Rigidbody>().velocity = Vector3.zero;
            view.transform.position = (Vector3)param;
        }

        protected override void ConfigureModel(ref Cannon model, in CannonView view, FactoryParam<Vector3> param) =>
            model = CreateModel(view, param);
        #endregion
    }
}
