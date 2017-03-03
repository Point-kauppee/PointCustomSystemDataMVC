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

namespace PointCustomSystemDataMVC.Controllers

{
    public class CustomersController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Customers
        public ActionResult Index()
        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Customer> customers = entities.Customer.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Customer customer in customers)
                {
                    CustomerViewModel view = new CustomerViewModel();
                    view.Customer_id = customer.Customer_id;
                    view.FirstName = customer.FirstName;
                    view.LastName = customer.LastName;
                    view.Identity = customer.Identity;
                    view.Email = customer.Email;
                    view.Address = customer.Address;
                    view.Notes = customer.Notes;

                    view.Phone_id = customer.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = customer.Phone?.FirstOrDefault()?.PhoneNum_1;

                    view.Post_id = customer.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = customer.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = customer.PostOffices?.FirstOrDefault()?.PostOffice;

                    view.User_id = customer.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = customer.User.FirstOrDefault()?.UserIdentity;

                    //haetaan seuraava varaus:
                    view.Reservation_id = customer.Reservation?.FirstOrDefault()?.Reservation_id;
                    view.Start = customer.Reservation?.FirstOrDefault()?.Start.Value;
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
        }

        //31.1.2017 Lisätty tietokantataulujen suodatukset:
        //public ActionResult Index(string sortOrder)
        //{
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.NamSortParm = sortOrder == "Nam" ? "nam_desc" : "Nam";
        //    var customer = from c in db.Customer
        //                   select c;
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            customer = customer.OrderByDescending(c => c.FirstName);
        //            break;
        //        case "Nam":
        //            customer = customer.OrderBy(c => c.LastName);
        //            break;
        //        case "nam_desc":
        //            customer = customer.OrderByDescending(c => c.LastName);
        //            break;
        //        default:
        //            customer = customer.OrderBy(c => c.FirstName);
        //            break;
        //    }

        //    return View(customer.ToList());
        //}

            //Lisätty 1.3.2017 oma koodi:
        //GET: Customers/Details/5
        public ActionResult Details(int? id)

        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Customer> customers = entities.Customer.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Customer custdetail in customers)
                {
                    CustomerViewModel view = new CustomerViewModel();
                    view.Customer_id = custdetail.Customer_id;
                    view.FirstName = custdetail.FirstName;
                    view.LastName = custdetail.LastName;
                    view.Identity = custdetail.Identity;
                    view.Email = custdetail.Email;
                    view.Address = custdetail.Address;
                    view.Notes = custdetail.Notes;

                    view.Phone_id = custdetail.Phone?.FirstOrDefault()?.Phone_id;
                    view.PhoneNum_1 = custdetail.Phone?.FirstOrDefault()?.PhoneNum_1;
                    view.Post_id = custdetail.PostOffices?.FirstOrDefault()?.Post_id;
                    view.PostalCode = custdetail.PostOffices?.FirstOrDefault()?.PostalCode;
                    view.PostOffice = custdetail.PostOffices?.FirstOrDefault()?.PostOffice;

                    view.User_id = custdetail.User?.FirstOrDefault()?.User_id;
                    view.UserIdentity = custdetail.User.FirstOrDefault()?.UserIdentity;

                    view.Reservation_id = custdetail.Reservation?.FirstOrDefault()?.Reservation_id;
                    view.Start = custdetail.Reservation?.FirstOrDefault()?.Start.Value;
                    view.End = custdetail.Reservation?.FirstOrDefault()?.End.Value;
                    view.Date = custdetail.Reservation?.FirstOrDefault()?.Date.Value;

                    model.Add(view);
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

            return View(model);

        }//details

        
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       

        //Oma kaava:
        // GET: Customers/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();
    
            //ViewBag.UserSeed = new SelectList(list, "User_id", "UserIdentity");
            CustomerViewModel model = new CustomerViewModel();
            return View(model);
        }//create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            ////ViewBag.UserSeed = new SelectList(list, "User_id", "UserIdentity");

            Customer cus = new Customer();
            cus.FirstName = model.FirstName;
            cus.LastName = model.LastName;
            cus.Identity = model.Identity;
            cus.Address = model.Address;
            cus.Email = model.Email;
            cus.Notes = model.Notes;

            db.Customer.Add(cus);
           
            User usr = new User();
            usr.UserIdentity = model.UserIdentity;
            usr.Password = "Customer";
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

            try { db.SaveChanges(); }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//cr*/


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
            view.FirstName = custdetail.FirstName;
            view.LastName = custdetail.LastName;
            view.Identity = custdetail.Identity;
            view.Email = custdetail.Email;
            view.Address = custdetail.Address;
            view.Notes = custdetail.Notes;

            view.Phone_id = custdetail.Phone?.FirstOrDefault()?.Phone_id;
            view.PhoneNum_1 = custdetail.Phone?.FirstOrDefault()?.PhoneNum_1;
            view.Post_id = custdetail.PostOffices?.FirstOrDefault()?.Post_id;
            view.PostalCode = custdetail.PostOffices?.FirstOrDefault()?.PostalCode;
            view.PostOffice = custdetail.PostOffices?.FirstOrDefault()?.PostOffice;

            view.User_id = custdetail.User?.FirstOrDefault()?.User_id;
            view.UserIdentity = custdetail.User.FirstOrDefault()?.UserIdentity;

            view.Reservation_id = custdetail.Reservation?.FirstOrDefault()?.Reservation_id;
            view.Start = custdetail.Reservation?.FirstOrDefault()?.Start.Value;
            view.End = custdetail.Reservation?.FirstOrDefault()?.End.Value;
            view.Date = custdetail.Reservation?.FirstOrDefault()?.Date.Value;
            return View(view);

        }//edit

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel model)

        {
            Customer cus = db.Customer.Find(model.Customer_id);

            cus.FirstName = model.FirstName;
            cus.LastName = model.LastName;
            cus.Identity = model.Identity;
            cus.Address = model.Address;
            cus.Email = model.Email;
            cus.Notes = model.Notes;

            if (cus.Phone == null)
            {
                Phone pho = new Phone();
                pho.PhoneNum_1 = model.PhoneNum_1;
                pho.Customer = cus;

                db.Phone.Add(pho);
            }
            else
            {
                cus.Phone.FirstOrDefault().PhoneNum_1 = model.PhoneNum_1;
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

                if (po != null) { 
                    po.PostalCode = model.PostalCode;
                po.PostOffice = model.PostOffice;
                }
            }
    
            db.SaveChanges();
            return View(model);

        }//edit

        // GET: Customers/Delete/5

        public ActionResult Delete(int? id)
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
            return View(customer);
        }//delete

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
        }//dispose



        //ASIAKKAAN HOITOTIETOJEN TALLENNUS

        //tehdään listaus kaikista kytkennöistä
        public ActionResult AddNotes()
        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<TreatmentReport> treatrepos = entities.TreatmentReport.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (TreatmentReport treat in treatrepos)
                {
                    CustomerViewModel cusw = new CustomerViewModel();
                    cusw.Customer_id = treat.Customer.Customer_id;
                    cusw.TreatmentReport_id = treat.TreatmentReport_id;
                    cusw.TreatmentReportName = treat.TreatmentReportName;
                    cusw.TreatmentTime = treat.TreatmentTime.Value;
                    cusw.TreatmentDate = treat.TreatmentDate.Value;
                    //custo.LastSeen = custome.LastSeen.Value.ToString(fiFi);

                    model.Add(cusw);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }

        public ActionResult ListJson()

        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<TreatmentReport> treatrepos = entities.TreatmentReport.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (TreatmentReport treat in treatrepos)
                {
                    CustomerViewModel cusw = new CustomerViewModel();
                    cusw.Customer_id = treat.Customer.Customer_id;
                    cusw.TreatmentReport_id = treat.TreatmentReport_id;
                    cusw.TreatmentReportName = treat.TreatmentReportName;
                    cusw.TreatmentTime = treat.TreatmentTime.Value;
                    cusw.TreatmentDate = treat.TreatmentDate.Value;
                    //custo.LastSeen = custome.LastSeen.Value.ToString(fiFi);

                    model.Add(cusw);
                }
            }

            finally

            {
                entities.Dispose();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //CustomerViewModel.cs - ASIAKASRAPORTIN TALLENTAMINEN (SQL) TIETOKANTAAN
        public JsonResult SavedReport(string TreatmentReport)
        {
            string json = Request.InputStream.ReadToEnd();
            CustomerViewModel inputData =
                JsonConvert.DeserializeObject<CustomerViewModel>(json);

            bool success = false;
            string error = "";

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                //haetaan ensin asiakkaan id-numero koodin perusteella:
                int customerId = (from c in entities.Customer
                                  where c.Customer_id == inputData.Customer_id
                                  select c.Customer_id).FirstOrDefault();


                //haetaan TreatmentReport id-numero koodin perusteella:
                int treatmentrepoId = (from t in entities.TreatmentReport
                                       where t.TreatmentReportText == inputData.TreatmentReportText
                                       select t.TreatmentReport_id).FirstOrDefault();

                if ((customerId > 0) && (treatmentrepoId > 0))
                {
                    //tallennetaan asikaan hoitotiedot tietokantaan:
                    TreatmentReport newEntry = new TreatmentReport();
                    newEntry.Customer_id = customerId;
                    newEntry.TreatmentReport_id = treatmentrepoId;
                    newEntry.TreatmentDate = DateTime.Now;

                    entities.TreatmentReport.Add(newEntry);
                    entities.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.GetType().Name + ": " + ex.Message;
            }
            finally
            {
                entities.Dispose();
            }

            //palautetaan JSON-muotoinen tulos kutsujalle
            var result = new { success = success, error = error };
            return Json(result);
        }
    }//controller
}//namespace
