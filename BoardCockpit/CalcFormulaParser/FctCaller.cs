using System;

namespace CalcFormulaParser
{
    public abstract class FctCaller<T>
    {
        public abstract T Call(params T[] parameters);

        public abstract int ParamCount { get; }
    }

    public abstract class FctCaller<T, Fct> : FctCaller<T>
    {
        public FctCaller(Fct fct)
        {
            this.fct = fct;
        }

        protected Fct fct;
    }

    public delegate T Fct<T>(T x);
    public delegate T Fct2<T>(T x, T y);
    public delegate T FctX<T>(params T[] xs);

    public class FctCaller1<T> : FctCaller<T, Fct<T>>
    {
        public FctCaller1(Fct<T> fct)
            : base(fct)
        {
        }

        public override T Call(params T[] parameters)
        {
            return fct(parameters[0]);
        }

        public override int ParamCount { get { return 1; } }
    }

    public class FctCaller2<T> : FctCaller<T, Fct2<T>>
    {
        public FctCaller2(Fct2<T> fct)
            : base(fct)
        {
        }

        public override T Call(params T[] parameters)
        {
            return fct(parameters[0], parameters[1]);
        }

        public override int ParamCount { get { return 2; } }
    }

    public class FctCallerX<T> : FctCaller<T, FctX<T>>
    {
        public FctCallerX(FctX<T> fct)
            : base(fct)
        {
        }

        public override T Call(params T[] parameters)
        {
            return fct(parameters);
        }

        public override int ParamCount { get { return -1; } }
    }
}
