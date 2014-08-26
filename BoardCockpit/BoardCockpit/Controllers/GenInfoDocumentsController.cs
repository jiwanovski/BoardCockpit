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
    public class GenInfoDocumentsController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: GenInfoDocuments
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            return View(db.GenInfoDocuments.ToList());
        }

        // GET: GenInfoDocuments/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenInfoDocument genInfoDocument = db.GenInfoDocuments.Find(id);
            if (genInfoDocument == null)
            {
                return HttpNotFound();
            }
            return View(genInfoDocument);
        }

        // GET: GenInfoDocuments/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            return View();
        }

        // POST: GenInfoDocuments/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenInfoDocumentID,GenerationDate")] GenInfoDocument genInfoDocument)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (ModelState.IsValid)
            {
                db.GenInfoDocuments.Add(genInfoDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genInfoDocument);
        }

        // GET: GenInfoDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenInfoDocument genInfoDocument = db.GenInfoDocuments.Find(id);
            if (genInfoDocument == null)
            {
                return HttpNotFound();
            }
            return View(genInfoDocument);
        }

        // POST: GenInfoDocuments/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenInfoDocumentID,GenerationDate")] GenInfoDocument genInfoDocument)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (ModelState.IsValid)
            {
                db.Entry(genInfoDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genInfoDocument);
        }

        // GET: GenInfoDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenInfoDocument genInfoDocument = db.GenInfoDocuments.Find(id);
            if (genInfoDocument == null)
            {
                return HttpNotFound();
            }
            return View(genInfoDocument);
        }

        // POST: GenInfoDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            GenInfoDocument genInfoDocument = db.GenInfoDocuments.Find(id);
            db.GenInfoDocuments.Remove(genInfoDocument);
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
