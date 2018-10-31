using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Lab1.Models
{
	public class IndexModel
	{
		public FileInfo[] Files { get; set; }
		public bool IsAdmin { get; set; } = false;
		public bool IsAuthenticated { get; set; } = false;
	}
}