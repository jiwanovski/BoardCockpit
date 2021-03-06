﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public enum ChartType : byte
    {
        [Display(Name = "Liniendiagramm")]
        AjaxLoadedDataClickablePoints = 1,
        [Display(Name = "Balkendiagramm")]
        FixedPlacementColumn = 2,
        [Display(Name = "Linien- und Balkendiagramm")]
        DualAxesLineAndColumn = 3
    }

    public enum UnitEnum : byte
    {
        [Display(Name = "%")]
        PerCent = 1,
        [Display(Name = "€")]
        Currency = 2,
        [Display(Name = "Tage")]
        Days = 3
    }
    public class Formula
    {

        public int FormulaID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Model))]
        public string Description { get; set; }

        public ChartType ChartType { get; set; }

        public UnitEnum Unit { get; set; }

        //[ForeignKey("LinkedFormula")]
        public int? LinkedFormulaID {get; set;}

        [Display(Name = "ToolTipDescription", ResourceType = typeof(Resources.Model))]
        public string ToolTipDescription { get; set; }

        [ForeignKey("LinkedFormulaID")]
        public virtual Formula LinkedFormula { get; set; }
        public virtual ICollection<FormulaDetail> FormulaDetails { get; set; }
    }
}