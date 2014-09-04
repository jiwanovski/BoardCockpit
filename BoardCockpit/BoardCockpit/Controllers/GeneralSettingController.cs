using BoardCockpit.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BoardCockpit.Models;

namespace BoardCockpit.Controllers
{
    public class GeneralSettingController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();
        // GET: GeneralSetting
        public ActionResult Setup()
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "GeneralSetting";

            GeneralSetting generalSetting;
            if (db.GeneralSetting.Count() == 0)
            {
                generalSetting = new GeneralSetting();
                ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name");
                
                return View();
            }
            else
            {
                generalSetting = db.GeneralSetting.Include(c => c.Company).First();
                ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", generalSetting.CompanyID);
                return View(generalSetting);
            }                       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup([Bind(Include = "GeneralSettingID,CompanyID")] GeneralSetting generalSetting)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "GeneralSetting";
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", generalSetting.CompanyID);

            if (ModelState.IsValid)
            {
                if (generalSetting.GeneralSettingID == 0) 
                {
                    db.GeneralSetting.Add(generalSetting);
                } 
                else 
                { 
                    db.Entry(generalSetting).State = EntityState.Modified;
                }
                db.SaveChanges();
                return View(generalSetting);
            }
            
            return View(generalSetting);
        }


    }
}