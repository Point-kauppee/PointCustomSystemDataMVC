using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointCustomSystemDataMVC.Models;
using System.Globalization;
using PointCustomSystemDataMVC.ViewModels;

namespace PointCustomSystemDataMVC.Controllers
{
    public class StudentxesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Studentxes
        public ActionResult Index()
        {
            List<StudentViewModel> model = new List<StudentViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Studentx> students = entities.Studentx.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Studentx student in students)
                {
                    StudentViewModel stu = new StudentViewModel();
                    stu.Student_id = student.Student_id;
                    stu.FirstName = student.FirstName;
                    stu.LastName = student.LastName;
                    stu.Identity = student.Identity;
                    stu.Address = student.Address;
                    stu.Email = student.Email;
                    stu.EnrollmentDateIN = student.EnrollmentDateIN.Value;
                    stu.EnrollmentDateOUT = student.EnrollmentDateOUT.Value;
                    stu.EnrollmentDateOFF = student.EnrollmentDateOFF;
                    stu.Notes = student.Notes;

                    stu.PhoneNum_1 = student.Phone?.FirstOrDefault()?.PhoneNum_1;

                    stu.PostalCode = student.PostOffices?.FirstOrDefault()?.PostalCode;
                    stu.PostOffice = student.PostOffices?.FirstOrDefault()?.PostOffice;

                    stu.User_id = student.User?.FirstOrDefault()?.User_id;
                    stu.UserIdentity = student.User?.FirstOrDefault()?.UserIdentity;

                    model.Add(stu);
                }
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }

            //return View(model);
            //var studentx = db.Studentx.Include(s => s.Phone1).Include(s => s.PostOffices1).Include(s => s.User).Include(s => s.Customer1).Include(s => s.Customer2).Include(s => s.Personnel1).Include(s => s.Reservation1).Include(s => s.TreatmentOffice1).Include(s => s.Treatment1).Include(s => s.TreatmentPlace1);
            //return View(studentx.ToList());
     

        // GET: Studentxes/Details/5
        public ActionResult Details(int? id)
        {
            List<StudentViewModel> model = new List<StudentViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Studentx> students = entities.Studentx.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Studentx studetail in students)
                {

                    StudentViewModel stu = new StudentViewModel();
                    stu.Student_id = studetail.Student_id;
                    stu.FirstName = studetail.FirstName;
                    stu.LastName = studetail.LastName;
                    stu.Identity = studetail.Identity;
                    stu.Address = studetail.Address;
                    stu.Email = studetail.Email;
                    stu.EnrollmentDateIN = studetail.EnrollmentDateIN;
                    stu.EnrollmentDateOUT = studetail.EnrollmentDateOUT;
                    stu.EnrollmentDateOFF = studetail.EnrollmentDateOFF;
                    stu.Notes = studetail.Notes;

                    stu.PhoneNum_1 = studetail.Phone?.FirstOrDefault()?.PhoneNum_1;

                    stu.PostalCode = studetail.PostOffices?.FirstOrDefault()?.PostalCode;
                    stu.PostOffice = studetail.PostOffices?.FirstOrDefault()?.PostOffice;

                    stu.User_id = studetail.User?.FirstOrDefault()?.User_id;
                    stu.UserIdentity = studetail.User?.FirstOrDefault()?.UserIdentity;
                    stu.Password = studetail.User?.FirstOrDefault()?.Password;

                    model.Add(stu);
                }
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    Studentx studentx = db.Studentx.Find(id);

                    if (studentx == null)

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

        // GET: Studentxes/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            StudentViewModel model = new StudentViewModel();
            return View(model);
        }//create

   
    // POST: Studentxes/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            Studentx stu = new Studentx();
            stu.FirstName = model.FirstName;
            stu.LastName = model.LastName;
            stu.Identity = model.Identity;
            stu.Address = model.Address;
            stu.Email = model.Email;
            stu.Notes = model.Notes;
            stu.EnrollmentDateIN = model.EnrollmentDateIN;
            stu.EnrollmentDateOUT = model.EnrollmentDateOUT;
            stu.EnrollmentDateOFF = model.EnrollmentDateOFF;

            db.Studentx.Add(stu);

            User usr = new User();
            usr.UserIdentity = model.UserIdentity;
            usr.Password = "Student";
            usr.Studentx = stu;

            db.User.Add(usr);

            Phone pho = new Phone();
            pho.PhoneNum_1 = model.PhoneNum_1;
            pho.Studentx = stu;

            db.Phone.Add(pho);

            PostOffices pos = new PostOffices();
            pos.PostalCode = model.PostalCode;
            pos.PostOffice = model.PostOffice;
            pos.Studentx = stu;

            db.PostOffices.Add(pos);

            try { db.SaveChanges(); }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
    }//cr*/

    // GET: Studentxes/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentx studetail = db.Studentx.Find(id);
            if (studetail == null)
            {
                return HttpNotFound();
            }
            StudentViewModel stu = new StudentViewModel();
            stu.Student_id = studetail.Student_id;
            stu.FirstName = studetail.FirstName;
            stu.LastName = studetail.LastName;
            stu.Identity = studetail.Identity;
            stu.Address = studetail.Address;
            stu.Email = studetail.Email;
            stu.EnrollmentDateIN = studetail.EnrollmentDateIN;
            stu.EnrollmentDateOUT = studetail.EnrollmentDateOUT;
            stu.EnrollmentDateOFF = studetail.EnrollmentDateOFF;
            stu.Notes = studetail.Notes;

            stu.PhoneNum_1 = studetail.Phone?.FirstOrDefault()?.PhoneNum_1;
            stu.PostalCode = studetail.PostOffices?.FirstOrDefault()?.PostalCode;
            stu.PostOffice = studetail.PostOffices?.FirstOrDefault()?.PostOffice;

            stu.User_id = studetail.User?.FirstOrDefault()?.User_id;
            stu.UserIdentity = studetail.User?.FirstOrDefault()?.UserIdentity;
            stu.Password = studetail.User?.FirstOrDefault()?.Password;

            return View(stu);
        }

        // POST: Studentxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel model)
        {
            Studentx stu = db.Studentx.Find(model.Student_id);

            stu.Student_id = model.Student_id;
            stu.FirstName = model.FirstName;
            stu.LastName = model.LastName;
            stu.Identity = model.Identity;
            stu.Address = model.Address;
            stu.Email = model.Email;
            stu.EnrollmentDateIN = model.EnrollmentDateIN;
            stu.EnrollmentDateOUT = model.EnrollmentDateOUT;
            stu.EnrollmentDateOFF = model.EnrollmentDateOFF;
            stu.Notes = model.Notes;

            if (stu.Phone == null)
            {
                Phone pho = new Phone();
                pho.PhoneNum_1 = model.PhoneNum_1;
                pho.Studentx = stu;

                db.Phone.Add(pho);
            }
            else
            {
                Phone pho = stu.Phone.FirstOrDefault();
                if (pho != null)
                {
                    pho.PhoneNum_1 = model.PhoneNum_1;
                }
            }

            if (stu.User == null)
            {
                User usr = new User();
                usr.UserIdentity = model.UserIdentity;
                usr.Password = "Student";
                usr.Studentx = stu;

                db.User.Add(usr);
            }
            else
            {
                User us = stu.User.FirstOrDefault();

                if (us != null) {

                    us.UserIdentity = model.UserIdentity;
                    us.Password = "Student";
                }
            }

            if (stu.PostOffices == null) 
            {
                PostOffices pos = new PostOffices();
                pos.PostalCode = model.PostalCode;
                pos.PostOffice = model.PostOffice;
                pos.Studentx = stu;

                db.PostOffices.Add(pos);
            }
            else
            {
                PostOffices po = stu.PostOffices.FirstOrDefault();

                if (po != null)
                {
                    po.PostalCode = model.PostalCode;
                    po.PostOffice = model.PostOffice;
                }
            }

            db.SaveChanges();
            return View(model);

        }//edit
 

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
