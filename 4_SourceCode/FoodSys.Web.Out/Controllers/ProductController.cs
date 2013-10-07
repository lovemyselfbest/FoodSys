using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Web.Base;

namespace FoodSys.Web.Out.Controllers
{
	public class ProductController : BaseController
	{
		//
		// GET: /Product/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Detail(Guid? id)
		{
			return View();
		}

	}
}
