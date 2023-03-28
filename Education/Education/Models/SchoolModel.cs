using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Models
{
	public class SchoolModel
	{
		public IEnumerable<SchoolGridView> SchoolGrid { get; set; } = new HashSet<SchoolGridView>();
		public int SchoolId { get; set; }
		public string SchoolCode { get; set; }
		public string SchoolName { get; set; }
		public string Phone { get; set; }
		public string Type { get; set; }
		public bool Active { get; set; }
        public string Office { get; set; }
        public string Adress { get; set; }
        public string Emile { get; set; }
    }
	public class SchoolGridView
	{
		public int SchoolId { get; set; }
		public string SchoolCode { get; set; }
		public string SchoolName { get; set; }
		public string Phone { get; set; }
		public string Type { get; set; }
	}
	public class SchoolDoropDownList
	{
		public int SchoolId { get; set; }
		public string SchoolName { get; set; }
	
	}
}