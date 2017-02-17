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
    public class PostOfficesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: PostOffices
        public ActionResult Index()
        {
            var postOffices = db.PostOffices.Include(p => p.Personnel1).Include(p => p.Phone1).Include(p => p.PostOffices1).Include(p => p.PostOffices2)/*.Include(p => p.Reservation)*/.Include(p => p.Treatment).Include(p => p.TreatmentOffice).Include(p => p.TreatmentPlace).Include(p => p.User).Include(p => p.Studentx);
            return View(postOffices.ToList());
        }

        // GET: PostOffices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOffices postOffices = db.PostOffices.Find(id);
            if (postOffices == null)
            {
                return HttpNotFound();
            }
            return View(postOffices);
        }

        // GET: PostOffices/Create
        public ActionResult Create()
        {
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            //ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            return View();
        }

        // POST: PostOffices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Post_id,PostalCode,PostOffice,Personnel_id,Phone_id,Customer_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] PostOffices postOffices)
        {
            if (ModelState.IsValid)
            {
                db.PostOffices.Add(postOffices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", postOffices.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", postOffices.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            //ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", postOffices.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", postOffices.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", postOffices.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", postOffices.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", postOffices.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", postOffices.Student_id);
            return View(postOffices);
        }

        // GET: PostOffices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOffices postOffices = db.PostOffices.Find(id);
            if (postOffices == null)
            {
                return HttpNotFound();
            }
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", postOffices.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", postOffices.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            //ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", postOffices.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", postOffices.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", postOffices.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", postOffices.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", postOffices.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", postOffices.Student_id);
            return View(postOffices);
        }

        // POST: PostOffices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Post_id,PostalCode,PostOffice,Personnel_id,Phone_id,Customer_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] PostOffices postOffices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postOffices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", postOffices.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", postOffices.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            //ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", postOffices.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", postOffices.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", postOffices.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", postOffices.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", postOffices.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", postOffices.Student_id);
            return View(postOffices);
        }

        // GET: PostOffices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOffices postOffices = db.PostOffices.Find(id);
            if (postOffices == null)
            {
                return HttpNotFound();
            }
            return View(postOffices);
        }

        // POST: PostOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostOffices postOffices = db.PostOffices.Find(id);
            db.PostOffices.Remove(postOffices);
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
