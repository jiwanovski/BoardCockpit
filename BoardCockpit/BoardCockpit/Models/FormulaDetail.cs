using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class FormulaDetail
    {
        public int FormulaDetailID { get; set; }

        public string FormulaExpression { get; set; }

        public int FormulaID { get; set; }

        public virtual Formula Formula { get; set; }

        public virtual ICollection<Taxonomy> Taxonomies { get; set; }
        public virtual ICollection<CalculatedKPI> CalculatedKPIs { get; set; }
    }
}