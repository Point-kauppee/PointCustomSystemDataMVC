using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PointCustomSystemDataMVC.Controllers
{
    public class HomeController : Controller
    {

        //[Authorize(Roles = "Personnel User,Student User")]
        public ActionResult Index()
        {
            //string username = User.Identity.Name;
            //string userid = ((ClaimsPrincipal)User).Claims?.Where(c => c.Type == ClaimTypes.GroupSid).FirstOrDefault()?.Value ?? "";
          
            return View();
        }

        public ActionResult StartPage()
        {
            //string username = User.Identity.Name;
            //string userid = ((ClaimsPrincipal)User).Claims?.Where(c => c.Type == ClaimTypes.GroupSid).FirstOrDefault()?.Value ?? "";

            return View();
        }



        CultureInfo fiFi = new CultureInfo("fi-FI");
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