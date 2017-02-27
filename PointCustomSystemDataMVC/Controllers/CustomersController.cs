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

                    //view.Phone_id = customer.Phone.Phone_id;
                    //view.PhoneNum_1 = customer.Phone.PhoneNum_1;

                    //view.Post_id = customer.PostOffices.Post_id;
                    //view.PostOffice = customer.PostOffices.PostalCode;
                    //view.PostOffice = customer.PostOffices.PostOffice;

                    //view.User_id = customer.User.User_id;
                    //view.UserIdentity = customer.User.UserIdentity;

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
        //GET: Customers/Details/5
        public ActionResult Details(int? id)

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
        }//details

    

        // GET: Customers/Create

        //public ActionResult Create()
        //{
        //    ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName");
        //    ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1");
                //    ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode");
        //    ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName");
                //    ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName");
        //    ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName");
                //    ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName");
        //    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity");
        //    ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName");
        //    return View();
        //}



        //// POST: Customers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Customer_id,FirstName,LastName,Identity,Notes,Email,Address,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Customer.Add(customer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", customer.Personnel_id);
        //    ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", customer.Phone_id);
        //    ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", customer.Post_id);
        //    ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", customer.Reservation_id);
        //    ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", customer.Treatment_id);
        //    ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", customer.TreatmentOffice_id);
        //    ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "Treatmentplace_id", "TreatmentPlaceName", customer.TreatmentPlace_id);
        //    ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", customer.User_id);
        //    ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", customer.Student_id);
        //    return View(customer);
        //}


        // GET: Customers/Create
        //public ActionResult Create()
        //{
        //    JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        //    List<User> list = db.User.ToList();
        //    ViewBag.UserSeed = new SelectList(list, "User_id", "UserIdentity");
        //    return View();
        //}//create

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(Customer model)
        //{
        //    JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            //List<User> list = db.User.ToList();
            //ViewBag.UserSeed = new SelectList(list, "User_id", "UserIdentity");

            //User user = new User();
            //user.Customer_id = model.Customer_id;

            ////int? latestUserId = user.User_id;

            ////db.User.Add(user);
            //db.SaveChanges();

            //int latestUseId = user.User_id;

            //Customer cus = new Customer();  
            //cus.FirstName = model.FirstName;
            //cus.LastName = model.LastName;
            //cus.Identity = model.Identity;
            //cus.Address = model.Address;
            //cus.Email = model.Email;
            //cus.Notes = model.Notes;
            //cus.User_id = model.User_id;
            //cus.User_id = latestUseId;
            ////cus.Customer_id = model.Customer_id;
                 
            //db.Customer.Add(cus);
            //db.SaveChanges();

            //int latestCusId = cus.Customer_id;
            
            //Phone pho = new Phone();
            //pho.PhoneNum_1 = model.PhoneNum_1;
            //pho.Customer_id = latestCusId;
            //pho.Customer_id = model.Customer_id;
          
            //db.Phone.Add(pho);
            //db.SaveChanges();

            //int latestPhoId = cus.Customer_id;
            //int latestPhoneId = pho.Phone_id;

            //PostOffices pos = new PostOffices();       
            //pos.PostalCode = model.PostalCode;
            //pos.PostOffice = model.PostOffice;    
            //pos.Customer_id = latestCusId;
            //pos.Customer_id = model.Customer_id;

            //db.PostOffices.Add(pos);
            //db.SaveChanges();

            //int latestPosId = cus.Customer_id;
            //int latestPostId = pos.Post_id;

            //return View(model);
       /* }//cr*/





        // GET: Customers/Edit/5

        public ActionResult Edit(int? id)

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

            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", customer.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", customer.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", customer.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", customer.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", customer.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", customer.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "TreatmentPlace_id", "TreatmentPlaceName", customer.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", customer.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", customer.Student_id);

            return View(customer);

        }//edit

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Customer_id,FirstName,LastName,Identity,Notes,Email,Address,Personnel_id,Phone_id,Post_id,Reservation_id,Student_id,Treatment_id,TreatmentOffice_id,TreatmentPlace_id,User_id")] Customer customer)
        {
            if (ModelState.IsValid)

            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Personnel_id = new SelectList(db.Personnel, "Personnel_id", "FirstName", customer.Personnel_id);
            ViewBag.Phone_id = new SelectList(db.Phone, "Phone_id", "PhoneNum_1", customer.Phone_id);
            ViewBag.Post_id = new SelectList(db.PostOffices, "Post_id", "PostalCode", customer.Post_id);
            ViewBag.Reservation_id = new SelectList(db.Reservation, "Reservation_id", "TreatmentName", customer.Reservation_id);
            ViewBag.Treatment_id = new SelectList(db.Treatment, "Treatment_id", "TreatmentName", customer.Treatment_id);
            ViewBag.TreatmentOffice_id = new SelectList(db.TreatmentOffice, "TreatmentOffice_id", "TreatmentOfficeName", customer.TreatmentOffice_id);
            ViewBag.TreatmentPlace_id = new SelectList(db.TreatmentPlace, "TreatmentPlace_id", "TreatmentPlaceName", customer.TreatmentPlace_id);
            ViewBag.User_id = new SelectList(db.User, "User_id", "UserIdentity", customer.User_id);
            ViewBag.Student_id = new SelectList(db.Studentx, "Student_id", "FirstName", customer.Student_id);

            return View(customer);
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
                List<Customer> customers = entities.Customer.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Customer custome in customers)
                {
                    CustomerViewModel custo = new CustomerViewModel();
                    custo.Customer_id = custome.Customer_id;
                    custo.FirstName = custome.FirstName;
                    custo.LastName = custome.LastName;
                    custo.TreatmentReport= custome.TreatmentReport;
                    //custo.LastSeen = custome.LastSeen.Value.ToString(fiFi);

                    model.Add(custo);
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
                List<Customer> customers = entities.Customer.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (Customer custome in customers)
                {
                    CustomerViewModel custo = new CustomerViewModel();
                    custo.Customer_id = custome.Customer_id;
                    custo.FirstName = custome.FirstName;
                    custo.LastName = custome.LastName;
                    custo.TreatmentReport = custome.TreatmentReport;
                    //custo.LastSeen = custome.LastSeen.Value.ToString(fiFi);

                    model.Add(custo);
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
                                  where c.TreatmentReport == inputData.TreatmentReport
                                  select c.Customer_id).FirstOrDefault();
              

                ////haetaan laitteen id-numero koodin perusteella:
                //int assetId = (from a in entities.Assets
                //               where a.Code == inputData.AssetCode
                //               select a.Id).FirstOrDefault();

                if ((customerId > 0)) /*&& (assetId > 0))*/
                {
                    //tallennetaan asikaan hoitotiedot tietokantaan:
                    Customer newEntry = new Customer();
                    newEntry.Customer_id = customerId;
                    newEntry.TreatmentReport = TreatmentReport;
                    //newEntry.AssetId = assetId;
                    //newEntry.LastSeen = DateTime.Now;

                    entities.Customer.Add(newEntry);
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


