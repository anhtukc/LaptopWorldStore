using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using LaptopWorldStore.Models;

namespace LaptopWorldStore.Areas.Admin.Controllers
{
    public class sellbillsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: Admin/sellbills
        public ActionResult Index(int page = 1)
        {
            var sellbills = db.sellbills.Include(s => s.customer);
            return View(sellbills.ToList().ToPagedList(page, 10));
        }

         [HttpGet]
        public ActionResult Index(string searchString, int page = 1)
        {
            var links = from l in db.sellbills
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.customer.customer_name.Contains(searchString));
            }

            return View(links.ToList().ToPagedList(page, 10));
        }
        // GET: Admin/sellbills/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sellbill sellbill = db.sellbills.Find(id);
            if (sellbill == null)
            {
                return HttpNotFound();
            }
            return View(sellbill);
        }

        // GET: Admin/sellbills/Create
        public ActionResult Create()
        {
            ViewBag.customerphonenumber = new SelectList(db.customers, "phonenumber", "customer_name");
            return View();
        }

        // POST: Admin/sellbills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sellbill_id,billDate,shippingtype,customerphonenumber,total_paid,flag")] sellbill sellbill)
        {
            if (ModelState.IsValid)
            {
                sellbill.sellbill_id = Guid.NewGuid();
                db.sellbills.Add(sellbill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerphonenumber = new SelectList(db.customers, "phonenumber", "customer_name", sellbill.customerphonenumber);
            return View(sellbill);
        }

        // GET: Admin/sellbills/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sellbill sellbill = db.sellbills.Find(id);
            if (sellbill == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerphonenumber = new SelectList(db.customers, "phonenumber", "customer_name", sellbill.customerphonenumber);
            return View(sellbill);
        }

        // POST: Admin/sellbills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sellbill_id,billDate,shippingtype,customerphonenumber,total_paid,flag")] sellbill sellbill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellbill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerphonenumber = new SelectList(db.customers, "phonenumber", "customer_name", sellbill.customerphonenumber);
            return View(sellbill);
        }

        // GET: Admin/sellbills/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sellbill sellbill = db.sellbills.Find(id);
            if (sellbill == null)
            {
                return HttpNotFound();
            }
            return View(sellbill);
        }

        // POST: Admin/sellbills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            sellbill sellbill = db.sellbills.Find(id);
            db.sellbills.Remove(sellbill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetDetails(string bill_id)
        {
            var bills = db.sellbilldetails.Select(b => new { b.sellbill_id, b.product_id, b.product_name, b.price, b.quantity, b.grand_paid }).Where(b => b.sellbill_id.ToString().ToLower() == bill_id).ToList();
            return Json(bills, JsonRequestBehavior.AllowGet);
            
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
