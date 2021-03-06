﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Report
    {
        public int ReportID { get; set; }

        public Int64 ReportIDXbrl { get; set; }

        [Display(Name = "AccordingToYearEnd", ResourceType = typeof(Resources.Model))]
        public DateTime AccordingToYearEnd { get; set; }

        [Display(Name = "ReportType", ResourceType = typeof(Resources.Model))]
        public string ReportType { get; set; }

        [Display(Name = "XbrlContextRef", ResourceType = typeof(Resources.Model))]
        public string XbrlContextRef { get; set; }

        public virtual ICollection<Context> Contexts { get; set; }
    }
}