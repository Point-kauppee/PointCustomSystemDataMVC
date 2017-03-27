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
using System.Globalization;

namespace PointCustomSystemDataMVC.Controllers
{
    public class TreatmentReportsController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: TreatmentReports
        public ActionResult Index()
        {
            List<TreatmentReportsViewModel> model = new List<TreatmentReportsViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<TreatmentReport> treatreports = entities.TreatmentReport.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");

                foreach (TreatmentReport treatreport in treatreports)
                {
                    TreatmentReportsViewModel view = new TreatmentReportsViewModel();

                    view.TreatmentReport_id = treatreport.TreatmentReport_id;
                    view.TreatmentReportName = treatreport.TreatmentReportName;
                    view.TreatmentReportText = treatreport.TreatmentReportText;
                    view.TreatmentDate = treatreport.TreatmentDate.Value;
                    view.TreatmentTime = treatreport.TreatmentTime.Value;

                    view.Customer_id = treatreport.Customer?.Customer_id;
                    view.FirstNameA = treatreport.Customer?.FirstName;
                    view.LastNameA = treatreport.Customer?.LastName;

                    view.User_id = treatreport.User?.User_id;
                    view.UserIdentity = treatreport.User?.UserIdentity;
                    //ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

                    view.Student_id = treatreport.Studentx?.Student_id;
                    view.FirstNameH = treatreport.Studentx?.FirstName;
                    view.LastNameH = treatreport.Studentx?.LastName;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }

        // GET: TreatmentReports/Details/5
        public ActionResult Details(int? id)
        {
            TreatmentReportsViewModel model = new TreatmentReportsViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<TreatmentReport> treatrepos = entities.TreatmentReport.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (TreatmentReport treatrepdetail in treatrepos)
                {
                    TreatmentReportsViewModel views = new TreatmentReportsViewModel();
                    views.TreatmentReport_id = treatrepdetail.TreatmentReport_id;
                    views.TreatmentReportText = treatrepdetail.TreatmentReportText;
                    views.TreatmentDate = treatrepdetail.TreatmentDate.Value;
                    views.TreatmentTime = treatrepdetail.TreatmentTime.Value;

                    views.Customer_id = treatrepdetail.Customer?.Customer_id;
                    views.FirstNameA = treatrepdetail.Customer?.FirstName;
                    views.LastNameA = treatrepdetail.Customer?.LastName;

                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    views.User_id = treatrepdetail.User?.User_id;
                    views.UserIdentity = treatrepdetail.User?.UserIdentity;

                    views.Student_id = treatrepdetail.Studentx?.Student_id;
                    views.FirstNameH = treatrepdetail.Studentx?.FirstName;
                    views.LastNameH = treatrepdetail.Studentx?.LastName;

                    model = views;
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TreatmentReport treatmentReport = db.TreatmentReport.Find(id);
                if (treatmentReport == null)
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

        // GET: TreatmentReports/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            TreatmentReportsViewModel model = new TreatmentReportsViewModel();
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            return View(model);
        }//create

        // POST: TreatmentReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TreatmentReportsViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            TreatmentReport tre = new TreatmentReport();
            tre.TreatmentReportName = model.TreatmentReportName;
            tre.TreatmentReportText = model.TreatmentReportText;
            tre.TreatmentDate = model.TreatmentDate;
            tre.TreatmentTime = model.TreatmentTime;
            db.TreatmentReport.Add(tre);

            // etsitään User-rivi kannasta valitun nimen perusteella
            int userId = int.Parse(model.UserIdentity);
            if (userId > 0)
            {
                User usr = db.User.Find(userId);
                tre.User_id = userId;
                tre.Customer_id = usr.Customer_id;
            }

            // etsitään Student-rivi kannasta valitun nimen perusteella
            int studentId = int.Parse(model.FullNameH2);
            if (studentId > 0)
            {
                Studentx stu = db.Studentx.Find(studentId);
                tre.Student_id = stu.Student_id;
            }

            // etsitään Customer-rivi kannasta valitun nimen perusteella
            int customerId = int.Parse(model.FullNameA);
            if (customerId > 0)
            {
                Customer stu = db.Customer.Find(studentId);
                tre.Customer_id = stu.Customer_id;
            }

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            ViewBag.FullNameH = new SelectList((from s in db.Studentx select new { Student_id = s.Student_id, FullNameH = s.FirstName + " " + s.LastName }), "Student_id", "FullNameH", null);
            CultureInfo fiFi = new CultureInfo("fi-FI");
            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//cr*/;


        // GET: TreatmentReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TreatmentReport treatrepdetail = db.TreatmentReport.Find(id);
            if (treatrepdetail == null)
            {
                return HttpNotFound();
            }

            TreatmentReportsViewModel view = new TreatmentReportsViewModel();

            view.TreatmentReport_id = treatrepdetail.TreatmentReport_id;
            view.TreatmentReportName = treatrepdetail.TreatmentReportName;
            view.TreatmentReportText = treatrepdetail.TreatmentReportText;
            view.TreatmentDate = treatrepdetail.TreatmentDate.Value;
            view.TreatmentTime = treatrepdetail.TreatmentTime.Value;

            view.Customer_id = treatrepdetail.Customer?.Customer_id;
            view.FirstNameA = treatrepdetail.Customer?.FirstName;
            view.LastNameA = treatrepdetail.Customer?.LastName;

            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            view.User_id = treatrepdetail.User?.User_id;
            view.UserIdentity = treatrepdetail.User?.UserIdentity;

            ViewBag.FullNameH = new SelectList((from s in db.Studentx select new { Student_id = s.Student_id, FullNameH = s.FirstName + " " + s.LastName }), "Student_id", "FullNameH", null);
            view.Student_id = treatrepdetail.Studentx?.Student_id;
            view.FirstNameH = treatrepdetail.Studentx?.FirstName;
            view.LastNameH = treatrepdetail.Studentx?.LastName;

            return View(view);

        }//edit

        // POST: TreatmentReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TreatmentReportsViewModel model)
        {
            TreatmentReport tre = db.TreatmentReport.Find(model.TreatmentReport_id);
            tre.TreatmentReportName = model.TreatmentReportName;
            tre.TreatmentReportText = model.TreatmentReportText;
            tre.TreatmentDate = model.TreatmentDate;
            tre.TreatmentTime = model.TreatmentTime;

            // etsitään User-rivi kannasta valitun nimen perusteella
            int userId = int.Parse(model.UserIdentity);
            if (userId > 0)
            {
                User usr = db.User.Find(userId);
                tre.User_id = userId;
                tre.Customer_id = usr.Customer_id;
            }

            // etsitään Student-rivi kannasta valitun nimen perusteella
            int studentId = int.Parse(model.FullNameH2);
            if (studentId > 0)
            {
                Studentx stu = db.Studentx.Find(studentId);
                tre.Student_id = stu.Student_id;
            }

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            db.SaveChanges();
            //return View(model);
            return RedirectToAction("Index");

        }//edit


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
