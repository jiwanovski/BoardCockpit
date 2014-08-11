namespace BoardCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        SizeClass = c.Int(nullable: false),
                        Name = c.String(),
                        Location = c.String(),
                        Street = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.Context",
                c => new
                    {
                        ContextID = c.Int(nullable: false, identity: true),
                        XbrlContextID = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Instant = c.DateTime(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContextID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.FinancialData",
                c => new
                    {
                        FinancialDataID = c.Int(nullable: false, identity: true),
                        Precision = c.Int(nullable: false),
                        Value = c.String(),
                        XbrlName = c.String(),
                        ContextID = c.Int(nullable: false),
                        UnitID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FinancialDataID)
                .ForeignKey("dbo.Context", t => t.ContextID, cascadeDelete: true)
                .ForeignKey("dbo.Unit", t => t.UnitID, cascadeDelete: true)
                .Index(t => t.ContextID)
                .Index(t => t.UnitID);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        XbrlUnitID = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.UnitId);
            
            CreateTable(
                "dbo.Industry",
                c => new
                    {
                        IndustryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IndustryID);
            
            CreateTable(
                "dbo.ImportNode",
                c => new
                    {
                        ImportNodeID = c.Int(nullable: false, identity: true),
                        ImportID = c.Int(nullable: false),
                        ContextRef = c.String(),
                        UnitRef = c.String(),
                        Precision = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ImportNodeID)
                .ForeignKey("dbo.Import", t => t.ImportID, cascadeDelete: true)
                .Index(t => t.ImportID);
            
            CreateTable(
                "dbo.Import",
                c => new
                    {
                        ImportID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Date = c.DateTime(nullable: false),
                        Directory = c.String(),
                        Error = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ImportID);
            
            CreateTable(
                "dbo.Taxonomy",
                c => new
                    {
                        TaxonomyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.TaxonomyID);
            
            CreateTable(
                "dbo.TaxonomyFile",
                c => new
                    {
                        TaxonomyFileID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FileName = c.String(),
                        TaxonomyID = c.Int(nullable: false),
                        Path = c.String(),
                        FullFilePath = c.String(),
                    })
                .PrimaryKey(t => t.TaxonomyFileID)
                .ForeignKey("dbo.Taxonomy", t => t.TaxonomyID, cascadeDelete: true)
                .Index(t => t.TaxonomyID);
            
            CreateTable(
                "dbo.IndustryCompany",
                c => new
                    {
                        Industry_IndustryID = c.Int(nullable: false),
                        Company_CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Industry_IndustryID, t.Company_CompanyID })
                .ForeignKey("dbo.Industry", t => t.Industry_IndustryID, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.Company_CompanyID, cascadeDelete: true)
                .Index(t => t.Industry_IndustryID)
                .Index(t => t.Company_CompanyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxonomyFile", "TaxonomyID", "dbo.Taxonomy");
            DropForeignKey("dbo.ImportNode", "ImportID", "dbo.Import");
            DropForeignKey("dbo.IndustryCompany", "Company_CompanyID", "dbo.Company");
            DropForeignKey("dbo.IndustryCompany", "Industry_IndustryID", "dbo.Industry");
            DropForeignKey("dbo.FinancialData", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.FinancialData", "ContextID", "dbo.Context");
            DropForeignKey("dbo.Context", "CompanyID", "dbo.Company");
            DropIndex("dbo.IndustryCompany", new[] { "Company_CompanyID" });
            DropIndex("dbo.IndustryCompany", new[] { "Industry_IndustryID" });
            DropIndex("dbo.TaxonomyFile", new[] { "TaxonomyID" });
            DropIndex("dbo.ImportNode", new[] { "ImportID" });
            DropIndex("dbo.FinancialData", new[] { "UnitID" });
            DropIndex("dbo.FinancialData", new[] { "ContextID" });
            DropIndex("dbo.Context", new[] { "CompanyID" });
            DropTable("dbo.IndustryCompany");
            DropTable("dbo.TaxonomyFile");
            DropTable("dbo.Taxonomy");
            DropTable("dbo.Import");
            DropTable("dbo.ImportNode");
            DropTable("dbo.Industry");
            DropTable("dbo.Unit");
            DropTable("dbo.FinancialData");
            DropTable("dbo.Context");
            DropTable("dbo.Company");
        }
    }
}
