using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Company
    {
        public int CompanyID { get; set; }

        [Display(Name = "SizeClass", ResourceType = typeof(Resources.Model))]
        public int SizeClass { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        [Display(Name = "Location", ResourceType = typeof(Resources.Model))]
        public string Location { get; set; }

        [Display(Name = "Street", ResourceType = typeof(Resources.Model))]
        public string Street { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(Resources.Model))]
        public string ZipCode { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resources.Model))]
        public string City { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Resources.Model))]
        public string Country { get; set; }

        public virtual ICollection<Industry> Industies { get; set; }
        public virtual ICollection<Context> Contexts { get; set; }
    }
}