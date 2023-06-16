using System;
using UnityEngine;
using System.Collections.Generic;

namespace MVP
{
    [DisallowMultipleComponent]
    public sealed class MvpComponent : MonoBehaviour
    {
        private readonly Dictionary<Component, (object model, Presenter presenter)> _components = new();

        #region Events
        public event Action<object, Component, Presenter> Enabled;
        public event Action<object, Component, Presenter> Disabled;
        public event Action<object, Component, Presenter> Destroying;
        #endregion

        #region Edit Methods
        public void Add(object model, Component component, Presenter presenter) =>
            _components.Add(component, (model, presenter));

        public void Change(object model, Component component, Presenter presenter) => 
            _components[component] = (model, presenter);

        public void Remove(Component component) =>
            _components.Remove(component);
        #endregion

        #region Get Methods
        public object GetModel<T>(Component component) => _components[component].model;

        public object GetPresenter<T>(Component component) => _components[component].presenter;
        #endregion
        
        #region Unity Methods
        private void OnEnable()
        {
            foreach (var (component, (model, presenter)) in _components)
                Enabled?.Invoke(model, component, presenter);
        }
        
        private void OnDisable()
        {
            foreach (var (component, (model, presenter)) in _components)
                Disabled?.Invoke(model, component, presenter);
        }
        
        private void OnDestroy()
        {
            foreach (var (component, (model, presenter)) in _components)
                Destroying?.Invoke(model, component, presenter);
        }
        #endregion
    }
}
