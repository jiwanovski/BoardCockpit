using System;
using System.Collections.Generic;

namespace CalcFormulaParser
{
    public class IntParser : FctParser<long>
    {
        public IntParser()
        {
            base.Functions = Functions;
            base.Operators = Operators;
            base.Constants = Constants;

            base.MaxOpPriority = 7;
        }

        static long Sign(long x)
        {
            return (long)Math.Sign(x);
        }

        static new Function[] Functions =
        {
            new Function("sign", Sign)
        };

        static long Add(long x, long y)
        {
            return x + y;
        }

        static long Sub(long x, long y)
        {
            return x - y;
        }

        static long Mul(long x, long y)
        {
            return x * y;
        }

        static long Div(long x, long y)
        {
            return x / y;
        }

        static long Mod(long x, long y)
        {
            return x % y;
        }

        static long Pow(long x, long y)
        {
            return (long)Math.Pow(x, y);
        }

        static long Root(long x, long y)
        {
            return (long)Math.Pow(x, 1.0d/y);
        }

        static long Shl(long x, long y)
        {
            return x << (int)y;
        }

        static long Shr(long x, long y)
        {
            return x >> (int)y;
        }

        static long Comp(long x, long y)
        {
            return ~y;
        }

        static long And(long x, long y)
        {
            return x & y;
        }

        static long Xor(long x, long y)
        {
            return x ^ y;
        }

        static long Or(long x, long y)
        {
            return x | y;
        }

        static new Operator[] Operators =
        {
            new Operator('+',  Add, OpType.Unary,  0),
            new Operator('-',  Sub, OpType.Unary,  0),
            new Operator('~', Comp, OpType.Unary,  0),
            new Operator("**", Pow, OpType.Binary, 7),
            new Operator('#', Root, OpType.Binary, 7),
            new Operator('*',  Mul, OpType.Binary, 6),
            new Operator('/',  Div, OpType.Binary, 6),
            new Operator('%',  Mod, OpType.Binary, 6),
            new Operator('+',  Add, OpType.Binary, 5),
            new Operator('-',  Sub, OpType.Binary, 5),
            new Operator("<<", Shl, OpType.Binary, 4),
            new Operator(">>", Shr, OpType.Binary, 4),
            new Operator('&',  And, OpType.Binary, 3),
            new Operator('^',  Xor, OpType.Binary, 2),
            new Operator('|',  Or,  OpType.Binary, 1)
        };

        static new Constant[] Constants =
        {
        };

        protected override long ParseNumber(ref char c)
        {
            return base.ParseInt(ref c);
        }
    }
}
