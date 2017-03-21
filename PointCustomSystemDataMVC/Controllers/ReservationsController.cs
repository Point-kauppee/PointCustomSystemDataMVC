using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointCustomSystemDataMVC.Models;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using DayPilot.Web.Mvc.Enums.Calendar;
using DayPilot.Web.Mvc.Utils;
using System.Globalization;
using DayPilot.Web.Mvc.Events.Month;
using DayPilot.Web.Mvc.Json;
using PointCustomSystemDataMVC.ViewModels;
using PointCustomSystemDataMVC.Utilities;

namespace PointCustomSystemDataMVC.Controllers
{
    public class ReservationsController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Reservations
        public ActionResult Index()
        {
            List<ReservationViewModel> model = new List<ReservationViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Reservation> reservations = entities.Reservation.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                foreach (Reservation reservation in reservations)
                {
                    CultureInfo fiFi = new CultureInfo("fi-FI");

                    ReservationViewModel res = new ReservationViewModel();

                    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    res.User_id = reservation.User?.User_id;
                    res.UserIdentity = reservation.User?.UserIdentity;
                
                    res.Customer_id = reservation.Customer?.Customer_id;
                    res.FirstNameA = reservation.Customer?.FirstName;
                    res.LastNameA = reservation.Customer?.LastName;
                    //res.Notes = reservation.Customer?.Notes;

                    res.Reservation_id = reservation.Reservation_id;
                    res.TreatmentName = reservation.TreatmentName;
                    res.Start = reservation.Start.GetValueOrDefault();
                    res.End = reservation.End.GetValueOrDefault();
                    res.Date = reservation.Date.GetValueOrDefault();
                    res.Note = reservation.Note;

                    res.Treatment_id = reservation.Treatment?.Treatment_id;
                    res.TreatmentName = reservation.Treatment?.TreatmentName;
                    res.TreatmentPrice = reservation.Treatment?.TreatmentPrice;

                    res.Treatmentplace_id = reservation.TreatmentPlace?.Treatmentplace_id;
                    res.TreatmentPlaceName = reservation.TreatmentPlace?.TreatmentPlaceName;
                    res.TreatmentPlaceNumber = reservation.TreatmentPlace?.TreatmentPlaceNumber;

                    res.Student_id = reservation.Studentx?.Student_id;
                    res.FirstNameH = reservation.Studentx?.FirstName;
                    res.LastNameH = reservation.Studentx?.LastName;

                    model.Add(res);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);

        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            ReservationViewModel model = new ReservationViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Reservation> reservations = entities.Reservation.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Reservation resdetail in reservations)
                {

                    ReservationViewModel res = new ReservationViewModel();
                    res.Reservation_id = resdetail.Reservation_id;
                    res.Start = resdetail.Start.Value;
                    res.End = resdetail.End.Value;
                    res.Date = resdetail.Date.Value;
                    res.Note = resdetail.Note;

                    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    res.User_id = resdetail.User?.User_id;
                    res.UserIdentity = resdetail.User?.UserIdentity;

                    res.Customer_id = resdetail.Customer?.Customer_id;
                    res.FirstNameA = resdetail.Customer?.FirstName;
                    res.LastNameA = resdetail.Customer?.LastName;
                    res.Notes = resdetail.Customer?.Notes;

                    res.Treatment_id = resdetail.Treatment?.Treatment_id;
                    res.TreatmentName = resdetail.Treatment?.TreatmentName;
                    res.TreatmentTime = resdetail.Treatment?.TreatmentTime;

                    res.Treatmentplace_id = resdetail.TreatmentPlace?.Treatmentplace_id;
                    res.TreatmentPlaceName = resdetail.TreatmentPlace?.TreatmentPlaceName;
                    res.TreatmentPlaceNumber = resdetail.TreatmentPlace?.TreatmentPlaceNumber;

                    ViewBag.FullNameH = new SelectList((from s in db.Studentx select new { Student_id = s.Student_id, FullNameH = s.FirstName + " " + s.LastName }), "Student_id", "FullNameH", null);
                    res.Student_id = resdetail.Studentx?.Student_id;
                    res.FirstNameH = resdetail.Studentx?.FirstName;
                    res.LastNameH = resdetail.Studentx?.LastName;

                    model = res;
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation reservation = db.Reservation.Find(id);
                if (reservation == null)
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


        // GET: Reservations/Create
        public ActionResult Create()

        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            ReservationViewModel model = new ReservationViewModel();

            //ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            ViewBag.FullNameH = new SelectList((from s in db.Studentx select new { Student_id = s.Student_id, FullNameH = s.FirstName + " " + s.LastName }), "Student_id", "FullNameH", null);

            //ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentName = new SelectList((from t in db.Treatment select new { Treatment_id = t.Treatment_id, TreatmentName = t.TreatmentName }), "Treatment_id", "TreatmentName", null);

            //ViewBag.Treatmentplace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.TreatmentPlaceName = new SelectList((from tp in db.TreatmentPlace select new { Treatmentplace_id = tp.Treatmentplace_id, TreatmentPlaceName = tp.TreatmentPlaceName }), "Treatmentplace_id", "TreatmentPlaceName", null);

            return View(model);
        }//create

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            CultureInfo fiFi = new CultureInfo("fi-FI");

