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
    public class FormulaDetailsController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: FormulaDetails
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            var formulaDetails = db.FormulaDetails.Include(f => f.Formula);
            return View(formulaDetails.ToList());
        }

        // GET: FormulaDetails/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormulaDetail formulaDetail = db.FormulaDetails.Find(id);
            if (formulaDetail == null)
            {
                return HttpNotFound();
            }
            return View(formulaDetail);
        }

        // GET: FormulaDetails/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.FormulaID = new SelectList(db.Formulas, "FormulaID", "Name");
            return View();
        }

        // POST: FormulaDetails/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormulaDetailID,FormulaExpression,FormulaID")] FormulaDetail formulaDetail)
        {
            ViewBag.Sidebar = true;
            if (ModelState.IsValid)
            {
                db.FormulaDetails.Add(formulaDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FormulaID = new SelectList(db.Formulas, "FormulaID", "Name", formulaDetail.FormulaID);
            return View(formulaDetail);
        }

        // GET: FormulaDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormulaDetail formulaDetail = db.FormulaDetails.Find(id);
            if (formulaDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormulaID = new SelectList(db.Formulas, "FormulaID", "Name", formulaDetail.FormulaID);
            return View(formulaDetail);
        }

        // POST: FormulaDetails/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormulaDetailID,FormulaExpression,FormulaID")] FormulaDetail formulaDetail)
        {
            ViewBag.Sidebar = true;
            if (ModelState.IsValid)
            {
                db.Entry(formulaDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormulaID = new SelectList(db.Formulas, "FormulaID", "Name", formulaDetail.FormulaID);
            return View(formulaDetail);
        }

        // GET: FormulaDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormulaDetail formulaDetail = db.FormulaDetails.Find(id);
            if (formulaDetail == null)
            {
                return HttpNotFound();
            }
            return View(formulaDetail);
        }

        // POST: FormulaDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            FormulaDetail formulaDetail = db.FormulaDetails.Find(id);
            db.FormulaDetails.Remove(formulaDetail);
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
