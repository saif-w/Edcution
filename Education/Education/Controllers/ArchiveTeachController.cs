using Education.Ado.BL;
using Education.Data;
using Education.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education.Controllers
{
    public class ArchiveTeachController : Controller
    {
        private DBSchoolsEntities db = new DBSchoolsEntities();

        // GET: ArchiveTeach
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
            SchoolClass sc = new SchoolClass();
            var id = Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);


            var data = GetFiles(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile,FormCollection form)
        {
            var btn = form["SubmitButton"];

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
            var id = Convert.ToInt32(sc.GetSchool(UserName.ToString()).Rows[0][0]);
        
            string IdNumber = form["IdNumber"];
            string Type = form["Type"];
            byte[] bytes;
            if (btn != null)
            {
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                //string constr = ConfigurationManager.ConnectionStrings["DBSchoolsEntities"].ConnectionString;
                using (SqlConnection con = new SqlConnection(@"Server= ws05.server.ly; Database=DBSchoolsEntities; Integrated security=false;User=user02; Password=!QA2ws3ed; MultipleActiveResultSets=true;"))
                {
                    string query = "INSERT INTO tblFiles VALUES (@Name, @ContentType, @Data,@ReferenceNumber,@Date,@Type,@SchoolId)";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Name", Path.GetFileName(postedFile.FileName));
                        cmd.Parameters.AddWithValue("@ContentType", postedFile.ContentType);
                        cmd.Parameters.AddWithValue("@Data", bytes);
                        cmd.Parameters.AddWithValue("@ReferenceNumber", IdNumber);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Type", Type);
                        cmd.Parameters.AddWithValue("@SchoolId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return View(GetFiles(id));
        }

        [HttpPost]
        public FileResult DownloadFile(int? fileId)
        {
            byte[] bytes;
            string fileName, contentType;
          //  string constr = ConfigurationManager.ConnectionStrings["DBSchoolsEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(@"Server= ws05.server.ly; Database=DBSchoolsEntities; Integrated security=false;User=user02; Password=!QA2ws3ed; MultipleActiveResultSets=true;"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Data, ContentType FROM tblFiles WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", fileId);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Data"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["Name"].ToString();
                    }
                    con.Close();
                }
            }

            return File(bytes, contentType, fileName);
        }

        private static List<FileModel> GetFiles(int id)
        {
           

            List<FileModel> files = new List<FileModel>();
           // string constr = ConfigurationManager.ConnectionStrings["DBSchoolsEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(@"Server= ws05.server.ly; Database=DBSchoolsEntities; Integrated security=false;User=user02; Password=!QA2ws3ed; MultipleActiveResultSets=true;"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, ReferenceNumber,Type FROM tblFiles where SchoolId ="+id+""))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new FileModel
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Name = sdr["ReferenceNumber"].ToString(),
                                Type= sdr["Type"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return files;
        }
    }
}