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
        public ActionResult Details(Guid id)
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
        [HttpGet]
        public JsonResult GetAllProduct()
        {
            var list = db.products.Select(pd => new { pd.product_id, pd.product_name, pd.quantity, pd.price }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
        public void CheckCustomerInfo(customer _ct)
        {
            customer ct = db.customers.Where(c => c.phonenumber == _ct.phonenumber).FirstOrDefault();
            if (ct == null)
            {
                _ct.customer_id = Guid.NewGuid();
                db.customers.Add(_ct);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public string Create(string phonenumber, string shippingtype, decimal totalprice, sellbilldetail[] list)
        {
            customer ct = db.customers.Where(c => c.phonenumber == phonenumber).FirstOrDefault();
            sellbill sb = new sellbill();
            sb.billDate = DateTime.Now;
            sb.sellbill_id = Guid.NewGuid();
            sb.customer = ct;
            sb.customer_id = ct.customer_id;
            sb.shippingtype = shippingtype;
            sb.total_paid = totalprice;
            sb.flag = true;
            foreach (var item in list)
            {
                
                var product = db.products.Find(item.product_id);
                product.quantity -= item.quantity;
                item.product = product;
                item.sellbill_id = sb.sellbill_id;
                item.sellbilldetail_id = Guid.NewGuid();
                sb.sellbilldetails.Add(item);
            }
            db.sellbills.Add(sb);

            db.SaveChanges();


            return "CreateSuccessful";

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
            ViewBag.customerphonenumber = new SelectList(db.customers, "phonenumber", "customer_name", sellbill.customer_id);
            return View(sellbill);
        }

        // POST: Admin/sellbills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string EditBills( string sellbill_id,int total_paid, sellbilldetail[] list)
        {
            var Guid_sellbill_id = Guid.Parse(sellbill_id);
            sellbill sb = db.sellbills.Find(Guid_sellbill_id);
            sb.total_paid = total_paid;
            db.SaveChanges();
            var details = db.sellbilldetails.Where(dt => dt.sellbill_id == Guid_sellbill_id);
            foreach (var item in details)
            {
                var product = db.products.Find(item.product_id);
                product.quantity += item.quantity;
            }
            db.sellbilldetails.RemoveRange(db.sellbilldetails.Where(detail => detail.sellbill_id == Guid_sellbill_id));
            foreach (var item in list)
            {
                var product = db.products.Find(item.product_id);
                product.quantity -= item.quantity;
                item.product = product;
                item.sellbill_id = sb.sellbill_id;
                item.sellbilldetail_id = Guid.NewGuid();
                sb.sellbilldetails.Add(item);
            }
            db.SaveChanges();
            return "Sửa thành công";
        }

      

        // POST: Admin/sellbills/Delete/5
        [HttpPost]
        public string Delete(string id)
        {
            var bill_id = Guid.Parse(id);
            sellbill sellbill = db.sellbills.Find(bill_id);
            var details = db.sellbilldetails.Where(dt => dt.sellbill_id == bill_id);
            foreach(var item in details)
            {
                var product = db.products.Find(item.product_id);
                product.quantity += item.quantity;
            }
            db.sellbilldetails.RemoveRange(db.sellbilldetails.Where(detail => detail.sellbill_id == bill_id));
            db.sellbills.Remove(sellbill);
            db.SaveChanges(); 
            return "DeletedSuccessful";
        }

        [HttpGet]
        public JsonResult GetDetails(string sellbill_id)
        {

            var SellBill_Guid = Guid.Parse(sellbill_id);
            
            var billDetails = db.sellbilldetails.Select(b => new
            {
                b.sellbill_id,
                b.product_id,
                b.product_name,
                b.price,
                b.quantity,
                b.grand_paid
            }).Where(b => b.sellbill_id == SellBill_Guid).ToList();
            
            return Json(billDetails, JsonRequestBehavior.AllowGet);


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
