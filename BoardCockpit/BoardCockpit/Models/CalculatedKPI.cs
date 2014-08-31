using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class CalculatedKPI
    {
        public int CalculatedKPIID { get; set; }

        [Display(Name = "Value", ResourceType = typeof(Resources.Model))]
        public decimal Value { get; set; }
        
        public int FormulaDetailID { get; set; }

        public int ContextContainerID { get; set; }
        public virtual FormulaDetail FormulaDetail { get; set; }
        public virtual ContextContainer ContextContainer { get; set; }
    }
}