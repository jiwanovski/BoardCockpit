using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Context
    {
        private DateTime startDate;
        private DateTime endDate;
        private DateTime instantDate;

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
        public Int64 CompanyID { get; set; }

        [Display(Name = "ReportID", ResourceType = typeof(Resources.Model))]
        public Int64 ReportID { get; set; }

        [Display(Name = "GenInfoDocumentID", ResourceType = typeof(Resources.Model))]
        public int GenInfoDocumentID { get; set; }

        public virtual Company Company { get; set; }

        public virtual Report Report { get; set; }

        public virtual GenInfoDocument GenInfoDocument { get; set; }
        
        public virtual ICollection<FinancialData> FinancialDatas { get; set; }
    }
}