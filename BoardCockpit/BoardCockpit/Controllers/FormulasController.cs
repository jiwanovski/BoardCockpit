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
using BoardCockpit.Helpers;

namespace BoardCockpit.Controllers
{
    public class FormulasController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: Formulas
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            return View(db.Formulas.ToList());
        }

        // GET: Formulas/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formula formula = db.Formulas.Find(id);
            if (formula == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChartTypes = formula.ChartType.ToSelectList();
            return View(formula);
        }

        // GET: Formulas/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            ChartType chartTypes = new ChartType();
            ViewBag.ChartType = chartTypes.ToSelectList();
            return View();
        }

        // POST: Formulas/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormulaID,Name,Description,ChartType,ToolTipDescription")] Formula formula)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            if (ModelState.IsValid)
            {
                db.Formulas.Add(formula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ChartType chartTypes = formula.ChartType;
            ViewBag.ChartType = chartTypes.ToSelectList();

            return View(formula);
        }

        // GET: Formulas/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formula formula = db.Formulas.Find(id);
            if (formula == null)
            {
                return HttpNotFound();
            }
            ChartType chartTypes = formula.ChartType;
            ViewBag.ChartType = chartTypes.ToSelectList();
            return View(formula);
        }

        // POST: Formulas/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormulaID,Name,Description,ChartType,ToolTipDescription")] Formula formula)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            if (ModelState.IsValid)
            {
                db.Entry(formula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ChartType chartTypes = formula.ChartType;
            ViewBag.ChartType = chartTypes.ToSelectList();
            return View(formula);
        }

        // GET: Formulas/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formula formula = db.Formulas.Find(id);
            if (formula == null)
            {
                return HttpNotFound();
            }
            return View(formula);
        }

        // POST: Formulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            Formula formula = db.Formulas.Find(id);
            db.Formulas.Remove(formula);
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
