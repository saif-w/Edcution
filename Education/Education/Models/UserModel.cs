using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Models
{
	public class UserModel
	{
		public string UserName { get; set; }
		public string Pass { get; set; }
		public string Emile { get; set; }
		public string Phone { get; set; }
		public int SchoolId { get; set; }
		public bool Active { get; set; }
		public int userid { get; set; }
	}
}