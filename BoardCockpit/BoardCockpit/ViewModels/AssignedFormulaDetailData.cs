using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.ViewModels
{
    public class AssignedFormulaDetailData
    {
        public int TaxonomyID { get; set; }

        public string TaxonomyName { get; set; }

        public bool Assigned { get; set; }
    }
}