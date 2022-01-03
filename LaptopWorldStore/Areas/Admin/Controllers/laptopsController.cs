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
    public class laptopsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        public JsonResult GetLaptop(string id)
        {
            var laptop = db.products.Select(pd=> new{
                pd.product_id,
                pd.product_name,
                pd.provider.provider_id,
                pd.quantity,
                pd.picture,
                pd.price,
                pd.decription,
                pd.laptops.FirstOrDefault().cpu,
                pd.laptops.FirstOrDefault().gpu,
                pd.laptops.FirstOrDefault().ram,
                pd.laptops.FirstOrDefault().ssd,
                pd.laptops.FirstOrDefault().battery,
                pd.laptops.FirstOrDefault().monitor,
                pd.laptops.FirstOrDefault().os,
                pd.laptops.FirstOrDefault().weight
            }).Where(pd=>pd.product_id == id);
            return Json(laptop, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            laptop laptop = db.laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // GET: Admin/laptops/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.products, "product_id", "category_id");
            return View();
        }

        // POST: Admin/laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public string CreateLaptop(product pd, laptop lt)
        {
            pd.laptops.Add(lt);
            db.products.Add(pd);
            db.SaveChanges();
            return "CreateSuccessful";
        }

        // GET: Admin/laptops/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            laptop laptop = db.laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "category_id", laptop.product_id);
            return View(laptop);
        }

        // POST: Admin/laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string EditLaptop(product pd,  laptop lt)
        {
            var product_found = db.products.Find(pd.product_id);
            db.laptops.Remove(product_found.laptops.FirstOrDefault());
            pd.laptops.Add(lt);
            product_found = pd;
            db.SaveChanges();
            return "EditSuccessful";
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
