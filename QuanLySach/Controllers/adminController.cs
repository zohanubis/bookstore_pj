using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySach.Controllers
{
    public class adminController : Controller
    {
        QLBSDataContext db = new QLBSDataContext();
        // GET: admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListSach()
        {
            return View(db.Saches);
        }
    }
}