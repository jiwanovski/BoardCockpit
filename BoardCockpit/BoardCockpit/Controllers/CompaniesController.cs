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
    public class CompaniesController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: Companies
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            return View(db.Companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            return View();
        }

        // POST: Companies/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,SizeClass,Name,Location,Street,ZipCode,City,Country")] Company company)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
             
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index", new { status = "new" });
            }
            

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,SizeClass,Name,Location,Street,ZipCode,City,Country")] Company company)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = company.CompanyID , status = "edit" });
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Companies";
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index", new { status = "delete" });
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
