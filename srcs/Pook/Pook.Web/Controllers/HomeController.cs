using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pook.Data;
using System.Data.Entity;
using Pook.Data.Entities;

namespace Pook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PookDbContext _context = new PookDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}