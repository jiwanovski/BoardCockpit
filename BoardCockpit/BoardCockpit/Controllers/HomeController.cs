using BoardCockpit.DAL;
using BoardCockpit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalcFormulaParser;
using System.Reflection;
using System.Drawing;
using BoardCockpit.Helpers;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;

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

        public static readonly object[] BerlinData = { -0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0 };
        public static readonly string[] Categories = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        public static readonly object[] LondonData = { 3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8 };
        public static readonly object[] NewYorkData = { -0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5 };
        public static readonly object[] TokioData = { 7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6 };
        public ActionResult Index(string Formulas)
        {
            ViewBag.ActiveSidebar = "Home";
            List<Formula> formulas = db.Formulas.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            //int i = 0;
            foreach (Formula item in formulas)
            {
                items.Add(new SelectListItem
                                    {
                                        Text = item.Name,
                                        Value = item.FormulaID.ToString()
                                    });
                
            }
            ViewBag.Formulas = items;

            List<List<ReportingValues>> totalReportingValues = new List<List<ReportingValues>>();
            List<string> categories = new List<string>();
            categories.Add("2008");
            categories.Add("2009");
            List<ReportingValues> reportingValues = new List<ReportingValues>();
            List<Company> companies = db.Companies.ToList();
            List<Series> testSeries = new List<Series>();
            foreach (Company company in companies)
            {
                foreach (string category in categories)
                {                                    
                    //List<Context> contexts = company.Contexts.Where(i => i.Instant.Year.ToString() == category).ToList();
                    
                    //foreach (Context context in contexts)
                    //{
                    //    List<CalculatedKPI> calcKPIs = context.CalculatedKPIs.Where(i => i.FormulaDetail.FormulaID.ToString() == Formulas).ToList();
                    //    if (calcKPIs.Count() > 0) 
                    //    {
                    //        CalculatedKPI calKPI = calcKPIs.First();
                    //        reportingValues.Add(
                    //            new ReportingValues
                    //            {
                    //                ContextID = context.ContextID,
                    //                FormulaID = calKPI.FormulaDetailID,
                    //                Date = context.Instant,
                    //                Value = calKPI.Value,
                    //                CompanyName = context.Company.Name
                    //            }
                    //        );
                        
                    //    }
                    //}
                    List<ContextContainer> contextContailers = company.ContextContainers.Where(i => i.Year.ToString() == category).ToList();
                    foreach (ContextContainer contextContainer in contextContailers)
                    {
                        List<CalculatedKPI> calcKPIs = contextContainer.CalculatedKPIs.Where(i => i.FormulaDetail.FormulaID.ToString() == Formulas).ToList();
                        if (calcKPIs.Count() > 0)
                        {
                            CalculatedKPI calKPI = calcKPIs.First();
                            reportingValues.Add(
                                new ReportingValues
                                {
                                    ContextContainerID = contextContainer.ContextContainerID,
                                    FormulaID = calKPI.FormulaDetailID,
                                    Year = contextContainer.Year,
                                    Value = calKPI.Value,
                                    CompanyName = contextContainer.Company.Name
                                }
                            );

                        }
                    }                    
                }
                //List<ReportingValues> reportingValues2 = new List<ReportingValues>(reportingValues);
                //totalReportingValues.Add(reportingValues2);
                //reportingValues.Clear();
                var nodeValues = from test2 in reportingValues
                                 select (object)test2.Value;
                testSeries.Add(new Series
                {
                    Name = company.Name,
                    Data = new Data(nodeValues.ToArray())
                });
                reportingValues.Clear();
            }

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart
                {
                    DefaultSeriesType = ChartTypes.Line,
                    MarginRight = 130,
                    MarginBottom = 25,
                    ClassName = "chart"
                })
                .SetTitle(new Title
                {
                    Text = "Monthly Average Temperature",
                    X = -20
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Source: WorldClimate.com",
                    X = -20
                })
                .SetXAxis(new XAxis { Categories = categories.ToArray() })
                .SetYAxis(new YAxis
                {
                    Title = new YAxisTitle { Text = "Temperature (°C)" },
                    PlotLines = new[]
                    {
                        new YAxisPlotLines
                        {
                            Value = 0,
                            Width = 1,
                            Color = ColorTranslator.FromHtml("#808080")
                        }
                    }
                })
                .SetTooltip(new Tooltip
                {
                    Formatter = @"function() {
                                        return '<b>'+ this.series.name +'</b><br/>'+
                                    this.x +': '+ this.y +'°C';
                                }"
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -10,
                    Y = 100,
                    BorderWidth = 0
                })
                .SetSeries(testSeries.ToArray());
                
            ViewBag.Chart = chart; 
            return View(chart);
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