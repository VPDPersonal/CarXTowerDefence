using System;
using UnityEngine;
using System.Collections.Generic;

namespace MVP
{
    public abstract class Manager<TModel, TView> : IDisposable
        where TModel : class
        where TView : Component
    {
        #region Fields
        private readonly Dictionary<TModel, TView> _views;
        private readonly Dictionary<TView, TModel> _models;
        #endregion

        #region Properties
        public IDictionary<TModel, TView> Views => _views;
        public IDictionary<TView, TModel> Models => _models;
        #endregion

        #region Constructor And Dispose
        public Manager()
        {
            _models = new Dictionary<TView, TModel>();
            _views = new Dictionary<TModel, TView>();
            World.Instance.Binding += BindingObject;
            World.Instance.ViewRebinding += OnViewRebind;
        }
        
        public void Dispose()
        {
            World.Instance.Binding -= BindingObject;
            World.Instance.ViewRebinding -= OnViewRebind;
        }
        #endregion

        #region Methods Get
        public TModel? GetModel(TView view) => 
            _models.TryGetValue(view, out var model) ? model : null;
        
        public TView? GetView(TModel model) =>
            _views.TryGetValue(model, out var view) ? view : null;
        #endregion

        private void BindingObject(object model, Component view, object presenter)
        {
            if (view is not TView tView || model is not TModel tModel) return;
            
            _views.Add(tModel, tView);
            _models.Add(tView, tModel);
        }

        private void OnViewRebind(object model, Component view, object presenter)
        {
            if (view is not TView tView || model is not TModel tModel) return;

            var oldModel = _models[tView];
            _models[tView] = tModel;
            _views.Remove(oldModel);
            _views.Add(tModel, tView);
        }
    }
}
