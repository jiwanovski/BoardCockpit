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
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            Highcharts chart = new Highcharts("chart");
            ViewBag.Chart = chart; 
            return View();
        }
    }
}