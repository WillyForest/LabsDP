using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1.Logger
{
	public class Logger
	{
		private static List<Log> _logs;

		public Logger()
		{
			if (_logs == null)
			{
				_logs = new List<Log>();
			}
		}

		public void WriteLog(string log)
		{
			_logs.Add(new Log()
			{
				Activity = log,	
				Created = DateTime.Now
			});
		}

		public List<Log> GetAllLogs()
		{
			return _logs;
		}
	}
}