using MVP;
using Towers.Detectors;
using UnityEngine;

namespace Towers.Guided
{
    public sealed class GuidedTowerPresenter : Presenter<GuidedTower, TowerView>
    {
        private readonly MonsterDetectorPresenter _detectorPresenter;

        public GuidedTowerPresenter(GuidedTower model, TowerView view, MonsterDetectorPresenter detectorPresenter)
            : base(model, view)
        {
            _detectorPresenter = detectorPresenter;
        }

        #region Enable And Disable Methods
        public override void Enable()
        {
            _detectorPresenter.Enable();
            
            UpdateViewPosition();
            
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
        private void SubscribeModel() { }

        private void UnsubscribeModel() { }
        #endregion

        #region Handlers
        private void OnUpdating() => Model.Update(Time.deltaTime);
        #endregion

        private void UpdateViewPosition() =>
            View.SelfTransform.position = Model.Transform.Position;
    }
}
