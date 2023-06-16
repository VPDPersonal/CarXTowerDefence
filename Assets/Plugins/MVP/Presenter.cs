using System;

namespace MVP
{
    public abstract class Presenter : IDisposable
    {
        #region Disable And Enable Methods
        public abstract void Enable();
    
        public abstract void Disable();
        #endregion

        public virtual void Dispose() { }
    }
    
    public abstract class Presenter<TModel, TView> : Presenter
    {
        #region Fields
        protected readonly TModel Model;
        protected readonly TView View;
        #endregion

        protected Presenter(TModel model, TView view)
        {
            Model = model;
            View = view;
        }
    }
}
