using BoardCockpit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.ViewModels
{
    public class TaxonomyIndexData
    {
        public IEnumerable<Taxonomy> Taxonomies { get; set; }
        public IEnumerable<TaxonomyFile> TaxonomyFiles { get; set; }
    }
}