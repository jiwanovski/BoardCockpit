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

namespace BoardCockpit.Controllers
{
    public class TaxonomyFilesController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: TaxonomyFiles
        public ActionResult Index()
        {
            var taxonomyFiles = db.TaxonomyFiles.Include(t => t.Taxonomy);
            return View(taxonomyFiles.ToList());
        }

        // GET: TaxonomyFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxonomyFile taxonomyFile = db.TaxonomyFiles.Find(id);
            if (taxonomyFile == null)
            {
                return HttpNotFound();
            }
            return View(taxonomyFile);
        }

        // GET: TaxonomyFiles/Create
        public ActionResult Create()
        {
            ViewBag.TaxonomyID = new SelectList(db.Taxonomies, "TaxonomyID", "Name");
            return View();
        }

        // POST: TaxonomyFiles/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaxonomyFileID,Name,FileName,TaxonomyID,Path,FullFilePath")] TaxonomyFile taxonomyFile)
        {
            if (ModelState.IsValid)
            {
                db.TaxonomyFiles.Add(taxonomyFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaxonomyID = new SelectList(db.Taxonomies, "TaxonomyID", "Name", taxonomyFile.TaxonomyID);
            return View(taxonomyFile);
        }

        // GET: TaxonomyFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxonomyFile taxonomyFile = db.TaxonomyFiles.Find(id);
            if (taxonomyFile == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaxonomyID = new SelectList(db.Taxonomies, "TaxonomyID", "Name", taxonomyFile.TaxonomyID);
            return View(taxonomyFile);
        }

        // POST: TaxonomyFiles/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaxonomyFileID,Name,FileName,TaxonomyID,Path,FullFilePath")] TaxonomyFile taxonomyFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxonomyFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaxonomyID = new SelectList(db.Taxonomies, "TaxonomyID", "Name", taxonomyFile.TaxonomyID);
            return View(taxonomyFile);
        }

        // GET: TaxonomyFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxonomyFile taxonomyFile = db.TaxonomyFiles.Find(id);
            if (taxonomyFile == null)
            {
                return HttpNotFound();
            }
            return View(taxonomyFile);
        }

        // POST: TaxonomyFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxonomyFile taxonomyFile = db.TaxonomyFiles.Find(id);
            db.TaxonomyFiles.Remove(taxonomyFile);
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
    }
}
