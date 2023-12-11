using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySach.Controllers
{
    public class HomeController : Controller
    {
        QLBSDataContext db = new QLBSDataContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MucLuc()
        {
            return View(db.ChuDes.Take(10));
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Shopping()
        {
            return View();
        }
        public ActionResult ComingSoon()
        {
            return View();
        }
    }
}