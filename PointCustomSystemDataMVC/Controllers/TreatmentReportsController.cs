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
                    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

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
            List<TreatmentReportsViewModel> model = new List<TreatmentReportsViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<TreatmentReport> treatrepos = entities.TreatmentReport.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");

                foreach (TreatmentReport treatrepdetail in treatrepos)
                {
                    TreatmentReportsViewModel view = new TreatmentReportsViewModel();

                    view.TreatmentReport_id = treatrepdetail.TreatmentReport_id;
                    view.TreatmentReportName = treatrepdetail.TreatmentReportName;
                    view.TreatmentReportText = treatrepdetail.TreatmentReportText;
                    view.TreatmentDate = treatrepdetail.TreatmentDate.Value;
                    view.TreatmentTime = treatrepdetail.TreatmentTime.Value;

                    view.Customer_id = treatrepdetail.Customer?.Customer_id;
                    view.FirstNameA = treatrepdetail.Customer?.FirstName;
                    view.LastNameA = treatrepdetail.Customer?.LastName;

                    view.User_id = treatrepdetail.User?.User_id;
                    view.UserIdentity = treatrepdetail.User?.UserIdentity;

                    model.Add(view);
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

            Customer cus = new Customer();
            cus.FirstName = model.FirstNameA;
            cus.LastName = model.LastNameA;

            db.Customer.Add(cus);

            User usr = new User();
            usr.UserIdentity = model.UserIdentity;
        
            db.User.Add(usr);

            TreatmentReport tre = new TreatmentReport();
            tre.TreatmentReportName = model.TreatmentReportName;
            tre.TreatmentReportText= model.TreatmentReportText;
            tre.TreatmentDate = model.TreatmentDate;
            tre.TreatmentTime = model.TreatmentTime;
            tre.User = usr;

            db.TreatmentReport.Add(tre);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
            return View(view);

        }//edit

        // POST: TreatmentReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TreatmentReportsViewModel model)
        {
            Customer cus = db.Customer.Find(model.Customer_id);

            cus.FirstName = model.FirstNameA;
            cus.LastName = model.LastNameA;


            if (cus.User == null)
            {
                User usr = new User();
                usr.UserIdentity = model.UserIdentity;
                usr.Password = "joku@joku.fi";
                usr.Customer = cus;

                db.User.Add(usr);
            }
            else
            {
                User user = cus.User.FirstOrDefault();
                if (user != null)
                {
                    user.UserIdentity = model.UserIdentity;
                }
            }

            if (cus.TreatmentReport == null)
            {
                TreatmentReport tre = new TreatmentReport();
                tre.TreatmentReportName = model.TreatmentReportName;
                tre.TreatmentReportText = model.TreatmentReportText;
                tre.TreatmentDate = model.TreatmentDate;
                tre.TreatmentTime = model.TreatmentTime;
                tre.Customer = cus;

                db.TreatmentReport.Add(tre);
            }
            else
            {
                TreatmentReport tre = cus.TreatmentReport.FirstOrDefault();
                if (tre != null)
                {
                    tre.TreatmentReportName = model.TreatmentReportName;
                    tre.TreatmentReportText = model.TreatmentReportText;
                    tre.TreatmentDate = model.TreatmentDate;
                    tre.TreatmentTime = model.TreatmentTime;
                }
            }
          
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
