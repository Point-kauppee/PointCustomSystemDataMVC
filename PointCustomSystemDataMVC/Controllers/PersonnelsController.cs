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
using System.Security.Claims;
using Rotativa.MVC;

namespace PointCustomSystemDataMVC.Controllers
{
    //[Authorize(Roles = "Personnel User,Student User")]
    public class PersonnelsController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Personnels
        public ActionResult Index()
        {
            //string username = User.Identity.Name;
            //string userid = ((ClaimsPrincipal)User).Claims?.Where(c => c.Type == ClaimTypes.GroupSid).FirstOrDefault()?.Value ?? "";

            List<PersonnelViewModel> model = new List<PersonnelViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Personnel> personnels = entities.Personnel.OrderBy(Personnel => Personnel.LastName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta           
                foreach (Personnel personnel in personnels)
                {
                    PersonnelViewModel view = new PersonnelViewModel();
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    view.User_id = personnel.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = personnel.User?.FirstOrDefault()?.UserIdentity;

                    view.Personnel_id = personnel.Personnel_id;
                    view.FirstNameP = personnel.FirstName;
                    view.LastNameP = personnel.LastName;
                    view.Identity = personnel.Identity;
                    view.Email = personnel.Email;
                    view.Notes = personnel.Notes;
                    view.CreatedAt = personnel.CreatedAt;
                    view.LastModifiedAt = personnel.LastModifiedAt;
                    view.DeletedAt = personnel.DeletedAt;
                    view.Active = personnel.Active;

                    view.Phone_id = personnel.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = personnel.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = personnel.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = personnel.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = personnel.PostOffices?.FirstOrDefault()?.PostOffice;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index

        CultureInfo fiFi = new CultureInfo("fi-FI");

        //PDF-tiedoston luominen:
        public ActionResult DownloadViewPDF(int? id)
        {
            PersonnelViewModel model = new PersonnelViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Personnel> personnels = entities.Personnel.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta         
                foreach (Personnel persdetail in personnels)
                {
                    PersonnelViewModel view = new PersonnelViewModel();
            
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    view.User_id = persdetail.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = persdetail.User?.FirstOrDefault()?.UserIdentity;

                    view.Personnel_id = persdetail.Personnel_id;
                    view.FirstNameP = persdetail.FirstName;
                    view.LastNameP = persdetail.LastName;
                    view.Identity = persdetail.Identity;
                    view.Email = persdetail.Email;
                    view.Notes = persdetail.Notes;
                    view.CreatedAt = persdetail.CreatedAt;
                    view.LastModifiedAt = persdetail.LastModifiedAt;
                    view.DeletedAt = persdetail.DeletedAt;
                    view.Active = persdetail.Active;
                    view.Information = persdetail.Information;

                    view.Phone_id = persdetail.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = persdetail.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = persdetail.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = persdetail.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = persdetail.PostOffices?.FirstOrDefault()?.PostOffice;

                    model = view;
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
            return new ViewAsPdf(model);
        }//DownloadViewPDF

        //Lisätty 10.3.2017
        // GET: Personnels/Details/5
        public ActionResult Details(int? id)
        {
            PersonnelViewModel model = new PersonnelViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                Personnel persdetail = entities.Personnel.Find(id);

                if (persdetail == null)
                {
                    return HttpNotFound();
                }
                // muodostetaan näkymämalli tietokannan rivien pohjalta         
                    PersonnelViewModel view = new PersonnelViewModel();
                    view.Personnel_id = persdetail.Personnel_id;
                    view.FirstNameP = persdetail.FirstName;
                    view.LastNameP = persdetail.LastName;
                    view.Identity = persdetail.Identity;
                    view.Email = persdetail.Email;
                    view.Notes = persdetail.Notes;
                    view.CreatedAt = persdetail.CreatedAt;
                    view.LastModifiedAt = persdetail.LastModifiedAt;
                    view.DeletedAt = persdetail.DeletedAt;
                    view.Active = persdetail.Active;
                    view.Information = persdetail.Information;

                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    view.User_id = persdetail.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = persdetail.User?.FirstOrDefault()?.UserIdentity;

                    view.Phone_id = persdetail.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = persdetail.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = persdetail.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = persdetail.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = persdetail.PostOffices?.FirstOrDefault()?.PostOffice;

                    model = view;
             
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

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

            return View(model);
        }//create

        // POST: Personnels/Create
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            Personnel per = new Personnel();
            per.FirstName = model.FirstNameP;
            per.LastName = model.LastNameP;
            per.Identity = model.Identity;
            per.Email = model.Email;
            per.Notes = model.Notes;
            per.CreatedAt = DateTime.Now;
            per.LastModifiedAt = DateTime.Now;
            per.DeletedAt = model.DeletedAt;
            per.Active = model.Active;

            db.Personnel.Add(per);

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
            view.FirstNameP = persdetail.FirstName;
            view.LastNameP = persdetail.LastName;
            view.Identity = persdetail.Identity;
            view.Email = persdetail.Email;
            view.Notes = persdetail.Notes;
            view.CreatedAt = persdetail.CreatedAt;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = persdetail.DeletedAt;
            view.Active = persdetail.Active;

            view.User_id = persdetail.User?.FirstOrDefault()?.User_id;
            view.UserIdentity = persdetail.User?.FirstOrDefault()?.UserIdentity;
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", view.User_id);

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
            per.Identity = model.Identity;
            per.Email = model.Email;
            per.Notes = model.Notes;
            per.CreatedAt = model.CreatedAt;
            per.LastModifiedAt = model.LastModifiedAt;
            per.DeletedAt = model.DeletedAt;
            per.Active = model.Active;

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
                User user = per.User.FirstOrDefault();
                if (user != null)
                {
                    user.UserIdentity = model.UserIdentity;
                }
            }
            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

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
                return RedirectToAction("Index");
        }//edit

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
            view.FirstNameP = persdetail.FirstName;
            view.LastNameP = persdetail.LastName;
            view.Identity = persdetail.Identity;
            view.Email = persdetail.Email;
            view.Notes = persdetail.Notes;
            view.CreatedAt = persdetail.CreatedAt;
            view.LastModifiedAt = persdetail.LastModifiedAt;
            view.DeletedAt = persdetail.DeletedAt;
            view.Active = persdetail.Active;

            view.Phone_id = persdetail.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = persdetail.Phone?.FirstOrDefault()?.PhoneNum_1;

            view.Post_id = persdetail.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = persdetail.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = persdetail.PostOffices?.FirstOrDefault()?.PostOffice;

            view.User_id = persdetail.User?.FirstOrDefault()?.User_id;
            view.UserIdentity = persdetail.User?.FirstOrDefault()?.UserIdentity;

            return View(view);
        }//delete

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
        }//Delete

        //SideMenu:
        public ActionResult SideMenu()
        {
            return PartialView("SideMenu");
        }


        // Henkilökunnan tietojen arkistointi:
        // GET: Customers/Archive/5
        public ActionResult Archive(int? id)
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

            PersonnelViewModel pvm = new PersonnelViewModel();
            pvm.Personnel_id = personnel.Personnel_id;
            pvm.Active = personnel.Active;
            pvm.DeletedAt = DateTime.Now;

            return View(pvm);
        }//Archive

        // POST: Customers/Archive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Archive(PersonnelViewModel model)
        {
            Personnel pvm = db.Personnel.Find(model.Personnel_id);
            pvm.Active = model.Active;
            pvm.DeletedAt = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index");

        }//Archive
    }
}


