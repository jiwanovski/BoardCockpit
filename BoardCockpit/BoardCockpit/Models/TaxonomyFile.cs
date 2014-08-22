using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class TaxonomyFile
    {
        public int TaxonomyFileID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        [Display(Name = "FileName", ResourceType = typeof(Resources.Model))]
        public string FileName { get; set; }

        [Display(Name = "TaxonomyID", ResourceType = typeof(Resources.Model))]
        public int TaxonomyID { get; set; }

        [Display(Name = "Path", ResourceType = typeof(Resources.Model))]
        public string Path { get; set; }

        [Display(Name = "FullFilePath", ResourceType = typeof(Resources.Model))]
        public string FullFilePath { get; set; }
        public virtual Taxonomy Taxonomy { get; set; }
        public virtual ICollection<TaxonomyFileNode> TaxonomyFileNodes { get; set; }
    }
}