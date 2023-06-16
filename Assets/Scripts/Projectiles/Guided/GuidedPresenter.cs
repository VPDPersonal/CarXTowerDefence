using MVP;
using Healths;
using UnityEngine;

namespace Projectiles.Guided
{
    public sealed class GuidedPresenter : Presenter<Guided, GuidedView>
    {
        public GuidedPresenter(Guided model, GuidedView view) :
            base(model, view) { }

        #region Enable And Disable Methods
        public override void Enable()
        {
            InitializeView();
            
            SubscribeView();
            SubscribeModel();
        }

        public override void Disable()
        {
            UnsubscribeView();
            UnsubscribeModel();
        }
        #endregion
        
        private void InitializeView() => UpdateViewPosition();

        #region Subscription View Methods
        private void SubscribeView()
        {
            View.Moving += OnMoving;
            View.DamageMaking += OnDamageMaking;
        }

        private void UnsubscribeView()
        {
            View.Moving -= OnMoving;
            View.DamageMaking -= OnDamageMaking;
        }
        #endregion
        
        #region Subscription Model Methods
        private void SubscribeModel()
        {
            Model.Destroyed += OnDestroyed;
            Model.Transform.PositionChanged += UpdateViewPosition;
        }

        private void UnsubscribeModel()
        {
            Model.Destroyed -= OnDestroyed;
            Model.Transform.PositionChanged -= UpdateViewPosition;
        }
        #endregion

        #region Handlers
        private void OnMoving() => Model.Move(Time.deltaTime);
        
        private void OnDamageMaking(IDamageable damageable) => Model.MakeDamage(damageable);
        
        private void OnDestroyed() => View.DestroyProjectile();
        #endregion

        private void UpdateViewPosition() => View.SelfTransform.position = Model.Transform.Position;
    }
}
