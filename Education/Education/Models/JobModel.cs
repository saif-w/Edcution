using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Models
{
	public class JobModel
	{
		public IEnumerable<JobGridRow> JobsGrid { get; set; } = new HashSet<JobGridRow>();
		public string Name { get; set; }
		public string Description { get; set; }
		public string FileLocation { get; set; }
	}
	public class JobGridRow
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string FileLocation { get; set; }
	}

}