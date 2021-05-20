using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployCRUDEntities db = new EmployCRUDEntities();

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.tbl_Employee.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Gender,Password,ConfirmPassword,DOB")] tbl_Employee tbl_Employee)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Employee.Add(tbl_Employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult Check(int ?Id)

        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(Id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Gender,DOB")] tbl_Employee tbl_Employee)

        {

            tbl_Employee tbl_EmployeeExists = db.tbl_Employee.Find(tbl_Employee.Id);
            ModelState.Where(m => m.Key == "Password").FirstOrDefault().Value.Errors.Clear();
            ModelState.Where(m => m.Key == "ConfirmPassword").FirstOrDefault().Value.Errors.Clear();
            if (ModelState.IsValid)
            {
                //db.Entry(tbl_Employee).State = EntityState.Modified;
                //db.tbl_Employee.Remove(tbl_EmployeeExists);
                //db.tbl_Employee.Attach(tbl_Employee);
                tbl_Employee.Password = tbl_EmployeeExists.Password;
                db.Set<tbl_Employee>().AddOrUpdate(tbl_Employee);
                db.Configuration.ValidateOnSaveEnabled = false;
                ///                db.Entry(tbl_Employee).Property(x => x.ConfirmPassword  ).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(id);
            db.tbl_Employee.Remove(tbl_Employee);
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
        public JsonResult IsAlreadySignedUpEmployee(string EmailId)
        {
            tbl_Employee tbl_Employee = new tbl_Employee();

            using (var context = new EmployCRUDEntities())
            {
                tbl_Employee = context.tbl_Employee.Where(a => a.Email.ToLower() == EmailId.ToLower()).FirstOrDefault();
            }


            bool status;
            if (tbl_Employee != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return Json(status, JsonRequestBehavior.AllowGet);

        }
    }
}
