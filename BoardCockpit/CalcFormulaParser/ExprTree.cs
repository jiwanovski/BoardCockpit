using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFormulaParser
{
    public class ExprTree<T>
    {
        public ExprTree(ExprNode expr)
        {
            this.expr = expr;
        }

        private ExprNode expr;

        public T Evaluate(T x)
        {
            ExprNode.XValue = x;
            return expr != null ? expr.Evaluate() : default(T);
        }

        public override string ToString()
        {
            return (expr != null) ? expr.ToString() : String.Empty;
        }

        public abstract class ExprNode
        {
            public abstract T Evaluate();

            static public T XValue; //??? NOT thread safe
        }

        public class OpNode : ExprNode
        {
            internal OpNode(FctParser<T>.Operator op, ExprNode left, ExprNode right)
            {
                this.op = op;
                this.left = left;
                this.right = right;
            }

            internal FctParser<T>.Operator op;
            internal ExprNode left, right;

            public override T Evaluate()
            {
                return op.op((left != null) ? left.Evaluate() : default(T), right.Evaluate());
            }

            public override string ToString()
            {
                StringBuilder str = new StringBuilder();
                if (left != null)
                {
                    AppendToStr(str, left);
                    str.Append(' ');
                }
                str.Append(op.sOp);
                if (left != null)
                    str.Append(' ');

                AppendToStr(str, right);

                return str.ToString();
            }

            private void AppendToStr(StringBuilder str, ExprNode expr_node)
            {
                OpNode op_node = expr_node as OpNode;
                bool bOpNode = (op_node != null) && op.priority > op_node.op.priority;
                if (bOpNode)
                    str.Append('(');
                str.Append(expr_node);
                if (bOpNode)
                    str.Append(')');
            }
        }

        public class FuncNode : ExprNode
        {
            internal FuncNode(FctParser<T>.Function func, IEnumerable<ExprNode> pars)
            {
                this.func = func;
                this.pars = pars;
            }

            internal FctParser<T>.Function func;
            internal IEnumerable<ExprNode> pars;

            public override T Evaluate()
            {
                List<T> ps = new List<T>();
                if (pars != null)
                    foreach (ExprNode p in pars)
                        ps.Add(p.Evaluate());

                return func.fct.Call(ps.ToArray());
            }

            public override string ToString()
            {
                StringBuilder str = new StringBuilder();
                str.Append(func.sFct);
                str.Append('(');
                if (pars != null)
                {
                    bool bComma = false;
                    foreach (ExprNode p in pars)
                    {
                        if (bComma)
                            str.Append(',');
                        else
                            bComma = true;

                        str.Append(p);
                    }
                }
                str.Append(')');
                return str.ToString();
            }
        }

        public class ValueNode : ExprNode
        {
            internal ValueNode(T value)
            {
                this.value = value;
            }

            internal T value;

            public override T Evaluate()
            {
                return value;
            }

            public override string ToString()
            {
                return value.ToString();
            }
        }

        public class VarNode : ExprNode
        {
            internal VarNode(string sVar)
            {
                this.sVar = sVar;
            }

            internal string sVar;

            public override T Evaluate()
            {
                return XValue; // todo ???
            }

            public override string ToString()
            {
                return sVar;
            }
        }

        public class ConstNode : ExprNode
        {
            internal ConstNode(FctParser<T>.Constant constant)
            {
                this.constant = constant;
            }

            internal FctParser<T>.Constant constant;

            public override T Evaluate()
            {
                return constant.value;
            }

            public override string ToString()
            {
                return constant.sConst;
            }
        }
    }
}
