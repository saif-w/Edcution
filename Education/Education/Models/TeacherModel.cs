using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Models
{
	public class TeacherModel
	{
		public IEnumerable<TeacherGridRow> TeacherGridView { get; set; } = new HashSet<TeacherGridRow>();
		public Nullable<int> SchoolId { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Address { get; set; }
		public bool Check { get; set; }
		public bool ID { get; set; }
	}
	public class TeacherGridRow
	{
		public int Id { get; set; }
		public Nullable<int> SchoolId { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Address { get; set; }
	}
}