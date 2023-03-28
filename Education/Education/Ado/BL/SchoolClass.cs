using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Education.Ado.BL
{
	public class SchoolClass:DL.DataBaseClass
	{
		public DataTable GetSchool(string UserName)
		{
			return EXUTETABLE("select SchoolId from AspNetUsers where UserName=@UserName ", CommandType.Text,
		   createparm("@UserName", SqlDbType.NVarChar, UserName ?? "")
		   );
		}
		public DataTable GetSchools()
		{
			return EXUTETABLE("select * from Schools", CommandType.Text);
		}
		public DataTable GetSchoolsID(int id)
		{
			return EXUTETABLE("select * from Schools where SchoolId=@SchoolId", CommandType.Text,
				 createparm("@SchoolId", SqlDbType.Int, id))
			;
		}
		
		public void _insert(string school_Code, string School_Name, string Phone,int type,bool active)
		{
			EXUTENONEQUARY("INSERT INTO [dbo].[Schools] ([SchoolId],[school_Code],[School_Name],[Phone],[Type],[IsActive])VALUES(@SchoolId,@school_Code,@School_Name,@Phone,@Type,@IsActive)", CommandType.Text,
		   createparm("@SchoolId", SqlDbType.Int, 0),
		   createparm("@school_Code", SqlDbType.NVarChar, school_Code ?? ""),
		   createparm("@School_Name", SqlDbType.NVarChar, School_Name),
		   createparm("@Phone", SqlDbType.NVarChar, Phone),
		   createparm("@Type", SqlDbType.Int,type),
		   createparm("@IsActive", SqlDbType.Bit,active)
		   );

		}

		//***************تعديل المستخدم**************//
		public void _update_User(int UserID, int SchoolId, string UserName, string Password, string Email, string Phone, bool active)
		{
			EXUTENONEQUARY("UPDATE[dbo].[AspNetUsers] SET[SchoolId] = @SchoolId ,[UserName] = @UserName ,[Password] = @Password ,[Email] = @Email ,[PhoneNumber] = @PhoneNumber ,[UR_IsActive] = @UR_IsActive WHERE  UserID=@UserID", CommandType.Text,
		   createparm("@SchoolId", SqlDbType.Int, SchoolId),
		   createparm("@UserName", SqlDbType.NVarChar, UserName ?? ""),
		   createparm("@Password", SqlDbType.NVarChar, Password),
		   createparm("@PhoneNumber", SqlDbType.NVarChar, Phone),
		   createparm("@Email", SqlDbType.NVarChar, Email),
		   createparm("@UR_IsActive", SqlDbType.Bit, active),
		   createparm("@UserID", SqlDbType.Int, UserID)
		   );

		}
	}
}