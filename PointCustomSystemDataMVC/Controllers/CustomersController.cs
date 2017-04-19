using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointCustomSystemDataMVC.Models;
using System.Collections;
using System.Globalization;
using PointCustomSystemDataMVC.ViewModels;
using Newtonsoft.Json;
using PointCustomSystemDataMVC.Utilities;
using System.Security.Claims;
using Rotativa.MVC;

namespace PointCustomSystemDataMVC.Controllers

{
    //[Authorize(Roles = "Personnel User,Student User")]
    public class CustomersController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Customers
        public ActionResult Index(string sortOrder)
        {
            //string username = User.Identity.Name;
            //string userid = ((ClaimsPrincipal)User).Claims?.Where(c => c.Type == ClaimTypes.GroupSid).FirstOrDefault()?.Value ?? "";

            //31.1.2017 Lisätty tietokantataulujen suodatukset:       
            List<CustomerViewModel> model = new List<CustomerViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Customer> customers = entities.Customer.OrderBy(Customer => Customer.LastName).ToList();
               
                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Customer customer in customers)
                {
                    CustomerViewModel view = new CustomerViewModel();
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    view.User_id = customer.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = customer.User?.FirstOrDefault()?.UserIdentity;

                    view.Customer_id = customer.Customer_id;
                    view.FirstNameA = customer.FirstName;
                    view.LastNameA = customer.LastName;
                    view.Identity = customer.Identity;
                    view.Email = customer.Email;
                    view.Address = customer.Address;
                    view.Notes = customer.Notes;
                    view.CreatedAt = customer.CreatedAt;
                    view.LastModifiedAt = customer.LastModifiedAt;
                    view.DeletedAt = customer.DeletedAt;
                    view.Active = customer.Active;
                    //view.Information = customer.Information;

                    view.Phone_id = customer.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = customer.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = customer.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = customer.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = customer.PostOffices?.FirstOrDefault()?.PostOffice;

                    //haetaan seuraava varaus:
                    view.Reservation_id = customer.Reservation?.FirstOrDefault()?.Reservation_id;
                    view.Start = customer.Reservation?.FirstOrDefault()?.Start;
                    view.End = customer.Reservation?.FirstOrDefault()?.End.Value;
                    view.Date = customer.Reservation?.FirstOrDefault()?.Date.Value;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            //25.3.2017 Lisätty sort -toiminto
            ViewBag.FirstnameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewBag.LastnameSortParm = sortOrder == "LastNameA" ? "lastname_desc" : "LastNameA";

            var custom = from c in db.Customer
                         orderby c.FirstName ascending
                         select c;

            switch (sortOrder)
            {
                case "firstname_desc":
                    custom = custom.OrderByDescending(c => c.FirstName);
                    break;
                case "LastNameA":
                    custom = custom.OrderBy(c => c.LastName);
                    break;
                case "lastname_desc":
                    custom = custom.OrderByDescending(c => c.LastName);
                    break;
                default:
                    custom = custom.OrderBy(c => c.FirstName);
                    break;
            }

            return View(model);
        }//Index

        CultureInfo fiFi = new CultureInfo("fi-FI");

