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
    public class ContextContainersController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: ContextContainers
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            var contextContainers = db.ContextContainers.Include(c => c.Company);
            return View(contextContainers.ToList());
        }

        // GET: ContextContainers/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContextContainer contextContainer = db.ContextContainers.Find(id);
            if (contextContainer == null)
            {
                return HttpNotFound();
            }
            return View(contextContainer);
        }

        // GET: ContextContainers/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name");
            return View();
        }

        // POST: ContextContainers/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContextContainerID,Year,CompanyID")] ContextContainer contextContainer)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (ModelState.IsValid)
            {
                db.ContextContainers.Add(contextContainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", contextContainer.CompanyID);
            return View(contextContainer);
        }

        // GET: ContextContainers/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContextContainer contextContainer = db.ContextContainers.Find(id);
            if (contextContainer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", contextContainer.CompanyID);
            return View(contextContainer);
        }

        // POST: ContextContainers/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContextContainerID,Year,CompanyID")] ContextContainer contextContainer)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (ModelState.IsValid)
            {
                db.Entry(contextContainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", contextContainer.CompanyID);
            return View(contextContainer);
        }

        // GET: ContextContainers/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContextContainer contextContainer = db.ContextContainers.Find(id);
            if (contextContainer == null)
            {
                return HttpNotFound();
            }
            return View(contextContainer);
        }

        // POST: ContextContainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            ContextContainer contextContainer = db.ContextContainers.Find(id);
            db.ContextContainers.Remove(contextContainer);
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
