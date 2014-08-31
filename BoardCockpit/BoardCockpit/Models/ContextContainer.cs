using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class ContextContainer
    {
        public int ContextContainerID { get; set; }

        public int Year { get; set; }

        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Context> Contexts { get; set; }
        public virtual ICollection<CalculatedKPI> CalculatedKPIs { get; set; }
    }
}