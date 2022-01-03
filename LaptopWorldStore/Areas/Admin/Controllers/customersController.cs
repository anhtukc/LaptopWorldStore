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
    public class customersController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: Admin/customers
        public ActionResult Index(int page = 1)
        {
            return View(db.customers.ToList().ToPagedList(page, 10));
        }

         [HttpGet]
        public ActionResult Index(string searchString, int page = 1)
        {
            var links = from l in db.customers
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.customer_name.Contains(searchString));
            }

            return View(links.ToList().ToPagedList(page, 10));
        }
        // GET: Admin/customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "phonenumber,customer_name,address,flag")] customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.customer_id = Guid.NewGuid();
                db.customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Admin/customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "phonenumber,customer_name,address,flag")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Admin/customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
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
