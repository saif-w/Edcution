using Education.Ado.BL;
using Education.Data;
using Education.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education.Controllers
{
    public class SchoolHomeController : Controller
    {
		// GET: SchoolHome
		private DBSchoolsEntities Db = new DBSchoolsEntities();
		public ActionResult Index()
        {
			var UserName = this.Session["Username"];
			if (UserName == null)
			{
				return RedirectToAction("Index", "Home");
			}
			int count = Db.Teachers.Count();
			var model = new HomeModel
			{
				SchoolCount = count,
			};
			//***********ChartTest
				List<object> data = new List<object>();

				List<string> Label = new List<string>();

				List<Int32> value = new List<Int32>();
				int counter = 0;
				foreach (var d in Db.Schools)
				{
					Label.Add(d.School_Name);


					var Teacher = Db.Teachers.Where(a => a.SchoolId == d.SchoolId).Count();
					value.Add(Teacher);

					counter++;
					if (counter == 5)
					{
						break;
					}
				}
				data.Add(value);
				data.Add(Label);

				ViewBag.Value = value;
				ViewBag.Label = Label;
			//**********end
			return View(model);
        }
		public ActionResult IndexSchool()
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
            if (UserName == null)
			{
				return RedirectToAction("Index", "Home");
			}
			SchoolClass sc = new SchoolClass();
			TeacherClass tc = new TeacherClass();
			var id = sc.GetSchool(UserName.ToString());
			if (Convert.ToInt32(id.Rows[0][0]) != null)
			{
				var schoolid = Convert.ToInt32(id.Rows[0][0]);
				int count = tc.GetTeacherswithSchool(schoolid).Rows.Count;
				
				var model=new HomeModel
				{
					SchoolCount = count,
				};
				return View(model);
			}
			return View();
		}
		public JsonResult GetSchoolData()
		{
			List<object> chData = new List<object>();

			var UserName = this.Session["Username"];
			List<string> Label = new List<string>();

			List<Int32> value = new List<Int32>();
			int counter = 0;
			foreach (var d in Db.Schools)
			{
				Label.Add(d.School_Name);
				

				var Teacher= Db.Teachers.Where(a=>a.SchoolId== d.SchoolId).Count();
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
	}
}