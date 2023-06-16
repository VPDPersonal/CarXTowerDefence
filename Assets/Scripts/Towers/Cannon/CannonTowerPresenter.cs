using MVP;
using UnityEngine;
using Towers.Detectors;

namespace Towers.Cannon
{
    public class CannonTowerPresenter : Presenter<CannonTower, CannonTowerView>
    {
        private readonly MonsterDetectorPresenter _detectorPresenter;
        
        public CannonTowerPresenter(CannonTower model, CannonTowerView view, MonsterDetectorPresenter detectorPresenter) : base(model, view)
        {
            _detectorPresenter = detectorPresenter;
        }

        #region Enable And Disable Methods
        public override void Enable()
        {
            _detectorPresenter.Enable();
            
            UpdateViewPosition();
            UpdateRotation();
            
            SubscribeView();
            SubscribeModel();
        }

        public override void Disable()
        {
            _detectorPresenter.Disable();
            
            UnsubscribeView();
            UnsubscribeModel();
        }
        #endregion
        
        #region Subscription View Methods
        private void SubscribeView() => View.Updating += OnUpdating;

        private void UnsubscribeView() => View.Updating -= OnUpdating;
        #endregion
        
        #region Subscription Model Methods
        private void SubscribeModel() => Model.TowerRotated += UpdateRotation;

        private void UnsubscribeModel() => Model.TowerRotated -= UpdateRotation;
        #endregion

        #region Handlers
        private void OnUpdating() => Model.Update(Time.deltaTime);
        #endregion

        private void UpdateViewPosition() =>
            View.SelfTransform.position = Model.Transform.Position;

        private void UpdateRotation() =>
            View.SetRotationWeapon(Model.Transform.Rotation);
    }
}