            Reservation res = new Reservation();
            res.Start = model.Start;
            res.End = model.End;
            res.Date = model.Date;
            res.Note = model.Note;
            db.Reservation.Add(res);

              
            //res.Customer_id = model.Customer_id;
            //Customer cus = new Customer();
            //cus.Notes = model.Notes;

            // etsitään User-rivi kannasta valitun nimen perusteella
            int userId = int.Parse(model.UserIdentity);
            if (userId > 0)
            {
                User usr = db.User.Find(userId);
                res.User_id = userId;
                res.Customer_id = usr.Customer_id;
            }

            // etsitään Treatment-rivi kannasta valitun nimen perusteella
            int treatmentId = int.Parse(model.TreatmentName);
            if (treatmentId > 0)
            {
                Treatment trtm = db.Treatment.Find(treatmentId);
                res.Treatment_id = trtm.Treatment_id;
            }

            // etsitään TreatmentPlace-rivi kannasta valitun nimen perusteella
            int treatmentPlaceId = int.Parse(model.TreatmentPlaceName);
            if (treatmentPlaceId > 0)
            {
                TreatmentPlace tp = db.TreatmentPlace.Find(treatmentPlaceId);
                res.TreatmentPlace_id = tp.Treatmentplace_id;
            }

            // etsitään Student-rivi kannasta valitun nimen perusteella
            int studentId = int.Parse(model.FullNameH2);
            if (studentId > 0)
            {
                Studentx stu = db.Studentx.Find(studentId);
                res.Student_id = stu.Student_id;
            }

