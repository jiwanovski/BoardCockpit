using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Formula
    {
        public int FormulaID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Model))]
        public string Description { get; set; }

        public virtual ICollection<FormulaDetail> FormulaDetails { get; set; }
    }
}