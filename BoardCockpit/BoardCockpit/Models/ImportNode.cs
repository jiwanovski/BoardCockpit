using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class ImportNode
    {
        public int ImportNodeID { get; set; }

        [Display(Name = "ImportID", ResourceType = typeof(Resources.Model))]
        public int ImportID { get; set; }

        [Display(Name = "ContextRef", ResourceType = typeof(Resources.Model))]
        public string ContextRef { get; set; }

        [Display(Name = "UnitRef", ResourceType = typeof(Resources.Model))]
        public string UnitRef { get; set; }

        [Display(Name = "Precision", ResourceType = typeof(Resources.Model))]
        public decimal Precision { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        [Display(Name = "Value", ResourceType = typeof(Resources.Model))]
        public string Value { get; set; }

        public virtual Import Import { get; set; }
    }
}