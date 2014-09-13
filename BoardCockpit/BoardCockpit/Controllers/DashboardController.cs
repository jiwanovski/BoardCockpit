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
        IChart chart;
        List<Company> companies;
        List<DotNet.Highcharts.Options.Series> series;
        List<string> categories;
        List<YAxis> yAxis;

        public ActionResult Dashboard2(string fromYear, string toYear, string fromSizeClass, string toSizeClass, string industryID)
        {
            int firstYear = Convert.ToInt16(fromYear);
            int lastYear = Convert.ToInt16(toYear);
            int smallestSize = Convert.ToInt16(fromSizeClass);
            int biggestSize = Convert.ToInt16(toSizeClass);
            int industryNo = Convert.ToInt16(industryID);

            FilterCriteria filter = new FilterCriteria(firstYear, lastYear, smallestSize, biggestSize);
            filter.IndustryNo = industryNo;

            var viewModel = GetViewModel(filter);

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
            ViewBag.IndustryID = new SelectList(viewModel.Industries, "IndustryID", "Name",industryNo);

            return View("Dashboard", viewModel);
        }
        // GET: Dashboard
        public ActionResult Dashboard()
        {            
            int firstYear = db.ContextContainers.Min(n => n.Year);
            int lastYear = db.ContextContainers.Max(n => n.Year);
            int smallestSize = db.Companies.Min(n => n.SizeClass);
            int biggestSize = db.Companies.Max(n => n.SizeClass);

            FilterCriteria filter = new FilterCriteria(firstYear, lastYear, smallestSize, biggestSize);

            var viewModel = GetViewModel(filter);
            
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
            ViewBag.IndustryID = new SelectList(viewModel.Industries, "IndustryID", "Name");

            Highcharts chart = new Highcharts("chart");
            //ViewBag.Chart = AjaxLoadedChart();
            return View(viewModel);
        }

        private DashboardData GetViewModel(FilterCriteria filter)
        {
            var viewModel = new DashboardData();
            int ownCompanyID = db.GeneralSetting.First().CompanyID;
            viewModel.Company = db.Companies.Find(ownCompanyID);
            viewModel.Filter = filter;
            viewModel.Companies = db.Companies.Include(i => i.ContextContainers).ToList();
            viewModel.ContextContainers = db.ContextContainers.Include(i => i.CalculatedKPIs).Include(i => i.Contexts).Include(i => i.Company).ToList();
            viewModel.Industries = db.Industries.Include(i => i.Companies).ToList();
            viewModel.Industries = viewModel.Industries.Where(i => i.NoOfCompanies > 0);
            if ((filter.FromYear != null) && (filter.ToYear != null) && (filter.FromYear != 0) && (filter.ToYear != 0))
            {
                viewModel.ContextContainers = viewModel.ContextContainers
                                                            .Where(n => n.Year >= filter.FromYear)
                                                            .Where(n => n.Year <= filter.ToYear);
            }

            // TODO !!!JIW CHANGE URGENT!!!
            if ((filter.IndustryNo != null) && (filter.IndustryNo != 0))
            {
                Industry industry = db.Industries.Find(filter.IndustryNo);
                viewModel.Companies = viewModel.Companies.Where(n => n.Industies.Contains(industry)).ToList();
            }

            if ((filter.FromSizeClass != null) && (filter.ToSizeClass != null) && (filter.FromSizeClass != 0) && (filter.ToSizeClass != 0))
            {
                viewModel.Companies = viewModel.Companies
                                                    .Where(n => n.SizeClass >= filter.FromSizeClass)
                                                    .Where(n => n.SizeClass <= filter.ToSizeClass).ToList();
            }

            return viewModel;
        } 

        public ActionResult GetChartPartialView(string chartName, string formulaID, string fromYear, string toYear, string fromSize, string toSize, string industryNo)
        {
            if (formulaID == null)
                return null;

            string viewName;
            int formulaID2 = Convert.ToInt16(formulaID);
            Formula formula = db.Formulas.Find(formulaID2);

            FilterCriteria filter = new FilterCriteria();
            if (!String.IsNullOrEmpty(fromYear))
            {
                filter.FromYear = Convert.ToInt16(fromYear);
            }
            if (!String.IsNullOrEmpty(toYear))
            {
                filter.ToYear = Convert.ToInt16(toYear);
            }
            if (!String.IsNullOrEmpty(fromSize))
            {
                filter.FromSizeClass = Convert.ToInt16(fromSize);
            }
            if (!String.IsNullOrEmpty(toSize))
            {
                filter.ToSizeClass = Convert.ToInt16(toSize);
            }
            if (!String.IsNullOrEmpty(industryNo))
            {
                filter.IndustryNo = Convert.ToInt16(industryNo);
            }

            var viewModel = GetViewModel(filter);
                                    
            //var data = from company in viewModel.Companies
            //           select company.CompanyID;
            //ViewBag.Companies = data.ToArray();
                                    
            switch (formula.ChartType) 
            {
                case ChartType.AjaxLoadedDataClickablePoints:
                    chart = new Helpers.AjaxLoadedChart();
                    viewName = "AjaxLoadedChart";
                    break;
                case ChartType.DualAxesLineAndColumn:
                    chart = new Helpers.DualAxesLineAndColumnChart();
                    viewName = "DualAxesLineAndColumnChart";
                    Formula relatedFormula = formula.LinkedFormula;
                    yAxis = new List<YAxis>();

                    YAxis selectedYAxes = new YAxis
                        {
                            Labels = new YAxisLabels 
                            {  
                                Formatter = "function() { return this.value; }",
                                Style = "color: '#89A54E'"
                            },
                            Title = new YAxisTitle
                            {
                                Text = formula.Name,
                                Style = "color: '#89A54E'"
                            }
                        };                

                    YAxis relatedYAxes = new YAxis
                        {
                            Labels = new YAxisLabels
                            {
                                Formatter = "function() { return this.value; }",
                                Style = "color: '#4572A7'"
                            },
                            Title = new YAxisTitle
                                {
                                    Text = formula.LinkedFormula.Name,
                                    Style = "color: '#4572A7'"
                                },
                            Opposite = true
                        };
                    yAxis.Add(selectedYAxes);
                    yAxis.Add(selectedYAxes);

                    chart.YAxisSeries = yAxis;                    
                    break;
                default: 
                    throw new Exception("ChartType is not supported");
                    break;
            }

            categories = new List<string>();
            series = new List<Series>();
            chart.Categories = categories;
            chart.DataSeries = series;
            chart.ChartName = chartName;

            ViewBag.FormulaID = formulaID;
            ViewBag.ChartName = chartName;
            ViewBag.Graph = chart.Chart;

            return PartialView(viewName, viewModel);           
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

            return PartialView(viewModel);
        }

        public EmptyResult SetPeriod(string _fromYear, string _toYear)
        {
            //fromYear = Convert.ToInt32(_fromYear);
            //toYear = Convert.ToInt32(_toYear);
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