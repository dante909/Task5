using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMVC.DAL;
using TaskMVC.Models;

namespace TaskMVC.Controllers
{
    public class SaleInfoesController : Controller
    {
        private DAL.AppContext db = new DAL.AppContext();

        // GET: SaleInfoes
        public ActionResult Index()
        {
            var salesInfo = db.SalesInfo.Include(s => s.Manager);
            return View(salesInfo.ToList());
        }

        // GET: SaleInfoes/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleInfo saleInfo = db.SalesInfo.Find(id);
            if (saleInfo == null)
            {
                return HttpNotFound();
            }
            return View(saleInfo);
        }

        // GET: SaleInfoes/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.ManagerID = new SelectList(db.Managers, "ManagerID", "LastName");
            return View();
        }

        // POST: SaleInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "SaleInfoID,ManagerID,ClientName,ProductName,ProductCost,DateOfSale")] SaleInfo saleInfo)
        {
            if (ModelState.IsValid)
            {
                db.SalesInfo.Add(saleInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerID = new SelectList(db.Managers, "ManagerID", "LastName", saleInfo.ManagerID);
            return View(saleInfo);
        }

        // GET: SaleInfoes/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleInfo saleInfo = db.SalesInfo.Find(id);
            if (saleInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerID = new SelectList(db.Managers, "ManagerID", "LastName", saleInfo.ManagerID);
            return View(saleInfo);
        }

        // POST: SaleInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "SaleInfoID,ManagerID,ClientName,ProductName,ProductCost,DateOfSale")] SaleInfo saleInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerID = new SelectList(db.Managers, "ManagerID", "LastName", saleInfo.ManagerID);
            return View(saleInfo);
        }

        // GET: SaleInfoes/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleInfo saleInfo = db.SalesInfo.Find(id);
            if (saleInfo == null)
            {
                return HttpNotFound();
            }
            return View(saleInfo);
        }

        // POST: SaleInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleInfo saleInfo = db.SalesInfo.Find(id);
            db.SalesInfo.Remove(saleInfo);
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
