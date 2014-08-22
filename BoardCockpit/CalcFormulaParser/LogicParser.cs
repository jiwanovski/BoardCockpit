using System;
using System.Collections.Generic;

namespace CalcFormulaParser
{
    public class LogicParser : FctParser<bool>
    {
        public LogicParser()
        {
            base.Functions = Functions;
            base.Operators = Operators;
            base.Constants = Constants;

            base.MaxOpPriority = 2;
        }

        static new Function[] Functions =
        {
        };

        static bool Not(bool x, bool y)
        {
            return !y;
        }

        static bool Nand(bool x, bool y)
        {
            return !(x && y);
        }

        static bool And(bool x, bool y)
        {
            return x && y;
        }

        static bool Nor(bool x, bool y)
        {
            return !(x || y);
        }

        static bool Or(bool x, bool y)
        {
            return x || y;
        }

        static new Operator[] Operators =
        {
            new Operator("!", Not, OpType.Unary,  0),
            new Operator("!&", Nand,  OpType.Binary, 2),
            new Operator("&", And, OpType.Binary, 2),
            new Operator("!|", Nor, OpType.Binary, 1),
            new Operator("|", Or,  OpType.Binary, 1)
        };

        static new Constant[] Constants =
        {
            new Constant("T", true),
            new Constant("F", false)
        };

        protected override bool ParseNumber(ref char c)
        {
            switch (c)
            {
                case '0':
                    c = GetChar();
                    return false;

                case '1':
                    c = GetChar();
                    return true;
            }

            throw new FctException("Only 0 and 1 allowed");
        }
    }
}
