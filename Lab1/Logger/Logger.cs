using Lab1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Lab1.Logger
{
	public class Logger
	{
		private string _file = "D:/logs.txt";

		public Logger()
		{
			if (!File.Exists(_file))
			{
				File.Create(_file);
			}
		}

		public void WriteLog(string log)
		{
			List<Log> fileStrs = new List<Log>();
			using (StreamReader sr = File.OpenText(_file))
			{
				string s;
				while ((s = sr.ReadLine()) != null)
				{
					fileStrs.Add(new Log()
					{
						Activity = s.Split('|')[1],
						Created = DateTime.Parse(s.Split('|')[0])
					});
				}
			}
			fileStrs.Add(new Log()
			{
				Activity = log,	
				Created = DateTime.Now
			});
			using (FileStream fs = File.OpenWrite(_file))
			{
				// Add some information to the file.
				string lines = "";
				foreach (var str in fileStrs)
				{
					lines += string.Format("{0}|{1}\n", str.Created, str.Activity);
				}
				var s = new UTF8Encoding(true).GetBytes(lines);
				fs.Write(s, 0, s.Length);
			}
		}

		public List<Log> GetAllLogs()
		{
			List<Log> fileStrs = new List<Log>();
			using (StreamReader sr = File.OpenText(_file))
			{
				string s;
				while ((s = sr.ReadLine()) != null)
				{
					fileStrs.Add(new Log()
					{
						Activity = s.Split('|')[1],
						Created = DateTime.Parse(s.Split('|')[0])
					});
				}
			}
			return fileStrs;
		}
	}
}