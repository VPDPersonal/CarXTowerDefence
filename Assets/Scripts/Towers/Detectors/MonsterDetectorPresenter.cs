using MVP;
using Monsters;
using System.Linq;
using System.Collections.Generic;

namespace Towers.Detectors
{
    public sealed class MonsterDetectorPresenter : Presenter<MonsterDetector, MonsterDetectorView>
    {
        private readonly MonsterManager _monsterManager;
        
        public MonsterDetectorPresenter(MonsterDetector model, MonsterDetectorView view, MonsterManager monsterManager) 
            : base(model, view)
        {
            _monsterManager = monsterManager;
        }

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
            SetRadius();
        }
        
        #region Subscription View Methods
        private void SubscribeView() => View.Detected += OnDetected;

        private void UnsubscribeView() => View.Detected -= OnDetected;
        #endregion
        
        #region Subscription Model Methods
        private void SubscribeModel() => Model.RadiusChanged += SetRadius;
        
        private void UnsubscribeModel() => Model.RadiusChanged -= SetRadius;
        #endregion

        private void OnDetected(IEnumerable<MonsterView> monsterView)
        {
            var monsters = monsterView.Select(monster => _monsterManager.GetModel(monster));
            Model.UpdateList(monsters);
        }
        
        private void SetRadius() => View.SetRadius(Model.Radius);
    }
}
