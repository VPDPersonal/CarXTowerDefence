namespace MVP.Factory
{
    public abstract class FactoryParam { }

    public class FactoryParam<TParam1> : FactoryParam
    {
        public readonly TParam1 Param1;
        
        public FactoryParam(TParam1 param1) => Param1 = param1;

        #region Operators
        public static explicit operator TParam1(FactoryParam<TParam1> param) => param.Param1;
        
        public static implicit operator FactoryParam<TParam1>(TParam1 param1) => new(param1);
        #endregion
    }

    public class FactoryParam<TParam1, TParam2> : FactoryParam<TParam1>
    {
        public readonly TParam2 Param2;

        public FactoryParam(TParam1 param1, TParam2 param2) :
            base(param1) => Param2 = param2;

        #region Operators
        public static explicit operator TParam1(FactoryParam<TParam1, TParam2> param) => param.Param1;
        
        public static explicit operator TParam2(FactoryParam<TParam1, TParam2> param) => param.Param2;

        public static implicit operator FactoryParam<TParam1, TParam2>(
            (TParam1 param1, TParam2 param2) parameters) =>
            new(parameters.param1, parameters.param2);
        #endregion
    }

    public class FactoryParam<TParam1, TParam2, TParam3> : FactoryParam<TParam1, TParam2>
    {
        public readonly TParam3 Param3;

        public FactoryParam(TParam1 param1, TParam2 param2, TParam3 param3) : 
            base(param1, param2) => Param3 = param3;

        #region Operators
        public static explicit operator TParam1(FactoryParam<TParam1, TParam2, TParam3> param) => param.Param1;
        
        public static explicit operator TParam2(FactoryParam<TParam1, TParam2, TParam3> param) => param.Param2;
        
        public static explicit operator TParam3(FactoryParam<TParam1, TParam2, TParam3> param) => param.Param3;
        
        public static implicit operator FactoryParam<TParam1, TParam2, TParam3>(
            (TParam1 param1, TParam2 param2, TParam3 param3) parameters) =>
            new(parameters.param1, parameters.param2, parameters.param3);
        #endregion
    }
    
    public class FactoryParam<TParam1, TParam2, TParam3, TParam4> : FactoryParam<TParam1, TParam2, TParam3>
    {
        public readonly TParam4 Param4;

        public FactoryParam(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) :
            base(param1, param2, param3) => Param4 = param4;

        #region Operators
        public static explicit operator TParam1(FactoryParam<TParam1, TParam2, TParam3, TParam4> param) => param.Param1;
        
        public static explicit operator TParam2(FactoryParam<TParam1, TParam2, TParam3, TParam4> param) => param.Param2;
        
        public static explicit operator TParam3(FactoryParam<TParam1, TParam2, TParam3, TParam4> param) => param.Param3;
        
        public static explicit operator TParam4(FactoryParam<TParam1, TParam2, TParam3, TParam4> param) => param.Param4;

        public static implicit operator FactoryParam<TParam1, TParam2, TParam3, TParam4>(
            (TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) parameters) =>
            new(parameters.param1, parameters.param2, parameters.param3, parameters.param4);
        #endregion
    }
}
