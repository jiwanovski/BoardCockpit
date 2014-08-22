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
        public DbSet<TaxonomyFileNode> TaxonomyFileNodes { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<ImportNode> ImportNodes { get; set; }
        public DbSet<ImportContainer> ImportContainers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Context> Contexts { get; set; }
        public DbSet<FinancialData> FinancialDatas { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<GenInfoDocument> GenInfoDocuments { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<FormulaDetail> FormulaDetails { get; set; }
        public DbSet<CalculatedKPI> CalculatedKPIs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        
    }
}