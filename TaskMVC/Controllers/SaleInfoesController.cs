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

        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var salesInfo = db.SalesInfo.Include(s => s.Manager);
            if (!String.IsNullOrEmpty(searchString))
            {
                salesInfo = salesInfo.Where(s => s.ProductName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ClientName.ToUpper().Contains(searchString.ToUpper())
                                       || s.Manager.LastName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    salesInfo = salesInfo.OrderByDescending(s => s.ProductName);
                    break;
                default:
                    salesInfo = salesInfo.OrderBy(s => s.ProductName);
                    break;
            }
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
