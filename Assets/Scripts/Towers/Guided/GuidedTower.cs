using Monsters;
using MVP.Factory;
using UnityEngine;
using Transformation;
using Towers.Detectors;
using Transformation.Targets;
using Transformation.Adapters;

namespace Towers.Guided
{
    public class GuidedTower : Tower
    {
        private readonly IFactoryModel<Projectiles.Guided.Guided, FactoryParam<Vector3, ITarget>> _guidedFactory;
        
        public GuidedTower(
            TransformModel transform, 
            MonsterDetector detector,
            ITarget shootPoint,
            IGuidedTowerData data,
            IFactoryModel<Projectiles.Guided.Guided, FactoryParam<Vector3, ITarget>> guidedFactory) : 
            base(transform, detector, shootPoint, data)
        {
            _guidedFactory = guidedFactory;
        }

        protected sealed override void Shoot(Monster monster)
        {
            var target = new PositionTargetAdapter(monster.Transform);
            _guidedFactory.Create((shootPoint.Position, target));
        }
    }
}
