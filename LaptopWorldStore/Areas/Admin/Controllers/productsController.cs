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
using System.IO;

namespace LaptopWorldStore.Areas.Admin.Controllers
{
    public class productsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: Admin/products
        public ActionResult Index(int page = 1)
        {
            var products = db.products.Include(p => p.category).Include(p => p.provider);
            return View(products.ToList().ToPagedList(page, 5));
        }

         [HttpGet]
        public ActionResult Index(string searchString, int page = 1)
        {
            var links = from l in db.products
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.product_name.Contains(searchString));
            }

            return View(links.ToList().ToPagedList(page, 5));
        }
        // GET: Admin/products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            ViewBag.laptop = db.laptops.FirstOrDefault(laptop => laptop.product_id == id);
            ViewBag.ram = db.rams.FirstOrDefault(ram => ram.product_id == id);
            ViewBag.ssd = db.ssds.FirstOrDefault(ssd => ssd.product_id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", db.categories.First());
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", db.providers.First());
            return View();
        }

        // POST: Admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,category_id,provider_id,product_name,picture,price,quantity,decription,flag")] product product
                                     ,HttpPostedFileBase picture)
        {
            if(picture != null)
            {
                var filename = Path.GetFileName(product.category_id + "/" + picture.FileName);
                var dicrectiontosave = Server.MapPath(Url.Content("~/Content/images/product" ));
                var pathtosave = Path.Combine(dicrectiontosave,filename);
                picture.SaveAs(pathtosave);
                product.picture = filename;
            }
            
            if (ModelState.IsValid)
            {
                db.products.Add(product);            
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id.First());
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", product.provider_id.First());
            return View(product);
        }

        // GET: Admin/products/Edit/5
        [HttpGet]
        public JsonResult GetAllProduct()
        {
            var list = db.products.Select(pd => new { pd.product_id, pd.product_name, pd.quantity, pd.price }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", product.provider_id);
            return View(product);
        }

        // POST: Admin/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,category_id,provider_id,product_name,picture,price,quantity,decription,flag")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", product.provider_id);
            return View(product);
        }

      

        // POST: Admin/products/Delete/5
        [HttpPost]
        public string Delete(string id)
        {
            var product_in_bill = db.sellbilldetails.Where(dt => dt.product_id == id).FirstOrDefault();
            if(product_in_bill != null)
            {
                var pd = db.products.Find(id);
                pd.flag = false;
                db.SaveChanges();
                return "hideproduct";
            }
            product product = db.products.Find(id);
             db.laptops.RemoveRange(db.laptops.Where(lt => lt.product_id == product.product_id));
            db.rams.RemoveRange(db.rams.Where(lt => lt.product_id == product.product_id));
            db.ssds.RemoveRange(db.ssds.Where(lt => lt.product_id == product.product_id));
            db.products.Remove(product);
            db.SaveChanges();
            return "deletesuccessful";
        }
        public ActionResult Signout()
        {
            Session.Clear();
            return RedirectToAction("Index", "home", new { area = "" });
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
