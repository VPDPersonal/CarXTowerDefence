namespace MVP.Factory
{
    public interface IFactoryModel<out TModel>
    {
        public TModel Create();
    }

    public interface IFactoryModel<out TModel, in TParam>
        where TParam : FactoryParam
    {
        public TModel Create(TParam param);
    }
}
