using Education.Ado.BL;
using Education.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Education.Controllers
{
	public class HomeController : Controller
	{
		Startup start = new Startup();
		public ActionResult Index(int? id)
		{
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
            }
            JobsClass j = new JobsClass();
            int IdWebiste = id ?? 0;
            if (IdWebiste == 0)
            {
                string UserName = Request?.Cookies["userName"]?.Value??"";
                this.Session["Username"] = "";
                HomeModels website = new HomeModels();
                return View(website);
            }
            else if(j.WebsiteUrl(id ?? 0).Rows.Count == 0)
            {
                string UserName = Request.Cookies["userName"].Value;
                this.Session["Username"] = "";
                HomeModels website = new HomeModels();
                return View(website);
            }
            else
            {
                var Data =j.WebsiteUrl(id??0).Rows[0][0];
            
                this.Session["Username"]=null;

                var website = new HomeModels
                {
                    Url = Data.ToString(),
                };
                return View(website);
            }
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		[HttpPost]
		public ActionResult Contact(ConTactUsModel model)
		{
			#region MyRegion
			//try
			//{
			//	if (ModelState.IsValid)
			//	{
			//		var senderEmail = new MailAddress("www.saifallh.com@gmail.com", "gmail");
			//		var receiverEmail = new MailAddress(model.Username, "Receiver");
			//		var password = "###";
			//		var sub = model.Subject;
			//		var body = model.Message;
			//		var smtp = new SmtpClient
			//		{
			//			Host = "smtp.gmail.com",
			//			Port = 587,
			//			EnableSsl = true,
			//			DeliveryMethod = SmtpDeliveryMethod.Network,
			//			UseDefaultCredentials = false,
			//			Credentials = new NetworkCredential(senderEmail.Address, password)
			//		};
			//		using (var mess = new MailMessage(senderEmail, receiverEmail)
			//		{
			//			Subject = model.Subject,
			//			Body = model.Message
			//		})
			//		{
			//			smtp.Send(mess);
			//		}
			//		return View();
			//	}
			//}
			//catch (Exception)
			//{
			//	ViewBag.Error = "Some Error";
			//} 
			#endregion

			if (ModelState.IsValid)
			{
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;

                // setup Smtp authentication
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential("your_account@gmail.com", "yourpassword");
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("your_account@gmail.com");
                msg.To.Add(new MailAddress("destination_address@someserver.com"));

                msg.Subject = "This is a test Email subject";
                msg.IsBodyHtml = true;
                msg.Body = string.Format("<html><head></head><body><b>Test HTML Email</b></body>");

                try
                {
                    client.Send(msg);
                  
                }
                catch (Exception ex)
                {
                    
                }
            }
			else
			{
				return View();
			}
			return View();
		}

		public ActionResult LoginMain()
		{
			
			return View();
		}
		[HttpPost]
		public ActionResult LoginMain(LoginModel model)
		{
			
			JobsClass j = new JobsClass();
			if (model.Password == null || model.Username == null)
			{
				return View();
			}
			var user=j.Login(model.Username, model.Password);
			if (user != null && user.Rows.Count!=0)
			{
				if ((int)user.Rows[0][1] != 0)
				{
                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["UserName"] = model.Username;
                    Response.Cookies.Add(userInfo);
                  //  this.Session["Username"]= model.Username;
					return RedirectToAction("IndexSchool", "SchoolHome");
				}
				if (user.Rows.Count != 0)
				{
                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["UserName"] = model.Username;
                    //userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                    Response.Cookies.Add(userInfo);
                    //  this.Session["Username"]= model.Username;
                    return RedirectToAction("index", "SchoolHome");
				}
				else
				{
					ViewBag.message="الرجاء التأكد من كلمة السر او اسم المستخدم";
					return View();
				}
			}
			else
			{
				ViewBag.message = "الرجاء التأكد من كلمة السر او اسم المستخدم";
				return View();
			}
			return View();
		}
		public ActionResult Test()
		{

			return View();
		}

        //	public ActionResult HtmlEditorPartial()
        //	{
        //		return PartialView("_HtmlEditorPartial");
        //	}
        //	public ActionResult HtmlEditorPartialImageSelectorUpload()
        //	{
        //		HtmlEditorExtension.SaveUploadedImage("HtmlEditor", HomeControllerHtmlEditorSettings.ImageSelectorSettings);
        //		return null;
        //	}
        //	public ActionResult HtmlEditorPartialImageUpload()
        //	{
        //		HtmlEditorExtension.SaveUploadedFile("HtmlEditor", HomeControllerHtmlEditorSettings.ImageUploadValidationSettings, HomeControllerHtmlEditorSettings.ImageUploadDirectory);
        //		return null;
        //	}
        //}
        //public class HomeControllerHtmlEditorSettings
        //{
        //	public const string ImageUploadDirectory = "~/Content/UploadImages/";
        //	public const string ImageSelectorThumbnailDirectory = "~/Content/Thumb/";

        //	public static DevExpress.Web.UploadControlValidationSettings ImageUploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        //	{
        //		AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".png" },
        //		MaxFileSize = 4000000
        //	};

        //	static DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings imageSelectorSettings;
        //	public static DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings ImageSelectorSettings
        //	{
        //		get
        //		{
        //			if (imageSelectorSettings == null)
        //			{
        //				imageSelectorSettings = new DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings(null);
        //				imageSelectorSettings.Enabled = true;
        //				imageSelectorSettings.UploadCallbackRouteValues = new { Controller = "Home", Action = "HtmlEditorPartialImageSelectorUpload" };
        //				imageSelectorSettings.CommonSettings.RootFolder = ImageUploadDirectory;
        //				imageSelectorSettings.CommonSettings.ThumbnailFolder = ImageSelectorThumbnailDirectory;
        //				imageSelectorSettings.CommonSettings.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif" };
        //				imageSelectorSettings.UploadSettings.Enabled = true;
        //			}
        //			return imageSelectorSettings;
        //		}
        //	}

    }

}