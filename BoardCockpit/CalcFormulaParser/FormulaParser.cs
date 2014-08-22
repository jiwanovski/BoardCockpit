using System;
using System.Collections.Generic;

namespace CalcFormulaParser
{
    public class FormulaParser : FctParser<double>
    {
        public FormulaParser()
        {
            base.Functions = Functions;
            base.Operators = Operators;
            base.Constants = Constants;

            base.MaxOpPriority = 3;
        }

        static double Log(double x)
        {
            return Math.Log(x);
        }

        static double Sign(double x)
        {
            return (double)Math.Sign(x);
        }

        static double Sum(params double[] xs)
        {
            double y = 0;
            foreach (double x in xs)
                y += x;

            return y;
        }

        static new Function[] Functions =
        {
            new Function("abs",   Math.Abs),
            new Function("acos",  Math.Acos),
            new Function("asin",  Math.Asin),
            new Function("atan",  Math.Atan),
            new Function("ceil",  Math.Ceiling),
            new Function("cos",   Math.Cos),
            new Function("cosh",  Math.Cosh),
            new Function("exp",   Math.Exp),
            new Function("floor", Math.Floor),
            new Function("ln",    Log),
            new Function("log",   Math.Log10),
            new Function("round", Math.Round),
            new Function("sgn",   Sign),
            new Function("sin",   Math.Sin),
            new Function("sinh",  Math.Sinh),
            new Function("sqrt",  Math.Sqrt),
            new Function("tan",   Math.Tan),
            new Function("tanh",  Math.Tanh),
            new Function("trunc", Math.Truncate),
            new Function("max",   Math.Max),
            new Function("min",   Math.Min),
            new Function("sum",   Sum)
        };

        static double Add(double x, double y)
        {
            return x + y;
        }

        static double Sub(double x, double y)
        {
            return x - y;
        }

        static double Mul(double x, double y)
        {
            return x * y;
        }

        static double Div(double x, double y)
        {
            return x / y;
        }

        static double Mod(double x, double y)
        {
            double d = x / y;
            return (d - Math.Floor(d)) * y;
        }

        static double Root(double x, double y)
        {
            return Math.Pow(x, (double)1 / y);
        }

        static new Operator[] Operators =
        {
            new Operator('+', Add,      OpType.Unary,  0),
            new Operator('-', Sub,      OpType.Unary,  0),
            new Operator('^', Math.Pow, OpType.Binary, 3),
            new Operator('#', Root,     OpType.Binary, 3),
            new Operator('*', Mul,      OpType.Binary, 2),
            new Operator('/', Div,      OpType.Binary, 2),
            new Operator('%', Mod,      OpType.Binary, 2),
            new Operator('+', Add,      OpType.Binary, 1),
            new Operator('-', Sub,      OpType.Binary, 1)
        };

        static new Constant[] Constants =
        {
            new Constant("E", Math.E),
            new Constant("PI", Math.PI)
        };
    }
}
