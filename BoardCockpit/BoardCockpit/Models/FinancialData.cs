using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class FinancialData
    {
        public int FinancialDataID { get; set; }

        [Display(Name = "Precision", ResourceType = typeof(Resources.Model))]
        public int Precision {get; set; }

        [Display(Name = "Value", ResourceType = typeof(Resources.Model))]
        public string Value { get; set; }

        [Display(Name = "XbrlName", ResourceType = typeof(Resources.Model))]
        public string XbrlName { get; set; }

        [Display(Name = "ContextID", ResourceType = typeof(Resources.Model))]
        public int ContextID { get; set; }

        [Display(Name = "UnitID", ResourceType = typeof(Resources.Model))]
        public int UnitID { get; set; }

        public virtual Context Context { get; set; }

        public virtual Unit Unit { get; set; }
    }
}