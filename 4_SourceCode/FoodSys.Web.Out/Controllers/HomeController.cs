using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Web.Base;
using Project.Common;
using Project.Web.Base.Utility;
using FoodSys.Biz.BizAccess;
using FoodSys.Web.Out.Models;
using FoodSys.Web.Out.Base;

namespace FoodSys.Web.Out.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
