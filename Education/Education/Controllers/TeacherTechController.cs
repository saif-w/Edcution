using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Education.Data;
using Education.Ado.BL;

namespace Education.Controllers
{
    public class TeacherTechController : Controller
    {
        private DBSchoolsEntities db = new DBSchoolsEntities();

        // GET: TeacherTech
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string UserName = "";
            if (reqCookies != null)
            {
                UserName = reqCookies["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //var UserName = this.Session["Username"];
            if (UserName == "")
            {
                TempData["shortMessage"] = "يوجد خطاء في العرض";
                
                return RedirectToAction("index", "Home");
            }
			SchoolClass sc = new SchoolClass();
			var id = Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);
            var data = db.Teachers.ToList().Where(a => a.Status == "" && a.SchoolId == id);

            return View(data);
        }

        // GET: TeacherTech/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: TeacherTech/Create
        public ActionResult Create()
        {
            //var UserName = this.Session["Username"];
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string UserName = "";
            if (reqCookies != null)
            {
                UserName = reqCookies["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            SchoolClass sc = new SchoolClass();
			var id =Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);
			ViewBag.SchoolId = new SelectList(db.Schools.Where(a=>a.SchoolId==id), "SchoolId", "School_Name");
			return View();
        }

        // POST: TeacherTech/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTeacher,SchoolId,Name,BirthLocation,BirthDate,Status,Sons,SonsYounger6,SonsYounger24,SonsOLder24,Email,MailBox,PostalCode,Mobile,Telephon1,Telephon2,Address,Religion,NationalCard,YearsExperice,TypeWork")] Teacher teacher)
        {

            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string UserName = "";
            if (reqCookies != null)
            {
                UserName = reqCookies["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                teacher.SepName = "";
                teacher.Status = "";
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			//var UserName = this.Session["Username"];
            
            SchoolClass sc = new SchoolClass();
			var id = Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);
			ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.SchoolId == id), "SchoolId", "School_Name", teacher.SchoolId);
			return View(teacher);
        }

        // GET: TeacherTech/Edit/5
        public ActionResult Edit(int? id)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string UserName = "";
            if (reqCookies != null)
            {
                UserName = reqCookies["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            SchoolClass sc = new SchoolClass();
			var idschool = Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);
			ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.SchoolId == idschool), "SchoolId", "School_Name");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: TeacherTech/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTeacher,SchoolId,Name,BirthLocation,BirthDate,Status,Sons,SonsYounger6,SonsYounger24,SonsOLder24,Email,MailBox,PostalCode,Mobile,Telephon1,Telephon2,Address,Religion,NationalCard,YearsExperice,TypeWork,")] Teacher teacher)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string UserName = "";
            if (reqCookies != null)
            {
                UserName = reqCookies["UserName"].ToString();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                teacher.Status = "";
                teacher.SepName = "";
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            SchoolClass sc = new SchoolClass();
			var idschool = Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);
			ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.SchoolId == idschool), "SchoolId", "School_Name");

			return View(teacher);
        }

        // GET: TeacherTech/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: TeacherTech/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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

		public ActionResult TeachersActive()
		{
			return View(db.Teachers.ToList());
		}
		[HttpPost]
		public ActionResult TeachersActive(int editTEacherId)
		{
			if (editTEacherId != null)
			{
				var teacher=db.Teachers.Find(editTEacherId);
				teacher.Status = teacher.Status==""? "1":"";
				db.SaveChanges();
				return RedirectToAction("TeachersActive");
			}
			return View();
		}
	}
}
