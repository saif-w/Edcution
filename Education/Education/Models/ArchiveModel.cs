using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Models
{
	public class ArchiveModel
	{
		public string FileNumber { get; set; }
		public DateTime Date { get; set; }
		public string Subject { get; set; }
		public IList<byte[]> SavedImages { get; set; } = new List<byte[]>();
		
	}
}