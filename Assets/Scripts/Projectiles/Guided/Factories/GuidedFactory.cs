using MVP.Factory;
using UnityEngine;
using Transformation.Targets;
using Transformation.Movements;

namespace Projectiles.Guided.Factories
{
    public sealed class GuidedFactory : 
        PoolFactory<Guided, GuidedView, GuidedPresenter, FactoryParam<Vector3, ITarget>>
    {
        #region Fields
        private readonly IGuidedData _data;
        private readonly GuidedView _prefab;
        #endregion

        public GuidedFactory(
            IGuidedData data,
            GuidedView prefab)
        {
            _data = data;
            _prefab = prefab;
        }

        #region Create MVP Methods
        protected override Guided CreateModel(GuidedView view, FactoryParam<Vector3, ITarget> param)
        {
            var targetMovement = new DirectTargetMovement(param.Param2);
            return new Guided(_data, targetMovement, view.SelfTransform);
        }

        protected override GuidedView CreateView(FactoryParam<Vector3, ITarget> param) =>
            Object.Instantiate(_prefab, (Vector3)param, Quaternion.identity);
        
        protected override GuidedPresenter CreatePresenter(Guided model, GuidedView view, FactoryParam<Vector3, ITarget> param) =>
            new(model, view);
        #endregion

        #region Configure Methods
        protected override void ConfigureView(in GuidedView view, FactoryParam<Vector3, ITarget> param) =>
            view.SelfTransform.position = (Vector3)param;

        protected override void ConfigureModel(ref Guided model, in GuidedView view, FactoryParam<Vector3, ITarget> param) =>
            model = CreateModel(view, param);
        #endregion
    }
}