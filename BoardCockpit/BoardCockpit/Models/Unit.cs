using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Unit
    {
        public int UnitId { get; set; }

        [Display(Name = "XbrlUnitID", ResourceType = typeof(Resources.Model))]
        public string XbrlUnitID { get; set; }

        [Display(Name = "Value", ResourceType = typeof(Resources.Model))]
        public string Value { get; set; }

        public virtual ICollection<FinancialData> FinancialDatas { get; set; }
    }
}