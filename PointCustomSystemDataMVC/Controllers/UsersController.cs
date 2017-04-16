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
    public class UsersController : Controller
    {
        //[Authorize(Roles = "Personnel User")]
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Users
        public ActionResult Index()
        {
            //string username = User.Identity.Name;
            //string userid = ((ClaimsPrincipal)User).Claims?.Where(c => c.Type == ClaimTypes.GroupSid).FirstOrDefault()?.Value ?? "";

            List<User> model = new List<User>();

            try
            { 
                 JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
                    model = entities.User.ToList();
                    entities.Dispose();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.GetType() + ": " + ex.Message;
            }
            //malliolion (model) välitys näkymälle- antaa instanssin asiakastietolistasta:
            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();
  
            List<User> model = new List<User>();
            return View(model);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            User usr = new User();   
            usr.UserIdentity = model.UserIdentity;
            usr.Password = "Customer";
        
            db.User.Add(usr);
 
            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }
    
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User userdetail = db.User.Find(id);
            if (userdetail == null)
            {
                return HttpNotFound();
            }

            User usr = new User();
            usr.User_id = userdetail.User_id;
            usr.UserIdentity = userdetail.UserIdentity;
            //usr.Password = "Customer";

            return View(usr);

    }//edit

    // POST: Users/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            User usr = db.User.Find(model.User_id);

            usr.UserIdentity = model.UserIdentity;

            db.SaveChanges();
    
            return RedirectToAction("Index");

        }//edit

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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


