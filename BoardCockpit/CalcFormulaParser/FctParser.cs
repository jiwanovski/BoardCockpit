using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Str = System.Collections.Generic.IEnumerable<char>;
using OpPriority = System.SByte;

namespace CalcFormulaParser
{
    public interface IParser
    {
        Type Type { get; }
        string GetInfo();
    }

    public interface IParser<T> : IParser
    {
        T Parse(Str function, T x);
    }

    public class FctParser<T> : IParser<T>
    {
        public T Parse(Str function, T x)
        {
            sFunction = function;
            xValue = x;

            Functionator = function.GetEnumerator();

            char c = '\0';
            T y = Expression(ref c);
            if (c != '\0')
                throw new FctException("Unexpected character at end of expression", c);

            return y;
        }

        public T Parse(ref char c, ref IEnumerator<char> functionator, T x)
        {
            xValue = x;
            Functionator = functionator;

            T y = Calculate(ref c, 1);

            functionator = Functionator;

            return y;
        }

        public Type Type
        {
            get { return typeof(T); }
        }

        public static CultureInfo Culture = new CultureInfo(String.Empty);

        protected char Dot
        {
            get { return Culture.NumberFormat.NumberDecimalSeparator[0]; }
        }

        protected bool IgnoreCase = true;

        // Variable

        protected string X = "x";

        // Bracket

        protected struct Bracket
        {
            public Bracket(char bra, char ket)
            {
                this.bra = bra;
                this.ket = ket;
            }

            public char bra, ket;

            public static Bracket Default = new Bracket('(', ')');
        }

        // Function

        //protected internal delegate T Fct(T x);
        //protected internal delegate T FctX(params T[] x);

        protected internal class Function
        {
            public Function(string sFct, Fct<T> fct)
            {
                this.sFct = sFct;
                this.fct = new FctCaller1<T>(fct);
            }
            public Function(string sFct, Fct2<T> fct)
            {
                this.sFct = sFct;
                this.fct = new FctCaller2<T>(fct);
            }
            public Function(string sFct, FctX<T> fct)
            {
                this.sFct = sFct;
                this.fct = new FctCallerX<T>(fct);
            }

            public string sFct;
            public FctCaller<T> fct;
        }

        protected IEnumerable<Function> Functions;
        protected Bracket FunctionBracket = Bracket.Default;
        protected char FunctionParameterSeparator = ',';

        // Operator

        protected internal delegate T Op(T x, T y);

        protected internal enum OpType
        {
            Unary, Binary
        }

        protected internal class Operator
        {
            public Operator(string sOp, Op op, OpType type, OpPriority pri)
            {
                this.sOp = sOp;
                this.op = op;
                this.type = type;
                this.priority = pri;
            }

            public Operator(char cOp, Op op, OpType type, OpPriority pri)
                : this(new string(cOp, 1), op, type, pri)
            {
            }

            public string sOp;
            public Op op;
            public OpType type;
            public OpPriority priority;
        }

        protected OpPriority MaxOpPriority;
        protected IEnumerable<Operator> Operators;
        protected Bracket OperatorBracket = Bracket.Default;

        // Constant

        protected internal class Constant
        {
            public Constant(string sConst, T value)
            {
                this.sConst = sConst;
                this.value = value;
            }

            public string sConst;
            public T value;
        }

        protected IEnumerable<Constant> Constants;

        // Fields

        protected Str sFunction;
        protected T xValue;

        protected IEnumerator<char> Functionator;

        // Methods

        protected char GetChar()
        {
            char c;
            do
            {
                c = GetNextChar();
            }
            while (c != '\0' && Char.IsWhiteSpace(c));

            return c;
        }

        protected char GetNextChar()
        {
            return Functionator.MoveNext() ? Functionator.Current : '\0';
        }

        protected void CopyFrom(FctParser<T> parser)
        {
            IgnoreCase = parser.IgnoreCase;
            X = parser.X;

            Functions = parser.Functions;
            FunctionBracket = parser.FunctionBracket;

            MaxOpPriority = parser.MaxOpPriority;
            Operators = parser.Operators;
            OperatorBracket = parser.OperatorBracket;

            Constants = parser.Constants;
        }

        protected virtual T ExecuteFunction(Function f, params T[] x)
        {
            return f.fct.Call(x);
        }

        protected virtual T ExecuteOperator(Operator op, T x, T y)
        {
            return op.op(x, y);
        }

        protected virtual T ExecuteConstant(Constant ct)
        {
            return ct.value;
        }

        protected virtual T ExecuteVariable(string sVar)
        {
            return xValue;
        }

