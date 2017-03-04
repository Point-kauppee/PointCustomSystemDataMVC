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

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Reservation reservation in reservations)
                {
                    ReservationViewModel res = new ReservationViewModel();
                    res.Reservation_id = reservation.Reservation_id;
                    res.TreatmentName = reservation.TreatmentName;
                    res.Start = reservation.Start.Value;
                    res.End = reservation.End.Value;
                    res.Date = reservation.Date.Value;

                    res.Treatment_id = reservation.Treatment?.Treatment_id;
                    res.TreatmentName = reservation.Treatment?.TreatmentName;
                
                    res.Customer_id = reservation.Customer?.Customer_id;
                    res.FirstName = reservation.Customer?.FirstName;
                    res.LastName = reservation.Customer?.LastName;

                    res.Student_id = reservation.Studentx?.Student_id;
                    res.FirstName = reservation.Studentx?.FirstName;
                    res.LastName = reservation.Studentx?.LastName;

                    res.Treatmentplace_id = reservation.TreatmentPlace?.Treatmentplace_id;
                    res.TreatmentPlaceName = reservation.TreatmentPlace?.TreatmentPlaceName;
                    res.TreatmentPlaceNumber = reservation.TreatmentPlace?.TreatmentPlaceNumber;

                    res.User_id = reservation.User?.User_id;
                    res.UserIdentity = reservation.User?.UserIdentity;

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
            List<ReservationViewModel> model = new List<ReservationViewModel>();

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

                    res.Treatment_id = resdetail.Treatment?.Treatment_id;
                    res.TreatmentName = resdetail.Treatment?.TreatmentName;
                    res.TreatmentTime = resdetail.Treatment?.TreatmentTime;

                    res.Treatmentplace_id = resdetail.TreatmentPlace?.Treatmentplace_id;
                    res.TreatmentPlaceName = resdetail.TreatmentPlace?.TreatmentPlaceName;
                    res.TreatmentPlaceNumber = resdetail.TreatmentPlace?.TreatmentPlaceNumber;

                    res.Customer_id = resdetail.Customer_id;
                    res.FirstName = resdetail.Customer?.FirstName;
                    res.LastName = resdetail.Customer?.LastName;

                    res.Student_id = resdetail.Student_id;
                    res.FirstName = resdetail.Studentx?.FirstName;
                    res.LastName = resdetail.Studentx?.LastName;

                    res.User_id = resdetail.User?.User_id;
                    res.UserIdentity = resdetail.User.UserIdentity;

                    model.Add(res);

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

            Customer cus = new Customer();
            cus.FirstName = model.FirstName;
            cus.LastName = model.LastName;
            cus.Notes = model.Notes;
          
            db.Customer.Add(cus);

            User usr = new User();
            usr.UserIdentity = model.UserIdentity;
            usr.Password = "Customer";
            usr.Customer = cus;

            db.User.Add(usr);

            Reservation res = new Reservation();
            res.Start = model.Start;
            res.End = model.End;
            res.Date = model.Date;
            res.Note = model.Note;
            res.Customer = cus;
            
            db.Reservation.Add(res);

            Studentx stu = new Studentx();
            stu.FirstName = model.FirstName;
            stu.LastName = model.LastName;
            stu.Notes = model.Notes;
            stu.Customer = cus;

            db.Studentx.Add(stu);

            Treatment tre = new Treatment();       
            tre.TreatmentName = model.TreatmentName;
            tre.Customer = cus;

            db.Treatment.Add(tre);

            TreatmentPlace trp = new TreatmentPlace();
            trp.TreatmentPlaceName = model.TreatmentPlaceName;
            trp.TreatmentPlaceNumber = model.TreatmentPlaceNumber;
            trp.Customer = cus;

            db.TreatmentPlace.Add(trp);

            try { db.SaveChanges();
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

            res.Customer_id = resdetail.Customer_id;
            res.FirstName = resdetail.Customer?.FirstName;
            res.LastName = resdetail.Customer?.LastName;

            res.Student_id = resdetail.Student_id;
            res.FirstName = resdetail.Studentx.FirstName;
            res.LastName = resdetail.Studentx?.LastName;

            res.User_id = resdetail.User?.User_id;
            res.UserIdentity = resdetail.User.UserIdentity;

            res.Treatment_id = resdetail.Treatment?.Treatment_id;
            res.TreatmentName = resdetail.Treatment?.TreatmentName;
            res.TreatmentTime = resdetail.Treatment?.TreatmentTime;

            res.Treatmentplace_id = resdetail.TreatmentPlace?.Treatmentplace_id;
            res.TreatmentPlaceName = resdetail.TreatmentPlace?.TreatmentPlaceName;
            res.TreatmentPlaceNumber = resdetail.TreatmentPlace?.TreatmentPlaceNumber;

            return View(res);

        }//edit

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservationViewModel model)
        {
            Customer cus = db.Customer.Find(model.Customer_id);
           
            cus.FirstName = model.FirstName;
            cus.LastName = model.LastName;
            cus.Notes = model.Notes;
       
            if (cus.Reservation == null)
            {
                CultureInfo fiFi = new CultureInfo("fi-FI");

                Reservation res = new Reservation();
                res.Start = model.Start;
                res.End = model.End;
                res.Date = model.Date;
                res.Note = model.Note;
                res.Customer = cus;

                db.Reservation.Add(res);
            }
            else
            {
                cus.Reservation.FirstOrDefault().Start = model.Start;
                cus.Reservation.FirstOrDefault().End = model.End;
                cus.Reservation.FirstOrDefault().Date = model.Date;
                cus.Reservation.FirstOrDefault().Note = model.Note;
            }

            if (cus.Studentx == null)
            {
                Studentx stu = new Studentx();
                stu.FirstName = model.FirstName;
                stu.LastName = model.LastName;
                stu.Customer = cus;

                db.Studentx.Add(stu);
            }
            else
            {
                Studentx st = cus.Studentx.FirstOrDefault();

                if (st != null)
                {
                    st.FirstName = model.FirstName;
                    st.LastName = model.LastName;
                }
            }

            if (cus.User == null)
            {
                User usr = new User();             
                usr.UserIdentity = model.UserIdentity;
                usr.Password = "Customer";
                usr.Customer = cus;

                db.User.Add(usr);
            }
            else
            {
                Studentx st = cus.Studentx.FirstOrDefault();

                if (st != null)
                {
                    st.FirstName = model.FirstName;
                    st.LastName = model.LastName;
                }
            }

            if (cus.Treatment == null)
             {
                 Treatment tre = new Treatment();
                    tre.TreatmentName = model.TreatmentName;

                    db.Treatment.Add(tre);
             }
             else
             {
                 Treatment tr = cus.Treatment.FirstOrDefault();

                 if (tr != null){
                     tr.TreatmentName = model.TreatmentName;                   
                 }
             }
           
            if (cus.TreatmentPlace == null)
            {
                TreatmentPlace trp = new TreatmentPlace();
                trp.TreatmentPlaceName = model.TreatmentPlaceName;
                trp.TreatmentPlaceNumber = model.TreatmentPlaceNumber;

                db.TreatmentPlace.Add(trp);
            }
            else
            {
                TreatmentPlace tp = cus.TreatmentPlace.FirstOrDefault();

                if (tp != null)
                {
                    tp.TreatmentPlaceName = model.TreatmentPlaceName;
                    tp.TreatmentPlaceNumber = model.TreatmentPlaceNumber;
                }
            }

            db.SaveChanges();
              return View(model);

        }//edit

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
            return View(reservation);
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
        }


        //DAYPILOT VIIKKO VIIKKONÄKYMÄ
        public ActionResult BackEnd()
        {
            return new Dpc().CallBack(this);
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
                DataTextField = "TreatmentName";
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
                DataTextField = "TreatmentName";
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
        }
    }


        
    

    




   

