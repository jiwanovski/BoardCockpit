using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class Import
    {
        public int ImportID { get; set; }
        
        [Display(Name = "FileName", ResourceType = typeof(Resources.Model))]
        public string FileName { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Resources.Model))]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Directory", ResourceType = typeof(Resources.Model))]
        public string Directory { get; set; }

        [Display(Name = "FilePath", ResourceType = typeof(Resources.Model))]
        public string FilePath
        {
            get
            {
                return Directory + FileName;
            }
        }

        [Display(Name = "Error", ResourceType = typeof(Resources.Model))]
        public bool Error { get; set; }

        // public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ImportNode> Nodes { get; set; }
    }
}