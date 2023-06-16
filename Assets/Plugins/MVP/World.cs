using System;
using UnityEngine;

namespace MVP
{
    public class World : MonoBehaviour
    {
        #region Events
        public event Action<object, Component, Presenter> Binding;
        public event Action<object, Component, Presenter> Enabling;
        public event Action<object, Component, Presenter> Disabling;
        public event Action<object, Component, Presenter> Destroying;
        public event Action<object, Component, Presenter> ViewRebinding;
        #endregion
        
        private static World _instance;
        
        public static World Instance => 
            _instance ? _instance : _instance = new GameObject("World").AddComponent<World>();

        private void Awake()
        {
            if (!_instance) _instance = this;
            else if (_instance != this) Destroy(this);
        }

        #region Create Methods
        public static MvpPack<TModel, TView, TPresenter> Create<TModel, TView, TPresenter>(
            Func<TView, TModel> createModel,
            Func<TView> createView,
            Func<TModel, TView, TPresenter> createPresenter)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            return Create(createModel, createView(), createPresenter);
        }
        
        public static MvpPack<TModel, TView, TPresenter> Create<TModel, TView, TPresenter>(
            Func<TView, TModel> createModel,
            TView view,
            Func<TModel, TView, TPresenter> createPresenter)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            return Create(createModel(view), view, createPresenter);
        }
        
        public static MvpPack<TModel, TView, TPresenter> Create<TModel, TView, TPresenter>(
            TModel model,
            Func<TView> createView,
            Func<TModel, TView, TPresenter> createPresenter)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            return Create(model, createView(), createPresenter);
        }
        
        public static MvpPack<TModel, TView, TPresenter> Create<TModel, TView, TPresenter>(
            TModel model,
            TView view,
            Func<TModel, TView, TPresenter> createPresenter)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            var presenter = createPresenter(model, view);
            Bind(model, view, presenter);
            return (model, view, presenter);
        }
        
        #endregion

        #region Bind Methods
        public static void Bind<TModel, TView, TPresenter>(MvpPack<TModel, TView, TPresenter> mvp)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            Bind(mvp.Model, mvp.View, mvp.Presenter);
        }
        
        public static void Bind<TModel, TView, TPresenter>(TModel model, TView view, TPresenter presenter)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            AddEnableComponent(view, model, presenter);
            Instance.Binding?.Invoke(model, view, presenter);
        }
        #endregion

        #region Rebbind View Methods
        public static void RebindView<TModel, TView, TPresenter>(MvpPack<TModel, TView, TPresenter> mvp)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            RebindView(mvp.Model, mvp.View, mvp.Presenter);
        }
        
        public static void RebindView<TModel, TView, TPresenter>(
            TModel model,
            TView view, 
            TPresenter presenter)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            view.GetComponent<MvpComponent>().Change(model, view, presenter);
            Instance.ViewRebinding?.Invoke(model, view, presenter);
        }
        #endregion

        private static void AddEnableComponent<TView, TModel, TPresenter>(
            TView view,
            TModel model,
            TPresenter presenter)
            where TView : Component
            where TPresenter : Presenter<TModel, TView>
        {
            var enable = view.GetComponent<MvpComponent>();
            if (!enable)
            {
                enable = view.gameObject.AddComponent<MvpComponent>();
                enable.Enabled += OnEnableView;
                enable.Disabled += OnDisableView;
                enable.Destroying += OnDestroyView;
            }
        
            enable.Add(model, view, presenter);
            if (enable.enabled) presenter.Enable();
        }
        
        #region Handlers
        private static void OnEnableView(object model, Component component, Presenter presenter)
        {
            presenter.Enable();
            _instance?.Enabling?.Invoke(model, component, presenter);
        }

        private static void OnDisableView(object model, Component component, Presenter presenter)
        {
            presenter.Disable();
            _instance?.Disabling?.Invoke(model, component, presenter);
        }
    
        private static void OnDestroyView(object model, Component component, Presenter presenter)
        {
            presenter.Dispose();
            _instance?.Destroying?.Invoke(model, component, presenter);
        }
        #endregion

        private void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
    }
}
