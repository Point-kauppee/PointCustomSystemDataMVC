using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointCustomSystemDataMVC.Models;
using System.Globalization;
using PointCustomSystemDataMVC.ViewModels;

namespace PointCustomSystemDataMVC.Controllers
{
    public class StudentxesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Studentxes
        public ActionResult Index()
        {
            List<StudentViewModel> model = new List<StudentViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Studentx> students = entities.Studentx.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Studentx student in students)
                {
                    StudentViewModel stu = new StudentViewModel();
                    stu.Customer_id = student.Customer_id;
                    stu.FirstName = student.FirstName;
                    stu.LastName = student.LastName;
                    stu.Identity = student.Identity;
                    stu.Email = student.Email;
                    stu.EnrollmentDateIN = student.EnrollmentDateIN.Value;
                    stu.EnrollmentDateOUT = student.EnrollmentDateOUT.Value;
                    stu.Notes = student.Notes;
                    //stu.PhoneNum_1 = student.Phone1.PhoneNum_1;
                    //stu.PostOffice = student.PostOffices1.PostalCode;
                    //stu.PostOffice = student.PostOffices1.PostOffice;

                    //stu.UserIdentity = student.User.UserIdentity;

                    model.Add(stu);
                }
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }

            //return View(model);
            //var studentx = db.Studentx.Include(s => s.Phone1).Include(s => s.PostOffices1).Include(s => s.User).Include(s => s.Customer1).Include(s => s.Customer2).Include(s => s.Personnel1).Include(s => s.Reservation1).Include(s => s.TreatmentOffice1).Include(s => s.Treatment1).Include(s => s.TreatmentPlace1);
            //return View(studentx.ToList());
     

        // GET: Studentxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentx studentx = db.Studentx.Find(id);
            if (studentx == null)
            {
                return HttpNotFound();
            }
            return View(studentx);
        }

        // GET: Studentxes/Create
        public ActionResult Create()
        {
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            return View();
        }

        // POST: Studentxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Student_id,FirstName,LastName,Identity,Notes,Email,EnrollmentDateIN,EnrollmentDateOUT,Phone_id,Post_id,User_id,Address,Personnel_id,Reservation_id,Treatment_id,Customer_id,TreatmentPlace_id,TreatmentOffice_id")] Studentx studentx)
        {
            if (ModelState.IsValid)
            {
                db.Studentx.Add(studentx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", studentx.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", studentx.Post_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", studentx.User_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", studentx.Customer_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", studentx.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", studentx.Personnel_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", studentx.Reservation_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", studentx.TreatmentOffice_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", studentx.Treatment_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", studentx.TreatmentPlace_id);
            return View(studentx);
        }

        // GET: Studentxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentx studentx = db.Studentx.Find(id);
            if (studentx == null)
            {
                return HttpNotFound();
            }
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", studentx.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", studentx.Post_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", studentx.User_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", studentx.Customer_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", studentx.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", studentx.Personnel_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", studentx.Reservation_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", studentx.TreatmentOffice_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", studentx.Treatment_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", studentx.TreatmentPlace_id);
            return View(studentx);
        }

        // POST: Studentxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Student_id,FirstName,LastName,Identity,Notes,Email,EnrollmentDateIN,EnrollmentDateOUT,Phone_id,Post_id,User_id,Address,Personnel_id,Reservation_id,Treatment_id,Customer_id,TreatmentPlace_id,TreatmentOffice_id")] Studentx studentx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", studentx.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", studentx.Post_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", studentx.User_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", studentx.Customer_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", studentx.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", studentx.Personnel_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", studentx.Reservation_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", studentx.TreatmentOffice_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", studentx.Treatment_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", studentx.TreatmentPlace_id);
            return View(studentx);
        }

        // GET: Studentxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentx studentx = db.Studentx.Find(id);
            if (studentx == null)
            {
                return HttpNotFound();
            }
            return View(studentx);
        }

        // POST: Studentxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Studentx studentx = db.Studentx.Find(id);
            db.Studentx.Remove(studentx);
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
