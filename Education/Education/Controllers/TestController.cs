using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Education.Data;
using Education.Models;
using System.Net.Mail;

namespace Education.Controllers
{
	public class TestController : Controller
	{
		private DBSchoolsEntities db = new DBSchoolsEntities();

        public JsonResult GetSchoolData()
        {
            List<object> chData = new List<object>();

            var UserName = this.Session["Username"];
            List<string> Label = new List<string>();

            List<Int32> value = new List<Int32>();
            int counter = 0;
            foreach (var d in db.Schools)
            {
                Label.Add(d.School_Name);


                var Teacher = db.Teachers.Where(a => a.SchoolId == d.SchoolId).Count();
                value.Add(Teacher);

                counter++;
                if (counter == 5)
                    break;

            }
            chData.Add(Label);
            chData.Add(value);

            //return data;
            return Json(chData, JsonRequestBehavior.AllowGet);
        }
        // GET: Test
        public ActionResult Index()
		{
			var teachers = db.Teachers.Include(t => t.School);
			return View(teachers.ToList());
		}

		// GET: Test/Details/5
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

		// GET: Test/Create
		public ActionResult Create()
		{
			ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "school_Code");
			return View();
		}

		// POST: Test/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "IDTeacher,SchoolId,Name,BirthLocation,BirthDate,Status,Sons,SonsYounger6,SonsYounger24,SonsOLder24,Email,MailBox,PostalCode,Mobile,Telephon1,Telephon2,Address")] Teacher teacher)
		{
			if (ModelState.IsValid)
			{
				db.Teachers.Add(teacher);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "school_Code", teacher.SchoolId);
			return View(teacher);
		}

		// GET: Test/Edit/5
		public ActionResult Edit(int? id)
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
			ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "school_Code", teacher.SchoolId);
			return View(teacher);
		}

		// POST: Test/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "IDTeacher,SchoolId,Name,BirthLocation,BirthDate,Status,Sons,SonsYounger6,SonsYounger24,SonsOLder24,Email,MailBox,PostalCode,Mobile,Telephon1,Telephon2,Address")] Teacher teacher)
		{
			if (ModelState.IsValid)
			{
				db.Entry(teacher).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "school_Code", teacher.SchoolId);
			return View(teacher);
		}

		// GET: Test/Delete/5
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

		// POST: Test/Delete/5
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

		public ActionResult Messages()
		{
			return View();
		}
        [HttpPost]
        public ActionResult Messages(MessagesModel model)
        {
            var mail = new MailMessage();
            var loginfo = new NetworkCredential("www.saifallh.com@gmail.com", "");
            mail.From = new MailAddress(model.Emile);
            mail.To.Add(new MailAddress("www.saifallh.com@gmail.com"));
            mail.Subject = model.Subject;
            mail.Body = model.Message;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginfo;
            smtpClient.Send(mail);
            return View();
        }
        public ActionResult Result()
		{
			return View();
		}
		public ActionResult Support()
		{
			return View();
		}
		public ActionResult Charts()
		{
			return View();
		}

	}
}
