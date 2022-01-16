using LaptopWorldStore.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LaptopWorldStore.Controllers
{
    public class productsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.category);
            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(string id)
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
            return View(product);
        }

        
        public JsonResult GetProduct()
        {
            var products = db.products.Select(pd => new { pd.product_id, pd.product_name}).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Laptoppage()
        {
            var products = db.products.Include(p => p.category).Where(p => p.category_id == "laptop"); ;
            return View(products.ToList());
        }

        public ActionResult Ssdpage()
        {
            var products = db.products.Include(p => p.category).Where(p => p.category_id == "ssd"); ;
            return View(products.ToList());
        }
    }
}
