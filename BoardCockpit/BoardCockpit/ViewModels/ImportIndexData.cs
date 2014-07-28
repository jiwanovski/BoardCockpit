using BoardCockpit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.ViewModels
{
    public class ImportIndexData
    {
        public Import Import { get; set; }
        public IEnumerable<ImportNode> ImportNodes { get; set; }
    }
}