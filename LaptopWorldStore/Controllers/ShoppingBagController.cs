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
using System.Data.Entity.Validation;

namespace LaptopWorldStore.Controllers
{
    public class ShoppingBagController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: ShoppingBag
        public ActionResult Index()
        {
            var sellbills = db.sellbills.Include(s => s.customer);
            return View();
        }

        public void CheckCustomerInfo(customer _ct)
        {
            customer ct = db.customers.Where(c => c.phonenumber == _ct.phonenumber).FirstOrDefault();
            if(ct == null)
            {
                _ct.customer_id = Guid.NewGuid();
                db.customers.Add(_ct);
                db.SaveChanges();
            }
        }
       public string Create(string phonenumber, string shippingtype,decimal totalprice, sellbilldetail[] list)
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
                    item.product = db.products.Find(item.product_id);
                    item.sellbill_id = sb.sellbill_id;
                    item.sellbilldetail_id = Guid.NewGuid();
                    sb.sellbilldetails.Add(item);
                }
                db.sellbills.Add(sb);

                db.SaveChanges();

            
             return "CreateSuccessful";
           
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
