using Education.Ado.BL;
using Education.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
        public ActionResult Index()
		{
			var model = GridView();
			return View(model);
		}
		JobModel GridView()
		{
			JobsClass j = new JobsClass();
			var JobGrid = j.GetJobs();
			List<JobGridRow> listrs = new List<JobGridRow>();

			foreach (DataRow dr in JobGrid.Rows)
			{

				listrs.Add(new JobGridRow
				{
					Id = (int)dr["Id"],
					Description = dr["Description"].ToString(),
					FileLocation = dr["FileLocation"].ToString(),
					Name = dr["Name"].ToString(),
					
				});

			}
			return new JobModel
			{
				 JobsGrid= listrs,
			};
		}
		
	}
}