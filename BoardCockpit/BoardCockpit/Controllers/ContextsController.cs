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
    public class ContextsController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: Contexts
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            var contexts = db.Contexts.Include(c => c.Company);
            return View(contexts.ToList());
        }

        // GET: Contexts/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Context context = db.Contexts.Find(id);
            if (context == null)
            {
                return HttpNotFound();
            }
            return View(context);
        }

        // GET: Contexts/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name");
            return View();
        }

        // POST: Contexts/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContextID,XbrlContextID,StartDate,EndDate,Instant,CompanyID")] Context context)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (ModelState.IsValid)
            {
                db.Contexts.Add(context);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", context.CompanyID);
            return View(context);
        }

        // GET: Contexts/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Context context = db.Contexts.Find(id);
            if (context == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", context.CompanyID);
            return View(context);
        }

        // POST: Contexts/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContextID,XbrlContextID,StartDate,EndDate,Instant,CompanyID")] Context context)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (ModelState.IsValid)
            {
                db.Entry(context).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", context.CompanyID);
            return View(context);
        }

        // GET: Contexts/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Context context = db.Contexts.Find(id);
            if (context == null)
            {
                return HttpNotFound();
            }
            return View(context);
        }

        // POST: Contexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            Context context = db.Contexts.Find(id);
            db.Contexts.Remove(context);
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
