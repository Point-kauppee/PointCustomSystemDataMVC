using PointCustomSystemDataMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PointCustomSystemDataMVC.Controllers
{
    public class IndexCustomerWiewController : Controller
    {
        // GET: IndexCustomerWiew
        public ActionResult Index()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            List<Customer> customer = db.Customer.ToList();

            List<IndexCustomerWiew> icw = customer.Select(x => new IndexCustomerWiew
            {
                Customer_id = x.Customer_id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                Notes = x.Notes,
                Email = x.Email,
                Identity = x.Identity,
                Phone_id = x.Phone_id,
                PhoneNum_1 = x.Phone.PhoneNum_1,
                Post_id = x.Post_id,
                PostalCode = x.PostalCode,
                PostOffice = x.PostOffice,
                User_id = x.User_id,
                UserIdentity = x.UserIdentity
            }).ToList();


            return View(icw);
        }
    }
}