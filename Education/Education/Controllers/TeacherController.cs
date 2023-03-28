using Education.Ado.BL;
using Education.Data;
using Education.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education.Controllers
{
    public class TeacherController : Controller
    {
		private DBSchoolsEntities db = new DBSchoolsEntities();
		public bool MyProperty { get; set; }
										   // GET: Teacher
		public ActionResult Index()
        {
			var model = GridView();
			return View(model);
		}
		[HttpPost]
		public ActionResult Index(TeacherModel model,FormCollection form )
		{
			var editjobId = form["editjobId"];
			var deletejobId = form["deletejobId"];
			if ( editjobId != null)
			{
				model=SelectTeacher(Convert.ToInt32(editjobId));
				return View(model);
			}
			if (deletejobId !=null)
			{
				var T=db.Teachers.Find(Convert.ToInt32(deletejobId));
				db.Teachers.Remove(T);
				db.SaveChanges();
			}
			else
			{
				if (ModelState.IsValid)
				{
					if (model.Check)
					{
					var teacher = new Teacher {
						Name = model.Name,
						Address = model.Address,
						BirthDate = model.BirthDate,
						Email = model.Email,
						Mobile = model.Mobile,
						SchoolId = model.SchoolId,
						BirthLocation = "",
						Status="",
					
						};
						db.Teachers.Add(teacher);
						db.SaveChanges();
						return RedirectToAction("Index");

					}
					else
					{
						var teacher = new Teacher
						{
							Name = model.Name,
							Address = model.Address,
							BirthDate = model.BirthDate,
							Email = model.Email,
							Mobile = model.Mobile,
							SchoolId = model.SchoolId,
							BirthLocation = "",
							Status = "",

						};
						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}
			}
			return View();
		}
		TeacherModel SelectTeacher(int id)
		{
			var teacher = db.Teachers.Find(id);
			var getdata = GridView();
			TeacherModel model = new TeacherModel();
			model.Name = teacher.Name;
			model.Address = teacher.Address;
			model.Mobile = teacher.Mobile;
			model.SchoolId = teacher.SchoolId;
			model.BirthDate = teacher.BirthDate ?? DateTime.Now;
			model.Email = teacher.Mobile;
			model.Check = false;
			model.TeacherGridView = getdata.TeacherGridView;

			return model;
		}
		TeacherModel GridView()
		{
			TeacherClass j = new TeacherClass();
			var GetTeachers = j.GetTeachers();
			List<TeacherGridRow> listrs = new List<TeacherGridRow>();

			foreach (DataRow dr in GetTeachers.Rows)
			{

				listrs.Add(new TeacherGridRow
				{
					Id=(int)dr["IDTeacher"],
					SchoolId=(int)dr["SchoolId"],
					Mobile=dr["Mobile"].ToString(),
					Name=dr["Name"].ToString(),
					Address=dr["Address"].ToString()
				});

			}
			return new TeacherModel
			{
				Check=true,
				TeacherGridView = listrs,
			};
		}
	}
}