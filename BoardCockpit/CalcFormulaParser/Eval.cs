using System;
using System.Collections.Generic;

using Str = System.Collections.Generic.IEnumerable<char>;

namespace CalcFormulaParser
{
    public class Eval<T> : FctParser<T>
    {
        public Eval()
        {
        }

        public Eval(bool bOptimize)
        {
            this.bOptimize = bOptimize;
        }

        public void Parse(FctParser<T> parser, Str function)
        {
            CopyFrom(parser);

            node_stack = new Stack<ExprTree<T>.ExprNode>();

            Parse(function, default(T));

            // ExprTree aufbauen
            expr = new ExprTree<T>(node_stack.Pop());
        }

        public T Evaluate(T x)
        {
            return (expr != null) ? expr.Evaluate(x) : default(T);
        }

        public override string ToString()
        {
            return (expr != null) ? expr.ToString() : String.Empty;
        }

        // protected

        protected bool bOptimize;

        protected ExprTree<T> expr;

        protected Stack<ExprTree<T>.ExprNode> node_stack;

        protected override T ExecuteFunction(FctParser<T>.Function f, params T[] x)
        {
            List<ExprTree<T>.ExprNode> pars = new List<ExprTree<T>.ExprNode>();
            for (int i = 0; i < x.Length; i++)
                pars.Insert(0, node_stack.Pop());

            if (bOptimize)
            {
                int i;
                for (i = 0; i < x.Length; i++)
                {
                    ExprTree<T>.ValueNode value = pars[i] as ExprTree<T>.ValueNode;
                    if (value != null)
                        x[i] = value.value;
                    else
                        break;
                }
                if (i >= x.Length)
                {
                    T y = f.fct.Call(x);
                    return SetNode(new ExprTree<T>.ValueNode(y));
                }
            }

            return SetNode(new ExprTree<T>.FuncNode(f, pars));
        }

        protected override T ExecuteOperator(FctParser<T>.Operator op, T x, T y)
        {
            bool bBinaryOp = (op.type == FctParser<T>.OpType.Binary);
            ExprTree<T>.ExprNode right = node_stack.Pop();
            ExprTree<T>.ExprNode left = bBinaryOp ? node_stack.Pop() : null;

            if (bOptimize)
            {
                ExprTree<T>.ValueNode value_left = left as ExprTree<T>.ValueNode;
                ExprTree<T>.ValueNode value_right = right as ExprTree<T>.ValueNode;
                if ((!bBinaryOp || value_left != null) && value_right != null)
                {
                    T v_left = bBinaryOp ? value_left.value : default(T);
                    value_right.value = op.op(v_left, value_right.value);

                    return SetNode(value_right);
                }
            }

            return SetNode(new ExprTree<T>.OpNode(op, left, right));
        }

        protected override T ExecuteConstant(FctParser<T>.Constant ct)
        {
            return SetNode(new ExprTree<T>.ConstNode(ct));
        }

        protected override T ExecuteVariable(string sVar)
        {
            return SetNode(new ExprTree<T>.VarNode(sVar));
        }

        protected override T ExecuteNumber(T x)
        {
            return SetNode(new ExprTree<T>.ValueNode(x));
        }

        private T SetNode(ExprTree<T>.ExprNode node)
        {
            node_stack.Push(node);

            return default(T);
        }
    }
}
