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
    public class ImportNodesController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: ImportNodes
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            var importNodes = db.ImportNodes.Include(i => i.Import);
            return View(importNodes.ToList());
        }

        // GET: ImportNodes/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportNode importNode = db.ImportNodes.Find(id);
            if (importNode == null)
            {
                return HttpNotFound();
            }
            return View(importNode);
        }

        // GET: ImportNodes/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            ViewBag.ImportID = new SelectList(db.Imports, "ImportID", "FileName");
            return View();
        }

        // POST: ImportNodes/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportNodeID,ImportID,ContextRef,UnitRef,Precision,Name,Value")] ImportNode importNode)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (ModelState.IsValid)
            {
                db.ImportNodes.Add(importNode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ImportID = new SelectList(db.Imports, "ImportID", "FileName", importNode.ImportID);
            return View(importNode);
        }

        // GET: ImportNodes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportNode importNode = db.ImportNodes.Find(id);
            if (importNode == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImportID = new SelectList(db.Imports, "ImportID", "FileName", importNode.ImportID);
            return View(importNode);
        }

        // POST: ImportNodes/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportNodeID,ImportID,ContextRef,UnitRef,Precision,Name,Value")] ImportNode importNode)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (ModelState.IsValid)
            {
                db.Entry(importNode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ImportID = new SelectList(db.Imports, "ImportID", "FileName", importNode.ImportID);
            return View(importNode);
        }

        // GET: ImportNodes/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportNode importNode = db.ImportNodes.Find(id);
            if (importNode == null)
            {
                return HttpNotFound();
            }
            return View(importNode);
        }

        // POST: ImportNodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Imports";
            ImportNode importNode = db.ImportNodes.Find(id);
            db.ImportNodes.Remove(importNode);
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
