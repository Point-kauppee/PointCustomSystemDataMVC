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
    public class PersonnelsController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Personnels
        public ActionResult Index()
        {
            var personnel = db.Personnel.Include(p => p.Personnel2).Include(p => p.Phone).Include(p => p.PostOffices).Include(p => p.Reservation).Include(p => p.Treatment).Include(p => p.TreatmentOffice).Include(p => p.TreatmentPlace).Include(p => p.User).Include(p => p.Studentx);
            return View(personnel.ToList());
        }

        // GET: Personnels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = db.Personnel.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // GET: Personnels/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            List<User> list = db.User.ToList();
            ViewBag.PersonSeed = new SelectList(list, "User_id", "UserIdentity");
            return View();
        }//create


        //ViewBag.Customer_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
        //ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
        //ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
        //ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
        //ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
        //ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
        //ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
        //ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
        //ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
        // return View(); }


        // POST: Personnels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
        JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        List<User> list = db.User.ToList();
        ViewBag.PersonSeed = new SelectList(list, "User_id", "UserIdentity");

        User user = new User();
        user.Identity = model.Identity;
            user.User_id = model.User_id;

        db.User.Add(user);
        db.SaveChanges();

            int latestPersId = user.User_id;

            Personnel pers = new Personnel();
            pers.User_id = latestPersId;
            pers.FirstName = model.FirstName;
            pers.LastName = model.LastName;
            pers.Email = model.Email;
            pers.Notes = model.Notes;

            db.Personnel.Add(pers);
            db.SaveChanges();

            int latestPhoId = user.User_id;

            Phone pho = new Phone();
            pho.User_id = latestPhoId;
            pho.PhoneNum_1 = model.PhoneNum_1;

            db.Phone.Add(pho);
            db.SaveChanges();

            return View(model);
        }//create


    //public ActionResult Create([Bind(Include = "Personnel_id,FirstName,LastName,Identity,Notes,Email,User_id,Customer_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id")] Personnel personnel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        db.Personnel.Add(personnel);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    ViewBag.Customer_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", personnel.Customer_id);
    //    ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", personnel.Phone_id);
    //    ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", personnel.Post_id);
    //    ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", personnel.Reservation_id);
    //    ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", personnel.Treatment_id);
    //    ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", personnel.TreatmentOffice_id);
    //    ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", personnel.TreatmentPlace_id);
    //    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", personnel.User_id);
    //    ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", personnel.Student_id);
    //    return View(personnel);
    //}

    // GET: Personnels/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = db.Personnel.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", personnel.Customer_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", personnel.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", personnel.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", personnel.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", personnel.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", personnel.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", personnel.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", personnel.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", personnel.Student_id);
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Personnel_id,FirstName,LastName,Identity,Notes,Email,User_id,Customer_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personnel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", personnel.Customer_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", personnel.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", personnel.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", personnel.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", personnel.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", personnel.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", personnel.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", personnel.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", personnel.Student_id);
            return View(personnel);
        }

        // GET: Personnels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = db.Personnel.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personnel personnel = db.Personnel.Find(id);
            db.Personnel.Remove(personnel);
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
