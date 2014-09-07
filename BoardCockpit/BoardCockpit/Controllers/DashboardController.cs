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

namespace BoardCockpit.Controllers
{
    public class DashboardController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();
        AjaxLoadedChart chart;
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
            ViewBag.Formulas = items;

            Highcharts chart = new Highcharts("chart");
            //ViewBag.Chart = AjaxLoadedChart();
            return View(viewModel);
        }

        public ActionResult AjaxLoadedChart(string chartName)
        //public Highcharts AjaxLoadedChart()
        {
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
            chart.DataSeries = test;
            ViewBag.Graph = chart.GetChart(chartName);
            ViewBag.ChartName = chartName;
            return PartialView(viewModel);
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
    }
}