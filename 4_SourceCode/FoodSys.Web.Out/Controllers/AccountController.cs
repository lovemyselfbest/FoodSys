using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Web.Base;

namespace FoodSys.Web.Out.Controllers
{
	public class AccountController : BaseController
	{
		//
		// GET: /Account/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Register()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		public ActionResult Password()
		{
			return View();
		}


	}
}
