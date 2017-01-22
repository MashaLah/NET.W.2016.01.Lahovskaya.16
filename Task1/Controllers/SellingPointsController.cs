using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task1;

namespace Task1.Controllers
{
    public class SellingPointsController : Controller
    {
        private ShawarmaEntities db = new ShawarmaEntities();

        // GET: SellingPoints
        public ActionResult Index()
        {
            var sellingPoints = db.SellingPoints.Include(s => s.SellingPointCategory);
            return View(sellingPoints.ToList());
        }

        // GET: SellingPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPoint sellingPoint = db.SellingPoints.Find(id);
            if (sellingPoint == null)
            {
                return HttpNotFound();
            }
            return View(sellingPoint);
        }

        // GET: SellingPoints/Create
        public ActionResult Create()
        {
            ViewBag.SellingPointCategoryID = new SelectList(db.SellingPointCategories, "SellingPointCategoryID", "SellingPointCategoryName");
            return View();
        }

        // POST: SellingPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SellingPointID,Adress,SellingPointCategoryID,ShawarmaTitle")] SellingPoint sellingPoint)
        {
            if (ModelState.IsValid)
            {
                db.SellingPoints.Add(sellingPoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SellingPointCategoryID = new SelectList(db.SellingPointCategories, "SellingPointCategoryID", "SellingPointCategoryName", sellingPoint.SellingPointCategoryID);
            return View(sellingPoint);
        }

        // GET: SellingPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPoint sellingPoint = db.SellingPoints.Find(id);
            if (sellingPoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellingPointCategoryID = new SelectList(db.SellingPointCategories, "SellingPointCategoryID", "SellingPointCategoryName", sellingPoint.SellingPointCategoryID);
            return View(sellingPoint);
        }

        // POST: SellingPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellingPointID,Adress,SellingPointCategoryID,ShawarmaTitle")] SellingPoint sellingPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellingPoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellingPointCategoryID = new SelectList(db.SellingPointCategories, "SellingPointCategoryID", "SellingPointCategoryName", sellingPoint.SellingPointCategoryID);
            return View(sellingPoint);
        }

        // GET: SellingPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPoint sellingPoint = db.SellingPoints.Find(id);
            if (sellingPoint == null)
            {
                return HttpNotFound();
            }
            return View(sellingPoint);
        }

        // POST: SellingPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SellingPoint sellingPoint = db.SellingPoints.Find(id);
            db.SellingPoints.Remove(sellingPoint);
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
