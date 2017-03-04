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
    public class TreatmentsController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Treatments
        public ActionResult Index()
        {
            List<Treatment> model = new List<Treatment>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Treatment> treatoffs = entities.Treatment.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                foreach (Treatment treatoff in treatoffs)
                {
                    Treatment view = new Treatment();
                    view.Treatment_id = treatoff.Treatment_id;
                    view.TreatmentName = treatoff.TreatmentName;
                    view.TreatmentTime = treatoff.TreatmentTime;
                    view.TreatmentPrice = treatoff.TreatmentPrice;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }

        // GET: Treatments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatment.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // GET: Treatments/Create
        public ActionResult Create()
        {
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            return View();
        }

        // POST: Treatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Treatment_id,TreatmentName,TreatmentTime,TreatmentPrice,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Customer_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Treatment.Add(treatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatment.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatment.Personnel_id);
           
          
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", treatment.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", treatment.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatment.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatment.Student_id);
            return View(treatment);
        }

        // GET: Treatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatment.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatment.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatment.Personnel_id);
         
       
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", treatment.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", treatment.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatment.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatment.Student_id);
            return View(treatment);
        }

        // POST: Treatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Treatment_id,TreatmentName,TreatmentTime,TreatmentPrice,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Customer_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatment.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatment.Personnel_id);
           
         
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", treatment.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", treatment.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatment.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatment.Student_id);
            return View(treatment);
        }

        // GET: Treatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatment.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treatment treatment = db.Treatment.Find(id);
            db.Treatment.Remove(treatment);
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