            //ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FullNameH");
            ViewBag.FullNameH = new SelectList((from s in db.Studentx select new { Student_id = s.Student_id, FullNameH = s.FirstName + " " + s.LastName }), "Student_id", "FullNameH", null);
            //ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentName = new SelectList((from t in db.Treatment select new { Treatment_id = t.Treatment_id, TreatmentName = t.TreatmentName }), "Treatment_id", "TreatmentName", null);
            //ViewBag.Treatmentplace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.TreatmentPlaceName = new SelectList((from tp in db.TreatmentPlace select new { Treatmentplace_id = tp.Treatmentplace_id, TreatmentPlaceName = tp.TreatmentPlaceName }), "Treatmentplace_id", "TreatmentPlaceName", null);


            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");

        }//cr*/;


        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            CultureInfo fiFi = new CultureInfo("fi-FI");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation resdetail = db.Reservation.Find(id);
            if (resdetail == null)
            {
                return HttpNotFound();
            }

            ReservationViewModel res = new ReservationViewModel();
            res.Reservation_id = resdetail.Reservation_id;
            res.Start = resdetail.Start.Value;
            res.End = resdetail.End.Value;
            res.Date = resdetail.Date.Value;

            res.Customer_id = resdetail.Customer?.Customer_id;
            res.FirstNameA = resdetail.Customer?.FirstName;
            res.LastNameA = resdetail.Customer?.LastName;
            res.Notes = resdetail.Customer?.Notes;

            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            res.User_id = resdetail.User?.User_id;
            res.UserIdentity = resdetail.User?.UserIdentity;

            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            ViewBag.FullNameH = new SelectList((from s in db.Studentx select new { Student_id = s.Student_id, FullNameH = s.FirstName + " " + s.LastName }), "Student_id", "FullNameH", null);
            res.Student_id = resdetail.Studentx?.Student_id;
            res.FirstNameH = resdetail.Studentx?.FirstName;
            res.LastNameH = resdetail.Studentx?.LastName;

            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentName = new SelectList((from t in db.Treatment select new { Treatment_id = t.Treatment_id, TreatmentName = t.TreatmentName }), "Treatment_id", "TreatmentName", null);
            res.Treatment_id = resdetail.Treatment?.Treatment_id;
            res.TreatmentName = resdetail.Treatment?.TreatmentName;

            ViewBag.Treatmentplace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.TreatmentPlaceName = new SelectList((from tp in db.TreatmentPlace select new { Treatmentplace_id = tp.Treatmentplace_id, TreatmentPlaceName = tp.TreatmentPlaceName }), "Treatmentplace_id", "TreatmentPlaceName", null);
            res.Treatmentplace_id = resdetail.TreatmentPlace?.Treatmentplace_id;
            res.TreatmentPlaceName = resdetail.TreatmentPlace?.TreatmentPlaceName;

            return View(res);

        }//edit

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservationViewModel model)
        {

            CultureInfo fiFi = new CultureInfo("fi-FI");

            Reservation res = db.Reservation.Find(model.Reservation_id);

            res.Start = model.Start;
            res.End = model.End;
            res.Date = model.Date;
            res.Note = model.Note;

            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentName = new SelectList((from t in db.Treatment select new { Treatment_id = t.Treatment_id, TreatmentName = t.TreatmentName }), "Treatment_id", "TreatmentName", null);
            res.Treatment_id = model.Treatment_id;

            ViewBag.Treatmentplace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.TreatmentPlaceName = new SelectList((from tp in db.TreatmentPlace select new { Treatmentplace_id = tp.Treatmentplace_id, TreatmentPlaceName = tp.TreatmentPlaceName }), "Treatmentplace_id", "TreatmentPlaceName", null);
            res.TreatmentPlace_id = model.Treatmentplace_id;

            res.Customer_id = model.Customer_id;
            res.Student_id = model.Student_id;
            res.User_id = model.User_id;

            db.SaveChanges();

            return RedirectToAction("Index");

        }//edit

        //Customer cus = db.Customer.Find(model.Customer_id);

        //cus.FirstName = model.FirstNameA;
        //cus.LastName = model.LastNameA;
        //cus.Notes = model.Notes;


        //ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

        //if (cus.User == null)
        //{
        //    User usr = new User();
        //    usr.UserIdentity = model.UserIdentity;
        //    usr.Password = "joku@joku.fi";
        //    usr.Customer = cus;

        //    db.User.Add(usr);
        //}
        //else
        //{
        //    User user = cus.User.FirstOrDefault();
        //    if (user != null)
        //    {
        //        user.UserIdentity = model.UserIdentity;

        //    }
        //}

        //ViewBag.TreatmentName = new SelectList((from t in db.Treatment select new { Treatment_id = t.Treatment_id, TreatmentName = t.TreatmentName }), "Treatment_id", "TreatmentName", null);

        //ViewBag.TreatmentPlaceName = new SelectList((from tp in db.TreatmentPlace select new { Treatmentplace_id = tp.Treatmentplace_id, TreatmentPlaceName = tp.TreatmentPlaceName }), "Treatmentplace_id", "TreatmentPlaceName", null);
        ////Reservation res = db.Reservation.Find(model.Reservation_id);
        //if (cus.Reservation == null)
        //{
        //    Reservation res = new Reservation();
        //    res.Start = model.Start;
        //    res.End = model.End;
        //    res.Date = model.Date;
        //    res.Note = model.Note;
        //    res.Treatment_id = model.Treatment_id;
        //    res.TreatmentPlace_id = model.Treatmentplace_id;
        //    res.Student_id = model.Student_id;
        //    res.Customer = cus;

        //    db.Reservation.Add(res);
        //}
        //else
        //{
        //    Reservation res = cus.Reservation.FirstOrDefault();
        //    if (res != null)
        //    {
        //        res.Start = model.Start;
        //        res.End = model.End;
        //        res.Date = model.Date;
        //        res.Note = model.Note;
        //        res.Treatment_id = model.Treatment_id;
        //        res.TreatmentPlace_id = model.Treatmentplace_id;
        //        res.Student_id = model.Student_id;
        //    }
        //}

        //if (res.Treatment == null)
        //{
        //    Treatment tre = new Treatment();
        //    tre.TreatmentName = model.TreatmentName;
        //    tre.TreatmentTime = model.TreatmentTime;

        //    db.Treatment.Add(tre);
        //}
        //else
        //{
        //    Treatment tr = res.Treatment.FirstOrDefault();

        //    if (tr != null)
        //    {
        //        tr.TreatmentName = model.TreatmentName;
        //    }
        //}

        //if (res.TreatmentPlace == null)
        //{
        //    TreatmentPlace trp = new TreatmentPlace();
        //    trp.TreatmentPlaceName = model.TreatmentPlaceName;
        //    trp.TreatmentPlaceNumber = model.TreatmentPlaceNumber;

        //    db.TreatmentPlace.Add(trp);
        //}

        //else

        //{
        //    TreatmentPlace tp = res.TreatmentPlace.FirstOrDefault();

        //    if (tp != null)
        //    {
        //        tp.TreatmentPlaceName = model.TreatmentPlaceName;
        //        tp.TreatmentPlaceNumber = model.TreatmentPlaceNumber;
        //    }
        //}



        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            ReservationViewModel res = new ReservationViewModel();

            res.Reservation_id = reservation.Reservation_id;
            res.Start = reservation.Start.Value;
            res.End = reservation.End.Value;
            res.Date = reservation.Date.Value;

            res.User_id = reservation.User?.User_id;
            res.UserIdentity = reservation.User?.UserIdentity;

            res.Customer_id = reservation.Customer?.Customer_id;
            res.FirstNameA = reservation.Customer?.FirstName;
            res.LastNameA = reservation.Customer?.LastName;
            res.Notes = reservation.Customer?.Notes;

            res.Student_id = reservation.Studentx?.Student_id;
            res.FirstNameH = reservation.Studentx?.FirstName;
            res.LastNameH = reservation.Studentx?.LastName;

            res.Treatment_id = reservation.Treatment?.Treatment_id;
            res.TreatmentName = reservation.Treatment?.TreatmentName;

            res.Treatmentplace_id = reservation.TreatmentPlace?.Treatmentplace_id;
            res.TreatmentPlaceName = reservation.TreatmentPlace?.TreatmentPlaceName;

            return View(res);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
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
        }//dispose


        //DAYPILOT VIIKKO VIIKKONÄKYMÄ
        public ActionResult BackEnd()
        {
            var data = new Dpc().CallBack(this);
            return data;
        }

        //class Dpc : DayPilotWeek
        class Dpc : DayPilotCalendar
        {
            public string DataDateField { get; set; }

            CultureInfo fiFi = new CultureInfo("fi-FI");
            //protected override void OnInit(InitArgs e)
            //{
            //    Update(CallBackUpdateType.Full);
            //}
            protected override void OnInit(DayPilot.Web.Mvc.Events.Calendar.InitArgs e)
            {
                JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();
                Events = from ev in db.Reservation select ev;

                DataIdField = "Reservation_id";
                DataTextField = "CalendarTitle";
                DataDateField = "Date";
                DataStartField = "Start";
                DataEndField = "End";


                Update();
            }

            protected override void OnCommand(DayPilot.Web.Mvc.Events.Calendar.CommandArgs e)
            {
                switch (e.Command)
                {
                    case "previous":
                        StartDate = StartDate.AddDays(-7);
                        Update(CallBackUpdateType.Full);
                        break;
                    case "next":
                        StartDate = StartDate.AddDays(7);
                        Update(CallBackUpdateType.Full);
                        break;
                    case "refresh":
                        Update();
                        break;
                }
                //base.OnCommand(e);
            }
            protected override void OnEventResize(DayPilot.Web.Mvc.Events.Calendar.EventResizeArgs e)
            {
                JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();
                var eid = Convert.ToInt32(e.Id);
                var toBeResized = (from ev in db.Reservation where ev.Reservation_id == eid select ev).First();
                toBeResized.Start = e.NewStart;
                toBeResized.End = e.NewEnd;

                db.SaveChanges();
                Update();
            }

            protected override void OnEventMove(DayPilot.Web.Mvc.Events.Calendar.EventMoveArgs e)
            {
                JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

                var eid = Convert.ToInt32(e.Id);
                var toBeResized = (from ev in db.Reservation where ev.Reservation_id == eid select ev).First();
                toBeResized.Start = e.NewStart;
                toBeResized.End = e.NewEnd;
                db.SaveChanges();
                Update();
            }

            //protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            //{
            //    string name = (string)e.Data["name"];
            //    if (String.IsNullOrEmpty(name))
            //    {
            //        name = "(default)";
            //    }
            //    new EventManager(Controller).EventCreate(e.Start, e.End, name);
            //    Update();
            //}

            //protected override void OnEventDelete(EventDeleteArgs e)
            //{
            //    kauppeedbEntities db = new kauppeedbEntities();
            //    var item = (from ev in db.Varaus where ev.Varaus_id == Convert.ToInt32(e.Id) select ev).First();
            //    db.Varaus.Remove(item);
            //    db.SaveChanges();
            //    Update();
            //}

            protected override void OnTimeRangeSelected(DayPilot.Web.Mvc.Events.Calendar.TimeRangeSelectedArgs e)
            {

                JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();
                //var edata = (string)e.Data["name"];
                //var edata = Convert.ToString(e.Data["name"]);
                //var toBeCreated = new Varaus { Alku = Convert.ToString(e.Start), Loppu = Convert.ToString(e.End), Palvelun_nimi = (string)e.Data["name"] };
                //var toBeCreated = new Varaus { Alku = Convert.ToString(e.Start), Loppu = Convert.ToString(e.End), Palvelun_nimi = (string)e.Data["name"] };
                var toBeCreated = new Reservation { Start = e.Start, End = e.End, TreatmentName = (string)e.Data["name"] };
                //var toBeCreated = new Varaus { Alku = e.Start, Loppu = e.End, Palvelun_nimi = Convert.ToString(e.Data["name"]) };
                //var toBeCreated = new Varaus { Alku = e.Start, Loppu = e.End, Palvelun_nimi = "Nettivaraus + [e.name]" };
                //var toBeCreated = new Varaus { Alku = e.Start, Loppu = e.End, Palvelun_nimi = edata};


                db.Reservation.Add(toBeCreated);
                db.SaveChanges();

                Update();
            }


            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }
                CultureInfo fiFi = new CultureInfo("fi-FI");
                JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();               //Events = new EventManager(Controller).Data.AsEnumerable();
                Events = from ev in db.Reservation select ev;

                DataIdField = "Reservation_id";
                DataTextField = "CalendarTitle";
                DataDateField = "Date";
                DataStartField = "Start";
                DataEndField = "End";

            }
        }//Dpc


        //DAYPILOT KALENTERI KUUKAUSINÄKYMÄ

        public ActionResult BackMonthEnd()
        {
            return new Dpm().CallBack(this);
        }

        class Dpm : DayPilotMonth
        {
            protected override void OnTimeRangeSelected(DayPilot.Web.Mvc.Events.Month.TimeRangeSelectedArgs e)
            {
                string name = (string)e.Data["name"];
                if (String.IsNullOrEmpty(name))
                {
                    name = "(default)";
                }
                new EventManager(Controller).EventCreate(e.Start, e.End, name);
                Update();
            }

            protected override void OnEventMove(DayPilot.Web.Mvc.Events.Month.EventMoveArgs e)
            {
                if (new EventManager(Controller).Get(e.Id) != null)
                {
                    new EventManager(Controller).EventMove(e.Id, e.NewStart, e.NewEnd);
                }

                Update();
            }

            protected override void OnEventResize(DayPilot.Web.Mvc.Events.Month.EventResizeArgs e)
            {
                new EventManager(Controller).EventMove(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            private int i = 0;
            protected override void OnBeforeEventRender(DayPilot.Web.Mvc.Events.Month.BeforeEventRenderArgs e)
            {
                if (Id == "dp_customization")
                {
                    // alternating color
                    int colorIndex = i % 4;
                    string[] backColors = { "#FFE599", "#9FC5E8", "#B6D7A8", "#EA9999" };
                    string[] borderColors = { "#F1C232", "#3D85C6", "#6AA84F", "#CC0000" };
                    e.BackgroundColor = backColors[colorIndex];
                    e.BorderColor = borderColors[colorIndex];
                    e.FontColor = "#000";
                    i++;
                }
            }

            protected override void OnCommand(DayPilot.Web.Mvc.Events.Month.CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime)e.Data["start"];
                        Update(CallBackUpdateType.Full);
                        break;

                    case "previous":
                        StartDate = StartDate.AddMonths(-1);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "next":
                        StartDate = StartDate.AddMonths(1);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "today":
                        StartDate = DateTime.Today;
                        Update(CallBackUpdateType.Full);
                        break;

                    case "refresh":
                        Update();
                        break;
                }
            }

            protected override void OnInit(DayPilot.Web.Mvc.Events.Month.InitArgs initArgs)
            {
                Update(CallBackUpdateType.Full);
            }

            protected override void OnFinish()
            {
                // only load the data if an update was requested by an Update() call
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                // this select is a really bad example, no where clause
                Events = new EventManager(Controller).Data.AsEnumerable();

                DataIdField = "Reservation_id";
                DataTextField = "TreatmentName";
                //DataDateField = "Date";
                DataStartField = "Start";
                DataEndField = "End";
            }//OnFinish
        }//Dpm

        //Daypilot Calendar:
        public ActionResult Light()
        {
            return RedirectToAction("ThemeTransparent");
        }

        public ActionResult Red()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Green()
        {
            return RedirectToAction("ThemeGreen");
        }

        public ActionResult ThemeGreen()
        {
            return View();
        }

        public ActionResult ThemeWhite()
        {
            return View();
        }

        public ActionResult ThemeTransparent()
        {
            return View();
        }

        public ActionResult ThemeGoogleLike()
        {
            return View();
        }

        public ActionResult ThemeTraditional()
        {
            return View();
        }

        public ActionResult ThemeBlue()
        {
            return View();
        }

        public ActionResult NextPrevious()
        {
            return View();
        }
        public ActionResult JQuery()
        {
            return View();
        }

        public ActionResult EventCreating()
        {
            return View();
        }

        public ActionResult EventMoving()
        {
            return View();
        }

        public ActionResult EventCustomization()
        {
            return View();
        }

        public ActionResult EventResizing()
        {
            return View();
        }

        public ActionResult NoEventHeader()
        {
            return RedirectToAction("Index");
        }

        public ActionResult CellHeight()
        {
            return View();
        }

        public ActionResult Hours24()
        {
            return View();
        }

        public ActionResult Day()
        {
            return RedirectToActionPermanent("ViewDay");
        }

        public ActionResult WorkWeek()
        {
            return RedirectToActionPermanent("ViewWorkWeek");
        }

        public ActionResult Week()
        {
            return RedirectToActionPermanent("ViewWeek");
        }

        public ActionResult ViewDay()
        {
            return View();
        }

        public ActionResult ViewWorkWeek()
        {
            return View();
        }

        public ActionResult ViewWeek()
        {
            return View();
        }

        public ActionResult ViewResources()
        {
            return View();
        }

        public ActionResult ViewDaysResources()
        {
            return View();
        }

        public ActionResult Rtl()
        {
            return View();
        }

        public ActionResult EventsAllDay()
        {
            return View();
        }

        public ActionResult EventActiveAreas()
        {
            return View();
        }

        public ActionResult EventArrangement()
        {
            return View();
        }

        public ActionResult EventContextMenu()
        {
            return View();
        }

        public ActionResult EventSelecting()
        {
            return View();
        }

        public ActionResult ExternalDragDrop()
        {
            return View();
        }

        public ActionResult RecurringEvents()
        {
            return View();
        }

        public ActionResult Crosshair()
        {
            return View();
        }

        public ActionResult Today()
        {
            return View();
        }

        public ActionResult FixedColumnWidth()
        {
            return View();
        }

        public ActionResult AutoRefresh()
        {
            return View();
        }

        public ActionResult Notify()
        {
            return View();
        }

        public ActionResult HeaderAutoFit()
        {
            return View();
        }

        public ActionResult TimeHeaderCellDuration()
        {
            return View();
        }

        public ActionResult DayRange()
        {
            return View();
        }

        public ActionResult AutoHide()
        {
            return View();
        }

        public ActionResult Message()
        {
            return View();
        }

        public ActionResult Height100Pct()
        {
            return View();
        }
    }
}












