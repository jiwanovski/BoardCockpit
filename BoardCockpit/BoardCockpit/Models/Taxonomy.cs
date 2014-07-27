using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Taxonomy
    {
        public int TaxonomyID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        [Display(Name = "Path", ResourceType = typeof(Resources.Model))]
        public string Path { get; set; }

        public virtual ICollection<TaxonomyFile> Files { get; set; }
    }
}