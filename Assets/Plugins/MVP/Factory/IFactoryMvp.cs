using UnityEngine;

namespace MVP.Factory
{
    public interface IFactoryMvp<TModel, TView, TPresenter> 
        where TView : Component
        where TPresenter : Presenter<TModel, TView>
    {
        public MvpPack<TModel, TView, TPresenter> Create();
    }

    public interface IFactoryMvp<TModel, TView, TPresenter, in TParam>
        where TView : Component
        where TPresenter : Presenter<TModel, TView>
        where TParam : FactoryParam
    {
        public MvpPack<TModel, TView, TPresenter> Create(TParam param);
    }
}
