using BoardCockpit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.ViewModels
{
    public class DashboardData
    {
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<ContextContainer> ContextContainers { get; set; }

        //public IEnumerable<Formula> Formulas { get; set; }
    }
}