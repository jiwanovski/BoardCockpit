using BoardCockpit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.DAL
{
    public class BoardCockpitInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BoardCockpitContext>
    {
        protected override void Seed(BoardCockpitContext context)
        {
            var taxonomies = new List<Taxonomy>
            {
                new Taxonomy{TaxonomyID=1, Name="de-gaap-ci-2007-12-01", Path="C:\\Temp\\"}
            };

            taxonomies.ForEach(s => context.Taxonomies.Add(s));
            context.SaveChanges();

            var taxonomyFiles = new List<TaxonomyFile>
            {
                new TaxonomyFile{TaxonomyID=1, FileName="de-gaap-ci-2007-12-01.xsd", FullFilePath="C:\\Temp\\de-gaap-ci-2007-12-01.xsd", Name="de-gaap-ci-2007-12-01", Path="C:\\Temp\\"}
            };

            taxonomyFiles.ForEach(s => context.TaxonomyFiles.Add(s));
            context.SaveChanges();
        }
    }
}