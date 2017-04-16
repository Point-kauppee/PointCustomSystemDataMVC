using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointCustomSystemDataMVC.Models;
using PointCustomSystemDataMVC.ViewModels;

namespace PointCustomSystemDataMVC.Controllers
{
    public class StudentGroupsController : Controller
    {
        //[Authorize(Roles = "Personnel User")]
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: StudentGroups
        public ActionResult Index()
        {

            //string username = User.Identity.Name;
            //string userid = ((ClaimsPrincipal)User).Claims?.Where(c => c.Type == ClaimTypes.GroupSid).FirstOrDefault()?.Value ?? "";

            List<StudentGroupViewModel> model = new List<StudentGroupViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<StudentGroup> treatoffs = entities.StudentGroup.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                foreach (StudentGroup stg in treatoffs)
                {
                    StudentGroupViewModel view = new StudentGroupViewModel();
                    view.StudentGroup_id = stg.StudentGroup_id;
                    view.StudentGroupName = stg.StudentGroupName;
                    view.Active = stg.Active;
                    view.CreatedAt = stg.CreatedAt;
                    view.LastModifiedAt = stg.LastModifiedAt;
                    view.DeletedAt = stg.DeletedAt;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index

        // GET: StudentGroups/Details/5
        public ActionResult Details(int? id)
        {
            StudentGroupViewModel model = new StudentGroupViewModel();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<StudentGroup> stugs = entities.StudentGroup.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (StudentGroup stg in stugs)
                {
                    StudentGroupViewModel view = new StudentGroupViewModel();
                    view.StudentGroup_id = stg.StudentGroup_id;
                    view.StudentGroupName = stg.StudentGroupName;
                    view.Active = stg.Active;
                    view.CreatedAt = stg.CreatedAt;
                    view.LastModifiedAt = stg.LastModifiedAt;
                    view.DeletedAt = stg.DeletedAt;

                    model = view;
                }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGroup studentGroup = db.StudentGroup.Find(id);
            if (studentGroup == null)
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

        // GET: StudentGroups/Create
        public ActionResult Create()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            StudentGroupViewModel model = new StudentGroupViewModel();

            return View();
        }//create

        // POST: StudentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            StudentGroup stug = new StudentGroup();
            stug.StudentGroupName = model.StudentGroupName;
            stug.Active = model.Active;
            stug.CreatedAt = DateTime.Now;
            stug.LastModifiedAt = DateTime.Now;
            stug.DeletedAt = model.DeletedAt;

            db.StudentGroup.Add(stug);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create

        // GET: StudentGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGroup stg = db.StudentGroup.Find(id);
            if (stg == null)
            {
                return HttpNotFound();
            }

            StudentGroupViewModel view = new StudentGroupViewModel();
            view.StudentGroup_id = stg.StudentGroup_id;
            view.StudentGroupName = stg.StudentGroupName;
            view.Active = stg.Active;
            view.CreatedAt = stg.CreatedAt;
            view.LastModifiedAt = DateTime.Now; ;
            view.DeletedAt = stg.DeletedAt;

            return View(view);
        }//edit

        // POST: StudentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel model)
        {
            StudentGroup stug = db.StudentGroup.Find(model.StudentGroup_id);
            stug.StudentGroupName = model.StudentGroupName;
            stug.Active = model.Active;
            stug.CreatedAt = model.CreatedAt;
            stug.LastModifiedAt = DateTime.Now;
            stug.DeletedAt = model.DeletedAt;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }//Edit


        // GET: StudentGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGroup stg = db.StudentGroup.Find(id);
            if (stg == null)
            {
                return HttpNotFound();
            }

            StudentGroupViewModel view = new StudentGroupViewModel();
            view.StudentGroup_id = stg.StudentGroup_id;
            view.StudentGroupName = stg.StudentGroupName;
            view.Active = stg.Active;
            view.CreatedAt = stg.CreatedAt;
            view.LastModifiedAt = stg.LastModifiedAt;
            view.DeletedAt = stg.DeletedAt;

            return View(view);
        }//Delete

        // POST: StudentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentGroup studentGroup = db.StudentGroup.Find(id);
            db.StudentGroup.Remove(studentGroup);
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
