using LaptopWorldStore.Models;
using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LaptopWorldStore.Areas.Admin.Controllers
{
    public class receiptbillsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: Admin/receiptbills
        public ActionResult Index(int page = 1)
        {
            var receiptbills = db.receiptbills.Include(r => r.employee).Include(r => r.provider);
            return View(receiptbills.ToList().ToPagedList(page, 10));
        }

        [HttpGet]
        public ActionResult Index(string searchString, int page = 1)
        {
            var links = from l in db.receiptbills
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.provider.provider_name.Contains(searchString));
            }

            return View(links.ToList().ToPagedList(page, 10));
        }
        // GET: Admin/receiptbills/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receiptbill receiptbill = db.receiptbills.Find(id);
            if (receiptbill == null)
            {
                return HttpNotFound();
            }
            return View(receiptbill);
        }

        public JsonResult GetProviders()
        {
            var list = db.providers.Select(pv => new { pv.provider_id, pv.provider_name}).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployees()
        {
            var list = db.employees.Select(em => new { em.employee_id, em.employee_name }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDetails(string id)
        {
            var Guid_receiptbill_id = Guid.Parse(id);
            var list = db.receiptbilldetails.Select(dt => new {dt.receiptbill_id, dt.product_id, dt.product_name, dt.price, dt.quantity, dt.grand_paid })
                .Where(dt => dt.receiptbill_id == Guid_receiptbill_id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/receiptbills/Create
        public ActionResult Create()
        {
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "employee_name");
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name");
            return View();
        }

        // POST: Admin/receiptbills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string CreateBill(receiptbill receiptbill, receiptbilldetail[] list)
        {
            receiptbill.receiptbill_id = Guid.NewGuid();
            receiptbill.date_created = DateTime.Now;
            db.receiptbills.Add(receiptbill);
            foreach (var item in list)
            {
                item.receiptbill_id = receiptbill.receiptbill_id;
                item.receiptbilldetail_id = Guid.NewGuid();
                receiptbill.receiptbilldetails.Add(item);
            }
            db.SaveChanges();
            return "CreateSuccessful";
        }

        // GET: Admin/receiptbills/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receiptbill receiptbill = db.receiptbills.Find(id);
            if (receiptbill == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "employee_name", receiptbill.employee_id);
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", receiptbill.provider_id);
            return View(receiptbill);
        }


        [HttpPost]
        public string EditBill(string receiptbill_id, receiptbilldetail[] list)
        {
            decimal total = 0;
            var Guid_receiptbill_id = Guid.Parse(receiptbill_id);
            var receiptbill = db.receiptbills.Find(Guid_receiptbill_id);
            db.receiptbilldetails.RemoveRange(db.receiptbilldetails.Where(dt => dt.receiptbill_id == Guid_receiptbill_id));
            foreach (var item in list)
            {
                total += item.grand_paid;
                item.receiptbill_id = receiptbill.receiptbill_id;
                item.receiptbilldetail_id = Guid.NewGuid();
                receiptbill.receiptbilldetails.Add(item);
            }
            
            receiptbill.total_paid = total;
            
            db.SaveChanges();
            return "EditSuccessful";
        }

       
        [HttpPost]
     
        public string DeleteBill(string id)
        {
            var Guid_receiptbill_id = Guid.Parse(id);
            receiptbill receiptbill = db.receiptbills.Find(Guid_receiptbill_id);
            db.receiptbilldetails.RemoveRange(db.receiptbilldetails.Where(dt => dt.receiptbill_id == Guid_receiptbill_id));
            db.receiptbills.Remove(receiptbill);
            db.SaveChanges();
            return "DeleteSuccessful";
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
