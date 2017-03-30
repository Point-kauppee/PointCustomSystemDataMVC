using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointCustomSystemDataMVC.Models;
using PointCustomSystemDataMVC.ViewModels;

namespace PointCustomSystemDataMVC.Controllers
{
    public class TreatmentOfficesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: TreatmentOffices
        public ActionResult Index()
        {
            List<TreatmentOfficeViewModel> model = new List<TreatmentOfficeViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<TreatmentOffice> treatoffs = entities.TreatmentOffice.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                foreach (TreatmentOffice treatoff in treatoffs)
                {
                    TreatmentOfficeViewModel view = new TreatmentOfficeViewModel();
                    view.TreatmentOffice_id = treatoff.TreatmentOffice_id;
                    view.TreatmentOfficeName = treatoff.TreatmentOfficeName;
                    view.Address = treatoff.Address;
                    view.Note = treatoff.Note;
                    view.MapPlace = treatoff.MapPlace;

                    view.Phone_id = treatoff.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = treatoff.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = treatoff.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = treatoff.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = treatoff.PostOffices?.FirstOrDefault()?.PostOffice;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index

        //Lisätty 28.3.2017 oma koodi:
        // GET: TreatmentOffices/Details/5
        public ActionResult Details(int? id)
        {
            TreatmentOfficeViewModel model = new TreatmentOfficeViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<TreatmentOffice> treatoffs = entities.TreatmentOffice.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (TreatmentOffice treatoff in treatoffs)
                {
                    TreatmentOfficeViewModel view = new TreatmentOfficeViewModel();
                    view.TreatmentOffice_id = treatoff.TreatmentOffice_id;
                    view.TreatmentOfficeName = treatoff.TreatmentOfficeName;
                    view.Address = treatoff.Address;
                    view.Note = treatoff.Note;
                    view.MapPlace = treatoff.MapPlace;


                    view.Phone_id = treatoff.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = treatoff.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = treatoff.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = treatoff.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = treatoff.PostOffices?.FirstOrDefault()?.PostOffice;

                    model = view;
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TreatmentOffice treatmentOffice = db.TreatmentOffice.Find(id);
                if (treatmentOffice == null)
                {
                    return HttpNotFound();
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//details

        // GET: TreatmentOffices/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            TreatmentOfficeViewModel model = new TreatmentOfficeViewModel();

            return View();
            }//create

        // POST: TreatmentOffices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(TreatmentOfficeViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            TreatmentOffice trmoff = new TreatmentOffice();
            trmoff.TreatmentOfficeName = model.TreatmentOfficeName;
            trmoff.Address = model.Address;
            trmoff.Note = model.Note;
            trmoff.MapPlace = model.MapPlace;

            db.TreatmentOffice.Add(trmoff);

            Phone pho = new Phone();
            pho.PhoneNum_1 = model.PhoneNum_1;
            pho.TreatmentOffice = trmoff;

            db.Phone.Add(pho);

            PostOffices pos = new PostOffices();
            pos.PostalCode = model.PostalCode;
            pos.PostOffice = model.PostOffice;
            pos.TreatmentOffice = trmoff;

            db.PostOffices.Add(pos);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create

        // GET: TreatmentOffices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentOffice treatoff = db.TreatmentOffice.Find(id);
            if (treatoff == null)
            {
                return HttpNotFound();
            }

            TreatmentOfficeViewModel view = new TreatmentOfficeViewModel();
            view.TreatmentOffice_id = treatoff.TreatmentOffice_id;
            view.TreatmentOfficeName = treatoff.TreatmentOfficeName;
            view.Address = treatoff.Address;
            view.Note = treatoff.Note;
            view.MapPlace = treatoff.MapPlace;

            view.Phone_id = treatoff.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = treatoff.Phone?.FirstOrDefault()?.PhoneNum_1;

            view.Post_id = treatoff.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = treatoff.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = treatoff.PostOffices?.FirstOrDefault()?.PostOffice;

            return View(view);
        }//edit

        // POST: TreatmentOffices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(TreatmentOfficeViewModel model)
        {
            TreatmentOffice trmoff = db.TreatmentOffice.Find(model.TreatmentOffice_id);
            trmoff.TreatmentOfficeName = model.TreatmentOfficeName;
            trmoff.Address = model.Address;
            trmoff.Note = model.Note;
            trmoff.MapPlace = model.MapPlace;

            if (trmoff.Phone == null)
            {
                Phone pho = new Phone();
                pho.PhoneNum_1 = model.PhoneNum_1;
                pho.TreatmentOffice = trmoff;

                db.Phone.Add(pho);
            }
            else
            {
                Phone phone = trmoff.Phone.FirstOrDefault();
                if (phone != null)
                {
                    phone.PhoneNum_1 = model.PhoneNum_1;
                }
            }

            if (trmoff.PostOffices == null)
            {
                PostOffices pos = new PostOffices();
                pos.PostalCode = model.PostalCode;
                pos.PostOffice = model.PostOffice;
                pos.TreatmentOffice = trmoff;

                db.PostOffices.Add(pos);
            }
            else
            {
                PostOffices po = trmoff.PostOffices.FirstOrDefault();
                if (po != null)
                {
                    po.PostalCode = model.PostalCode;
                    po.PostOffice = model.PostOffice;
                }
            }

                db.SaveChanges();
            return RedirectToAction("Index");
        }//Edit


        // GET: TreatmentOffices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentOffice treatoff = db.TreatmentOffice.Find(id);
            if (treatoff == null)
            {
                return HttpNotFound();
            }

            TreatmentOfficeViewModel view = new TreatmentOfficeViewModel();
            view.TreatmentOffice_id = treatoff.TreatmentOffice_id;
            view.TreatmentOfficeName = treatoff.TreatmentOfficeName;
            view.Address = treatoff.Address;
            view.Note = treatoff.Note;
            view.MapPlace= treatoff.MapPlace;

            view.Phone_id = treatoff.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = treatoff.Phone?.FirstOrDefault()?.PhoneNum_1;

            view.Post_id = treatoff.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = treatoff.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = treatoff.PostOffices?.FirstOrDefault()?.PostOffice;

            return View(view);
        }//Delete

        // POST: TreatmentOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TreatmentOffice treatmentOffice = db.TreatmentOffice.Find(id);
            db.TreatmentOffice.Remove(treatmentOffice);
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
