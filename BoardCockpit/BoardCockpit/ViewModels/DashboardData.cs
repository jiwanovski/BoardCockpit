using BoardCockpit.Helpers;
using BoardCockpit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardCockpit.ViewModels
{
    public class DashboardData
    {
        public Company Company { get; set; }
        public FilterCriteria Filter { get; set; }
        public List<Company> Companies { get; set; }
        public IEnumerable<ContextContainer> ContextContainers { get; set; }
        public IEnumerable<Industry> Industries { get; set; }
        public IEnumerable<CalculatedKPI> TileKPIs { get; set; }
        public SelectList Formulas1 { get; set; }
        public SelectList Formulas2 { get; set; }
        public SelectList Formulas3 { get; set; }
        public SelectList Formulas4 { get; set; }
        //public IEnumerable<Formula> Formulas { get; set; }
    }
}