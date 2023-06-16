using UnityEngine;

namespace MVP.Factory
{
    public abstract class Factory<TModel, TView, TPresenter> : 
        IFactoryMvp<TModel, TView, TPresenter>, IFactoryModel<TModel>, IFactoryView<TView>
        where TView : Component
        where TPresenter : Presenter<TModel, TView>
    {
        #region Create Methods
        TModel IFactoryModel<TModel>.Create() => Create().Model;

        TView IFactoryView<TView>.Create() => Create().View;
        
        public MvpPack<TModel, TView, TPresenter> Create()
        {
            var view = CreateView();
            var model = CreateModel(view);
            var presenter = CreatePresenter(model, view);
            World.Bind(model, view, presenter);

            AfterCreate(model, view, presenter);
            return (model, view, presenter);
        }

        protected virtual void AfterCreate(TModel model, TView view, TPresenter presenter) { }
        #endregion
        
        #region Create MVP Methods
        protected abstract TView CreateView();
        
        protected abstract TModel CreateModel(TView view);

        protected abstract TPresenter CreatePresenter(TModel model, TView view);
        #endregion
    }
    
    public abstract class Factory<TModel, TView, TPresenter, TParam> :
        IFactoryMvp<TModel, TView, TPresenter, TParam>, IFactoryModel<TModel, TParam>, IFactoryView<TView, TParam>
        where TView : Component
        where TPresenter : Presenter<TModel, TView>
        where TParam : FactoryParam
    {
        #region Create Methods
        TModel IFactoryModel<TModel, TParam>.Create(TParam param) => Create(param).Model;

        TView IFactoryView<TView, TParam>.Create(TParam param) => Create(param).View;
        
        public MvpPack<TModel, TView, TPresenter> Create(TParam param)
        {
            var view = CreateView(param);
            var model = CreateModel(view, param);
            var presenter = CreatePresenter(model, view, param);
            World.Bind(model, view, presenter);
            
            AfterCreate(model, view, presenter, param);
            return (model, view, presenter);
        }
        
        protected virtual void AfterCreate(TModel model, TView view, TPresenter presenter, TParam param) { }
        #endregion

        #region Create MVP Methods
        protected abstract TView CreateView(TParam param);
        
        protected abstract TModel CreateModel(TView view, TParam param);

        protected abstract TPresenter CreatePresenter(TModel model, TView view, TParam param);
        #endregion
    }
}
