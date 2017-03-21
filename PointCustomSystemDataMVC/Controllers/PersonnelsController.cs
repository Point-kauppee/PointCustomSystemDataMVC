using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointCustomSystemDataMVC.Models;
using PointCustomSystemDataMVC.Utilities;
using System.Globalization;
using Newtonsoft.Json;
using PointCustomSystemDataMVC.ViewModels;

namespace PointCustomSystemDataMVC.Controllers
{
    public class PersonnelsController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Personnels
        public ActionResult Index()
        {
            List<PersonnelViewModel> model = new List<PersonnelViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Personnel> personnels = entities.Personnel.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Personnel personnel in personnels)
                {
                    PersonnelViewModel per = new PersonnelViewModel();
                    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    per.User_id = personnel.User?.FirstOrDefault()?.User_id;
                    per.UserIdentity = personnel.User?.FirstOrDefault()?.UserIdentity;

                    per.Personnel_id = personnel.Personnel_id;
                    per.FirstNameP = personnel.FirstName;
                    per.LastNameP = personnel.LastName;
                    per.Identity = personnel.Identity;
                    per.Email = personnel.Email;
                    per.Notes = personnel.Notes;

                    per.Phone_id = personnel.Phone?.FirstOrDefault()?.Phone_id;
                    per.PhoneNum_1 = personnel.Phone?.FirstOrDefault()?.PhoneNum_1;

                    per.Post_id = personnel.PostOffices?.FirstOrDefault()?.Post_id;
                    per.PostOffice = personnel.PostOffices?.FirstOrDefault()?.PostalCode;
                    per.PostOffice = personnel.PostOffices?.FirstOrDefault()?.PostOffice;

                    model.Add(per);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }


        //Lisätty 10.3.2017
        // GET: Personnels/Details/5
        public ActionResult Details(int? id)
        {
            List<PersonnelViewModel> model = new List<PersonnelViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Personnel> personnels = entities.Personnel.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Personnel persdetail in personnels)
                {
                    PersonnelViewModel view = new PersonnelViewModel();

                    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    view.User_id = persdetail.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = persdetail.User?.FirstOrDefault()?.UserIdentity;
                    view.Password = persdetail.User?.FirstOrDefault()?.Password;

                    view.Personnel_id = persdetail.Personnel_id;
                    view.FirstNameA = persdetail.FirstName;
                    view.LastNameA = persdetail.LastName;
                    view.Identity = persdetail.Identity;
                    view.Email = persdetail.Email;
                    view.Notes = persdetail.Notes;

                    view.Phone_id = persdetail.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = persdetail.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = persdetail.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = persdetail.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = persdetail.PostOffices?.FirstOrDefault()?.PostOffice;

                    model.Add(view);
                }
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Personnel personnel = db.Personnel.Find(id);
                    if (personnel == null)
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

        // GET: Personnels/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            PersonnelViewModel model = new PersonnelViewModel();

            //ViewBag.PersonSeed = new SelectList(list, "User_id", "UserIdentity");
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

            return View(model);
        }//create

        // POST: Personnels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            //ViewBag.PersonSeed = new SelectList(list, "User_id", "UserIdentity");

            Personnel per = new Personnel();
            per.FirstName = model.FirstNameP;
            per.LastName = model.LastNameP;
            per.Email = model.Email;
            per.Notes = model.Notes;

            db.Personnel.Add(per);

            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);        
            User usr = new User();
            usr.UserIdentity = model.UserIdentity;
            usr.Password = "point@point.fi";
            usr.Personnel = per;

            db.User.Add(usr);
         
            Phone pho = new Phone();        
            pho.PhoneNum_1 = model.PhoneNum_1;
            pho.Personnel = per;

            db.Phone.Add(pho);

            PostOffices pos = new PostOffices();
            pos.PostalCode = model.PostalCode;
            pos.PostOffice = model.PostOffice;
            pos.Personnel = per;

            db.PostOffices.Add(pos);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create



        // GET: Personnels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel persdetail = db.Personnel.Find(id);
            if (persdetail == null)
            {
                return HttpNotFound();
            }

            PersonnelViewModel view = new PersonnelViewModel();
            view.Personnel_id = persdetail.Personnel_id;
            view.FirstNameA = persdetail.FirstName;
            view.LastNameA = persdetail.LastName;
            view.Identity = persdetail.Identity;
            view.Email = persdetail.Email;
            view.Notes = persdetail.Notes;

            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            view.User_id = persdetail.User?.FirstOrDefault()?.User_id;
            view.UserIdentity = persdetail.User?.FirstOrDefault()?.UserIdentity;

            view.Phone_id = persdetail.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = persdetail.Phone?.FirstOrDefault()?.PhoneNum_1;

            view.Post_id = persdetail.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = persdetail.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = persdetail.PostOffices?.FirstOrDefault()?.PostOffice;
          
            return View(view);

        }//edit

        // POST: Personnels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonnelViewModel model)
        {
            Personnel per = db.Personnel.Find(model.Personnel_id);

            per.FirstName = model.FirstNameP;
            per.LastName = model.LastNameP;
            per.Email = model.Email;
            per.Notes = model.Notes;

            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            if (per.User == null)
            {
                User usr = new User();
                usr.UserIdentity = model.UserIdentity;
                usr.Password = "point@point.fi";
                usr.Personnel = per;

                db.User.Add(usr);
            }
            else
            {
                User usr = per.User.FirstOrDefault();
                if (usr != null)
                {
                    usr.UserIdentity = model.UserIdentity;
                    usr.Password = "point@point.fi";
                }
            }

            if (per.Phone == null)
            {
                Phone pho = new Phone();
                pho.PhoneNum_1 = model.PhoneNum_1;
                pho.Personnel = per;

                db.Phone.Add(pho);
            }
            else
            {
                Phone phone = per.Phone.FirstOrDefault();
                if (phone != null)
                {
                    phone.PhoneNum_1 = model.PhoneNum_1;
                }
            }

            if (per.PostOffices == null)
            {
                PostOffices pos = new PostOffices();
                pos.PostalCode = model.PostalCode;
                pos.PostOffice = model.PostOffice;
                pos.Personnel = per;

                db.PostOffices.Add(pos);
            }
            else
            {
                PostOffices po = per.PostOffices.FirstOrDefault();
                if (po != null)
                {
                    po.PostalCode = model.PostalCode;
                    po.PostOffice = model.PostOffice;
                }
            }

            db.SaveChanges();
            //return View(model);
            return RedirectToAction("Index");
        }

        // GET: Personnels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel persdetail = db.Personnel.Find(id);
            if (persdetail == null)
            {
                return HttpNotFound();
            }

            PersonnelViewModel view = new PersonnelViewModel();
            view.Personnel_id = persdetail.Personnel_id;
            view.FirstNameA = persdetail.FirstName;
            view.LastNameA = persdetail.LastName;
            view.Identity = persdetail.Identity;
            view.Email = persdetail.Email;
            view.Notes = persdetail.Notes;

            view.Phone_id = persdetail.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = persdetail.Phone?.FirstOrDefault()?.PhoneNum_1;
            view.Post_id = persdetail.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = persdetail.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = persdetail.PostOffices?.FirstOrDefault()?.PostOffice;

            view.User_id = persdetail.User?.FirstOrDefault()?.User_id;
            view.UserIdentity = persdetail.User?.FirstOrDefault()?.UserIdentity;

            return View(view);
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


