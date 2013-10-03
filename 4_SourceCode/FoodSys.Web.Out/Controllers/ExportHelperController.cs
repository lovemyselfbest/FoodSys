using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodSys.Entity;
using System.Reflection;
using Project.Common;
using FoodSys.Web.Out.Base;
using Project.Web.Base;
using Project.Entity.Base;
using Project.Web.Base.Utility;

namespace FoodSys.Web.Out.Controllers.Common
{
	public class ExportHelperController : BaseController
	{
		//
		// GET: /ExportHelper/
		public ActionResult Index(string type)
		{
			type = StringUtility.DecodeBase64(type);
			type = string.Format("{0},{1}",type,type.Substring(0,type.LastIndexOf('.')));
			IList<ExportAttribute> attributes = ExportHelper.ListExportAttributes(Type.GetType(type));
			return View(attributes);
		}
	}
}
