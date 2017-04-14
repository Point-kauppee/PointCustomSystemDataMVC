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
    public class PostOfficesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: PostOffices
        public ActionResult Index()
        {
            List<PostOffices> model = new List<PostOffices>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<PostOffices> postoff = entities.PostOffices.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
           
                foreach (PostOffices post in postoff)
                {
                    PostOffices view = new PostOffices();
                    view.Post_id = post.Post_id;
                    view.PostalCode = post.PostalCode;
                    view.PostOffice = post.PostOffice;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index




        // GET: PostOffices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOffices postOffices = db.PostOffices.Find(id);
            if (postOffices == null)
            {
                return HttpNotFound();
            }
            return View(postOffices);
        }

        // GET: PostOffices/Create
        public ActionResult Create()
        {
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            return View();
        }

        // POST: PostOffices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Post_id,PostalCode,PostOffice,Personnel_id,Phone_id,Customer_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] PostOffices postOffices)
        {
            if (ModelState.IsValid)
            {
                db.PostOffices.Add(postOffices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", postOffices.Personnel_id);        
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", postOffices.Student_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", postOffices.TreatmentOffice_id);
        
            return View(postOffices);
        }

        // GET: PostOffices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOffices postOffices = db.PostOffices.Find(id);
            if (postOffices == null)
            {
                return HttpNotFound();
            }
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", postOffices.Personnel_id);
       
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);

            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", postOffices.Student_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", postOffices.TreatmentOffice_id);
     
            return View(postOffices);
        }

        // POST: PostOffices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Post_id,PostalCode,PostOffice,Personnel_id,Phone_id,Customer_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] PostOffices postOffices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postOffices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", postOffices.Personnel_id);
         
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", postOffices.Post_id);

            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", postOffices.Student_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", postOffices.TreatmentOffice_id);
       
            return View(postOffices);
        }

        // GET: PostOffices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOffices postOffices = db.PostOffices.Find(id);
            if (postOffices == null)
            {
                return HttpNotFound();
            }
            return View(postOffices);
        }

        // POST: PostOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostOffices postOffices = db.PostOffices.Find(id);
            db.PostOffices.Remove(postOffices);
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
