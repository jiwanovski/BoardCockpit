using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public enum ChartType
    {
        AjaxLoadedDataClickablePoints = 1,
        FixedPlacementColumn = 2,
        DualAxesLineAndColumn = 3
    }
    public class Formula
    {

        public int FormulaID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Model))]
        public string Description { get; set; }

        public ChartType ChartType { get; set; }

        public virtual ICollection<FormulaDetail> FormulaDetails { get; set; }
    }
}