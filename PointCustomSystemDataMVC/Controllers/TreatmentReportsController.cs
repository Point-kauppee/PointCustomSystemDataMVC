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
    public class TreatmentReportsController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: TreatmentReports
        public ActionResult Index()
        {
            var treatmentReport = db.TreatmentReport.Include(t => t.Personnel).Include(t => t.Studentx).Include(t => t.User).Include(t => t.Reservation1).Include(t => t.Customer1);
            return View(treatmentReport.ToList());
        }

        // GET: TreatmentReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentReport treatmentReport = db.TreatmentReport.Find(id);
            if (treatmentReport == null)
            {
                return HttpNotFound();
            }
            return View(treatmentReport);
        }

        // GET: TreatmentReports/Create
        public ActionResult Create()
        {
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
            return View();
        }

        // POST: TreatmentReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TreatmentReport_id,TreatmentReportName,TreatmentDate,TreatmentTime,User_id,Customer_id,Student_id,Personnel_id,Reservation_id,TreatmentReportText")] TreatmentReport treatmentReport)
        {
            if (ModelState.IsValid)
            {
                db.TreatmentReport.Add(treatmentReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatmentReport.Personnel_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatmentReport.Student_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatmentReport.User_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", treatmentReport.Reservation_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatmentReport.Customer_id);
            return View(treatmentReport);
        }

        // GET: TreatmentReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentReport treatmentReport = db.TreatmentReport.Find(id);
            if (treatmentReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatmentReport.Personnel_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatmentReport.Student_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatmentReport.User_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", treatmentReport.Reservation_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatmentReport.Customer_id);
            return View(treatmentReport);
        }

        // POST: TreatmentReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TreatmentReport_id,TreatmentReportName,TreatmentDate,TreatmentTime,User_id,Customer_id,Student_id,Personnel_id,Reservation_id,TreatmentReportText")] TreatmentReport treatmentReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatmentReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatmentReport.Personnel_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatmentReport.Student_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatmentReport.User_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", treatmentReport.Reservation_id);
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatmentReport.Customer_id);
            return View(treatmentReport);
        }

        // GET: TreatmentReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentReport treatmentReport = db.TreatmentReport.Find(id);
            if (treatmentReport == null)
            {
                return HttpNotFound();
            }
            return View(treatmentReport);
        }

        // POST: TreatmentReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TreatmentReport treatmentReport = db.TreatmentReport.Find(id);
            db.TreatmentReport.Remove(treatmentReport);
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
