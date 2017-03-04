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
    public class TreatmentPlacesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: TreatmentPlaces
        public ActionResult Index()
        {
            List<TreatmentPlace> model = new List<TreatmentPlace>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<TreatmentPlace> treatplas = entities.TreatmentPlace.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                foreach (TreatmentPlace treatpla in treatplas)
                {
                    TreatmentPlace view = new TreatmentPlace();
                    view.Treatmentplace_id = treatpla.Treatmentplace_id;
                    view.TreatmentPlaceName = treatpla.TreatmentPlaceName;
                    view.TreatmentPlaceNumber = treatpla.TreatmentPlaceNumber;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }

        // GET: TreatmentPlaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentPlace treatmentPlace = db.TreatmentPlace.Find(id);
            if (treatmentPlace == null)
            {
                return HttpNotFound();
            }
            return View(treatmentPlace);
        }

        // GET: TreatmentPlaces/Create
        public ActionResult Create()
        {
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            return View();
        }

        // POST: TreatmentPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Treatmentplace_id,TreatmentPlaceName,TreatmentPlaceNumber,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,Customer_id,User_id")] TreatmentPlace treatmentPlace)
        {
            if (ModelState.IsValid)
            {
                db.TreatmentPlace.Add(treatmentPlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

         
            
        
           
            return View(treatmentPlace);
        }

        // GET: TreatmentPlaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentPlace treatmentPlace = db.TreatmentPlace.Find(id);
            if (treatmentPlace == null)
            {
                return HttpNotFound();
            }
    
     
            return View(treatmentPlace);
        }

        // POST: TreatmentPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Treatmentplace_id,TreatmentPlaceName,TreatmentPlaceNumber,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,Customer_id,User_id")] TreatmentPlace treatmentPlace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatmentPlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
       
         
          
            return View(treatmentPlace);
        }

        // GET: TreatmentPlaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentPlace treatmentPlace = db.TreatmentPlace.Find(id);
            if (treatmentPlace == null)
            {
                return HttpNotFound();
            }
            return View(treatmentPlace);
        }

        // POST: TreatmentPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TreatmentPlace treatmentPlace = db.TreatmentPlace.Find(id);
            db.TreatmentPlace.Remove(treatmentPlace);
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
