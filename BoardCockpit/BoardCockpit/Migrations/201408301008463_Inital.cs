namespace BoardCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Context", "CompanyID", "dbo.Company");
            DropIndex("dbo.Context", new[] { "CompanyID" });
            CreateTable(
                "dbo.CalculatedKPI",
                c => new
                    {
                        CalculatedKPIID = c.Int(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormulaDetailID = c.Int(nullable: false),
                        ContextID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CalculatedKPIID)
                .ForeignKey("dbo.Context", t => t.ContextID, cascadeDelete: true)
                .ForeignKey("dbo.FormulaDetail", t => t.FormulaDetailID, cascadeDelete: true)
                .Index(t => t.FormulaDetailID)
                .Index(t => t.ContextID);
            
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
                        ReportID = c.Long(nullable: false, identity: true),
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
            
            AddColumn("dbo.Company", "CompanyIDXbrl", c => c.Int(nullable: false));
            AddColumn("dbo.Context", "ReportID", c => c.Long(nullable: false));
            AddColumn("dbo.Context", "GenInfoDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.Context", "Company_CompanyID", c => c.Int());
            AddColumn("dbo.Import", "ImportContainerID", c => c.Int(nullable: false));
            AddColumn("dbo.Import", "TaxonomyID", c => c.Int(nullable: false));
            AlterColumn("dbo.Company", "ZipCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Context", "CompanyID", c => c.Long(nullable: false));
            CreateIndex("dbo.Context", "ReportID");
            CreateIndex("dbo.Context", "GenInfoDocumentID");
            CreateIndex("dbo.Context", "Company_CompanyID");
            CreateIndex("dbo.Import", "ImportContainerID");
            CreateIndex("dbo.Import", "TaxonomyID");
            AddForeignKey("dbo.Context", "GenInfoDocumentID", "dbo.GenInfoDocument", "GenInfoDocumentID", cascadeDelete: true);
            AddForeignKey("dbo.Context", "ReportID", "dbo.Report", "ReportID", cascadeDelete: true);
            AddForeignKey("dbo.Import", "TaxonomyID", "dbo.Taxonomy", "TaxonomyID", cascadeDelete: true);
            AddForeignKey("dbo.Import", "ImportContainerID", "dbo.ImportContainer", "ImportContainerID", cascadeDelete: true);
            AddForeignKey("dbo.Context", "Company_CompanyID", "dbo.Company", "CompanyID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Context", "Company_CompanyID", "dbo.Company");
            DropForeignKey("dbo.Import", "ImportContainerID", "dbo.ImportContainer");
            DropForeignKey("dbo.Import", "TaxonomyID", "dbo.Taxonomy");
            DropForeignKey("dbo.TaxonomyFormulaDetail", "FormulaDetail_FormulaDetailID", "dbo.FormulaDetail");
            DropForeignKey("dbo.TaxonomyFormulaDetail", "Taxonomy_TaxonomyID", "dbo.Taxonomy");
            DropForeignKey("dbo.TaxonomyFileNode", "TaxonomyFileID", "dbo.TaxonomyFile");
            DropForeignKey("dbo.FormulaDetail", "FormulaID", "dbo.Formula");
            DropForeignKey("dbo.CalculatedKPI", "FormulaDetailID", "dbo.FormulaDetail");
            DropForeignKey("dbo.Context", "ReportID", "dbo.Report");
            DropForeignKey("dbo.Context", "GenInfoDocumentID", "dbo.GenInfoDocument");
            DropForeignKey("dbo.CalculatedKPI", "ContextID", "dbo.Context");
            DropIndex("dbo.TaxonomyFormulaDetail", new[] { "FormulaDetail_FormulaDetailID" });
            DropIndex("dbo.TaxonomyFormulaDetail", new[] { "Taxonomy_TaxonomyID" });
            DropIndex("dbo.Import", new[] { "TaxonomyID" });
            DropIndex("dbo.Import", new[] { "ImportContainerID" });
            DropIndex("dbo.TaxonomyFileNode", new[] { "TaxonomyFileID" });
            DropIndex("dbo.FormulaDetail", new[] { "FormulaID" });
            DropIndex("dbo.Context", new[] { "Company_CompanyID" });
            DropIndex("dbo.Context", new[] { "GenInfoDocumentID" });
            DropIndex("dbo.Context", new[] { "ReportID" });
            DropIndex("dbo.CalculatedKPI", new[] { "ContextID" });
            DropIndex("dbo.CalculatedKPI", new[] { "FormulaDetailID" });
            AlterColumn("dbo.Context", "CompanyID", c => c.Int(nullable: false));
            AlterColumn("dbo.Company", "ZipCode", c => c.String());
            DropColumn("dbo.Import", "TaxonomyID");
            DropColumn("dbo.Import", "ImportContainerID");
            DropColumn("dbo.Context", "Company_CompanyID");
            DropColumn("dbo.Context", "GenInfoDocumentID");
            DropColumn("dbo.Context", "ReportID");
            DropColumn("dbo.Company", "CompanyIDXbrl");
            DropTable("dbo.TaxonomyFormulaDetail");
            DropTable("dbo.ImportContainer");
            DropTable("dbo.TaxonomyFileNode");
            DropTable("dbo.Formula");
            DropTable("dbo.FormulaDetail");
            DropTable("dbo.Report");
            DropTable("dbo.GenInfoDocument");
            DropTable("dbo.CalculatedKPI");
            CreateIndex("dbo.Context", "CompanyID");
            AddForeignKey("dbo.Context", "CompanyID", "dbo.Company", "CompanyID", cascadeDelete: true);
        }
    }
}
