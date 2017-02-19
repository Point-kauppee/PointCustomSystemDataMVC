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
    public class TreatmentOfficesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: TreatmentOffices
        public ActionResult Index()
        {
            //JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            //List<TreatmentOffice> treatmentoffice = db.TreatmentOffice.ToList();

            //List<Phone> phone = treatmentoffice.Select(x => new Phone
            //{ TreatmentOffice_id = x.TreatmentOffice_id,
            //    Phone_id = x.Phone_id,
            //    PhoneNum_1 = x.PhoneNum_1
            //}).ToList();

            //return View(treatmentoffice);

            //List<PostOffices> postoffice = treatmentoffice.Select(x => new PostOffices
            //{
            //    TreatmentOffice_id = x.TreatmentOffice_id,
            //    Post_id = x.Post_id,
            //    PostalCode = x.PostalCode,
            //    PostOffice = x.PostOffice

            //}).ToList();

            //return View(postoffice);

            var treatmentOffice = db.TreatmentOffice.Include(t => t.Customer1).Include(t => t.Personnel1).Include(t => t.Phone1).Include(t => t.PostOffices1).Include(t => t.Reservation1).Include(t => t.Studentx1).Include(t => t.Treatment1).Include(t => t.TreatmentPlace).Include(t => t.User);
            return View(treatmentOffice.ToList());
        }
    

        // GET: TreatmentOffices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentOffice treatmentOffice = db.TreatmentOffice.Find(id);
            if (treatmentOffice == null)
            {
                return HttpNotFound();
            }
            return View(treatmentOffice);
        }

        // GET: TreatmentOffices/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            List<TreatmentOffice> treatmentofficelist = db.TreatmentOffice.ToList();
            ViewBag.TreatmentOfficeSeed = new SelectList(treatmentofficelist, "TreatmentOffice_id", "TreatmentOfficeName");
            return View();
            }//create


        //    ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName");
        //    ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
        //    ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
        //    ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
        //    ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
        //    ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
        //    ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
        //    ViewBag.Treatmentplace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
        //    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
        //    return View();
        //}





        // POST: TreatmentOffices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "TreatmentOffice_id,TreatmentOfficeName,Address,Note,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,Customer_id,Treatmentplace_id,User_id,PhoneNum_1,PostalCode,PostOffice,PostOffice1")] TreatmentOffice treatmentOffice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TreatmentOffice.Add(treatmentOffice);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatmentOffice.Customer_id);
        //    ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatmentOffice.Personnel_id);
        //    ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", treatmentOffice.Phone_id);
        //    ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", treatmentOffice.Post_id);
        //    ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", treatmentOffice.Reservation_id);
        //    ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatmentOffice.Student_id);
        //    ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", treatmentOffice.Treatment_id);
        //    ViewBag.Treatmentplace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", treatmentOffice.Treatmentplace_id);
        //    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatmentOffice.User_id);
        //    return View(treatmentOffice);
        //}

        public ActionResult Create(TreatmentOffice model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            List<TreatmentOffice> treatmentofficelist = db.TreatmentOffice.ToList();
            ViewBag.TreatmentOfficeSeed = new SelectList(treatmentofficelist,"TreatmentOffice_id", "TreatmentOfficeName");

            TreatmentOffice treatmentoffice = new TreatmentOffice();
            treatmentoffice.TreatmentOfficeName = model.TreatmentOfficeName;
            treatmentoffice.Address = model.Address;

            db.TreatmentOffice.Add(treatmentoffice);
            db.SaveChanges();

            int latestTreatOffId = treatmentoffice.TreatmentOffice_id;

            Phone pho = new Phone();
            pho.TreatmentOffice_id = latestTreatOffId;
            pho.PhoneNum_1 = model.PhoneNum_1;

            db.Phone.Add(pho);
            db.SaveChanges();

            int latestPostId = treatmentoffice.TreatmentOffice_id;

            PostOffices pos = new PostOffices();
            pos.TreatmentOffice_id = latestPostId;
            pos.PostalCode = model.PostalCode;
            pos.PostOffice = model.PostOffice;
            
            db.PostOffices.Add(pos);
            db.SaveChanges();

            return View(model);
        }//create

        // GET: TreatmentOffices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentOffice treatmentOffice = db.TreatmentOffice.Find(id);
            if (treatmentOffice == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatmentOffice.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatmentOffice.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", treatmentOffice.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", treatmentOffice.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", treatmentOffice.Reservation_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatmentOffice.Student_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", treatmentOffice.Treatment_id);
            ViewBag.Treatmentplace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", treatmentOffice.Treatmentplace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatmentOffice.User_id);
            return View(treatmentOffice);
        }

        // POST: TreatmentOffices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TreatmentOffice_id,TreatmentOfficeName,Address,Note,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,Customer_id,Treatmentplace_id,User_id,PhoneNum_1,PostalCode,PostOffice,PostOffice1")] TreatmentOffice treatmentOffice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatmentOffice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_id = new SelectList(db.Customer, "Customer_id", "FirstName", treatmentOffice.Customer_id);
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", treatmentOffice.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", treatmentOffice.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", treatmentOffice.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", treatmentOffice.Reservation_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", treatmentOffice.Student_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", treatmentOffice.Treatment_id);
            ViewBag.Treatmentplace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", treatmentOffice.Treatmentplace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", treatmentOffice.User_id);
            return View(treatmentOffice);
        }

        // GET: TreatmentOffices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentOffice treatmentOffice = db.TreatmentOffice.Find(id);
            if (treatmentOffice == null)
            {
                return HttpNotFound();
            }
            return View(treatmentOffice);
        }

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
