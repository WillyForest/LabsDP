using Lab1.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab1.Controllers
{
    public class HomeController : Controller
	{												  
		private ApplicationUserManager _userManager;
		Logger.Logger logger = new Logger.Logger();
		public HomeController()
		{
		}

		public HomeController(ApplicationUserManager userManager)
		{
			UserManager = _userManager;	  
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		} 

		public ActionResult Index()
		{
			var model = new IndexModel();
			if (!User.Identity.IsAuthenticated)
			{
				model.IsAuthenticated = false;
				return View(model);
			}
			else
			{
				model.IsAuthenticated = true;
			}
			try
			{
				var admin = UserManager.IsInRoleAsync(Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity), "Admin");
				if (admin.Result)
				{
					ViewBag.Message = "Your role is admin";
					model.IsAdmin = true;
					DirectoryInfo d = new DirectoryInfo(@"D:\Test");//Assuming Test is your Folder
					FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
					model.Files = Files;
				}
				else
				{
					model.IsAdmin = false;
					DirectoryInfo d = new DirectoryInfo(@"D:\Test");//Assuming Test is your Folder
					FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
					model.Files = Files;
					ViewBag.Message = "Your role is not admin";
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = Newtonsoft.Json.JsonConvert.SerializeObject(ex);
			}
			return View(model);
		}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

		public ActionResult Logs()
		{
			if (UserManager.IsInRoleAsync(Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity), "Admin").Result)
			{
				return View(logger.GetAllLogs());
			}
			return RedirectToAction("Index");
		}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

		public ActionResult DeleteFile(string file)
		{
			System.IO.File.Delete(file);
			logger.WriteLog("Пользователь " + User.Identity.Name + " удалил файл " + file + ".");					   
			return RedirectToAction("Index");
		} 

		public ActionResult OpenFile(string file)
		{
			logger.WriteLog("Пользователь " + User.Identity.Name + " просматривает файл " + file + ".");
			ViewBag.File = file;
			ViewBag.IsAdmin = UserManager.IsInRoleAsync(Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity), "Admin").Result;
			List<string> fileStrs = new List<string>();
			using (StreamReader sr = System.IO.File.OpenText(file))
			{
				string s;
				while ((s = sr.ReadLine()) != null)
				{
					fileStrs.Add(s);
				}
			}
			return View(fileStrs);
		}
    }
}