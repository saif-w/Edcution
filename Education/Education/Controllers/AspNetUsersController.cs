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
using Education.Ado.BL;

namespace Education.Controllers
{
    public class AspNetUsersController : Controller
    {
        private DBSchoolsEntities db = new DBSchoolsEntities();

        // GET: AspNetUsers
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
			ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "School_Name",0,"test");

			return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model,string gridCheck1,string confPassord)
        {
			
            if (ModelState.IsValid)
            {
				string name = db.AspNetUsers.Where(a => a.UserName == model.UserName).Count() > 1 ? db.AspNetUsers.Where(a => a.UserName == model.UserName).FirstOrDefault().UserName : "";
				if (model.UserName == name)
				{
					ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "School_Name", 0, "test");

					ViewBag.message = "اسم المستخدم موجود مسبقا";
					return View();
				}
				if (confPassord != model.Pass)
				{
					ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "School_Name", 0, "test");

					ViewBag.message = "الرجاء مطابقة كلمة السر";
					return View();
				}
				
				if (gridCheck1 != null)
				{
					var aspNetUser1 = new AspNetUser
					{
						Email = model.Emile,
						Password = model.Pass,
						UserName = model.UserName,
						PhoneNumber = model.Phone,
						SchoolId = model.SchoolId,
					};
					db.AspNetUsers.Add(aspNetUser1);
				}
				else
				{
					var aspNetUser1 = new AspNetUser
					{
						Email = model.Emile,
						Password = model.Pass,
						UserName = model.UserName,
						PhoneNumber = model.Phone,
						SchoolId = 0,
					};
					db.AspNetUsers.Add(aspNetUser1);
				}
				
				
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(int? id)
        {
			UserModel um = new UserModel();
			if (id == null)
            {
				ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "School_Name", 0, "test");

				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			
			AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
				var model = db.AspNetUsers.Find(id);

				um.Active = model.UR_IsActive??false;
				um.Emile = model.Email;
				um.userid = id??0;
				um.UserName = model.UserName;
				um.Pass = model.Password;
				um.Phone = model.PhoneNumber;

				ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "School_Name", 0, "test");

				return HttpNotFound();
            }
			if (id != null)
			{
				var model = db.AspNetUsers.Find(id);

			ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "School_Name", 0, "test");
			um.Active = model.UR_IsActive ?? false;
			um.Emile = model.Email;
			um.userid = id ?? 0;
			um.UserName = model.UserName;
			um.Pass = model.Password;
			um.Phone = model.PhoneNumber;
				return View(um);
			}
			return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model, string gridCheck1)
        {
            if (ModelState.IsValid)
            {
				SchoolClass sc = new SchoolClass();
				if (gridCheck1 != null)
				{
					var aspNetUser1 = db.AspNetUsers.Find(model.userid);
					 aspNetUser1 = new AspNetUser
					{
						Email = model.Emile,
						Password = model.Pass,
						UserName = model.UserName,
						PhoneNumber = model.Phone,
						SchoolId = model.SchoolId,
					};
					sc._update_User(model.userid, model.SchoolId, model.UserName, model.Pass, model.Emile, model.Phone, model.Active);
					//db.SaveChanges();
					//db.Entry(aspNetUser1).State = EntityState.Modified;
				}
				else
				{
					var aspNetUser1 = db.AspNetUsers.Find(model.userid);

					 aspNetUser1 = new AspNetUser
					{
						Email = model.Emile,
						Password = model.Pass,
						UserName = model.UserName,
						PhoneNumber = model.Phone,
						SchoolId = 0,
					};
					sc._update_User(model.userid, model.SchoolId, model.UserName, model.Pass, model.Emile, model.Phone, model.Active);

					//db.SaveChanges();
					////db.Entry(aspNetUser1).State = EntityState.Modified;
				}

				
              
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
