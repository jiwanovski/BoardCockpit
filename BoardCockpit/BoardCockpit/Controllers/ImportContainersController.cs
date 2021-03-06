﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoardCockpit.DAL;
using BoardCockpit.Models;
using MvcFileUploader.Models;
using MvcFileUploader;
using BoardCockpit.Helpers;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Data.SqlTypes;

namespace BoardCockpit.Controllers
{
    public class ImportContainersController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: ImportContainers
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            return View(db.ImportContainers.ToList());
        }

        // GET: ImportContainers/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportContainer importContainer = db.ImportContainers.Find(id);
            if (importContainer == null)
            {
                return HttpNotFound();
            }
            return View(importContainer);
        }

        // GET: ImportContainers/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            return View();
        }

        // POST: ImportContainers/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportContainerID,Name,Date")] ImportContainer importContainer)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (ModelState.IsValid)
            {
                db.ImportContainers.Add(importContainer);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Upload", new { ImportContainerID = importContainer.ImportContainerID, status = "new" });
            }

            return View(importContainer);
        }

        // GET: ImportContainers/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportContainer importContainer = db.ImportContainers.Find(id);
            if (importContainer == null)
            {
                return HttpNotFound();
            }
            return View(importContainer);
        }

        // POST: ImportContainers/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportContainerID,Name,Date")] ImportContainer importContainer)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (ModelState.IsValid)
            {
                db.Entry(importContainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(importContainer);
        }

        // GET: ImportContainers/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportContainer importContainer = db.ImportContainers.Find(id);
            if (importContainer == null)
            {
                return HttpNotFound();
            }
            return View(importContainer);
        }

        // POST: ImportContainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            ImportContainer importContainer = db.ImportContainers.Find(id);
            db.ImportContainers.Remove(importContainer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // FileUpload
        public ActionResult Upload(int importContainerID)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            ViewBag.ImportContainerID = importContainerID;            

            return View();
        }

        public ActionResult UploadFile(int? entityId, int importContainerId) // optionally receive values specified with Html helper
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            string path = Server.MapPath("~/Content/ImportedFiles/XBRL");

            // here we can send in some extra info to be included with the delete url 
            var statuses = new List<ViewDataUploadFileResult>();
            for (var i = 0; i < Request.Files.Count; i++)
            {


                if (Request.Files[i].FileName != "")
                {
                    var st = FileSaver.StoreFile(x =>
                    {
                        x.File = Request.Files[i];
                        // note how we are adding an additional value to be posted with delete request
                        // and giving it the same value posted with upload
                        x.DeleteUrl = Url.Action("DeleteFile", new { entityId = entityId });
                        x.StorageDirectory = path;
                        x.UrlPrefix = "/Content/ImportedFiles/Taxonomy";// this is used to generate the relative url of the file

                        //overriding defaults
                        x.FileName = Request.Files[i].FileName;// default is filename suffixed with filetimestamp
                        x.ThrowExceptions = true;//default is false, if false exception message is set in error property
                    });

                    if (db.Imports.Where(a => a.FileName == st.SavedFileName).Count() > 0)
                    {
                        st.error = String.Format("Die Datei {0} wurde bereits am {1} hochgeladen.", st.SavedFileName, db.Imports.Where(a => a.FileName == st.SavedFileName).Single().Date);
                    }
                    statuses.Add(st);
                }
            }

            if (statuses.Count > 0)
            {
                //statuses contains all the uploaded files details (if error occurs then check error property is not null or empty)
                //todo: add additional code to generate thumbnail for videos, associate files with entities etc

                //adding thumbnail url for jquery file upload javascript plugin
                // TODO: JIW: Change Image
                statuses.ForEach(x => x.thumbnailUrl = "/Images/XBRL_Icon.png"); // uses ImageResizer httpmodule to resize images from this url

                //setting custom download url instead of direct url to file which is default
                statuses.ForEach(x => x.url = Url.Action("DownloadFile", new { fileUrl = x.url }));

                //server side error generation, generate some random error if entity id is 13

                //if (entityId == 13)
                //{
                //    var rnd = new Random();
                //    statuses.ForEach(x =>
                //    {
                //        //setting the error property removes the deleteUrl, thumbnailUrl and url property values
                //        x.error = rnd.Next(0, 2) > 0 ? "We do not have any entity with unlucky Id : '13'" : String.Format("Your file size is {0} bytes which is un-acceptable", x.size);
                //        //delete file by using FullPath property
                //        if (System.IO.File.Exists(x.FullPath)) System.IO.File.Delete(x.FullPath);
                //    });
                //}

                var viewresult = Json(new { files = statuses });
                //for IE8 which does not accept application/json
                if (Request.Headers["Accept"] != null && !Request.Headers["Accept"].Contains("application/json"))
                    viewresult.ContentType = "text/plain";

                var statuses2 = new List<ViewDataUploadFileResult>();
                statuses2 = statuses.Where(b => b.error == null).ToList();
                //statuses2.ForEach(x =>
                //{
                //    statuses.Remove(x);


                //    //if (System.IO.File.Exists(x.FullPath)) System.IO.File.Delete(x.FullPath);
                //});
                List<Industry> industries = db.Industries.Include(n => n.Companies).ToList();

                List<ImportNode> nodes = new List<ImportNode>();
                foreach (ViewDataUploadFileResult file in statuses2)
                {
                    //XmlTextReader reader = new XmlTextReader(file.FullPath);
                    //XmlSchema myschema = XmlSchema.Read(reader, ValidationCallback);
                    // Make Entry in Import
                    Import import = new Import
                    {
                        FileName = file.SavedFileName,
                        Directory = path,
                        Date =  DateTime.Now,
                        ImportContainerID = importContainerId
                    };

                    // Make Entry in Import Nodes --> GEPSIO   
                    ImportXBRL importXBRL = new ImportXBRL();
                    importXBRL.Import(import, ref nodes, file.FullPath, Server.MapPath("~/Content/ImportedFiles/Taxonomy"));

                    JeffFerguson.Gepsio.XbrlDocument xbrlDoc = importXBRL.XbrlDocument;
                    JeffFerguson.Gepsio.XbrlSchema xbrlSchema = xbrlDoc.XbrlFragments.First().Schemas.First();
                    string schemeDirectoryName = Path.GetDirectoryName(xbrlSchema.Path);
                    Taxonomy taxonomy = db.Taxonomies.First();
                    //Taxonomy taxonomy = db.Taxonomies.Where(i => i.Path == schemeDirectoryName).First();
                        //Where(i => i.Path == xbrlSchema.Path).First();

                    import.TaxonomyID = taxonomy.TaxonomyID;

                    if (ModelState.IsValid)
                    {
                        db.Imports.Add(import);
                        db.SaveChanges();
                    }
                    
                    foreach (ImportNode node in nodes)
                    {
                        if (ModelState.IsValid)
                        {
                            db.ImportNodes.Add(node);
                            db.SaveChanges();
                        }
                        import.Nodes.Add(node);
                    }

                    if (ModelState.IsValid)
                    {
                        db.Entry(import).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    // Import in Puffer abgeschlossen... 
                    // Nun in die richtigen Tabellen...

                    // ----- Check Variablen ---------
                    bool companyExists = false;
                    bool reportExists = false;
                    bool contextExists = false;

                    // ----- COMPANY -------                    
                    Company company2 = null;
                    if (db.Companies.Where(i => i.CompanyIDXbrl == importXBRL.Company.CompanyID).Count() > 0)
                        company2 = db.Companies.Where(i => i.CompanyIDXbrl == importXBRL.Company.CompanyID).Single();

                    Company company = importXBRL.DbCompanyUpdate(company2, ref companyExists, industries);

                    // ----- REPORT -------
                    if (db.Reports.Where(i => i.ReportIDXbrl == importXBRL.Report.ReportID).Count() > 0)
                        reportExists = true;

                    Report report = new Report();
                    GenInfoDocument genInfoDocument = new GenInfoDocument();

                    if (!reportExists)
                    {
                        // ------ Report -------

                        if (importXBRL.Report.AccordingToYearEnd == DateTime.MinValue)
                            report.AccordingToYearEnd = (DateTime)SqlDateTime.MinValue;// DateTime.MinValue;//new DateTime(1753, 1, 1);
                        else
                            report.AccordingToYearEnd = importXBRL.Report.AccordingToYearEnd;
                        report.ReportType = importXBRL.Report.ReportType;
                        report.ReportIDXbrl = importXBRL.Report.ReportID;

                        // ----- GENINFO -------

                        if (importXBRL.Document.GenerationDate == DateTime.MinValue)
                            genInfoDocument.GenerationDate = (DateTime)SqlDateTime.MinValue;//DateTime.MinValue;//new DateTime(1753, 1, 1);
                        else
                            genInfoDocument.GenerationDate = importXBRL.Document.GenerationDate;
                    }
                    else 
                    {
                        report = db.Reports.Where(i => i.ReportIDXbrl == importXBRL.Report.ReportID).First();
                        //genInfoDocument = db.GenInfoDocuments.Where(i => i.GenInfoDocumentID == importXBRL.Document.DocumentID).First();
                    }

                    if (importXBRL.Document.GenerationDate == DateTime.MinValue)
                        genInfoDocument.GenerationDate = (DateTime)SqlDateTime.MinValue;//DateTime.MinValue;//new DateTime(1753, 1, 1);
                    else
                        genInfoDocument.GenerationDate = importXBRL.Document.GenerationDate;

                    if (ModelState.IsValid)
                    {
                        if (!companyExists)
                        {
                            db.Companies.Add(company);
                        }

                        if (!reportExists)
                        {
                            db.Reports.Add(report);
                            db.GenInfoDocuments.Add(genInfoDocument);
                        }

                        db.SaveChanges();
                    }                    

                    // ----- CONTEXT -------
                    ICollection<Context> contexts = new List<Context>();
                    ICollection<ContextContainer> contextContainers = new List<ContextContainer>();
                    foreach (JeffFerguson.Gepsio.Context context in importXBRL.Contexts)
                    {
                        // ----- CONTEXT CONTAINER -------
                        ContextContainer contextContainer = new ContextContainer();
                        int contextYear;
                        if (context.PeriodEndDate == DateTime.MinValue) {
                            contextYear = context.InstantDate.Year;
                        } else {
                            contextYear = context.PeriodEndDate.Year;
                        }
                        List<ContextContainer> existingContextContainers = db.ContextContainers
                                                                                .Where(i => i.CompanyID == company.CompanyID)
                                                                                .Where(i => i.Year == contextYear)
                                                                                .ToList();
                        if (existingContextContainers.Count > 0)
                        {
                            contextContainer = existingContextContainers.First();
                        }
                        else
                        {
                            contextContainer = new ContextContainer
                                                    {
                                                        CompanyID = company.CompanyID,
                                                        Company = company,
                                                        Year = contextYear
                                                    };
                            if (ModelState.IsValid)
                            {                                
                                db.ContextContainers.Add(contextContainer);
                                db.SaveChanges();
                            }
                        }
                        if (contextContainers.Where(i => i.ContextContainerID == contextContainer.ContextContainerID).Count() == 0) {
                            contextContainers.Add(contextContainer);
                        }
                        
                        // TODO
                        if (db.Contexts.Where(i => i.XbrlContextID == context.Id).Count() > 0)
                            contextExists = true;

                        if (!contextExists)
                        {
                            Context context2 = new Context();
                            context2.XbrlContextID = context.Id;
                            context2.StartDate = (DateTime)SqlDateTime.MinValue;
                            context2.EndDate = (DateTime)SqlDateTime.MinValue;
                            context2.Instant = (DateTime)SqlDateTime.MinValue;

                            if (context.PeriodStartDate != DateTime.MinValue)                                
                                context2.StartDate = context.PeriodStartDate;

                            if (context.PeriodEndDate != DateTime.MinValue)                                
                                context2.EndDate = context.PeriodEndDate;

                            if (context.InstantDate != DateTime.MinValue)                                
                                context2.Instant = context.InstantDate;

                            // JIW context2.ContextContainerID = contextContainer.ContextContainerID;//company.CompanyID;
                            context2.ContextContainer = contextContainer;
                            context2.ReportID = report.ReportID;
                            context2.Report = report;
                            context2.GenInfoDocumentID = genInfoDocument.GenInfoDocumentID;
                            context2.GenInfoDocument = genInfoDocument;
                            context2.Type = ContextType.None;
                            if (context2.EndDate != (DateTime)SqlDateTime.MinValue) {
                                context2.Type = ContextType.IncomeStatement;
                            }
                            if (context2.Instant != (DateTime)SqlDateTime.MinValue)
                            {                                
                                context2.Type = ContextType.BalanceSheet;
                            }
                            contexts.Add(context2);
                        }
                    }

                    if (contexts.Count() > 0)
                    {
                        ContextContainer contextContainer;

                        if (ModelState.IsValid)
                        {
                            foreach (Context context in contexts)
                            {
                                db.Contexts.Add(context);
                                db.SaveChanges();
                                // TODO JIW!!!!!!!
                                contextContainer = contextContainers.Where(i => i.ContextContainerID == context.ContextContainerID).Single();
                                // if (context.EndDate == (DateTime)SqlDateTime.MinValue)
                                // {
                                //     contextContainer.BalanceSheetContext = context;
                                // }
                                // else
                                // {
                                //     contextContainer.IncomeStatementContext = context;                                    
                                // }
                                contextContainer.Contexts.Add(context);
                                db.Entry(contextContainer).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            db.SaveChanges();
                        }

                        // ----- Company -------
                        company.ContextContainers = contextContainers;

                        // ----- Report -------
                        report.Contexts = contexts;

                        // ----- GenInfo -------
                        genInfoDocument.Contexts = contexts;

                        if (ModelState.IsValid)
                        {
                            db.Entry(company).State = EntityState.Modified;
                            db.Entry(report).State = EntityState.Modified;
                            db.Entry(genInfoDocument).State = EntityState.Modified;

                            db.SaveChanges();
                        }

                        // ----- UNITS -------
                        ICollection<Unit> units = new List<Unit>();
                        foreach (JeffFerguson.Gepsio.Unit unit in importXBRL.Units)
                        {
                            Unit unit2 = new Unit
                            {
                                XbrlUnitID = unit.Id,
                            };
                            units.Add(unit2);
                        }

                        if (ModelState.IsValid)
                        {
                            foreach (Unit unit in units)
                            {
                                db.Units.Add(unit);
                            }
                            db.SaveChanges();
                        }

                        // ----- FINANCIAL DATA -------
                        ICollection<FinancialData> financialDatas = new List<FinancialData>();
                        foreach (JeffFerguson.Gepsio.Fact fact in importXBRL.FinancialFacts)
                        {
                            FinancialData financialData = new FinancialData
                            {
                                ContextID = contexts.Where(i => i.XbrlContextID == ((JeffFerguson.Gepsio.Item)(fact)).ContextRefName).Single().ContextID,
                                Context = contexts.Where(i => i.XbrlContextID == ((JeffFerguson.Gepsio.Item)(fact)).ContextRefName).Single(),
                                UnitID = units.Where(i => i.XbrlUnitID == ((JeffFerguson.Gepsio.Item)(fact)).UnitRefName).Single().UnitId,
                                Unit = units.Where(i => i.XbrlUnitID == ((JeffFerguson.Gepsio.Item)(fact)).UnitRefName).Single(),
                                XbrlName = fact.Name,
                                Precision = ((JeffFerguson.Gepsio.Item)(fact)).Precision,
                                Value = ((JeffFerguson.Gepsio.Item)(fact)).Value
                            };
                            financialDatas.Add(financialData);
                        }

                        if (ModelState.IsValid)
                        {
                            foreach (FinancialData data in financialDatas)
                            {
                                db.FinancialDatas.Add(data);
                            }
                            db.SaveChanges();
                        }

                        // ----- CONTEXT -------
                        foreach (Context context in contexts)
                        {
                            context.FinancialDatas = new List<FinancialData>();

                            foreach (FinancialData data in financialDatas.Where(i => i.ContextID == context.ContextID))
                            {
                                context.FinancialDatas.Add(data);
                            };
                        }

                        // ----- UNIT -------
                        foreach (Unit unit in units)
                        {
                            foreach (FinancialData data in financialDatas.Where(i => i.UnitID == unit.UnitId))
                            {
                                unit.FinancialDatas.Add(data);
                            };
                        }


                        if (ModelState.IsValid)
                        {
                            foreach (Context context in contexts)
                            {
                                db.Entry(context).State = EntityState.Modified;
                            }

                            foreach (Unit unit in units)
                            {
                                db.Entry(unit).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }

                        // Calculate KPIs
                        Calculator calculator = new Calculator();
                        ICollection<CalculatedKPI> calculatedKPIs = new List<CalculatedKPI>();

                        foreach (ContextContainer contextContainer2 in contextContainers)
                        {
                            contextContainer2.CalculatedKPIs = new List<CalculatedKPI>();
                            foreach (FormulaDetail formula in taxonomy.FormulaDetails)
                            {
                                decimal result = 0;
                                List<FinancialData> isFinancialDatas = new List<FinancialData>();
                                List<FinancialData> bsFinancialDatas = new List<FinancialData>();
                                if (contextContainer2.Contexts.Where(i => i.Type == ContextType.IncomeStatement).Count() > 0) {
                                    isFinancialDatas = contextContainer2.Contexts.Where(i => i.Type == ContextType.IncomeStatement).Single().FinancialDatas.ToList();
                                }
                                if (contextContainer2.Contexts.Where(i => i.Type == ContextType.BalanceSheet).Count() > 0) { 
                                    bsFinancialDatas = contextContainer2.Contexts.Where(i => i.Type == ContextType.BalanceSheet).Single().FinancialDatas.ToList();
                                }
                                
                                if (calculator.CalculateDetail(isFinancialDatas, bsFinancialDatas, formula.FormulaExpression, ref result))
                                {
                                    CalculatedKPI calculatedKPI = new CalculatedKPI
                                                                            {
                                                                                ContextContainerID = contextContainer2.ContextContainerID,
                                                                                FormulaDetailID = formula.FormulaDetailID,
                                                                                Value = result,
                                                                                ContextContainer = contextContainer2,
                                                                                FormulaDetail = formula
                                                                            };
                                    calculatedKPIs.Add(calculatedKPI);
                                    //db.CalculatedKPIs.Add(calculatedKPI);
                                    contextContainer2.CalculatedKPIs.Add(calculatedKPI);
                                    //if (ModelState.IsValid)
                                    //{
                                    //    db.CalculatedKPIs.Add(calculatedKPI);
                                    //    db.SaveChanges();
                                    //}
                                }
                            }
                            //contextContainer2.CalculatedKPIs = calculatedKPIs;
                            //if (ModelState.IsValid) { 
                            //    db.Entry(contextContainer2).State = EntityState.Modified;
                            //    db.SaveChanges();
                            //}
                        }

                        if (ModelState.IsValid)
                        {
                            foreach (CalculatedKPI calculatedKPI in calculatedKPIs)
                            {
                                db.CalculatedKPIs.Add(calculatedKPI);
                            }

                            foreach (ContextContainer contextContainer2 in contextContainers)
                            {
                                db.Entry(contextContainer2).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }
                    }
                }

                return viewresult;
            }

            return RedirectToAction("Index");
        }

        //here i am receving the extra info injected
        [HttpPost] // should accept only post
        public ActionResult DeleteFile(int? entityId, string fileUrl)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            var filePath = Server.MapPath("~" + fileUrl);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            var viewresult = Json(new { error = String.Empty });
            //for IE8 which does not accept application/json
            if (Request.Headers["Accept"] != null && !Request.Headers["Accept"].Contains("application/json"))
                viewresult.ContentType = "text/plain";

            return viewresult; // trigger success
        }

        public ActionResult DownloadFile(string fileUrl, string mimetype)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            var filePath = Server.MapPath("~" + fileUrl);

            if (System.IO.File.Exists(filePath))
                return File(filePath, "XBRL");
            else
            {
                return new HttpNotFoundResult("File not found");
            }
        }

        static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.Write("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.Write("ERROR: ");

            Console.WriteLine(args.Message);
        }
    }
}
