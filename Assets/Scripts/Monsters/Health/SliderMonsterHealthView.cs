using UnityEngine;
using UnityEngine.UI;

namespace Monsters.Health
{
    public sealed class SliderMonsterHealthView : MonsterHealthView
    {
        [SerializeField] private Slider _hpView;

        #region Set Hp Methods
        public override void SetHp(int hp) => _hpView.value = hp;
        
        public override void SetMaxHp(int maxHp) => _hpView.maxValue = maxHp;
        #endregion

        public override void Die() => gameObject.SetActive(false);
    }
}
