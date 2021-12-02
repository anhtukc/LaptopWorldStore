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
    public class providersController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: Admin/providers
        public ActionResult Index(int page = 1)
        {
            return View(db.providers.ToList().ToPagedList(page, 10));
        }

         [HttpGet]
        public ActionResult Index(string searchString, int page = 1)
        {
            var links = from l in db.providers
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.provider_name.Contains(searchString));
            }

            return View(links.ToList().ToPagedList(page, 10));
        }
        // GET: Admin/providers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            provider provider = db.providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // GET: Admin/providers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "provider_id,provider_name,address,phonenumber,flag")] provider provider)
        {
            if (ModelState.IsValid)
            {
                db.providers.Add(provider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(provider);
        }

        // GET: Admin/providers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            provider provider = db.providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: Admin/providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "provider_id,provider_name,address,phonenumber,flag")] provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provider);
        }

        // GET: Admin/providers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            provider provider = db.providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: Admin/providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            provider provider = db.providers.Find(id);
            db.providers.Remove(provider);
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
