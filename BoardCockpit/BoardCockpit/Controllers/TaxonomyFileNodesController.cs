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
    public class TaxonomyFileNodesController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: TaxonomyFileNodes
        public ActionResult Index()
        {
            ViewBag.FinancialDatas = db.FinancialDatas.ToList();
            var taxonomyFileNodes = db.TaxonomyFileNodes.Include(t => t.TaxonomyFile);
            return View(taxonomyFileNodes.ToList());
        }

        // GET: TaxonomyFileNodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxonomyFileNode taxonomyFileNode = db.TaxonomyFileNodes.Find(id);
            if (taxonomyFileNode == null)
            {
                return HttpNotFound();
            }
            return View(taxonomyFileNode);
        }

        // GET: TaxonomyFileNodes/Create
        public ActionResult Create()
        {
            ViewBag.TaxonomyFileID = new SelectList(db.TaxonomyFiles, "TaxonomyFileID", "Name");
            return View();
        }

        // POST: TaxonomyFileNodes/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaxonomyFileNodeID,TaxonomyFileID,NodeName,LabelDE,TaxonomyID")] TaxonomyFileNode taxonomyFileNode)
        {
            if (ModelState.IsValid)
            {
                db.TaxonomyFileNodes.Add(taxonomyFileNode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaxonomyFileID = new SelectList(db.TaxonomyFiles, "TaxonomyFileID", "Name", taxonomyFileNode.TaxonomyFileID);
            return View(taxonomyFileNode);
        }

        // GET: TaxonomyFileNodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxonomyFileNode taxonomyFileNode = db.TaxonomyFileNodes.Find(id);
            if (taxonomyFileNode == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaxonomyFileID = new SelectList(db.TaxonomyFiles, "TaxonomyFileID", "Name", taxonomyFileNode.TaxonomyFileID);
            return View(taxonomyFileNode);
        }

        // POST: TaxonomyFileNodes/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaxonomyFileNodeID,TaxonomyFileID,NodeName,LabelDE,TaxonomyID")] TaxonomyFileNode taxonomyFileNode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxonomyFileNode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaxonomyFileID = new SelectList(db.TaxonomyFiles, "TaxonomyFileID", "Name", taxonomyFileNode.TaxonomyFileID);
            return View(taxonomyFileNode);
        }

        // GET: TaxonomyFileNodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxonomyFileNode taxonomyFileNode = db.TaxonomyFileNodes.Find(id);
            if (taxonomyFileNode == null)
            {
                return HttpNotFound();
            }
            return View(taxonomyFileNode);
        }

        // POST: TaxonomyFileNodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxonomyFileNode taxonomyFileNode = db.TaxonomyFileNodes.Find(id);
            db.TaxonomyFileNodes.Remove(taxonomyFileNode);
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
