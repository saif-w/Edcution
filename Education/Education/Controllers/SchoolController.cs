using Education.Data;
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
    public class SchoolController : Controller
    {
		private DBSchoolsEntities db = new DBSchoolsEntities();
		// GET: School
		public ActionResult Index()
        {
			var model = GridView();
			return View(model);
        }
		[HttpPost]
		public ActionResult Index(SchoolModel model)
		{
			
			if(model!=null)
			{
				SchoolClass sc = new SchoolClass();
				sc._insert(model.SchoolCode, model.SchoolName, model.Phone, Convert.ToInt32(model.Type), model.Active);
				return RedirectToAction("Index");
			}
			return View();
		}
		SchoolModel GridView()
		{
			SchoolClass j = new SchoolClass();
			var schoolGrid = j.GetSchools();
			List<SchoolGridView> listrs = new List<SchoolGridView>();

			foreach (DataRow dr in schoolGrid.Rows)
			{

				listrs.Add(new SchoolGridView
				{
					SchoolId = (int)dr["SchoolId"],
					SchoolCode = dr["school_Code"].ToString(),
					SchoolName = dr["School_Name"].ToString(),
					Phone = dr["Phone"].ToString(),

				});

			}
			return new SchoolModel
			{
				SchoolGrid = listrs,
			};
		}
		public ActionResult Update()
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
			var id=sc.GetSchool(UserName.ToString());
			if(Convert.ToInt32(id.Rows[0][0])!=0)
			{

			var school=sc.GetSchoolsID(Convert.ToInt32(id.Rows[0][0]));
				var model=new SchoolModel
				{
					SchoolId=Convert.ToInt32(school.Rows[0][0]),
					SchoolName =school.Rows[0][3].ToString(),
					SchoolCode= school.Rows[0][2].ToString(),
					Phone= school.Rows[0][7].ToString(),
                    Adress= school.Rows[0][17].ToString(),
                    Emile= school.Rows[0][8].ToString(),
                    Office = school.Rows[0][16].ToString(),
                };
				return View(model);
			}
			return View();
		}
		[HttpPost]
		public ActionResult Update(SchoolModel model,int Type)
		{
			if (model != null)
			{
				var schoolupdate=db.Schools.Find(model.SchoolId);


				schoolupdate.School_Name = model.SchoolName;
				schoolupdate.school_Code = model.SchoolCode;
				schoolupdate.Phone = model.Phone;
				schoolupdate.IsActive = model.Active;
                schoolupdate.Type = Type;
                schoolupdate.Office = model.Office;
                schoolupdate.Email = model.Emile;
                schoolupdate.Address = model.Adress;

                db.SaveChanges();
			}
			return RedirectToAction("IndexSchool", "SchoolHome");
		}
	}
}