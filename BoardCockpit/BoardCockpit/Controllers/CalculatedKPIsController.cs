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
    public class CalculatedKPIsController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: CalculatedKPIs
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            var calculatedKPIs = db.CalculatedKPIs.Include(c => c.Context).Include(c => c.FormulaDetail);
            return View(calculatedKPIs.ToList());
        }

        // GET: CalculatedKPIs/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalculatedKPI calculatedKPI = db.CalculatedKPIs.Find(id);
            if (calculatedKPI == null)
            {
                return HttpNotFound();
            }
            return View(calculatedKPI);
        }

        // GET: CalculatedKPIs/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ContextID = new SelectList(db.Contexts, "ContextID", "XbrlContextID");
            ViewBag.FormulaDetailID = new SelectList(db.FormulaDetails, "FormulaDetailID", "FormulaExpression");
            return View();
        }

        // POST: CalculatedKPIs/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalculatedKPIID,Value,FormulaDetailID,ContextID")] CalculatedKPI calculatedKPI)
        {
            ViewBag.Sidebar = true;

            if (ModelState.IsValid)
            {
                db.CalculatedKPIs.Add(calculatedKPI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContextID = new SelectList(db.Contexts, "ContextID", "XbrlContextID", calculatedKPI.ContextID);
            ViewBag.FormulaDetailID = new SelectList(db.FormulaDetails, "FormulaDetailID", "FormulaExpression", calculatedKPI.FormulaDetailID);
            return View(calculatedKPI);
        }

        // GET: CalculatedKPIs/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalculatedKPI calculatedKPI = db.CalculatedKPIs.Find(id);
            if (calculatedKPI == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContextID = new SelectList(db.Contexts, "ContextID", "XbrlContextID", calculatedKPI.ContextID);
            ViewBag.FormulaDetailID = new SelectList(db.FormulaDetails, "FormulaDetailID", "FormulaExpression", calculatedKPI.FormulaDetailID);
            return View(calculatedKPI);
        }

        // POST: CalculatedKPIs/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CalculatedKPIID,Value,FormulaDetailID,ContextID")] CalculatedKPI calculatedKPI)
        {
            ViewBag.Sidebar = true;

            if (ModelState.IsValid)
            {
                db.Entry(calculatedKPI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContextID = new SelectList(db.Contexts, "ContextID", "XbrlContextID", calculatedKPI.ContextID);
            ViewBag.FormulaDetailID = new SelectList(db.FormulaDetails, "FormulaDetailID", "FormulaExpression", calculatedKPI.FormulaDetailID);
            return View(calculatedKPI);
        }

        // GET: CalculatedKPIs/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalculatedKPI calculatedKPI = db.CalculatedKPIs.Find(id);
            if (calculatedKPI == null)
            {
                return HttpNotFound();
            }
            return View(calculatedKPI);
        }

        // POST: CalculatedKPIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;

            CalculatedKPI calculatedKPI = db.CalculatedKPIs.Find(id);
            db.CalculatedKPIs.Remove(calculatedKPI);
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
