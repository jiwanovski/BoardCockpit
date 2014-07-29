using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Context
    {
        public int ContextID { get; set; }

        [Display(Name = "XbrlContextID", ResourceType = typeof(Resources.Model))]
        public string XbrlContextID { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Resources.Model))]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(Resources.Model))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Instant", ResourceType = typeof(Resources.Model))]
        public DateTime Instant { get; set; }

        [Display(Name = "CompanyID", ResourceType = typeof(Resources.Model))]
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
        
        public virtual ICollection<FinancialData> FinancialDatas { get; set; }
    }
}