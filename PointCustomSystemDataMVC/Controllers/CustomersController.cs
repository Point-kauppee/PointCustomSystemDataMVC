using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointCustomSystemDataMVC.Models;

namespace PointCustomSystemDataMVC.Controllers
{
    public class CustomersController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Customers
        public ActionResult Index()
        {
            var customer = db.Customer.Include(c => c.Personnel).Include(c => c.Phone).Include(c => c.PostOffices).Include(c => c.Treatment).Include(c => c.TreatmentOffice).Include(c => c.TreatmentPlace).Include(c => c.User).Include(c => c.Studentx);
            return View(customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
        //    ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
        //    ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
        //    ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
        //    ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
        //    ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
        //    ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
        //    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
        //    ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
        //    return View();
        //}

        //// POST: Customers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Customer_id,FirstName,LastName,Identity,Notes,Email,Address,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Customer.Add(customer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", customer.Personnel_id);
        //    ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", customer.Phone_id);
        //    ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", customer.Post_id);
        //    ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", customer.Reservation_id);
        //    ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", customer.Treatment_id);
        //    ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", customer.TreatmentOffice_id);
        //    ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", customer.TreatmentPlace_id);
        //    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", customer.User_id);
        //    ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", customer.Student_id);
        //    return View(customer);
        //}

        // GET: Customers/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            List<User> list = db.User.ToList();
            ViewBag.UserSeed = new SelectList(list, "User_id", "UserIdentity");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            List<User> list = db.User.ToList();
            ViewBag.UserSeed = new SelectList(list, "User_id", "UserIdentity");

            Customer cus = new Customer();
            cus.FirstName = model.FirstName;
            cus.LastName = model.LastName;
            cus.Identity = model.Identity;
            cus.Address = model.Address;
            cus.Email = model.Email;
            cus.Notes = model.Notes;
            cus.Post_id = model.Post_id;

            int latestUseId = model.User_id;

            db.Customer.Add(cus);
            db.SaveChanges();

            int latestCusId = cus.Customer_id;

            Phone pho = new Phone();
            pho.PhoneNum_1 = model.PhoneNum_1;
            pho.Customer_id = latestCusId;

            db.Phone.Add(pho);
            db.SaveChanges();

            PostOffices pos = new PostOffices();
            pos.PostalCode = model.PostalCode;
            pos.PostOffice = model.PostOffice;
            pos.User_id = latestUseId;

            db.PostOffices.Add(pos);
            db.SaveChanges();

            return View(model);
        }


        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", customer.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", customer.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", customer.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", customer.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", customer.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", customer.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", customer.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", customer.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", customer.Student_id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_id,FirstName,LastName,Identity,Notes,Email,Address,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", customer.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", customer.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", customer.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", customer.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", customer.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", customer.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", customer.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", customer.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", customer.Student_id);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
