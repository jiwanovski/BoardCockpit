using BoardCockpit.DAL;
using BoardCockpit.Helpers;
using BoardCockpit.Models;
using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using BoardCockpit.ViewModels;
using System.Net;
using BoardCockpit.DAL;
using BoardCockpit.Models;
using DotNet.Highcharts.Options;
using System.Drawing;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;

namespace BoardCockpit.Controllers
{
    public class DashboardController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();
        
        //List<ContextContainer> contextContailers;
        List<Company> companies;
        private int fromYear;
        private int toYear;

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            var viewModel = new DashboardData();
            viewModel.Companies = db.Companies.Include(i => i.ContextContainers).ToList();
            viewModel.ContextContainers = db.ContextContainers.Include(i => i.CalculatedKPIs).Include(i => i.Contexts).Include(i => i.Company).ToList();

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
            ViewBag.Formulas1 = items;
            ViewBag.Formulas2 = items;
            ViewBag.Formulas3 = items;
            ViewBag.Formulas4 = items;
            ViewBag.FromPeriod = "2008";
            ViewBag.ToPeriod = "2009";

            Highcharts chart = new Highcharts("chart");
            //ViewBag.Chart = AjaxLoadedChart();
            return View(viewModel);
        }

        public ActionResult AjaxLoadedChart(string chartName, string formulaID)
        //public Highcharts AjaxLoadedChart()
        {
            AjaxLoadedChart chart;
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
            ViewBag.Formulas1 = items;
            ViewBag.Formulas2 = items;
            ViewBag.Formulas3 = items;
            ViewBag.Formulas4 = items;
            ViewBag.FormulaID = formulaID;

            var viewModel = new DashboardData();
            viewModel.Companies = db.Companies.Include(i => i.ContextContainers).ToList();
            viewModel.ContextContainers = db.ContextContainers.Include(i => i.CalculatedKPIs).Include(i => i.Contexts).Include(i => i.Company).ToList();

            companies = db.Companies.Include(n => n.ContextContainers).ToList();
            var data = from company in companies
                       select company.CompanyID;
            ViewBag.Companies = data.ToArray();

            chart = new Helpers.AjaxLoadedChart();
            chart.Title = "Mein erster Titel";
            chart.Subtitle = "Mein erster Untertitel";
            List<string> years = new List<string>();
            //years.Add("2008");
            //years.Add("2009");
            chart.Categories = years;
            List<DotNet.Highcharts.Options.Series> test = new List<DotNet.Highcharts.Options.Series>();
            //test.Add(new DotNet.Highcharts.Options.Series { Name = "Test" });
            chart.DataSeries = new List<DotNet.Highcharts.Options.Series>(); //test;
            chart.ChartName = chartName;
            ViewBag.Graph = chart.Chart;
            //ViewBag.Graph = chart.GetChart(chartName);
            ViewBag.ChartName = chartName;
            return PartialView(viewModel);
        }

        public ActionResult DualAxesLineAndColumnChart(string chartName, string formulaID)
        {
            formulaID = "1";
            chartName = "graph2";
            IChart chart;
            List<Formula> formulas = db.Formulas.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            Formula selectedFormula = db.Formulas.Find(Convert.ToInt16(formulaID));
            Formula relatedFormula = selectedFormula.LinkedFormula;
            //int i = 0;
            foreach (Formula item in formulas)
            {
                items.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.FormulaID.ToString()
                });

            }
            ViewBag.Formulas1 = items;
            ViewBag.Formulas2 = items;
            ViewBag.Formulas3 = items;
            ViewBag.Formulas4 = items;
            ViewBag.FormulaID = formulaID;

            var viewModel = new DashboardData();
            viewModel.Companies = db.Companies.Include(i => i.ContextContainers).ToList();
            viewModel.ContextContainers = db.ContextContainers.Include(i => i.CalculatedKPIs).Include(i => i.Contexts).Include(i => i.Company).ToList();

            companies = db.Companies.Include(n => n.ContextContainers).ToList();
            var data = from company in companies
                       select company.CompanyID;
            ViewBag.Companies = data.ToArray();

            chart = new Helpers.DualAxesLineAndColumnChart();
            chart.Title = "Mein erster Titel";
            chart.Subtitle = "Mein erster Untertitel";
            chart.YAxisSeries = new List<YAxis>();
            chart.DataSeries = new List<Series>();
            //chart.Categories = new List<string>();
            List<string> years = new List<string>();
            //years.Add("2008");
            //years.Add("2009");
            //years.Add("2010");
            //years.Add("2011");
            //years.Add("2012");
            //years.Add("2013");
            //years.Add("2014");
            chart.Categories = years;
            // YAxis for selectedFormula
            YAxis selectedYAxes = new YAxis
                                        {
                                            Labels = new YAxisLabels
                                            {
                                                Formatter = "function() { return this.value; }",
                                                Style = "color: '#89A54E'"
                                            },
                                            Title = new YAxisTitle
                                            {
                                                Text = selectedFormula.Name,
                                                Style = "color: '#89A54E'"
                                            }
                                        };
            chart.YAxisSeries.Add(selectedYAxes);

            //YAxis for relatedFormula
            YAxis relatedYAxes = new YAxis
                                        {
                                            Labels = new YAxisLabels
                                            {
                                                Formatter = "function() { return this.value; }",
                                                Style = "color: '#4572A7'"
                                            },
                                            Title = new YAxisTitle
                                            {
                                                Text = relatedFormula.Name,
                                                Style = "color: '#4572A7'"
                                            },
                                            Opposite = true
                                        };
            chart.YAxisSeries.Add(relatedYAxes);

            //List<string> years = new List<string>();            
            //chart.Categories = years;
            List<DotNet.Highcharts.Options.Series> test = new List<DotNet.Highcharts.Options.Series>();
            
            chart.DataSeries = test;
            chart.ChartName = chartName;
            ViewBag.Graph = chart.Chart;
            ViewBag.ChartName = chartName;

            return View(viewModel);
        }

        public EmptyResult SetPeriod(string _fromYear, string _toYear)
        {
            fromYear = Convert.ToInt32(_fromYear);
            toYear = Convert.ToInt32(_toYear);
            //contextContailers = db.ContextContainers
            //                        .Where(n => n.Year >= fromYear)
            //                        .Where(n => n.Year <= toYear).ToList();

            return new EmptyResult();
        }

        public JsonResult GetPeriod(string fromYear, string toYear)//(int? fromYear, int? toYear)
        {
            List<ContextContainer> contextContainers = db.ContextContainers.ToList();
            contextContainers.OrderBy(n => n.Year);
            
            //reportingYears.Add( new ReportingYear { ReportingYearID = 1, Year = 2008 } );
            //reportingYears.Add( new ReportingYear { ReportingYearID = 1, Year = 2009 } );
            var data = from container in contextContainers
                       select container.Year;
            data.Distinct();
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanies()//(int? fromYear, int? toYear)
        {
            var data = from company in companies
                       select company.CompanyID;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAjaxLoadedChartData(string formulaID, string companyID, string fromYear, string toYear)//(int formulaID, int companyID)
        {
            List<AjaxLoadedChartData> chartDatas = new List<AjaxLoadedChartData>();
            Company company = db.Companies.Find(Convert.ToInt32(companyID));
            List<ContextContainer> contextContainers = company.ContextContainers
                                                                .Where(n => n.Year >= Convert.ToInt32(fromYear))
                                                                .Where(n => n.Year <= Convert.ToInt32(toYear)).ToList();
            
            foreach (ContextContainer item in contextContainers)
	        {
                CalculatedKPI calcKPI;
                if (item.CalculatedKPIs.Where(n => n.FormulaDetail.FormulaID == Convert.ToInt32(formulaID)).Count() > 0) 
                {
                    calcKPI = item.CalculatedKPIs.Where(n => n.FormulaDetail.FormulaID == Convert.ToInt32(formulaID)).First();
                    chartDatas.Add(new AjaxLoadedChartData { Year = item.Year, Value = calcKPI.Value });
                }                 		 
	        }

            return Json(chartDatas.OrderBy(i => i.Year), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDualAxesLineAndColumnChartCatagories(string year)
        {
            int year2 = Convert.ToInt16(year);
            List<ContextContainer> contextContainers = db.ContextContainers
                                                                .Where(n => n.Year == year2).ToList();

            var data = from container in contextContainers
                       select container.Company.Name;
            //data.Distinct();
            return Json(data, JsonRequestBehavior.AllowGet);    
        }

        public JsonResult GetDualAxesLineAndColumnChartData(string companyID, string formulaID, string fromYear, string toYear)//(int formulaID, int companyID)
        {
            List<Series> chartDatas = new List<Series>();
            int companyId2 = Convert.ToInt16(companyID);
            int fromYear2 = Convert.ToInt16(fromYear);
            int toYear2 = Convert.ToInt16(toYear);

            List<ContextContainer> contextContainers = db.ContextContainers
                                                                .Where(n => n.CompanyID == companyId2)
                                                                .Where(n => n.Year >= fromYear2)
                                                                .Where(n => n.Year <= toYear2).ToList();

            Formula selectedFormula = db.Formulas.Find(Convert.ToInt16(formulaID));
            Formula relatedFormula = selectedFormula.LinkedFormula;
            List<object> selectedFormulaValues = new List<object>();
            List<object> relatedFormulaValues = new List<object>();
            CalculatedKPI calcKPI;
            List<AjaxLoadedChartData> selectedChartDatas = new List<AjaxLoadedChartData>();
            List<AjaxLoadedChartData> relatedChartDatas = new List<AjaxLoadedChartData>();

            foreach (var contextContainer in contextContainers)
            {
                if (contextContainer.CalculatedKPIs.Where(n => n.FormulaDetail.FormulaID == selectedFormula.FormulaID).Count() > 0) { 
                    calcKPI = contextContainer.CalculatedKPIs.Where(n => n.FormulaDetail.FormulaID == selectedFormula.FormulaID).First();
                    //selectedFormulaValues.Add(calcKPI.Value);
                    selectedChartDatas.Add(new AjaxLoadedChartData { Year = contextContainer.Year, Value = calcKPI.Value } );
                }
                else
                {
                    selectedFormulaValues.Add(new AjaxLoadedChartData { Year = contextContainer.Year, Value = 0 } );
                }

                if (contextContainer.CalculatedKPIs.Where(n => n.FormulaDetail.FormulaID == relatedFormula.FormulaID).Count() > 0) 
                {
                    calcKPI = contextContainer.CalculatedKPIs.Where(n => n.FormulaDetail.FormulaID == relatedFormula.FormulaID).First();
                    // relatedFormulaValues.Add(calcKPI.Value);
                    relatedChartDatas.Add(new AjaxLoadedChartData {Year = contextContainer.Year, Value = calcKPI.Value } );
                } else {
                    relatedChartDatas.Add(new AjaxLoadedChartData {Year = contextContainer.Year, Value = 0 });
                }
            }
            
            //for (int i = fromYear2; i <= toYear2; i++)
            //{
                //calcKPI = item.CalculatedKPIs.Where(n => n.FormulaDetail.FormulaID == Convert.ToInt32(formulaID)).First();
            //    chartDatas2.Add(new AjaxLoadedChartData { Year = i, Value = 10 });
            //}
            chartDatas.Add(new Series {
                                        Name = selectedFormula.Name, 
                                        Color = ColorTranslator.FromHtml("#4572A7"), 
                                        Type = ChartTypes.Column, 
                                        YAxis = "1",
                                        Data = new Data( selectedChartDatas.OrderBy(n => n.Year).ToArray() )//49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6 })//selectedFormulaValues.ToArray() ) chartDatas2.OrderBy(i => i.Year).ToArray() )
                                      });
            chartDatas.Add(new Series
                                    {
                                        Name = relatedFormula.Name,
                                        Color = ColorTranslator.FromHtml("#89A54E"),
                                        Type = ChartTypes.Spline,
                                        Data = new Data( relatedChartDatas.OrderBy(n => n.Year).ToArray() )//7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2 })//relatedFormulaValues.ToArray()) chartDatas2.OrderBy(i => i.Year).ToArray())
                                    });            

            return Json(chartDatas, JsonRequestBehavior.AllowGet);
        }
    }
}