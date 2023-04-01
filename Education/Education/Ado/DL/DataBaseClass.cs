using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Education.Ado.DL
{
	public class DataBaseClass
	{
        static SqlConnection conn = new SqlConnection(@"Server=ws05.server.ly; Database=DBSchoolsEntities; Integrated security=false;User=user02; Password=!QA2ws3ed; MultipleActiveResultSets=true;");
        // static SqlConnection conn = new SqlConnection(@"Server= 41.208.73.39; Database=DBSchoolsEntities; Integrated security=false;User=SchoolUsers; Password=P@ssw0rd; MultipleActiveResultSets=true;");
        //static SqlConnection conn = new SqlConnection(@"Server=.; Database=DBSchoolsEntities; Integrated security=false;User=sa; Password=!QA2ws3ed; MultipleActiveResultSets=true;");
        static SqlCommand cmd;

		private static void openconn()
		{
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
		}

		private static void closeconn()
		{
			if (conn.State == ConnectionState.Open)
			{
				conn.Close();
			}
		}

		protected static DataTable EXUTETABLE(string quary, CommandType type, params SqlParameter[] param)
		{
			conn.Open();
			cmd = new SqlCommand(quary, conn);
			cmd.CommandType = type;
			cmd.Parameters.AddRange(param);

			DataTable dt = new DataTable();
			dt.Load(cmd.ExecuteReader());

			conn.Close();
			return dt;
		}

		protected static void EXUTENONEQUARY(string quary, CommandType type, params SqlParameter[] param)
		{
			conn.Open();
			cmd = new SqlCommand(quary, conn);
			cmd.CommandType = type;
			cmd.Parameters.AddRange(param);


			cmd.ExecuteNonQuery();

			conn.Close();

		}

		protected static SqlParameter createparm(string name, SqlDbType type, object value)
		{
			SqlParameter parme = new SqlParameter();
			parme.ParameterName = name;
			parme.SqlDbType = type;
			parme.Value = value;

			return parme;
		}
	}
}