using MVP;

namespace Monsters.Health
{
    public sealed class MonsterHealthPresenter : Presenter<MonsterHealth, MonsterHealthView>
    {
        public MonsterHealthPresenter(MonsterHealth model, MonsterHealthView view) : 
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

        private void InitializeView()
        {
            UpdateViewMaxHp();
            UpdateViewHp();
        }

        #region Subscription View Methods
        private void SubscribeView() => View.DamageTaking += OnDamageTaking;

        private void UnsubscribeView() => View.DamageTaking -= OnDamageTaking;
        #endregion
        
        #region Subscription Model Methods
        private void SubscribeModel()
        {
            Model.Died += OnDied;
            Model.HpChanged += UpdateViewHp;
            Model.MaxHpChanged += UpdateViewHp;
        }

        private void UnsubscribeModel()
        {
            Model.Died -= OnDied;
            Model.HpChanged -= UpdateViewHp;
            Model.MaxHpChanged -= UpdateViewHp;
        }
        #endregion

        #region Handlers
        private void OnDamageTaking(int damage) => Model.TakeDamage(damage);

        private void OnDied() => View.Die();
        #endregion

        #region Update Hp Methods
        private void UpdateViewHp() => View.SetHp(Model.Hp);
        
        private void UpdateViewMaxHp() => View.SetMaxHp(Model.MaxHp);
        #endregion
    }
}
