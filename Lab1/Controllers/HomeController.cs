using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab1.Controllers
{
    public class HomeController : Controller
	{													  
		private ApplicationUserManager _userManager;
		
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
			try
			{
				var admin = UserManager.IsInRoleAsync(Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity), "Admin");
				if (admin.Result)
				{
					ViewBag.Message = "Your role is admin";
				}
				else
				{
					ViewBag.Message = "Your role is not admin";
				}
			}
			catch (Exception ex)
			{

			}

			return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}