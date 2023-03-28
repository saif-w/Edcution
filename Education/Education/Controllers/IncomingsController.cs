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
using System.IO;

namespace Education.Controllers
{
    public class IncomingsController : Controller
    {
        private DBSchoolsEntities db = new DBSchoolsEntities();

        // GET: Incomings
        public ActionResult Index()
        {
            return View(db.Incomings.ToList());
        }

        // GET: Incomings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incoming incoming = db.Incomings.Find(id);
            if (incoming == null)
            {
                return HttpNotFound();
            }
            return View(incoming);
        }

        // GET: Incomings/Create
        public ActionResult Create()
        {
			var UserName = this.Session["Username"];

			SchoolClass sc = new SchoolClass();
			var id = Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);
			ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.SchoolId == id), "SchoolId", "School_Name");

			return View();
        }

        // POST: Incomings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,SchoolId,Date,Source,Subject,FileNumber,Type,Attachment,IsDeleted,FileLocation,Comment")] Incoming incoming)
        {
            if (ModelState.IsValid)
            {
				var UserName = this.Session["Username"];

				SchoolClass sc = new SchoolClass();
				var id = Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);
				ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.SchoolId == id), "SchoolId", "School_Name", incoming.SchoolId);

				

				db.Incomings.Add(incoming);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incoming);
        }

        // GET: Incomings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incoming incoming = db.Incomings.Find(id);
            if (incoming == null)
            {
                return HttpNotFound();
            }
            return View(incoming);
        }

		// POST: Incomings/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Number,SchoolId,Date,Source,Subject,FileNumber,Type,Attachment,IsDeleted,FileLocation,Comment")] Incoming incoming, FormCollection fc, HttpPostedFileBase File1)
		{
			#region TestImageUpload
			//var i = UploadImageInDataBase(file, incoming);
			//if (i == 1)
			//{
			//	return RedirectToAction("Index");
			//}
			//return View(model);
			//if (ModelState.IsValid)
			//         {
			//             db.Entry(incoming).State = EntityState.Modified;
			//             db.SaveChanges();
			//             return RedirectToAction("Index");
			//         } 
			#endregion
			#region Test2
			//	IncomingsImage tbl = new IncomingsImage();
			//	var allowedExtensions = new[] {
			//	".Jpg", ".png", ".jpg", "jpeg"
			//};
			//	var fi = fc["file"];
			//	tbl.IncomingId = incoming.Id;
			//	tbl.Image = fi;//file.ToString(); //getting complete url  

			//	var fileName = Path.GetFileName(fi); //getting only file name(ex-ganesh.jpg)  
			//	var ext = Path.GetExtension(fi); //getting the extension(ex-.jpg)  
			//	if (allowedExtensions.Contains(ext)) //check what type of extension  
			//	{
			//		string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
			//		string myfile = name + "_" + tbl.Id + ext; //appending the name with id  
			//												   // store the file inside ~/project folder(Img)  
			//		var path = Path.Combine(Server.MapPath("~/Img"), myfile);
			//		tbl.Image = path;
			//		db.IncomingsImages.Add(tbl);
			//		db.SaveChanges();
			//		file.SaveAs(path);
			//		return View(incoming);
			//	} 
			#endregion
			#region test3
			// file1 to store image in binary formate and file2 to store path and url  
			// we are checking file1 and file2 are null or not according to that different case are there  
			//IncomingsImage Details = new IncomingsImage();
			//if (File1 != null && File1.ContentLength > 0 && File2 != null)
			//{

			//	Details.Image = new byte[File1.ContentLength]; // file1 to store image in binary formate  
			//	File1.InputStream.Read(Details.BinaryPhoto, 0, File1.ContentLength);
			//	string ImageName = System.IO.Path.GetFileName(File2.FileName); //file2 to store path and url  
			//	string physicalPath = Server.MapPath("~/img/" + ImageName);
			//	// save image in folder  
			//	File2.SaveAs(physicalPath);
			//	// store path in database  
			//	Details.PathPhoto = "img/" + ImageName;
			//	db.CandidateDetails.Add(Details);
			//	db.SaveChanges();
			//	return PartialView("detail");
			//}
			//if (File1 != null && File1.ContentLength > 0 && File2 == null)
			//{
			//	Details.BinaryPhoto = new byte[File1.ContentLength]; // file1 to store image in binary formate  
			//	File1.InputStream.Read(Details.BinaryPhoto, 0, File1.ContentLength);
			//	db.CandidateDetails.Add(Details);
			//	db.SaveChanges();
			//	return PartialView("detail");
			//}
			//if (File1 == null && File2 != null)
			//{
			//	string ImageName = System.IO.Path.GetFileName(File2.FileName); //file2 to store path and url  
			//	string physicalPath = Server.MapPath("~/img/" + ImageName);
			//	// save image in folder  
			//	File2.SaveAs(physicalPath);
			//	Details.PathPhoto = "img/" + ImageName;
			//	db.CandidateDetails.Add(Details);
			//	db.SaveChanges();
			//	return PartialView("detail");
			//}
			//else
			//{ //if both file1 and file2 are null then we can store others details without any image  
			//	db.CandidateDetails.Add(Details);
			//	db.SaveChanges();
			//	return PartialView("detail");
			//} 
			#endregion
			return RedirectToAction("Index");
		}
		//public int UploadImageInDataBase(HttpPostedFileBase file, IncomingsImage model)
		//{
		//	model.Image = ConvertToBytes(file);
		//	var Content = new IncomingsImage
		//	{
		//		Image= ConvertToBytes(file),
		//	};
		//	db.IncomingsImages.Add(Content);
		//	int i = db.SaveChanges();
		//	if (i == 1)
		//	{
		//		return 1;
		//	}
		//	else
		//	{
		//		return 0;
		//	}
		//}
		public byte[] ConvertToBytes(HttpPostedFileBase image)
		{
			byte[] imageBytes = null;
			BinaryReader reader = new BinaryReader(image.InputStream);
			imageBytes = reader.ReadBytes((int)image.ContentLength);
			return imageBytes;
		}
		//**************//
		// GET: Incomings/Delete/5
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incoming incoming = db.Incomings.Find(id);
            if (incoming == null)
            {
                return HttpNotFound();
            }
            return View(incoming);
        }

        // POST: Incomings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incoming incoming = db.Incomings.Find(id);
            db.Incomings.Remove(incoming);
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
