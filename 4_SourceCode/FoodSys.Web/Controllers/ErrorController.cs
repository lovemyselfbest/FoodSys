using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodSys.Web.Controllers.Common
{
	public class ErrorController : Controller
	{
		//
		// GET: /Error/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Code404()
		{
			return View();
		}

		public ActionResult Code500()
		{
			return View();
		}

	}
}
