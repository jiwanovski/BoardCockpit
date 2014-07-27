using BoardCockpit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BoardCockpit.DAL
{
    public class BoardCockpitContext : DbContext
    {
        public BoardCockpitContext()
            : base("BoardCockpitContext")
        {
        }

        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<TaxonomyFile> TaxonomyFiles { get; set; }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}