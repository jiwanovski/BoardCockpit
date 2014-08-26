using BoardCockpit.DAL;
using BoardCockpit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalcFormulaParser;
using System.Reflection;

namespace BoardCockpit.Controllers
{
    public class HomeController : Controller
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
        private BoardCockpitContext db = new BoardCockpitContext();
        public ActionResult Index()
        {
            ViewBag.ActiveSidebar = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.ActiveSidebar = "Home";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.ActiveSidebar = "Home";

            return View();
        }

        public ActionResult SetupView()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Home";
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Search2(string tags)
        {            
            calcFormula = "";
            int i = 1;
            string[] words = tags.Split(' ');
            foreach (string word in words)
            {
                if (word != " ")
                {
                    if ((i % 2) == 0) 
                    {
                        calcFormula += word;
                    }
                    else 
                    { 
                        FinancialData data = db.FinancialDatas.Where(x => x.ContextID == 168).Where(y => y.XbrlName == word).Single();
                        calcFormula += data.Value;
                    }
                }
                i += 1;
            }
            FillCBParsers();
            int nParser = 3;
            fp = (IParser)Activator.CreateInstance(parsers[nParser]);
            MethodInfo calculate = GetType().GetMethod("Calculate", BindingFlags.Instance | BindingFlags.NonPublic);
            if (calculate != null)
                calculate.MakeGenericMethod(fp.Type).Invoke(this, new object[] { fp });

            ViewBag.Result = calcFormula;
            return View("Search");
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
                //textBoxResult.Text = y.ToString(); //"G14");
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

                //comboBoxParser.Items.Add(sName);
            }

            //comboBoxParser.SelectedIndex = 3;
        }

        //private void Start<T>(FctParser<T> fp)
        //{
        //    //Cursor.Current = Cursors.WaitCursor;

        //    string sFct = textBoxFct.Text;
        //    bool bEvaluate = checkBoxEvaluate.Checked;
        //    long counter = (long)numericUpDownCounter.Value;

        //    Eval<T> eval = new Eval<T>(true);

        //    Stopwatch sw = new Stopwatch();

        //    try
        //    {
        //        try
        //        {
        //            if (bEvaluate)
        //            {
        //                textBoxResult.Text = "";
        //                eval.Parse(fp, sFct);
        //            }

        //            sw.Start();

        //            if (bEvaluate)
        //            {
        //                T y = default(T);
        //                for (long x = 0; x <= counter; x++)
        //                    y = eval.Evaluate(y);
        //            }
        //            else
        //            {
        //                T y = default(T);
        //                for (long x = 0; x <= counter; x++)
        //                    y = fp.Parse(sFct, y);
        //            }
        //        }
        //        finally
        //        {
        //            sw.Stop();

        //            Cursor.Current = Cursors.Default;
        //        }

        //        if (bEvaluate)
        //            textBoxResult.Text = eval.ToString();

        //        decimal time = sw.ElapsedMilliseconds / 1000m;
        //        textBoxTime.Text = time.ToString();
        //    }
        //    catch (FctException ex)
        //    {
        //        textBoxTime.Text = "";
        //        MessageBox.Show(ex.Message, "Parser Error");
        //    }
        //}

        public JsonResult GetCustomers()
        {
            BoardCockpitContext db = new BoardCockpitContext();
            var customers = from cust in db.TaxonomyFileNodes
                            select cust.NodeName;
            customers = customers.Distinct();
            return Json(customers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomers3(string term)
        {
            BoardCockpitContext db = new BoardCockpitContext();
            var customers = from cust in db.TaxonomyFileNodes.Where(c => c.NodeName.Contains(term))
                            select cust.NodeName;
            customers = customers.Distinct();
            return Json(customers, JsonRequestBehavior.AllowGet);
        }
    }
}