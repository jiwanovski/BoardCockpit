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
    public class IndustriesController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: Industries
        public ActionResult Index()
        {
            ViewBag.Sidebar = true;
            return View(db.Industries.ToList());
        }

        // GET: Industries/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Industry industry = db.Industries.Find(id);
            if (industry == null)
            {
                return HttpNotFound();
            }
            return View(industry);
        }

        // GET: Industries/Create
        public ActionResult Create()
        {
            ViewBag.Sidebar = true;
            return View();
        }

        // POST: Industries/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IndustryID,Name")] Industry industry)
        {
            ViewBag.Sidebar = true;
            if (ModelState.IsValid)
            {
                db.Industries.Add(industry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(industry);
        }

        // GET: Industries/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Industry industry = db.Industries.Find(id);
            if (industry == null)
            {
                return HttpNotFound();
            }
            return View(industry);
        }

        // POST: Industries/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IndustryID,Name")] Industry industry)
        {
            ViewBag.Sidebar = true;
            if (ModelState.IsValid)
            {
                db.Entry(industry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(industry);
        }

        // GET: Industries/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Industry industry = db.Industries.Find(id);
            if (industry == null)
            {
                return HttpNotFound();
            }
            return View(industry);
        }

        // POST: Industries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Sidebar = true;
            Industry industry = db.Industries.Find(id);
            db.Industries.Remove(industry);
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
