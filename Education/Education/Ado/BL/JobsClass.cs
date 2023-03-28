using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Education.Ado.BL
{
	public class JobsClass:DL.DataBaseClass
	{
		public DataTable GetJobs()
		{
			return EXUTETABLE("select * from Jobs", CommandType.Text);
		}
        public DataTable WebsiteUrl(int id)
        {
            return EXUTETABLE("select WebsiteUrl from Websites where Number=@Number ", CommandType.Text,
           createparm("@Number", SqlDbType.Int, id)
           );
        }
        public DataTable Login(string UserName,string pass)
		{
			return EXUTETABLE("select * from AspNetUsers where UserName=@UserName and Password=@Password ", CommandType.Text,
		   createparm("@UserName", SqlDbType.NVarChar, UserName ?? ""),
		   createparm("@Password", SqlDbType.NVarChar, pass)
		   );
		}
		//***************************اضافة***************************
		public void _insert(string Name,string Description,string FileLocation)
		{
			EXUTENONEQUARY("INSERT INTO [dbo].[Jobs] ([Name],[Description],[FileLocation])VALUES(@Name,@Description,@FileLocation)", CommandType.Text,
		   createparm("@Name", SqlDbType.NVarChar, Name ?? ""),
		   createparm("@Description", SqlDbType.NVarChar, Description ?? ""),
		   createparm("@FileLocation", SqlDbType.NVarChar, FileLocation));

		}

	}
}