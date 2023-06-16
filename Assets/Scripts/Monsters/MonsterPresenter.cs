using MVP;
using UnityEngine;

namespace Monsters
{
    public sealed class MonsterPresenter : Presenter<Monster, MonsterView>
    {
        public MonsterPresenter(Monster model, MonsterView view) :
            base(model, view) { }

        #region Enable And Disable Methods
        public override void Enable()
        {
            Initialize();
            
            SubscribeView();
            SubscribeModel();
        }
        
        public override void Disable()
        {
            UnsubscribeView();
            UnsubscribeModel();
        }
        #endregion

        private void Initialize() => UpdateViewPosition();

        #region Subscription View Methods
        private void SubscribeView() => View.Moving += OnMoving;

        private void UnsubscribeView() => View.Moving -= OnMoving;
        #endregion
        
        #region Subscription Model Methods
        private void SubscribeModel() => Model.Transform.PositionChanged += UpdateViewPosition;
        
        private void UnsubscribeModel() => Model.Transform.PositionChanged -= UpdateViewPosition;
        #endregion
        
        private void OnMoving() => Model.Move(Time.deltaTime);

        private void UpdateViewPosition() => View.SelfTransform.position = Model.Transform.Position;
    }
}
