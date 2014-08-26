using BoardCockpit.Models;
using CalcFormulaParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BoardCockpit.Helpers
{
    public class Calculator
    {
        private IParser fp;
        private Type[] parsers =
        {
            typeof(ConditionalParser<double, FormulaParser>),
            typeof(ConditionalParser<long, IntParser>),
            typeof(ConditionalParser<bool, LogicParser>),
            typeof(FormulaParser),
            typeof(IntParser),
            typeof(LogicParser)
        };
        private int nParser = -1;
        private string sInfo;

        private string calcFormula = "";

        public Calculator()
        {
            FillCBParsers();
            int nParser = 3;
            fp = (IParser)Activator.CreateInstance(parsers[nParser]);
        }

        public bool CalculateDetail(List<FinancialData> financialDatas, string formula, ref decimal result) 
        {
            calcFormula = "";
            bool resolvedFormula = true;
            int i = 1;
            string[] elements = formula.Split(' ');
            foreach (string element in elements)
            {
                if (element != " ")
                {
                    if ((i % 2) == 0)
                    {
                        calcFormula += element;
                    }
                    else
                    {
                        if (financialDatas.Where(y => y.XbrlName == element).Count() > 0)
                        {
                            List<FinancialData> test = financialDatas.Where(y => y.XbrlName == element).ToList();
                            FinancialData data = financialDatas.Where(y => y.XbrlName == element).Single();
                            calcFormula += data.Value;
                        }
                        else 
                        { 
                            resolvedFormula = false;
                        }
                    }
                }
                i += 1;
            }

            if (resolvedFormula) { 
                MethodInfo calculate = GetType().GetMethod("Calculate", BindingFlags.Instance | BindingFlags.NonPublic);
                if (calculate != null)
                    calculate.MakeGenericMethod(fp.Type).Invoke(this, new object[] { fp });

                result = System.Convert.ToDecimal(calcFormula); 
            }
            
            return resolvedFormula;
        }

        private void Calculate<T>(FctParser<T> fp)
        {
            try
            {
                string sFct = calcFormula;
                double x;

                Double.TryParse("", out x);

                T t = (T)Convert.ChangeType(x, typeof(T));
                T y = fp.Parse(sFct, t);

                calcFormula = y.ToString();
            }
            catch (FctException ex)
            {

            }
        }

        private void FillCBParsers()
        {
            foreach (Type type in parsers)
            {
                string sName = type.Name;
                if (type.IsGenericType)
                {
                    sName = sName.Remove(sName.IndexOf('`'));
                    Type[] args = type.GetGenericArguments();
                    string[] sArgs = new string[args.Length];
                    for (int i = 0; i < args.Length; i++)
                        sArgs[i] += args[i].Name;

                    sName += '<' + String.Join(", ", sArgs) + '>';
                }                
            }            
        }
    }
}