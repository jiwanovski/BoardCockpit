using System;
using System.Collections.Generic;

namespace CalcFormulaParser
{
    public class ConditionalParser<T, Parser> : FctParser<T>
        where T : IComparable<T>, IEquatable<T>
        where Parser : FctParser<T>, new()
    {
        public ConditionalParser()
        {
            base.Functions = Functions;
            base.Operators = Operators;
            base.Constants = Constants;

            base.MaxOpPriority = 4;

            parser = new Parser();
        }

        Parser parser;

        static new Function[] Functions =
        {
        };

        static T Nor(T x, T y)
        {
            return Not(Or(x, y));
        }

        static T Nand(T x, T y)
        {
            return Not(And(x, y));
        }

        static T Not(T x)
        {
            return x.Equals(False) ? True : False;
        }

        static T Not(T x, T y)
        {
            return Not(y);
        }

        static T Or(T x, T y)
        {
            return !x.Equals(False) ? True :
                    y.Equals(False) ? False : True;
        }

        static T And(T x, T y)
        {
            return x.Equals(False) ? False :
                   y.Equals(False) ? False : True;
        }

        static T Less(T x, T y)
        {
            return x.CompareTo(y) < 0 ? True : False; // x < y
        }

        static T LessEqual(T x, T y)
        {
            return x.CompareTo(y) <= 0 ? True : False; // x <= y
        }

        static T Greater(T x, T y)
        {
            return x.CompareTo(y) > 0 ? True : False; // x > y
        }

        static T GreaterEqual(T x, T y)
        {
            return x.CompareTo(y) >= 0 ? True : False; // x >= y
        }

        static T Equal(T x, T y)
        {
            return x.Equals(y) ? True : False;
        }

        static T NotEqual(T x, T y)
        {
            return !x.Equals(y) ? True : False;
        }

        static new Operator[] Operators =
        {
            new Operator("!", Not, OpType.Unary,  0),
            new Operator("<", Less, OpType.Binary, 4),
            new Operator("<=", LessEqual, OpType.Binary, 4),
            new Operator(">", Greater, OpType.Binary, 4),
            new Operator(">=", GreaterEqual, OpType.Binary, 4),
            new Operator("==", Equal, OpType.Binary, 3),
            new Operator("!=", NotEqual, OpType.Binary, 3),
            new Operator("<>", NotEqual, OpType.Binary, 3),
            new Operator("!&", Nand, OpType.Binary, 2),
            new Operator("&&", And, OpType.Binary, 2),
            new Operator("!|", Nor, OpType.Binary, 1),
            new Operator("||", Or,  OpType.Binary, 1)
        };

        static new Constant[] Constants =
        {
            new Constant("T", True),
            new Constant("F", False),
            new Constant("true", True),
            new Constant("false", False)
        };

        static readonly T True = (T)Convert.ChangeType(1, typeof(T));

        static readonly T False = (T)Convert.ChangeType(0, typeof(T));

        protected override bool IsNumberChar(char c)
        {
            return true;
        }

        protected override T ParseNumber(ref char c)
        {
            return parser.Parse(ref c, ref Functionator, xValue);
        }
    }
}