        protected virtual T ExecuteNumber(T x)
        {
            return x;
        }

        // Algorithmus
        /*
        Funktionstext zeichenweise parsen:
        Terminalsymbole sind Zahlen, Variablen, Konstanten u. Funktionen
        Funktionen werden erst nach Parsen des Parameters ausgeführt (Präfix-Notation)
        Unäre Operatoren können vor einem Terminalsymbol stehen (Präfix-Notation)
        Binäre Operatoren stehen zwischen zwei Terminalsymbolen (Infix-Notation)
        Je nach Priorität werden die Operationen ausgeführt,
        bei gleicher Priorität erfolgt die Auswertung von links nach rechts
        Operatoren-Klammern haben die höchste Priorität
        */

        T Expression(ref char c)
        {
            c = GetChar();
            T y = Calculate(ref c, 1);

            return y;
        }

        T Calculate(ref char c, OpPriority op_pri)
        {
            T y = Calc(ref c, op_pri);

            Operator op;
            while ((op = GetOperator(ref c, OpType.Binary, op_pri)) != null)
            {
                T y2 = Calc(ref c, op_pri);

                y = ExecuteOperator(op, y, y2);  // Binäre Operation ausführen
            }

            return y;
        }

        T Calc(ref char c, OpPriority op_pri)
        {
            T y;

            if (op_pri < MaxOpPriority)
                y = Calculate(ref c, (OpPriority)(op_pri + 1));
            else
                y = GetTerminal(ref c);

            return y;
        }

        T GetTerminal(ref char c)
        {
            T y;
            Operator op = GetOperator(ref c, OpType.Unary, 0);

            if (c == OperatorBracket.bra)
            {
                y = Expression(ref c);
                if (c != OperatorBracket.ket)
                    throw new FctException("Missing bracket", OperatorBracket.ket);
                c = GetChar();
            }
            else if (IsNumberChar(c))
            {
                y = GetNumber(ref c);
                y = ExecuteNumber(y);
            }
            else if (IsIdentifierChar(c, true))
            {
                string sId = GetIdentifier(ref c);
                // Variable, Konstante oder Funktion
                if (IsVariable(sId))
                    y = ExecuteVariable(sId);
                else
                {
                    Constant ct = IsConstant(sId);
                    if (ct != null)
                        y = ExecuteConstant(ct);
                    else
                    {
                        Function f = IsFunction(sId);
                        if (f != null)
                        {
                            if (c != FunctionBracket.bra)
                                throw new FctException("Missing function bracket", FunctionBracket.bra);

                            List<T> xs = new List<T>();
                            int paramCount = f.fct.ParamCount;
                            while (paramCount < 0 || xs.Count < paramCount)
                            {
                                T x = Expression(ref c);
                                xs.Add(x);

                                if (paramCount < 0 && c == FunctionBracket.ket)
                                    break;

                                if (paramCount < 0 || xs.Count < paramCount)
                                    if (c != FunctionParameterSeparator)
                                        throw new FctException("Missing function parameter");
                            }
                            if (c == FunctionParameterSeparator)
                                throw new FctException("Too many function parameters");
                            if (c != FunctionBracket.ket)
                                throw new FctException("Missing function bracket", FunctionBracket.ket);

                            c = GetChar();
                            y = ExecuteFunction(f, xs.ToArray()); // Funktion ausführen
                        }
                        else
                            throw new FctException("Unknown identifier", sId);
                    }
                }
            }
            else
                throw new FctException("Number or identifier expected instead of", c);

            if (op != null)
                y = ExecuteOperator(op, default(T), y); // Unäre Operation ausführen

            return y;
        }

        T GetNumber(ref char c)
        {
            T y = ParseNumber(ref c);

            if (Char.IsWhiteSpace(c))
                c = GetChar();

            return y;
        }

        protected virtual bool IsNumberChar(char c)
        {
            return Char.IsDigit(c) || c == Dot;
        }

        protected virtual T ParseNumber(ref char c)
        {
            double y = ParseDouble(ref c);

            return (T)Convert.ChangeType(y, typeof(T));
        }

        protected double ParseDouble(ref char c)
        {
            StringBuilder s = new StringBuilder(32);

            bool bDot = (c == Dot);

            do
            {
                s.Append(c);
                c = GetNextChar();
                if (c == Dot)
                {
                    if (bDot)
                        break;
                    else
                        bDot = true;
                }
            }
            while (Char.IsDigit(c) || c == Dot);

            if (Char.ToUpper(c) == 'E')
            {
                s.Append(c);
                c = GetNextChar();
                if (c == '+' || c == '-')
                {
                    s.Append(c);
                    c = GetNextChar();
                }

                while (Char.IsDigit(c))
                {
                    s.Append(c);
                    c = GetNextChar();
                }
            }

            return Double.Parse(s.ToString(), Culture.NumberFormat);
        }