        //Asiakastietojen tulostus pdf lomakkeelle
        public ActionResult DownLoadCustomerPDF()
        {

            List<CustomerViewModel> model = new List<CustomerViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Customer> customers = entities.Customer.OrderBy(Customer => Customer.LastName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Customer customer in customers)
                {
                    CustomerViewModel view = new CustomerViewModel();
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    view.User_id = customer.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = customer.User?.FirstOrDefault()?.UserIdentity;

                    view.Customer_id = customer.Customer_id;
                    view.FirstNameA = customer.FirstName;
                    view.LastNameA = customer.LastName;
                    view.Identity = customer.Identity;
                    view.Email = customer.Email;
                    view.Address = customer.Address;
                    view.Notes = customer.Notes;
                    view.CreatedAt = customer.CreatedAt;
                    view.LastModifiedAt = customer.LastModifiedAt;
                    view.DeletedAt = customer.DeletedAt;
                    view.Active = customer.Active;
                    //view.Information = customer.Information;

                    view.Phone_id = customer.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = customer.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = customer.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = customer.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = customer.PostOffices?.FirstOrDefault()?.PostOffice;

                    //haetaan seuraava varaus:
                    view.Reservation_id = customer.Reservation?.FirstOrDefault()?.Reservation_id;
                    view.Start = customer.Reservation?.FirstOrDefault()?.Start;
                    view.End = customer.Reservation?.FirstOrDefault()?.End.Value;
                    view.Date = customer.Reservation?.FirstOrDefault()?.Date.Value;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//Index

        //Asiakastietojen PDF-tiedoston luominen:
        public ActionResult DownloadViewPDF(int? id)
        {           
            CustomerViewModel model = new CustomerViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Customer> customers = entities.Customer.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta          
                foreach (Customer custdetail in customers)
                {
                    CustomerViewModel cview = new CustomerViewModel();

                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    cview.User_id = custdetail.User?.FirstOrDefault()?.User_id;
                    cview.UserIdentity = custdetail.User?.FirstOrDefault()?.UserIdentity;

                    cview.Customer_id = custdetail.Customer_id;
                    cview.FirstNameA = custdetail.FirstName;
                    cview.LastNameA = custdetail.LastName;
                    cview.Identity = custdetail.Identity;
                    cview.Email = custdetail.Email;
                    cview.Address = custdetail.Address;
                    cview.Notes = custdetail.Notes;
                    cview.CreatedAt = custdetail.CreatedAt;
                    cview.LastModifiedAt = custdetail.LastModifiedAt;
                    cview.DeletedAt = custdetail.DeletedAt;
                    cview.Active = custdetail.Active;
                    cview.Information = custdetail.Information;

                    cview.Phone_id = custdetail.Phone?.FirstOrDefault()?.Phone_id;
                    cview.PhoneNum_1 = custdetail.Phone?.FirstOrDefault()?.PhoneNum_1;

                    cview.Post_id = custdetail.PostOffices?.FirstOrDefault()?.Post_id;
                    cview.PostalCode = custdetail.PostOffices?.FirstOrDefault()?.PostalCode;
                    cview.PostOffice = custdetail.PostOffices?.FirstOrDefault()?.PostOffice;

                    //haetaan seuraava varaus:
                    cview.Reservation_id = custdetail.Reservation?.FirstOrDefault()?.Reservation_id;
                    cview.Start = custdetail.Reservation?.FirstOrDefault()?.Start.Value;
                    cview.End = custdetail.Reservation?.FirstOrDefault()?.End.Value;
                    cview.Date = custdetail.Reservation?.FirstOrDefault()?.Date.Value;

                    model = cview;
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customer.Find(id);
                if (customer == null)
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


        //Lisätty 1.3.2017 oma koodi, jota muokattu 12.4.2017:
        //GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            ReservationDetailViewModel model = new ReservationDetailViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {

                Customer custdetail = entities.Customer.Find(id);
                if (custdetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta        
                    ReservationDetailViewModel view = new ReservationDetailViewModel();          
                    ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
                    view.User_id = custdetail.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = custdetail.User?.FirstOrDefault()?.UserIdentity;

                    view.Customer_id = custdetail.Customer_id;
                    view.FirstNameA = custdetail.FirstName;
                    view.LastNameA = custdetail.LastName;
                    view.Identity = custdetail.Identity;
                    view.Email = custdetail.Email;
                    view.Address = custdetail.Address;
                    view.Notes = custdetail.Notes;
                    view.CreatedAt = custdetail.CreatedAt;
                    view.LastModifiedAt = custdetail.LastModifiedAt;
                    view.DeletedAt = custdetail.DeletedAt;
                    view.Active = custdetail.Active;
                    view.Information = custdetail.Information;

                    view.Phone_id = custdetail.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = custdetail.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = custdetail.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = custdetail.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = custdetail.PostOffices?.FirstOrDefault()?.PostOffice;

                    //muodostetaan Customer -näkymän alitiedostona asiakkaan palvelutiedot
                    view.Customreservations = new List<TreatmentDetailViewModel>();
                    //foreach (Reservation res in custdetail.Reservation.OrderBy(r => r.Date).ThenBy(r => r.Start))

                    foreach (Reservation res in custdetail.Reservation.OrderByDescending(r => r.Date))
                    {
                    view.Customreservations.Add(new TreatmentDetailViewModel()
                    {
                        Date = res.Date,
                        Start = res.Start,
                        End = res.End,
                        TreatmentName = res.Treatment?.TreatmentName,
                        TreatmentTime = res.Treatment?.TreatmentTime,
                        TreatmentCompleted = res.TreatmentCompleted,
                        TreatmentPrice = res.Treatment?.TreatmentPrice,
                        TreatmentPaidDate = res.TreatmentPaidDate,
                        FirstNameH = res.Studentx?.FirstName,
                        LastNameH = res.Studentx?.LastName,
                        Notes = res.Note,
                        TreatmentReportTexts = res.TreatmentReportTexts
                        
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

     
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         
        //public ActionResult CustomDetailList(int? id)
        //{
        //    var customerParentViewModel = new CustomerParentViewModel();
        //    var customerViewModel = new CustomerViewModel();

        //    customerParentViewModel.CustomerViewModel = customerViewModel;
        //    return View(customerParentViewModel);
        //}

        // GET: Customers/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            CustomerViewModel model = new CustomerViewModel();

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);

            return View(model);
        }//create

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();
        
            Customer cus = new Customer();
            cus.FirstName = model.FirstNameA;
            cus.LastName = model.LastNameA;
            cus.Identity = model.Identity;
            cus.Address = model.Address;
            cus.Email = model.Email;
            cus.Notes = model.Notes;
            cus.CreatedAt = DateTime.Now;
            cus.LastModifiedAt = DateTime.Now;
            cus.DeletedAt = model.DeletedAt;
            cus.Active = model.Active;
            //cus.Information = model.Information;

            db.Customer.Add(cus);

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            User usr = new User();
            usr.UserIdentity = model.UserIdentity;
            usr.Password = "joku@joku.fi";
            usr.Customer = cus;

            db.User.Add(usr);

            Phone pho = new Phone();
            pho.PhoneNum_1 = model.PhoneNum_1;
            pho.Customer = cus;

            db.Phone.Add(pho);

            PostOffices pos = new PostOffices();
            pos.PostalCode = model.PostalCode;
            pos.PostOffice = model.PostOffice;
            pos.Customer = cus;

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


        //[AllowAnonymous]
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer custdetail = db.Customer.Find(id);
            if (custdetail == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel view = new CustomerViewModel();
            view.Customer_id = custdetail.Customer_id;
            view.FirstNameA = custdetail.FirstName;
            view.LastNameA = custdetail.LastName;
            view.Identity = custdetail.Identity;
            view.Email = custdetail.Email;
            view.Address = custdetail.Address;
            view.Notes = custdetail.Notes;
            view.CreatedAt = custdetail.CreatedAt;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = custdetail.DeletedAt;
            view.Active = custdetail.Active;
            //view.Information = custdetail.Information;

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            view.User_id = custdetail.User?.FirstOrDefault()?.User_id;
            view.UserIdentity = custdetail.User?.FirstOrDefault()?.UserIdentity;

            view.Phone_id = custdetail.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = custdetail.Phone?.FirstOrDefault()?.PhoneNum_1;

            view.Post_id = custdetail.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = custdetail.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = custdetail.PostOffices?.FirstOrDefault()?.PostOffice;

            view.Reservation_id = custdetail.Reservation?.FirstOrDefault()?.Reservation_id;
            view.Start = custdetail.Reservation?.FirstOrDefault()?.Start.Value;
            view.End = custdetail.Reservation?.FirstOrDefault()?.End.Value;
            view.Date = custdetail.Reservation?.FirstOrDefault()?.Date.Value;

            return View(view);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel model)
        {
            Customer cus = db.Customer.Find(model.Customer_id);

            cus.FirstName = model.FirstNameA;
            cus.LastName = model.LastNameA;
            cus.Identity = model.Identity;
            cus.Address = model.Address;
            cus.Email = model.Email;
            cus.Notes = model.Notes;
            cus.CreatedAt = model.CreatedAt;
            cus.LastModifiedAt = DateTime.Now;
            cus.DeletedAt = model.DeletedAt;
            cus.Active = model.Active;
            //cus.Information = model.Information;

            ViewBag.UserIdentity = new SelectList((from u in db.User select new { User_id = u.User_id, UserIdentity = u.UserIdentity }), "User_id", "UserIdentity", null);
            if (cus.User == null)
            {
                User usr = new User();
                usr.UserIdentity = model.UserIdentity;
                usr.Password = "joku@joku.fi";
                usr.Customer = cus;

                db.User.Add(usr);
            }
            else
            {
                User user = cus.User.FirstOrDefault();
                if (user != null)
                {
                    user.UserIdentity = model.UserIdentity;
                }
            }

            if (cus.Phone == null)
            {
                Phone pho = new Phone();
                pho.PhoneNum_1 = model.PhoneNum_1;
                pho.Customer = cus;

                db.Phone.Add(pho);
            }
            else
            {
                Phone phone = cus.Phone.FirstOrDefault();
                if (phone != null)
                {
                    phone.PhoneNum_1 = model.PhoneNum_1;
                }
            }

            if (cus.PostOffices == null)
            {
                PostOffices pos = new PostOffices();
                pos.PostalCode = model.PostalCode;
                pos.PostOffice = model.PostOffice;
                pos.Customer = cus;

                db.PostOffices.Add(pos);
            }
            else
            {
                PostOffices po = cus.PostOffices.FirstOrDefault();
                if (po != null)
                {
                    po.PostalCode = model.PostalCode;
                    po.PostOffice = model.PostOffice;
                }
            }

            db.SaveChanges();       
            return RedirectToAction("Index");
        }//edit

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer custdetail = db.Customer.Find(id);
            if (custdetail == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel view = new CustomerViewModel();
            view.Customer_id = custdetail.Customer_id;
            view.FirstNameA = custdetail.FirstName;
            view.LastNameA = custdetail.LastName;
            view.Identity = custdetail.Identity;
            view.Email = custdetail.Email;
            view.Address = custdetail.Address;
            view.Notes = custdetail.Notes;
            view.CreatedAt = custdetail.CreatedAt;
            view.LastModifiedAt = custdetail.LastModifiedAt;
            view.DeletedAt = custdetail.DeletedAt;
            view.Active = custdetail.Active;
            //view.Information = custdetail.Information;

            view.Phone_id = custdetail.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = custdetail.Phone?.FirstOrDefault()?.PhoneNum_1;
            view.Post_id = custdetail.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = custdetail.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = custdetail.PostOffices?.FirstOrDefault()?.PostOffice;

            view.User_id = custdetail.User?.FirstOrDefault()?.User_id;
            view.UserIdentity = custdetail.User?.FirstOrDefault()?.UserIdentity;

            view.Reservation_id = custdetail.Reservation?.FirstOrDefault()?.Reservation_id;
            view.Start = custdetail.Reservation?.FirstOrDefault()?.Start.Value;
            view.End = custdetail.Reservation?.FirstOrDefault()?.End.Value;
            view.Date = custdetail.Reservation?.FirstOrDefault()?.Date.Value;

            return View(view);
        }//Delete

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
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



        // Asiakkaan tietojen arkistointi:
        // GET: Customers/Archive/5
        public ActionResult Archive(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel cvm = new CustomerViewModel();
            cvm.Customer_id = customer.Customer_id;
            cvm.Active = customer.Active;
            cvm.DeletedAt = DateTime.Now;

            return View(cvm);
        }//Archive

        // POST: Customers/Archive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Archive(CustomerViewModel model)
        {
            Customer cvm = db.Customer.Find(model.Customer_id);
            cvm.Active = model.Active;
            cvm.DeletedAt = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index");

        }//Archive




        //[HttpPost]
        ////CustomerViewModel.cs - ASIAKASRAPORTIN TALLENTAMINEN (SQL) TIETOKANTAAN
        //public JsonResult SavedReport(string TreatmentReport)
        //{
        //    string json = Request.InputStream.ReadToEnd();
        //    CustomerViewModel inputData =
        //        JsonConvert.DeserializeObject<CustomerViewModel>(json);

        //    bool success = false;
        //    string error = "";

        //    JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

        //    try
        //    {
        //        //haetaan ensin asiakkaan id-numero koodin perusteella:
        //        int customerId = (from c in entities.Customer
        //                          where c.Customer_id == inputData.Customer_id
        //                          select c.Customer_id).FirstOrDefault();


        //        //haetaan TreatmentReport id-numero koodin perusteella:
        //        int treatmentrepoId = (from t in entities.TreatmentReport
        //                               where t.TreatmentReportText == inputData.TreatmentReportText
        //                               select t.TreatmentReport_id).FirstOrDefault();

        //        if ((customerId > 0) && (treatmentrepoId > 0))
        //        {
        //            //tallennetaan asikaan hoitotiedot tietokantaan:
        //            TreatmentReport newEntry = new TreatmentReport();
        //            newEntry.Customer_id = customerId;
        //            newEntry.TreatmentReport_id = treatmentrepoId;
        //            newEntry.TreatmentDate = DateTime.Now;

        //            entities.TreatmentReport.Add(newEntry);
        //            entities.SaveChanges();

        //            success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.GetType().Name + ": " + ex.Message;
        //    }
        //    finally
        //    {
        //        entities.Dispose();
        //    }

        //    //palautetaan JSON-muotoinen tulos kutsujalle
        //    var result = new { success = success, error = error };
        //    return Json(result);
        //}
    }//controller
}//namespace
