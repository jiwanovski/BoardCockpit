using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Industry
    {
        public int IndustryID { get; set; }

        public int IndustryKey { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        public virtual int NoOfCompanies { get { return Companies.Count; } }

        public virtual ICollection<Company> Companies { get; set; }
    }
}