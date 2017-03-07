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

                    view.User_id = treatreport.User?.User_id;
                    view.UserIdentity = treatreport.User?.UserIdentity;
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

            User usr = db.User.Find(model.User_id);

            usr.UserIdentity = model.UserIdentity;

       

            if (usr.TreatmentReport == null)
            {
                TreatmentReport tre = new TreatmentReport();
                tre.TreatmentReportName = model.TreatmentReportName;
                tre.TreatmentReportText = model.TreatmentReportText;
                tre.TreatmentDate = model.TreatmentDate;
                tre.TreatmentTime = model.TreatmentTime;
                tre.User = usr;

                db.TreatmentReport.Add(tre);
            }
            else
            {
                usr.TreatmentReport.FirstOrDefault().TreatmentReportName = model.TreatmentReportName;
                usr.TreatmentReport.FirstOrDefault().TreatmentReportText = model.TreatmentReportText;
                usr.TreatmentReport.FirstOrDefault().TreatmentDate = model.TreatmentDate;
                usr.TreatmentReport.FirstOrDefault().TreatmentTime = model.TreatmentTime;
            }

            db.SaveChanges();
            return View(model);

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
