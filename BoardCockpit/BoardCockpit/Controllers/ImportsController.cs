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
using BoardCockpit.Helpers;

namespace BoardCockpit.Controllers
{
    public class ImportsController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: Imports
        public ActionResult Index()
        {
            return View(db.Imports.ToList());
        }

        // GET: Imports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Import import = db.Imports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }
            return View(import);
        }

        // GET: Imports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Imports/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportID,FileName,Date,Directory,Error")] Import import)
        {
            if (ModelState.IsValid)
            {
                db.Imports.Add(import);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(import);
        }

        // GET: Imports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Import import = db.Imports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }
            return View(import);
        }

        // POST: Imports/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportID,FileName,Date,Directory,Error")] Import import)
        {
            if (ModelState.IsValid)
            {
                db.Entry(import).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(import);
        }

        // GET: Imports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Import import = db.Imports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }
            return View(import);
        }

        // POST: Imports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Import import = db.Imports.Find(id);
            db.Imports.Remove(import);
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
        public ActionResult Upload()
        {            
            return View();
        }

        public ActionResult UploadFile(int? entityId) // optionally receive values specified with Html helper
        {
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
                    statuses.Add(st);
                }
            }

            if (statuses.Count > 0)
            {
                //statuses contains all the uploaded files details (if error occurs then check error property is not null or empty)
                //todo: add additional code to generate thumbnail for videos, associate files with entities etc

                //adding thumbnail url for jquery file upload javascript plugin
                // TODO: JIW: Change Image
                statuses.ForEach(x => x.thumbnailUrl = "/Images/XBRL_Icon.jpg"); // uses ImageResizer httpmodule to resize images from this url

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
                List<ImportNode> nodes = new List<ImportNode>();
                foreach (ViewDataUploadFileResult file in statuses)
                {
                    // Make Entry in Import
                    Import import = new Import
                    {                        
                        FileName = file.SavedFileName,
                        Directory = path,
                        Date = DateTime.Now                        
                    };

                    if (ModelState.IsValid)
                    {
                        db.Imports.Add(import);
                        db.SaveChanges();
                    }

                    // Make Entry in Import Nodes --> GEPSIO   
                    ImportXBRL importXBRL = new ImportXBRL();
                    importXBRL.Import(import, ref nodes, file.FullPath, Server.MapPath("~/Content/ImportedFiles/Taxonomy"));

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
                }                

                return viewresult;
            }

            return RedirectToAction("Index");
        }

        //here i am receving the extra info injected
        [HttpPost] // should accept only post
        public ActionResult DeleteFile(int? entityId, string fileUrl)
        {
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
