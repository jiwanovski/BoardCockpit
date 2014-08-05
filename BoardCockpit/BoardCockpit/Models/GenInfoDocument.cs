using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class GenInfoDocument
    {
        public int GenInfoDocumentID { get; set; }

        public DateTime GenerationDate { get; set; }

        public virtual ICollection<Context> Contexts { get; set; }
    }
}