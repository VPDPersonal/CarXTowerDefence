using UnityEngine;

namespace MVP.Factory
{
    public interface IFactoryView<out TView> 
        where TView : Component
    {
        public TView Create();
    }

    public interface IFactoryView<out TView, in TParam>
        where TView : Component
        where TParam : FactoryParam
    {
        public TView Create(TParam param);
    }
}
