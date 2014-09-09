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
using BoardCockpit.ViewModels;
using BoardCockpit.Helpers;
using System.Data.Entity.Infrastructure;

namespace BoardCockpit.Controllers
{
    public class FormulaDetailsController : Controller
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        // GET: FormulaDetails
        public ActionResult Index(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            ViewBag.FormulaID = id;

            var formulaDetails = db.FormulaDetails
                                        .Where(i => i.FormulaID == id)
                                        .Include(f => f.Formula);
            return View(formulaDetails.ToList());
        }

        // GET: FormulaDetails/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";

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
        public ActionResult Create(int? formulaId)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";

            ViewBag.FormulaID = new SelectList(db.Formulas, "FormulaID", "Name", formulaId);
            FormulaDetail formulaDetail = null;
            PopulateAssignedFormulaDetailData(formulaDetail);
            return View();
        }

        // POST: FormulaDetails/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormulaDetailID,FormulaExpression,FormulaID")] FormulaDetail formulaDetail, string[] selectedTaxonomies)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";

            if (ModelState.IsValid)
            {
                db.FormulaDetails.Add(formulaDetail);
                db.SaveChanges();

                if (TryUpdateModel(formulaDetail, "",
                new string[] { "FormulaDetailID", "FormulaExpression", "FormulaID" }))
                {
                    try
                    {
                        formulaDetail.Taxonomies = new List<Taxonomy>();
                        UpdateFormulaDetailTaxonomies(selectedTaxonomies, formulaDetail);

                        db.Entry(formulaDetail).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "FormulaDetails", new {id = formulaDetail.FormulaID, status = "new" });
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.FormulaID = new SelectList(db.Formulas, "FormulaID", "Name", formulaDetail.FormulaID);
            return View(formulaDetail);
        }

        // GET: FormulaDetails/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";

            FormulaDetail formulaDetail = db.FormulaDetails
                .Include(i => i.CalculatedKPIs).Where(i => i.FormulaDetailID == id)
                .Include(i => i.Formula)
                .Include(i => i.Taxonomies).Where(i => i.FormulaDetailID == id)
                .Single();
            PopulateAssignedFormulaDetailData(formulaDetail);
            if (formulaDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.FormulaID = new SelectList(db.Formulas, "FormulaID", "Name", formulaDetail.FormulaID);
            return View(formulaDetail);
        }

        private void PopulateAssignedFormulaDetailData(FormulaDetail formulaDetail)
        {
            var allTaxonomies = db.Taxonomies;
            var formulaDetailTaxonomies = new HashSet<int>();

            if (formulaDetail != null)
                formulaDetailTaxonomies = new HashSet<int>(formulaDetail.Taxonomies.Select(c => c.TaxonomyID));
            var viewModel = new List<AssignedFormulaDetailData>();
            foreach (var taxonomy in allTaxonomies)
            {
                viewModel.Add(new AssignedFormulaDetailData
                {
                    TaxonomyID = taxonomy.TaxonomyID,
                    TaxonomyName = taxonomy.Name,
                    Assigned = formulaDetailTaxonomies.Contains(taxonomy.TaxonomyID)
                });
            }
            ViewBag.Taxonomies = viewModel;
        }

        // POST: FormulaDetails/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "FormulaDetailID,FormulaExpression,FormulaID")] FormulaDetail formulaDetail)
        public ActionResult Edit(int? id, string[] selectedTaxonomy)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            var formulaDetailToUpdate = db.FormulaDetails
                .Include(i => i.CalculatedKPIs).Where(i => i.FormulaDetailID == id)
                .Include(i => i.Formula)
                .Include(i => i.Taxonomies).Where(i => i.FormulaDetailID == id)
                .Single();

            if (TryUpdateModel(formulaDetailToUpdate, "",
                new string[] { "FormulaDetailID", "FormulaExpression" , "FormulaID" }))
            {
                try
                {
                    UpdateFormulaDetailTaxonomies(selectedTaxonomy, formulaDetailToUpdate);

                    db.Entry(formulaDetailToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { id = formulaDetailToUpdate.FormulaID});
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            PopulateAssignedFormulaDetailData(formulaDetailToUpdate);
            return View(formulaDetailToUpdate);
        }

        private void UpdateFormulaDetailTaxonomies(string[] selectedTaxonomies, FormulaDetail formulaDetailToUpdate)
        {
            if (selectedTaxonomies == null)
            {
                formulaDetailToUpdate.Taxonomies = new List<Taxonomy>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedTaxonomies);
            var instructorCourses = new HashSet<int>
                (formulaDetailToUpdate.Taxonomies.Select(c => c.TaxonomyID));
            foreach (var taxonomy in db.Taxonomies)
            {
                if (selectedCoursesHS.Contains(taxonomy.TaxonomyID.ToString()))
                {
                    if (!instructorCourses.Contains(taxonomy.TaxonomyID))
                    {
                        formulaDetailToUpdate.Taxonomies.Add(taxonomy);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(taxonomy.TaxonomyID))
                    {
                        formulaDetailToUpdate.Taxonomies.Remove(taxonomy);
                    }
                }
            }
        }

        // GET: FormulaDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Sidebar = true;
            ViewBag.ActiveSidebar = "Formulas";
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
            ViewBag.ActiveSidebar = "Formulas";
            FormulaDetail formulaDetail = db.FormulaDetails.Find(id);
            db.FormulaDetails.Remove(formulaDetail);
            db.SaveChanges();
            return RedirectToAction("Index", "FormulaDetails", new {id = formulaDetail.FormulaID, status = "delete" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult GetTaxonomyNode(string term, string cb)
        {
            string[] split = cb.Split(',');
            string first = split.First();
            List<Object> nodes ;
            string y = null;
            //var node = new { };
            List<string> texts = new List<string>();
            //foreach (var item in split)            
            //{

                var node = from taxNode in db.TaxonomyFileNodes.Where(i => i.TaxonomyID.ToString() == first)
                               .Where(c => c.NodeName.Contains(term))
                           select taxNode.NodeName;                                                       
                node = node.Distinct();
                //List<TaxonomyFileNode> jsonList = db.TaxonomyFileNodes.Where(i => i.TaxonomyID.ToString() == item).ToList();//.Where(c => c.NodeName.Contains(term)).ToList();
                //foreach (var item2 in jsonList)
                //{
                //    texts.Add(item2.NodeName);
                //}
                                        
            //}
            //texts.Distinct();
            //var jsonString = texts.ToJSON();
            //return Json(jsonString, JsonRequestBehavior.AllowGet);
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
