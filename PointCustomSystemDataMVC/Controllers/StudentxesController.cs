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
    public class StudentxesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Studentxes
        public ActionResult Index()
        {
            var studentx = db.Studentx.Include(s => s.Phone1).Include(s => s.PostOffices1).Include(s => s.User);
            return View(studentx.ToList());
        }

        // GET: Studentxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentx studentx = db.Studentx.Find(id);
            if (studentx == null)
            {
                return HttpNotFound();
            }
            return View(studentx);
        }

        // GET: Studentxes/Create
        public ActionResult Create()
        {
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            return View();
        }

        // POST: Studentxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Student_id,FirstName,LastName,Identity,Notes,Email,EnrollmentDateIN,EnrollmentDateOUT,Phone_id,Post_id,User_id")] Studentx studentx)
        {
            if (ModelState.IsValid)
            {
                db.Studentx.Add(studentx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", studentx.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", studentx.Post_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", studentx.User_id);
            return View(studentx);
        }

        // GET: Studentxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentx studentx = db.Studentx.Find(id);
            if (studentx == null)
            {
                return HttpNotFound();
            }
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", studentx.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", studentx.Post_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", studentx.User_id);
            return View(studentx);
        }

        // POST: Studentxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Student_id,FirstName,LastName,Identity,Notes,Email,EnrollmentDateIN,EnrollmentDateOUT,Phone_id,Post_id,User_id")] Studentx studentx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", studentx.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", studentx.Post_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", studentx.User_id);
            return View(studentx);
        }

        // GET: Studentxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentx studentx = db.Studentx.Find(id);
            if (studentx == null)
            {
                return HttpNotFound();
            }
            return View(studentx);
        }

        // POST: Studentxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Studentx studentx = db.Studentx.Find(id);
            db.Studentx.Remove(studentx);
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
