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
    public class PriceControllersController : Controller
    {
        private ShawarmaEntities db = new ShawarmaEntities();

        // GET: PriceControllers
        public ActionResult Index()
        {
            var priceControllers = db.PriceControllers.Include(p => p.Shawarma).Include(p => p.SellingPoint);
            return View(priceControllers.ToList());
        }

        // GET: PriceControllers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceController priceController = db.PriceControllers.Find(id);
            if (priceController == null)
            {
                return HttpNotFound();
            }
            return View(priceController);
        }

        // GET: PriceControllers/Create
        public ActionResult Create()
        {
            ViewBag.ShawarmaID = new SelectList(db.Shawarmas, "ShawarmaID", "ShawarmaName");
            ViewBag.SellingPointID = new SelectList(db.SellingPoints, "SellingPointID", "Adress");
            return View();
        }

        // POST: PriceControllers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriceControllerID,ShawarmaID,Price,SellingPointID,Comment")] PriceController priceController)
        {
            if (ModelState.IsValid)
            {
                db.PriceControllers.Add(priceController);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShawarmaID = new SelectList(db.Shawarmas, "ShawarmaID", "ShawarmaName", priceController.ShawarmaID);
            ViewBag.SellingPointID = new SelectList(db.SellingPoints, "SellingPointID", "Adress", priceController.SellingPointID);
            return View(priceController);
        }

        // GET: PriceControllers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceController priceController = db.PriceControllers.Find(id);
            if (priceController == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShawarmaID = new SelectList(db.Shawarmas, "ShawarmaID", "ShawarmaName", priceController.ShawarmaID);
            ViewBag.SellingPointID = new SelectList(db.SellingPoints, "SellingPointID", "Adress", priceController.SellingPointID);
            return View(priceController);
        }

        // POST: PriceControllers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceControllerID,ShawarmaID,Price,SellingPointID,Comment")] PriceController priceController)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priceController).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShawarmaID = new SelectList(db.Shawarmas, "ShawarmaID", "ShawarmaName", priceController.ShawarmaID);
            ViewBag.SellingPointID = new SelectList(db.SellingPoints, "SellingPointID", "Adress", priceController.SellingPointID);
            return View(priceController);
        }

        // GET: PriceControllers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceController priceController = db.PriceControllers.Find(id);
            if (priceController == null)
            {
                return HttpNotFound();
            }
            return View(priceController);
        }

        // POST: PriceControllers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PriceController priceController = db.PriceControllers.Find(id);
            db.PriceControllers.Remove(priceController);
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
