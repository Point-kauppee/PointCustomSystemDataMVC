using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PointCustomSystemDataMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CultureInfo fiFi = new CultureInfo("fi-FI");
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