using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Education.Data;

namespace Education.Controllers
{
    public class SchoolsTechController : Controller
    {
        private DBSchoolsEntities db = new DBSchoolsEntities();

        // GET: SchoolsTech
        public ActionResult Index()
        {
            return View(db.Schools.ToList());
        }

        // GET: SchoolsTech/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: SchoolsTech/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolsTech/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchoolId,IDRef,school_Code,School_Name,logo,Mobile1,Mobile2,Phone,Email,Type,Area,TimeStartWork,TimeEndWork,VacationNumber,StatisticaFigure,EducationalLevel,Office,Address,TotalClass,TotalStudents,TotalStaff,RentedBuildingsNo,GovernmentBuildingsNo,YearFounded,IsActive,IsDeleted,FaceBook,Notes")] School school,string Type)
        {
            if (ModelState.IsValid)
            {
                school.Type = Convert.ToInt32(Type);
                db.Schools.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(school);
        }

        // GET: SchoolsTech/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: SchoolsTech/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchoolId,IDRef,school_Code,School_Name,logo,Mobile1,Mobile2,Phone,Email,Type,Area,TimeStartWork,TimeEndWork,VacationNumber,StatisticaFigure,EducationalLevel,Office,Address,TotalClass,TotalStudents,TotalStaff,RentedBuildingsNo,GovernmentBuildingsNo,YearFounded,IsActive,IsDeleted,FaceBook,Notes")] School school,string Type)
        {
            if (ModelState.IsValid)
            {
                school.Type =Convert.ToInt32(Type);
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(school);
        }

        // GET: SchoolsTech/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: SchoolsTech/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = db.Schools.Find(id);
            db.Schools.Remove(school);
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
