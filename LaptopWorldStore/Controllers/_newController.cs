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

namespace LaptopWorldStore.Controllers
{
    public class _newController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: _new
        public ActionResult Index(int page = 1)
        {
            var news = db.news.Include(_ => _.category);
            return View(news.ToList().ToPagedList(page, 10));
        }

       
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _new _new = db.news.Find(id);
            if (_new == null)
            {
                return HttpNotFound();
            }
            return View(_new);
        }

      public JsonResult GetNewHomePage()
        {
            var news = db.news.Select(n => new { n.New_id, n.new_name,n.Date_created,n.picture, n.author_name, n.Content}).ToList().Take(4);
            return Json(news, JsonRequestBehavior.AllowGet);
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
