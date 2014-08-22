using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class TaxonomyFileNode
    {
        public int TaxonomyFileNodeID { get; set; }

        public int TaxonomyFileID { get; set; }

        public string NodeName { get; set; }

        public string LabelDE { get; set; }
    }
}