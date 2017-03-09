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

            try { 

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
                model = entities.TreatmentPlace.ToList();
                entities.Dispose();
                //List<TreatmentPlace> treatplas = entities.TreatmentPlace.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                //foreach (TreatmentPlace treatpla in treatplas)
                //{
                //    TreatmentPlace view = new TreatmentPlace();
                //    view.Treatmentplace_id = treatpla.Treatmentplace_id;
                //    view.TreatmentPlaceName = treatpla.TreatmentPlaceName;
                //    view.TreatmentPlaceNumber = treatpla.TreatmentPlaceNumber;

                //    model.Add(view);
                //}
                //}
                //finally
                //{
                //    entities.Dispose();
                //}
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.GetType() + ": " + ex.Message;
            }
            //malliolion (model) välitys näkymälle- antaa instanssin asiakastietolistasta:
       
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
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            List<TreatmentPlace> model = new List<TreatmentPlace>();
            return View(model);

            //ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");     
            //return View();
        }

        // POST: TreatmentPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TreatmentPlace model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            TreatmentPlace trp = new TreatmentPlace();
            trp.TreatmentPlaceName = model.TreatmentPlaceName;
            trp.TreatmentPlaceNumber = model.TreatmentPlaceNumber;

            db.TreatmentPlace.Add(trp);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }
            //return View(user);
            return RedirectToAction("Index");
        }

        //if (ModelState.IsValid)
        //{
        //    db.TreatmentPlace.Add(treatmentPlace);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        //return View(treatmentPlace);
    

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
        public ActionResult Edit([Bind(Include = "Treatmentplace_id,TreatmentPlaceName,TreatmentPlaceNumber")] TreatmentPlace treatmentPlace)
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
