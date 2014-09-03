using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public enum ChartType : byte
    {
        [Display(Name = "Drama!")]
        AjaxLoadedDataClickablePoints = 1,
        [Display(Name = "Drama1")]
        FixedPlacementColumn = 2,
        [Display(Name = "Drama2")]
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

        [Display(Name = "ToolTipDescription", ResourceType = typeof(Resources.Model))]
        public string ToolTipDescription { get; set; }

        public virtual ICollection<FormulaDetail> FormulaDetails { get; set; }
    }
}