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
                   

                    //res.TreatmentName = reservation.Treatment.TreatmentName;
                    //res.Customer_id = reservation.Customer_id;
                    //res.FirstName = reservation.Customer1.FirstName;
                    //res.LastName = reservation.Customer1.LastName;
                    //res.Identity = reservation.Customer1.Identity;
                    //res.Email = reservation.Customer1.Email;
                    //res.Notes = reservation.Customer1.Notes;

                    //res.Phone_id = reservation.Phone_id;
                    //res.PhoneNum_1 = reservation.Phone1.PhoneNum_1;
                    //res.Post_id = reservation.Post_id;
                    //res.PostOffice = reservation.PostOffices1.PostalCode;
                    //res.PostOffice = reservation.PostOffices1.PostOffice;

                    res.User_id = reservation.User_id;
                    res.UserIdentity = reservation.User.UserIdentity;

                    model.Add(res);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        
        //var reservation = db.Reservation.Include(r => r.Customer).Include(r => r.Personnel).Include(r => r.Phone1).Include(r => r.PostOffices).Include(r => r.Treatment).Include(r => r.TreatmentOffice).Include(r => r.TreatmentPlace).Include(r => r.User).Include(r => r.Studentx);
        //return View(reservation.ToList());
    }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
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

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reservation_id,TreatmentName,Start,End,Date,Type,Note,Personnel_id,Phone_id,Post_id,Customer_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", reservation.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", reservation.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", reservation.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", reservation.Post_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", reservation.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", reservation.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", reservation.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", reservation.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", reservation.Student_id);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", reservation.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", reservation.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", reservation.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", reservation.Post_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", reservation.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", reservation.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", reservation.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", reservation.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", reservation.Student_id);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Reservation_id,TreatmentName,Start,End,Date,Type,Note,Personnel_id,Phone_id,Post_id,Customer_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", reservation.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", reservation.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", reservation.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", reservation.Post_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", reservation.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", reservation.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", reservation.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", reservation.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", reservation.Student_id);
            return View(reservation);
        }

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


        
    

    




   

