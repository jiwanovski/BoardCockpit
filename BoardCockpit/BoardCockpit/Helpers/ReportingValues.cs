using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Helpers
{
    public class ReportingValues
    {
        public int ContextContainerID { get; set; }
        public int FormulaID { get; set; }
        public int Year { get; set; }
        public decimal Value { get; set; }
        public string CompanyName { get; set; }
    }
}