using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Education.Ado.BL
{
	public class TeacherClass:DL.DataBaseClass
	{
		
		public DataTable GetTeachers()
		{
			return EXUTETABLE("select * from Teachers", CommandType.Text);
		}

		public DataTable GetTeacherswithSchool(int id)
		{
			return EXUTETABLE("select * from Teachers where SchoolId=@SchoolId", CommandType.Text,
				createparm("@SchoolId", SqlDbType.Int, id));
		}
	}
}