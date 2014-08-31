namespace BoardCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalculatedKPI",
                c => new
                    {
                        CalculatedKPIID = c.Int(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormulaDetailID = c.Int(nullable: false),
                        ContextContainerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CalculatedKPIID)
                .ForeignKey("dbo.ContextContainer", t => t.ContextContainerID, cascadeDelete: true)
                .ForeignKey("dbo.FormulaDetail", t => t.FormulaDetailID, cascadeDelete: true)
                .Index(t => t.FormulaDetailID)
                .Index(t => t.ContextContainerID);
            
            CreateTable(
                "dbo.ContextContainer",
                c => new
                    {
                        ContextContainerID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContextContainerID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyIDXbrl = c.Int(nullable: false),
                        SizeClass = c.Int(nullable: false),
                        Name = c.String(),
                        Location = c.String(),
                        Street = c.String(),
                        ZipCode = c.Int(nullable: false),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.Industry",
                c => new
                    {
                        IndustryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IndustryID);
            
            CreateTable(
                "dbo.Context",
                c => new
                    {
                        ContextID = c.Int(nullable: false, identity: true),
                        XbrlContextID = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Instant = c.DateTime(nullable: false),
                        ContextContainerID = c.Int(nullable: false),
                        ReportID = c.Int(nullable: false),
                        GenInfoDocumentID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContextID)
                .ForeignKey("dbo.ContextContainer", t => t.ContextContainerID, cascadeDelete: true)
                .ForeignKey("dbo.GenInfoDocument", t => t.GenInfoDocumentID, cascadeDelete: true)
                .ForeignKey("dbo.Report", t => t.ReportID, cascadeDelete: true)
                .Index(t => t.ContextContainerID)
                .Index(t => t.ReportID)
                .Index(t => t.GenInfoDocumentID);
            
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
                "dbo.GenInfoDocument",
                c => new
                    {
                        GenInfoDocumentID = c.Int(nullable: false, identity: true),
                        GenerationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GenInfoDocumentID);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        ReportID = c.Int(nullable: false, identity: true),
                        ReportIDXbrl = c.Long(nullable: false),
                        AccordingToYearEnd = c.DateTime(nullable: false),
                        ReportType = c.String(),
                        XbrlContextRef = c.String(),
                    })
                .PrimaryKey(t => t.ReportID);
            
            CreateTable(
                "dbo.FormulaDetail",
                c => new
                    {
                        FormulaDetailID = c.Int(nullable: false, identity: true),
                        FormulaExpression = c.String(),
                        FormulaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormulaDetailID)
                .ForeignKey("dbo.Formula", t => t.FormulaID, cascadeDelete: true)
                .Index(t => t.FormulaID);
            
            CreateTable(
                "dbo.Formula",
                c => new
                    {
                        FormulaID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FormulaID);
            
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
                "dbo.TaxonomyFileNode",
                c => new
                    {
                        TaxonomyFileNodeID = c.Int(nullable: false, identity: true),
                        TaxonomyFileID = c.Int(nullable: false),
                        NodeName = c.String(),
                        LabelDE = c.String(),
                        TaxonomyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaxonomyFileNodeID)
                .ForeignKey("dbo.TaxonomyFile", t => t.TaxonomyFileID, cascadeDelete: true)
                .Index(t => t.TaxonomyFileID);
            
            CreateTable(
                "dbo.ImportContainer",
                c => new
                    {
                        ImportContainerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ImportContainerID);
            
            CreateTable(
                "dbo.Import",
                c => new
                    {
                        ImportID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Date = c.DateTime(nullable: false),
                        Directory = c.String(),
                        Error = c.Boolean(nullable: false),
                        ImportContainerID = c.Int(nullable: false),
                        TaxonomyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImportID)
                .ForeignKey("dbo.Taxonomy", t => t.TaxonomyID, cascadeDelete: true)
                .ForeignKey("dbo.ImportContainer", t => t.ImportContainerID, cascadeDelete: true)
                .Index(t => t.ImportContainerID)
                .Index(t => t.TaxonomyID);
            
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
                "dbo.ReportingYear",
                c => new
                    {
                        ReportingYearID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReportingYearID);
            
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
            
            CreateTable(
                "dbo.TaxonomyFormulaDetail",
                c => new
                    {
                        Taxonomy_TaxonomyID = c.Int(nullable: false),
                        FormulaDetail_FormulaDetailID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Taxonomy_TaxonomyID, t.FormulaDetail_FormulaDetailID })
                .ForeignKey("dbo.Taxonomy", t => t.Taxonomy_TaxonomyID, cascadeDelete: true)
                .ForeignKey("dbo.FormulaDetail", t => t.FormulaDetail_FormulaDetailID, cascadeDelete: true)
                .Index(t => t.Taxonomy_TaxonomyID)
                .Index(t => t.FormulaDetail_FormulaDetailID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Import", "ImportContainerID", "dbo.ImportContainer");
            DropForeignKey("dbo.Import", "TaxonomyID", "dbo.Taxonomy");
            DropForeignKey("dbo.ImportNode", "ImportID", "dbo.Import");
            DropForeignKey("dbo.TaxonomyFormulaDetail", "FormulaDetail_FormulaDetailID", "dbo.FormulaDetail");
            DropForeignKey("dbo.TaxonomyFormulaDetail", "Taxonomy_TaxonomyID", "dbo.Taxonomy");
            DropForeignKey("dbo.TaxonomyFileNode", "TaxonomyFileID", "dbo.TaxonomyFile");
            DropForeignKey("dbo.TaxonomyFile", "TaxonomyID", "dbo.Taxonomy");
            DropForeignKey("dbo.FormulaDetail", "FormulaID", "dbo.Formula");
            DropForeignKey("dbo.CalculatedKPI", "FormulaDetailID", "dbo.FormulaDetail");
            DropForeignKey("dbo.Context", "ReportID", "dbo.Report");
            DropForeignKey("dbo.Context", "GenInfoDocumentID", "dbo.GenInfoDocument");
            DropForeignKey("dbo.FinancialData", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.FinancialData", "ContextID", "dbo.Context");
            DropForeignKey("dbo.Context", "ContextContainerID", "dbo.ContextContainer");
            DropForeignKey("dbo.IndustryCompany", "Company_CompanyID", "dbo.Company");
            DropForeignKey("dbo.IndustryCompany", "Industry_IndustryID", "dbo.Industry");
            DropForeignKey("dbo.ContextContainer", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.CalculatedKPI", "ContextContainerID", "dbo.ContextContainer");
            DropIndex("dbo.TaxonomyFormulaDetail", new[] { "FormulaDetail_FormulaDetailID" });
            DropIndex("dbo.TaxonomyFormulaDetail", new[] { "Taxonomy_TaxonomyID" });
            DropIndex("dbo.IndustryCompany", new[] { "Company_CompanyID" });
            DropIndex("dbo.IndustryCompany", new[] { "Industry_IndustryID" });
            DropIndex("dbo.ImportNode", new[] { "ImportID" });
            DropIndex("dbo.Import", new[] { "TaxonomyID" });
            DropIndex("dbo.Import", new[] { "ImportContainerID" });
            DropIndex("dbo.TaxonomyFileNode", new[] { "TaxonomyFileID" });
            DropIndex("dbo.TaxonomyFile", new[] { "TaxonomyID" });
            DropIndex("dbo.FormulaDetail", new[] { "FormulaID" });
            DropIndex("dbo.FinancialData", new[] { "UnitID" });
            DropIndex("dbo.FinancialData", new[] { "ContextID" });
            DropIndex("dbo.Context", new[] { "GenInfoDocumentID" });
            DropIndex("dbo.Context", new[] { "ReportID" });
            DropIndex("dbo.Context", new[] { "ContextContainerID" });
            DropIndex("dbo.ContextContainer", new[] { "CompanyID" });
            DropIndex("dbo.CalculatedKPI", new[] { "ContextContainerID" });
            DropIndex("dbo.CalculatedKPI", new[] { "FormulaDetailID" });
            DropTable("dbo.TaxonomyFormulaDetail");
            DropTable("dbo.IndustryCompany");
            DropTable("dbo.ReportingYear");
            DropTable("dbo.ImportNode");
            DropTable("dbo.Import");
            DropTable("dbo.ImportContainer");
            DropTable("dbo.TaxonomyFileNode");
            DropTable("dbo.TaxonomyFile");
            DropTable("dbo.Taxonomy");
            DropTable("dbo.Formula");
            DropTable("dbo.FormulaDetail");
            DropTable("dbo.Report");
            DropTable("dbo.GenInfoDocument");
            DropTable("dbo.Unit");
            DropTable("dbo.FinancialData");
            DropTable("dbo.Context");
            DropTable("dbo.Industry");
            DropTable("dbo.Company");
            DropTable("dbo.ContextContainer");
            DropTable("dbo.CalculatedKPI");
        }
    }
}
