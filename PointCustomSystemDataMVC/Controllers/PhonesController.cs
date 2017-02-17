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
    public class PhonesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Phones
        public ActionResult Index()
        {
            var phone = db.Phone.Include(p => p.Customer1).Include(p => p.Personnel1).Include(p => p.PostOffices).Include(p => p.Reservation).Include(p => p.Treatment).Include(p => p.TreatmentOffice).Include(p => p.TreatmentPlace).Include(p => p.User).Include(p => p.Studentx);
            return View(phone.ToList());
        }

        // GET: Phones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Phone_id,PhoneNum_1,PhoneNum_2,PhoneNum_3,User_id,Personnel_id,Customer_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phone.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", phone.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", phone.Personnel_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", phone.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", phone.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", phone.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", phone.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", phone.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", phone.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", phone.Student_id);
            return View(phone);
        }

        // GET: Phones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", phone.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", phone.Personnel_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", phone.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", phone.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", phone.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", phone.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", phone.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", phone.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", phone.Student_id);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Phone_id,PhoneNum_1,PhoneNum_2,PhoneNum_3,User_id,Personnel_id,Customer_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", phone.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", phone.Personnel_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", phone.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", phone.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", phone.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", phone.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", phone.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", phone.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", phone.Student_id);
            return View(phone);
        }

        // GET: Phones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phone phone = db.Phone.Find(id);
            db.Phone.Remove(phone);
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