        protected long ParseInt(ref char c)
        {
            int y = 0;

            while (Char.IsDigit(c))
            {
                y = 10 * y + (c - '0');

                c = GetNextChar();
            }

            return y;
        }

        delegate bool IsTokenChar(char c);

        string GetToken(ref char c, IsTokenChar IsTokenChar)
        {
            string s = String.Empty;

            s += c;
            while (true)
            {
                c = GetNextChar();
                if (!IsTokenChar(c))
                    break;

                s += c;
            }

            if (Char.IsWhiteSpace(c))
                c = GetChar();

            return s;
        }

        Operator GetOperator(ref char c, OpType op_type, OpPriority op_pri)
        {
            if (!IsOpChar(c))
                return null;

            // save current Functionator and character
            IEnumerator<char> functionator = (IEnumerator<char>)((CharEnumerator)Functionator).Clone();
            char cOp = c;

            string sOp = GetOperator(ref c);
            Operator op = IsOperator(sOp, op_type, op_pri);
            if (op == null)
            {
                Functionator = functionator;
                c = cOp;
            }

            return op;
        }

        string GetOperator(ref char c)
        {
            return GetToken(ref c, IsOpChar);
        }

        protected bool IsBracketChar(Bracket bracket, char c)
        {
            return (c == bracket.bra) || (c == bracket.ket);
        }

        protected bool IsOperatorBracket(char c)
        {
            return IsBracketChar(OperatorBracket, c);
        }

        protected bool IsOpChar(char c)
        {
            return !IsOperatorBracket(c) && IsOperatorChar(c);
        }

        protected virtual bool IsOperatorChar(char c)
        {
            return (c != Dot) && (Char.IsPunctuation(c) || Char.IsSymbol(c));
        }

        string GetIdentifier(ref char c)
        {
            return GetToken(ref c, delegate(char cId) { return IsIdentifierChar(cId, false); });
        }

        protected virtual bool IsIdentifierChar(char c, bool bFirstChar)
        {
            return Char.IsLetter(c);
        }

        bool IsEqual(string sX, string sY)
        {
            return String.Compare(sX, sY, IgnoreCase, Culture) == 0;
        }

        bool IsVariable(string sVar)
        {
            return IsEqual(sVar, X);
        }

        Function IsFunction(string sFct)
        {
            foreach (Function f in Functions)
                if (IsEqual(sFct, f.sFct))
                    return f;

            return null;
        }

        Operator IsOperator(string sOp, OpType op_type, OpPriority op_pri)
        {
            foreach (Operator op in Operators)
                if (op_type == op.type && sOp == op.sOp)
                    if (op_pri == 0 || op_pri == op.priority)
                        return op;

            return null;
        }

        Constant IsConstant(string sConst)
        {
            foreach (Constant c in Constants)
                if (IsEqual(sConst, c.sConst))
                    return c;

            return null;
        }

        // Info

        public string GetInfo()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(GetInfo<Function>("Functions", Functions, delegate(Function f) { return f.sFct; }));
            str.AppendLine(GetInfo<Operator>("Operators", Operators, delegate(Operator op) { return op.sOp; }));
            str.AppendLine(GetInfo<Constant>("Constants", Constants, delegate(Constant ct) { return ct.sConst; }));
            str.Append(GetVariablesInfo("Variables"));

            return str.ToString();
        }

        protected delegate string InfoHandler<TList>(TList t);

        protected string GetInfo<TList>(string sName, IEnumerable<TList> list, InfoHandler<TList> info)
        {
            StringBuilder str = new StringBuilder();

            str.Append(sName);
            str.AppendLine(":");
            bool bList = false;
            foreach (TList t in list)
            {
                str.Append(info(t));
                str.Append(' ');

                bList = true;
            }
            if (bList)
                str.AppendLine();

            return str.ToString();
        }

        protected string GetVariablesInfo(string sName)
        {
            StringBuilder str = new StringBuilder();

            str.Append(sName);
            str.AppendLine(":");
            str.Append(X);

            return str.ToString();
        }
    }

    public class FctException : Exception
    {
        public FctException(string sMessage)
            : base(sMessage)
        {
        }

        public FctException(string sMessage, object parameter)
            : base(sMessage + ": " + parameter.ToString())
        {
        }
    }
}
