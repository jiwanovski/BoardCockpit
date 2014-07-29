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
    public class FinancialDatasController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: FinancialDatas
        public ActionResult Index()
        {
            var financialDatas = db.FinancialDatas.Include(f => f.Context).Include(f => f.Unit);
            return View(financialDatas.ToList());
        }

        // GET: FinancialDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialData financialData = db.FinancialDatas.Find(id);
            if (financialData == null)
            {
                return HttpNotFound();
            }
            return View(financialData);
        }

        // GET: FinancialDatas/Create
        public ActionResult Create()
        {
            ViewBag.ContextID = new SelectList(db.Contexts, "ContextID", "XbrlContextID");
            ViewBag.UnitID = new SelectList(db.Units, "UnitId", "XbrlUnitID");
            return View();
        }

        // POST: FinancialDatas/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FinancialDataID,Precision,Value,XbrlName,ContextID,UnitID")] FinancialData financialData)
        {
            if (ModelState.IsValid)
            {
                db.FinancialDatas.Add(financialData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContextID = new SelectList(db.Contexts, "ContextID", "XbrlContextID", financialData.ContextID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitId", "XbrlUnitID", financialData.UnitID);
            return View(financialData);
        }

        // GET: FinancialDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialData financialData = db.FinancialDatas.Find(id);
            if (financialData == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContextID = new SelectList(db.Contexts, "ContextID", "XbrlContextID", financialData.ContextID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitId", "XbrlUnitID", financialData.UnitID);
            return View(financialData);
        }

        // POST: FinancialDatas/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FinancialDataID,Precision,Value,XbrlName,ContextID,UnitID")] FinancialData financialData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financialData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContextID = new SelectList(db.Contexts, "ContextID", "XbrlContextID", financialData.ContextID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitId", "XbrlUnitID", financialData.UnitID);
            return View(financialData);
        }

        // GET: FinancialDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialData financialData = db.FinancialDatas.Find(id);
            if (financialData == null)
            {
                return HttpNotFound();
            }
            return View(financialData);
        }

        // POST: FinancialDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FinancialData financialData = db.FinancialDatas.Find(id);
            db.FinancialDatas.Remove(financialData);
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
