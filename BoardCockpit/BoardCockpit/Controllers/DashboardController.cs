using BoardCockpit.DAL;
using BoardCockpit.Helpers;
using BoardCockpit.Models;
using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardCockpit.Controllers
{
    public class DashboardController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            Highcharts chart = new Highcharts("chart");
            ViewBag.Chart = chart; 
            return View();
        }

        public ActionResult AjaxLoadedChart()
        {
            AjaxLoadedChart chart = new Helpers.AjaxLoadedChart();
            chart.Title = "Mein erster Titel";
            chart.Subtitle = "Mein erster Untertitel";
            List<string> years = new List<string>();
            years.Add("2008");
            years.Add("2009");
            chart.Categories = years;
            List<DotNet.Highcharts.Options.Series> test = new List<DotNet.Highcharts.Options.Series>();
            test.Add(new DotNet.Highcharts.Options.Series { Name = "Test" });
            chart.DataSeries = test;
            ViewBag.Graph = chart.GetChart();
            return View();
        }

        public JsonResult GetPeriod(string fromYear, string toYear)//(int? fromYear, int? toYear)
        {
            List<ReportingYear> reportingYears = new List<ReportingYear>();//db.ReportingYears.ToList();
            reportingYears.Add( new ReportingYear { ReportingYearID = 1, Year = 2008 } );
            reportingYears.Add( new ReportingYear { ReportingYearID = 1, Year = 2009 } );
            var data = from year in reportingYears
                       select year.ReportingYearID;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAjaxLoadedChartData()//(int formulaID, int companyID)
        {
            List<AjaxLoadedChartData> chartDatas = new List<AjaxLoadedChartData>();
            
            //List<CalculatedKPI> calcKPIs = db.CalculatedKPIs
            //                                    .Where(i => i.FormulaDetail.FormulaID == formulaID)
            //                                    .Where(i => i.ContextContainer.CompanyID == companyID).ToList();
            //foreach (CalculatedKPI item in calcKPIs)
            //{
            //    chartDatas.Add(new AjaxLoadedChartData { Year = item.ContextContainer.Year, Value = item.Value });    
            //}
            
            

            chartDatas.Add(new AjaxLoadedChartData { Year = 2008, Value = 10 });
            chartDatas.Add(new AjaxLoadedChartData { Year = 2009, Value = 15000 });



            return Json(chartDatas.OrderBy(i => i.Year), JsonRequestBehavior.AllowGet);
        }
    }
}