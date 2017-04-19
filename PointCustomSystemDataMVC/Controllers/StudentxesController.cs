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
using Newtonsoft.Json;
using PointCustomSystemDataMVC.Utilities;
using System.Security.Claims;
using Rotativa.MVC;

namespace PointCustomSystemDataMVC.Controllers
{
    //[Authorize(Roles = "Personnel User")]
    public class StudentxesController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Studentxes
        public ActionResult Index()
        {
            //string username = User.Identity.Name;
            //string userid = ((ClaimsPrincipal)User).Claims?.Where(c => c.Type == ClaimTypes.GroupSid).FirstOrDefault()?.Value ?? "";

            List<StudentViewModel> model = new List<StudentViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Studentx> students = entities.Studentx.OrderBy(Studentx => Studentx.LastName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta         
                foreach (Studentx student in students)
                {
                    StudentViewModel stu = new StudentViewModel();

                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    stu.User_id = student.User?.FirstOrDefault()?.User_id;
                    stu.UserIdentity = student.User?.FirstOrDefault()?.UserIdentity;
                   
                    stu.Student_id = student.Student_id;
                    stu.FirstNameH = student.FirstName;
                    stu.LastNameH = student.LastName;
                    stu.Identity = student.Identity;
                    stu.Address = student.Address;
                    stu.Email = student.Email;
                    stu.EnrollmentDateIN = student.EnrollmentDateIN;
                    stu.EnrollmentDateOUT = student.EnrollmentDateOUT;
                    stu.EnrollmentDateOFF = student.EnrollmentDateOFF;
                    stu.Notes = student.Notes;
                    stu.CreatedAt = student.CreatedAt;
                    stu.LastModifiedAt = student.LastModifiedAt;
                    stu.DeletedAt = student.DeletedAt;

                    ViewBag.Active = new SelectList((from a in db.Studentx select new { Student_id = a.Student_id, Active = a.Active }), "Student_id", "Active", null);
                    stu.Active = student.Active;
                    stu.Information = student.Information;

                    stu.Phone_id = student.Phone?.FirstOrDefault()?.Phone_id;
                    stu.PhoneNum_1 = student.Phone?.FirstOrDefault()?.PhoneNum_1;

                    stu.Post_id = student.PostOffices?.FirstOrDefault()?.Post_id;
                    stu.PostalCode = student.PostOffices?.FirstOrDefault()?.PostalCode;
                    stu.PostOffice = student.PostOffices?.FirstOrDefault()?.PostOffice;

                    ViewBag.StudentGroupName = new SelectList((from s in db.StudentGroup select new { StudentGroup_id = s.StudentGroup_id, StudentGroupName = s.StudentGroupName }), "StudentGroup_id", "StudentGroupName", null);
                    stu.StudentGroup_id = student.StudentGroup?.StudentGroup_id;
                    stu.StudentGroupName = student.StudentGroup?.StudentGroupName;

                    model.Add(stu);
                }
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }

        CultureInfo fiFi = new CultureInfo("fi-FI");

        public ActionResult DownloadViewPDF(int? id)
        {
            ReservationDetailViewModel model = new ReservationDetailViewModel();
            //List<StudentViewModel> model = new List<StudentViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Studentx> students = entities.Studentx.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta             
                foreach (Studentx studetail in students)
                {
                    ReservationDetailViewModel stu = new ReservationDetailViewModel();

                    stu.User_id = studetail.User?.FirstOrDefault()?.User_id;
                    stu.UserIdentity = studetail.User?.FirstOrDefault()?.UserIdentity;
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    //stu.Password = studetail.User?.FirstOrDefault()?.Password; 

                    stu.Student_id = studetail.Student_id;
                    stu.FirstNameH = studetail.FirstName;
                    stu.LastNameH = studetail.LastName;
                    stu.Identity = studetail.Identity;
                    stu.Address = studetail.Address;
                    stu.Email = studetail.Email;
                    stu.EnrollmentDateIN = studetail.EnrollmentDateIN;
                    stu.EnrollmentDateOUT = studetail.EnrollmentDateOUT;
                    stu.EnrollmentDateOFF = studetail.EnrollmentDateOFF;
                    stu.Notes = studetail.Notes;
                    stu.CreatedAt = studetail.CreatedAt;
                    stu.LastModifiedAt = studetail.LastModifiedAt;
                    stu.DeletedAt = studetail.DeletedAt;
                    stu.Active = studetail.Active;
                    ViewBag.Active = new SelectList((from a in db.Studentx select new { Student_id = a.Student_id, Active = a.Active }), "Student_id", "Active", null);
                    stu.Information = studetail.Information;

                    stu.Phone_id = studetail.Phone?.FirstOrDefault()?.Phone_id;
                    stu.PhoneNum_1 = studetail.Phone?.FirstOrDefault()?.PhoneNum_1;

                    stu.Post_id = studetail.PostOffices?.FirstOrDefault()?.Post_id;
                    stu.PostalCode = studetail.PostOffices?.FirstOrDefault()?.PostalCode;
                    stu.PostOffice = studetail.PostOffices?.FirstOrDefault()?.PostOffice;

                    stu.StudentGroup_id = studetail.StudentGroup?.StudentGroup_id;
                    stu.StudentGroupName = studetail.StudentGroup?.StudentGroupName;
                    ViewBag.StudentGroupName = new SelectList((from s in db.StudentGroup select new { StudentGroup_id = s.StudentGroup_id, StudentGroupName = s.StudentGroupName }), "StudentGroup_id", "StudentGroupName", null);

                    // muodostetaan Studentx - näkymän alitiedostona asiakkaan palvelutiedot
                    stu.Studentreservations = new List<StudentDetailViewModel>();

                    foreach (Reservation res in studetail.Reservation.OrderBy(r => r.Start))
                    {
                        stu.Studentreservations.Add(new StudentDetailViewModel()
                        {
                            Date = res.Date,
                            Start = res.Start,
                            End = res.End,
                            TreatmentName = res.Treatment?.TreatmentName,
                            TreatmentTime = res.Treatment?.TreatmentTime,
                            FirstNameA = res.Customer?.FirstName,
                            LastNameA = res.Customer?.LastName,
                            Notes = res.Note
                        });
                    }
                    model = stu;
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

            return new ViewAsPdf(model);
        }//

        // GET: Studentxes/Details/5
        public ActionResult Details(int? id)
        {
            ReservationDetailViewModel model = new ReservationDetailViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                Studentx studetail = entities.Studentx.Find(id);
                if (studetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta             
                ReservationDetailViewModel view = new ReservationDetailViewModel();
                view.User_id = studetail.User?.FirstOrDefault()?.User_id;
                view.UserIdentity = studetail.User?.FirstOrDefault()?.UserIdentity;
                ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                //stu.Password = studetail.User?.FirstOrDefault()?.Password; 

                view.Student_id = studetail.Student_id;
                view.FirstNameH = studetail.FirstName;
                view.LastNameH = studetail.LastName;
                view.Identity = studetail.Identity;
                view.Address = studetail.Address;
                view.Email = studetail.Email;
                view.EnrollmentDateIN = studetail.EnrollmentDateIN;
                view.EnrollmentDateOUT = studetail.EnrollmentDateOUT;
                view.EnrollmentDateOFF = studetail.EnrollmentDateOFF;
                view.Notes = studetail.Notes;
                view.CreatedAt = studetail.CreatedAt;
                view.LastModifiedAt = studetail.LastModifiedAt;
                view.DeletedAt = studetail.DeletedAt;
                view.Active = studetail.Active;
                ViewBag.Active = new SelectList((from a in db.Studentx select new { Student_id = a.Student_id, Active = a.Active }), "Student_id", "Active", null);
                view.Information = studetail.Information;

                view.Phone_id = studetail.Phone?.FirstOrDefault()?.Phone_id;
                view.PhoneNum_1 = studetail.Phone?.FirstOrDefault()?.PhoneNum_1;

                view.Post_id = studetail.PostOffices?.FirstOrDefault()?.Post_id;
                view.PostalCode = studetail.PostOffices?.FirstOrDefault()?.PostalCode;
                view.PostOffice = studetail.PostOffices?.FirstOrDefault()?.PostOffice;

                view.StudentGroup_id = studetail.StudentGroup?.StudentGroup_id;
                view.StudentGroupName = studetail.StudentGroup?.StudentGroupName;
                ViewBag.StudentGroupName = new SelectList((from s in db.StudentGroup select new { StudentGroup_id = s.StudentGroup_id, StudentGroupName = s.StudentGroupName }), "StudentGroup_id", "StudentGroupName", null);

                // muodostetaan Studentx - näkymän alitiedostona asiakkaan palvelutiedot
                view.Studentreservations = new List<StudentDetailViewModel>();
                
                foreach (Reservation res in studetail.Reservation.OrderBy(r => r.Start))
                {
                    view.Studentreservations.Add(new StudentDetailViewModel()
                    {
                        Date = res.Date,
                        Start = res.Start,
                        End = res.End,
                        TreatmentName = res.Treatment?.TreatmentName,
                        TreatmentTime = res.Treatment?.TreatmentTime,
                        FirstNameA = res.Customer?.FirstName,
                        LastNameA = res.Customer?.LastName,
                        Notes = res.Note
                    });
                }
                model = view;
   
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
        
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            ViewBag.StudentGroupName = new SelectList((from s in db.StudentGroup select new { StudentGroup_id = s.StudentGroup_id, StudentGroupName = s.StudentGroupName }), "StudentGroup_id", "StudentGroupName", null);
            ViewBag.Active = new SelectList((from a in db.Studentx select new { Student_id = a.Student_id, Active = a.Active }), "Student_id", "Active", null);

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
            stu.FirstName = model.FirstNameH;
            stu.LastName = model.LastNameH;
            stu.Identity = model.Identity;
            stu.Address = model.Address;
            stu.Email = model.Email;
            stu.Notes = model.Notes;
            stu.EnrollmentDateIN = model.EnrollmentDateIN;
            stu.EnrollmentDateOUT = model.EnrollmentDateOUT;
            stu.EnrollmentDateOFF = model.EnrollmentDateOFF;
            stu.CreatedAt = DateTime.Now;
            stu.LastModifiedAt = DateTime.Now;
            stu.DeletedAt = model.DeletedAt;           
            stu.Active = model.Active;
            stu.Information = model.Information;

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

            // etsitään StudentGroup-rivi kannasta valitun nimen perusteella           
            int studentgroupId = int.Parse(model.StudentGroupName);
            if (studentgroupId > 0)
            {
                StudentGroup stg = db.StudentGroup.Find(studentgroupId);
                stu.StudentGroup_id = stg.StudentGroup_id;
            }

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            ViewBag.StudentGroupName = new SelectList((from s in db.StudentGroup select new { StudentGroup_id = s.StudentGroup_id, StudentGroupName = s.StudentGroupName }), "StudentGroup_id", "StudentGroupName", null);
            ViewBag.Active = new SelectList((from a in db.Studentx select new { Student_id = a.Student_id, Active = a.Active }), "Student_id", "Active", null);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
    }//create

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

            StudentViewModel view = new StudentViewModel();
            view.Student_id = studetail.Student_id;
            view.FirstNameH = studetail.FirstName;
            view.LastNameH = studetail.LastName;
            view.Identity = studetail.Identity;
            view.Address = studetail.Address;
            view.Email = studetail.Email;
            view.EnrollmentDateIN = studetail.EnrollmentDateIN;
            view.EnrollmentDateOUT = studetail.EnrollmentDateOUT;
            view.EnrollmentDateOFF = studetail.EnrollmentDateOFF;
            view.Notes = studetail.Notes;
            view.CreatedAt = studetail.CreatedAt;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = studetail.DeletedAt;
            view.Active = studetail.Active;
            ViewBag.Active = new SelectList((from a in db.Studentx select new { Student_id = a.Student_id, Active = a.Active }), "Student_id", "Active", view.Student_id);
            view.Information = studetail.Information;

            view.User_id = studetail.User?.FirstOrDefault()?.User_id;
            view.UserIdentity = studetail.User?.FirstOrDefault()?.UserIdentity;
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", view.User_id);
            //stu.Password = studetail.User?.FirstOrDefault()?.Password;

            view.Phone_id = studetail.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = studetail.Phone?.FirstOrDefault()?.PhoneNum_1;

            view.Post_id = studetail.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = studetail.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = studetail.PostOffices?.FirstOrDefault()?.PostOffice;

            view.StudentGroup_id = studetail.StudentGroup?.StudentGroup_id;
            view.StudentGroupName = studetail.StudentGroup?.StudentGroupName;
            ViewBag.StudentGroupName = new SelectList((from s in db.StudentGroup select new { StudentGroup_id = s.StudentGroup_id, StudentGroupName = s.StudentGroupName }), "StudentGroup_id", "StudentGroupName", view.StudentGroup_id);

            return View(view);
        }

        // POST: Studentxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel model)
        {
            Studentx stu = db.Studentx.Find(model.Student_id);

            stu.FirstName = model.FirstNameH;
            stu.LastName = model.LastNameH;
            stu.Identity = model.Identity;
            stu.Address = model.Address;
            stu.Email = model.Email;
            stu.EnrollmentDateIN = model.EnrollmentDateIN;
            stu.EnrollmentDateOUT = model.EnrollmentDateOUT;
            stu.EnrollmentDateOFF = model.EnrollmentDateOFF;
            stu.Notes = model.Notes;
            stu.CreatedAt = model.CreatedAt;
            stu.LastModifiedAt = DateTime.Now;
            stu.DeletedAt = model.DeletedAt;
            stu.Active = model.Active;
            ViewBag.Active = new SelectList((from a in db.Studentx select new { Student_id = a.Student_id, Active = a.Active }), "Student_id", "Active", stu.Student_id);
            stu.Information = model.Information;

            //ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
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
                User user = stu.User.FirstOrDefault();
                if (user != null)
                {
                    user.UserIdentity = model.UserIdentity;
                }
            }
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

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

            int studentgroupId = int.Parse(model.StudentGroupName);
            if (studentgroupId > 0)
            {
                StudentGroup stg = db.StudentGroup.Find(studentgroupId);
                stu.StudentGroup_id = stg.StudentGroup_id;
            }
            ViewBag.StudentGroupName = new SelectList((from s in db.StudentGroup select new { StudentGroup_id = s.StudentGroup_id, StudentGroupName = s.StudentGroupName }), "StudentGroup_id", "StudentGroupName", null);

            db.SaveChanges();
            return RedirectToAction("Index");

        }//edit
 

        // GET: Studentxes/Delete/5
        public ActionResult Delete(int? id)
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
            stu.FirstNameH = studetail.FirstName;
            stu.LastNameH = studetail.LastName;
            stu.Identity = studetail.Identity;
            stu.Address = studetail.Address;
            stu.Email = studetail.Email;
            stu.EnrollmentDateIN = studetail.EnrollmentDateIN;
            stu.EnrollmentDateOUT = studetail.EnrollmentDateOUT;
            stu.EnrollmentDateOFF = studetail.EnrollmentDateOFF;
            stu.Notes = studetail.Notes;
            stu.CreatedAt = studetail.CreatedAt;
            stu.LastModifiedAt = studetail.LastModifiedAt;
            stu.DeletedAt = studetail.DeletedAt;
            stu.Active = studetail.Active;
            stu.Information = studetail.Information;

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            stu.User_id = studetail.User?.FirstOrDefault()?.User_id;
            stu.UserIdentity = studetail.User?.FirstOrDefault()?.UserIdentity;
            stu.Password = studetail.User?.FirstOrDefault()?.Password;

            stu.Phone_id = studetail.Phone?.FirstOrDefault()?.Phone_id;
            stu.PhoneNum_1 = studetail.Phone?.FirstOrDefault()?.PhoneNum_1;

            stu.Post_id = studetail.PostOffices?.FirstOrDefault()?.Post_id;
            stu.PostalCode = studetail.PostOffices?.FirstOrDefault()?.PostalCode;
            stu.PostOffice = studetail.PostOffices?.FirstOrDefault()?.PostOffice;

            ViewBag.StudentGroupName = new SelectList((from s in db.StudentGroup select new { StudentGroup_id = s.StudentGroup_id, StudentGroupName = s.StudentGroupName }), "StudentGroup_id", "StudentGroupName", null);
            stu.StudentGroup_id = studetail.StudentGroup?.StudentGroup_id;
            stu.StudentGroupName = studetail.StudentGroup?.StudentGroupName;

            return View(stu);
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

        // Hoitajatietojen arkistointi:
        // GET: Customers/Archive/5
        public ActionResult Archive(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentx student = db.Studentx.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            StudentViewModel svm = new StudentViewModel();
            svm.Student_id =student.Student_id;
            svm.Active = student.Active;
            svm.DeletedAt = DateTime.Now;

            return View(svm);
        }//Archive

        // POST: Customers/Archive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Archive(StudentViewModel model)
        {
            Studentx svm = db.Studentx.Find(model.Student_id);
            svm.Active = model.Active;
            svm.DeletedAt = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index");

        }//Archive   
    }
}
