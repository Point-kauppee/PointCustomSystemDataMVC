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
    public class UsersController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Users
        public ActionResult Index()
        {
            var user = db.User.Include(u => u.Customer1).Include(u => u.Personnel1).Include(u => u.Phone1).Include(u => u.PostOffices1).Include(u => u.Reservation1).Include(u => u.Studentx1).Include(u => u.Treatment1).Include(u => u.TreatmentOffice1).Include(u => u.TreatmentPlace1);
            return View(user.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_id,UserIdentity,Password,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentPlace_id,Customer_id,TreatmentOffice_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", user.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", user.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", user.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", user.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", user.Reservation_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", user.Student_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", user.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", user.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", user.TreatmentPlace_id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", user.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", user.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", user.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", user.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", user.Reservation_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", user.Student_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", user.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", user.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", user.TreatmentPlace_id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_id,UserIdentity,Password,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentPlace_id,Customer_id,TreatmentOffice_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", user.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", user.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", user.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", user.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", user.Reservation_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", user.Student_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", user.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", user.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", user.TreatmentPlace_id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
