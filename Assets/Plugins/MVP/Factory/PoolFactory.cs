using MVP.Pools;
using UnityEngine;

namespace MVP.Factory
{
    public abstract class PoolFactory<TModel, TView, TPresenter> :
        IFactoryMvp<TModel, TView, TPresenter>, IFactoryModel<TModel>, IFactoryView<TView>
        where TView : Component
        where TPresenter : Presenter<TModel, TView>
    {
        private readonly Pool<TModel, TView, TPresenter> _pool;

        protected PoolFactory()
        {
            _pool = new Pool<TModel, TView, TPresenter>(
                OnCreate, OnGet, null, OnDestroy);
        }

        #region Create Methods
        TModel IFactoryModel<TModel>.Create() => Create().Model;

        TView IFactoryView<TView>.Create() => Create().View;
        
        public MvpPack<TModel, TView, TPresenter> Create() => _pool.Get();
        #endregion

        #region Handlers
        private MvpPack<TModel, TView, TPresenter> OnCreate()
        {
            var view = CreateView();
            var model = CreateModel(view);
            var presenter = CreatePresenter(model, view);
            World.Bind(model, view, presenter);
            view.GetComponent<MvpComponent>().Disabled += Realise;

            AfterOnCreate(model, view, presenter);
            return (model, view, presenter);
        }

        protected virtual void AfterOnCreate(TModel model, TView view, TPresenter presenter) { }
        
        private void OnGet(MvpPack<TModel, TView, TPresenter> mvp)
        {
            var (model, view, _) = mvp;
            ConfigureView(in view);
            ConfigureModel(ref model, in view);
            var presenter = CreatePresenter(model, view);
            
            World.RebindView(model, view, presenter);
            AfterOnGet(model, view, presenter);
            view.gameObject.SetActive(true);
        }
        
        protected virtual void AfterOnGet(TModel model, TView view, TPresenter presenter) { }
        
        private void OnDestroy(MvpPack<TModel, TView, TPresenter> mvp)
        {
            mvp.View.GetComponent<MvpComponent>().Disabled -= Realise;
            // TODO destroy script or gameObject
            Object.Destroy(mvp.View.gameObject);
        }
        #endregion

        #region Create MVP Methods
        protected abstract TView CreateView();
        
        protected abstract TModel CreateModel(TView view);

        protected abstract TPresenter CreatePresenter(TModel model, TView view);
        #endregion

        #region Configure Methods
        protected abstract void ConfigureView(in TView view);

        protected abstract void ConfigureModel(ref TModel model, in TView view);
        #endregion

        private void Realise(object model, Component view, object presenter)
        {
            if (model is TModel tModel && view is TView tView && presenter is TPresenter tPresenter)
                _pool.Release((tModel, tView, tPresenter));
        }
    }
    
    public abstract class PoolFactory<TModel, TView, TPresenter, TParam> :
        IFactoryMvp<TModel, TView, TPresenter, TParam>, IFactoryModel<TModel, TParam>, IFactoryView<TView, TParam>
        where TView : Component
        where TPresenter : Presenter<TModel, TView>
        where TParam : FactoryParam
    {
        #region Fields
        private TParam _param;
        private readonly Pool<TModel, TView, TPresenter> _pool;
        #endregion
        
        protected PoolFactory()
        {
            _pool = new Pool<TModel, TView, TPresenter>(
                OnCreate, OnGet, null, OnDestroy);
        }

        #region Create Methods
        TModel IFactoryModel<TModel, TParam>.Create(TParam param) => Create(param).Model;

        TView IFactoryView<TView, TParam>.Create(TParam param) => Create(param).View;
        
        public MvpPack<TModel, TView, TPresenter> Create(TParam param)
        {
            _param = param;
            return _pool.Get();
        }
        #endregion

        #region Handlers
        private MvpPack<TModel, TView, TPresenter> OnCreate()
        {
            var view = CreateView(_param);
            var model = CreateModel(view, _param);
            var presenter = CreatePresenter(model, view, _param);
            World.Bind(model, view, presenter);
            view.GetComponent<MvpComponent>().Disabled += Realise;

            var mvp = (model, view, presenter);
            AfterOnCreate(mvp, _param);
            return mvp;
        }
        
        protected virtual void AfterOnCreate(MvpPack<TModel, TView, TPresenter> mvp, TParam param) { }
        
        private void OnGet(MvpPack<TModel, TView, TPresenter> mvp)
        {
            var (model, view, _) = mvp;
            ConfigureView(in view, _param);
            ConfigureModel(ref model, in view, _param);
            var presenter = CreatePresenter(model, view, _param);
            
            World.RebindView(model, view, presenter);
            AfterOnGet(model, view, presenter, _param);
            view.gameObject.SetActive(true);
        }
        
        protected virtual void AfterOnGet(TModel model, TView view, TPresenter presenter, TParam param) { }
            
        private void OnDestroy(MvpPack<TModel, TView, TPresenter> mvp)
        {
            mvp.View.GetComponent<MvpComponent>().Disabled -= Realise;
            // TODO destroy script or gameObject
            Object.Destroy(mvp.View.gameObject);
        }
        #endregion

        #region Create MVP Methods
        protected abstract TView CreateView(TParam param);
        
        protected abstract TModel CreateModel(TView view, TParam param);

        protected abstract TPresenter CreatePresenter(TModel model, TView view, TParam param);
        #endregion
        
        #region Configure Methods
        protected abstract void ConfigureView(in TView view, TParam param);
    
        protected abstract void ConfigureModel(ref TModel model, in TView view, TParam param);
        #endregion
    
        private void Realise(object model, Component view, object presenter)
        {
            if (model is TModel tModel && view is TView tView && presenter is TPresenter tPresenter)
                _pool.Release((tModel, tView, tPresenter));
        } 
    }
}
