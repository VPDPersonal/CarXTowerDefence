using MVP;
using Healths;

namespace Projectiles.Cannon
{
    public class CannonPresenter : Presenter<Cannon, CannonView>
    {
        public CannonPresenter(Cannon model, CannonView view) 
            : base(model, view) { }

        #region Enable And Disable Methods
        public override void Enable()
        {
            SubscribeView();
            SubscribeModel();
        }
        
        public override void Disable()
        {
            UnsubscribeView();
            UnsubscribeModel();
        }
        #endregion

        #region Subscription View Methods
        private void SubscribeView() => View.DamageMaking += OnDamageMaking;

        private void UnsubscribeView() => View.DamageMaking -= OnDamageMaking;
        #endregion
        
        #region Subscription Model Methods
        private void SubscribeModel() => Model.Destroyed += OnDestroyed;


        private void UnsubscribeModel() => Model.Destroyed -= OnDestroyed;
        #endregion

        #region Handlers
        private void OnDamageMaking(IDamageable damageable) => Model.MakeDamage(damageable);
        
        private void OnDestroyed() => View.DestroyProjectile();
        #endregion
    }
}
