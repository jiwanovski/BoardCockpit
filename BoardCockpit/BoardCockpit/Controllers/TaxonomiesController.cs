using System;
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
using System.IO;
using BoardCockpit.ViewModels;
using System.Xml;
using System.Xml.Schema;

namespace BoardCockpit.Controllers
{
    public class TaxonomiesController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: Taxonomies
        public ActionResult Index(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";
            var viewModel = new TaxonomyIndexData();
            viewModel.Taxonomies = db.Taxonomies;

            if (id != null)
            {
                ViewBag.TaxonomyID = id.Value;
                // Lazy Loading
                viewModel.TaxonomyFiles = viewModel.Taxonomies.Where(
                    i => i.TaxonomyID == id.Value).Single().Files;
            }

            return View(viewModel);
        }

        // GET: Taxonomies/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Taxonomy taxonomy = db.Taxonomies.Find(id);
            if (taxonomy == null)
            {
                return HttpNotFound();
            }

            return View(taxonomy);
        }

        // GET: Taxonomies/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";
            return View();
        }

        // POST: Taxonomies/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaxonomyID,Name,Path")] Taxonomy taxonomy)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";

            if (ModelState.IsValid)
            {
                db.Taxonomies.Add(taxonomy);
                db.SaveChanges();
                // return RedirectToAction("Index");
                return RedirectToAction("Upload", new { TaxonomyID = taxonomy.TaxonomyID });
            }

            return View(taxonomy);
        }

        // GET: Taxonomies/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxonomy taxonomy = db.Taxonomies.Find(id);
            if (taxonomy == null)
            {
                return HttpNotFound();
            }

            return View(taxonomy);
        }

        // POST: Taxonomies/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaxonomyID,Name,Path")] Taxonomy taxonomy)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";

            if (ModelState.IsValid)
            {
                db.Entry(taxonomy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Taxonomies", new { id=taxonomy.TaxonomyID,status="edit"});
            }

            return View(taxonomy);
        }

        // GET: Taxonomies/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxonomy taxonomy = db.Taxonomies.Find(id);
            if (taxonomy == null)
            {
                return HttpNotFound();
            }

            return View(taxonomy);
        }

        // POST: Taxonomies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";

            Taxonomy taxonomy = db.Taxonomies.Find(id);
            db.Taxonomies.Remove(taxonomy);
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
        public ActionResult Upload(int taxonomyID)
        {
            ViewBag.TaxonomyID = taxonomyID;
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";

            return View();
        }

        public ActionResult UploadFile(int? entityId, int taxonomyId) // optionally receive values specified with Html helper
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";

            Taxonomy taxonomy = db.Taxonomies.Find(taxonomyId);
            if (taxonomy == null)
            {
                return HttpNotFound();
            }

            string path = Server.MapPath("~/Content/ImportedFiles/Taxonomy/" + taxonomy.Name);

            if (Directory.Exists(path))
            {
                // TODO JIW Dateipfad existiert schon
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

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
                        x.UrlPrefix = "/Content/ImportedFiles/Taxonomy/" + taxonomy.Name;// this is used to generate the relative url of the file

                        //overriding defaults
                        x.FileName = Request.Files[i].FileName;// default is filename suffixed with filetimestamp
                        x.ThrowExceptions = true;//default is false, if false exception message is set in error property
                    });
                    statuses.Add(st);
                }
            }

            if (statuses.Count > 0)
            {
                //statuses contains all the uploaded files details (if error occurs then check error property is not null or empty)
                //todo: add additional code to generate thumbnail for videos, associate files with entities etc

                //adding thumbnail url for jquery file upload javascript plugin
                // TODO: JIW: Change Image
                statuses.ForEach(x =>
                {
                    if (x.type == "application/xml")
                    {
                        x.thumbnailUrl = "/Images/XSD_Icon.png";
                    }
                    if (x.type == "text/xml")
                    {
                        x.thumbnailUrl = "/Images/XML_Icon.png";
                    }
                    //x.thumbnailUrl = "/Images/XBRL_Icon.jpg" 
                }); // uses ImageResizer httpmodule to resize images from this url

                //setting custom download url instead of direct url to file which is default
                statuses.ForEach(x => x.url = Url.Action("DownloadFile", new { fileUrl = x.url }));


                //server side error generation, generate some random error if entity id is 13
                if (entityId == 13)
                {
                    var rnd = new Random();
                    statuses.ForEach(x =>
                    {
                        //setting the error property removes the deleteUrl, thumbnailUrl and url property values
                        x.error = rnd.Next(0, 2) > 0 ? "We do not have any entity with unlucky Id : '13'" : String.Format("Your file size is {0} bytes which is un-acceptable", x.size);
                        //delete file by using FullPath property
                        if (System.IO.File.Exists(x.FullPath)) System.IO.File.Delete(x.FullPath);
                    });
                }

                var viewresult = Json(new { files = statuses });
                //for IE8 which does not accept application/json
                if (Request.Headers["Accept"] != null && !Request.Headers["Accept"].Contains("application/json"))
                    viewresult.ContentType = "text/plain";

                //foreach (ViewDataUploadFileResult fileResult in statuses)
                //{
                //    ImportXBRL importXBRL = new ImportXBRL();
                //    importXBRL.Import(1, fileResult.FullPath);

                //}

                // List<TaxonomyFile> taxonomyFiles = new List<TaxonomyFile>;
                foreach (ViewDataUploadFileResult file in statuses)
                {
                    TaxonomyFile taxonomyFile = new TaxonomyFile
                                                {
                                                    TaxonomyID = taxonomyId,
                                                    Name = file.SavedFileName,
                                                    Path = path,
                                                    FullFilePath = file.FullPath,
                                                    FileName = file.SavedFileName,
                                                    Taxonomy = taxonomy
                                                };

                    if (ModelState.IsValid)
                    {
                        db.TaxonomyFiles.Add(taxonomyFile);
                        db.SaveChanges();
                    }

                    taxonomy.Files.Add(taxonomyFile);

                    if (file.type == "application/xml") 
                    {
                        TaxonomyFile taxonomyFile2 = db.TaxonomyFiles.Where(c => c.FileName == file.SavedFileName).First();
                        if (taxonomyFile2.TaxonomyFileNodes == null)
                            taxonomyFile2.TaxonomyFileNodes = new List<TaxonomyFileNode>();
                        XmlTextReader reader = new XmlTextReader(file.FullPath);
                        XmlSchema myschema = XmlSchema.Read(reader, ValidationCallback);
                        XmlDocument deLabelDoc = new XmlDocument();

                        // Read Items
                        foreach (var item in myschema.Items)
                        {
                            if (item is XmlSchemaAnnotation) 
                            {
                                XmlSchemaAnnotation xmlSchemaAnnotation = (XmlSchemaAnnotation)item;

                                XmlSchemaAppInfo xmlSchemaAppInfo = (XmlSchemaAppInfo)xmlSchemaAnnotation.Items[0];

                                foreach (var markup in xmlSchemaAppInfo.Markup.Where(x => x.LocalName == "linkbaseRef"))
                                {
                                    if (markup.OuterXml.Contains("label-de")) {
                                        string deLabelName = "";
                                        foreach (XmlAttribute attribute in markup.Attributes)
                                        {
                                            if (attribute.LocalName == "href") 
                                            {
                                                deLabelName = attribute.Value;
                                            }
                                        }

                                        deLabelDoc.Load(path + "\\" + deLabelName);
                                    }
                                }                                                           
                            }

                            if (item is XmlSchemaElement) 
                            {
                                XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)item;

                                TaxonomyFileNode taxonomyFileNode = new TaxonomyFileNode
                                {
                                    TaxonomyFileID = taxonomyFile2.TaxonomyFileID,
                                    NodeName = xmlSchemaElement.Name,
                                    TaxonomyID = taxonomyFile2.TaxonomyID
                                };

                                taxonomyFile2.TaxonomyFileNodes.Add(taxonomyFileNode);

                                if (ModelState.IsValid)
                                {
                                    db.TaxonomyFileNodes.Add(taxonomyFileNode);
                                    db.Entry(taxonomyFile2).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }                            
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    taxonomy.Path = path;
                    db.Entry(taxonomy).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }

                return viewresult;
            }

            return RedirectToAction("Index");
        }

        static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.Write("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.Write("ERROR: ");

            Console.WriteLine(args.Message);
        }

        //here i am receving the extra info injected
        [HttpPost] // should accept only post
        public ActionResult DeleteFile(int? entityId, string fileUrl)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Taxonomies";
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
            ViewBag.ActiveSidebar = "Taxonomies";
            var filePath = Server.MapPath("~" + fileUrl);

            if (System.IO.File.Exists(filePath))
                return File(filePath, "XBRL");
            else
            {
                return new HttpNotFoundResult("File not found");
            }
        }
    }
}
