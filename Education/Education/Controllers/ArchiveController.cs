using Education.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education.Controllers
{
    public class ArchiveController : Controller
    {
        // GET: Archive
        public ActionResult Index()
        {
            return View();
        }
		[HttpPost]
		
		public virtual ActionResult Index(ArchiveModel model, bool? uploadAvatar,FormCollection form ,IEnumerable<HttpPostedFileBase> uploadedImages)
		{
			var img = form["uploadedImages"];
			if (uploadAvatar ?? false)
				return Upload(model, uploadedImages);



			return View();
		}
		private PartialViewResult Upload(ArchiveModel model, IEnumerable<HttpPostedFileBase> uploadedImages)
		{
			
			if (uploadedImages != null)
			{
				var images = uploadedImages as IList<HttpPostedFileBase> ?? uploadedImages.ToList();
				if (images.Any())
				{
					ModelState.Clear();
					foreach (var image in images)
					{


						var stream = new MemoryStream();
						image.InputStream.CopyTo(stream);
						model.SavedImages.Add(stream.ToArray());
					}
				}
			}
			ModelState.Clear();
			return PartialView("_DocumentModelForm", model);
		}
	}
}