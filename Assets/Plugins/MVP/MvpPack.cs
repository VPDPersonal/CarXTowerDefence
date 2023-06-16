namespace MVP
{
    public readonly struct MvpPack<TModel, TView, TPresenter>
    {
        #region Fields
        public readonly TView View;
        public readonly TModel Model;
        public readonly TPresenter Presenter;
        #endregion

        public MvpPack(TModel model, TView view, TPresenter presenter)
        {
            View = view;
            Model = model;
            Presenter = presenter;
        }
        
        public void Deconstruct(out TModel model, out TView view, out TPresenter presenter)
        {
            view = View;
            model = Model;
            presenter = Presenter;
        }

        public static implicit operator MvpPack<TModel, TView, TPresenter>((TModel model, TView view, TPresenter presenter) pack) =>
            new (pack.model, pack.view, pack.presenter);
    }
}
