using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Project.Common;
using Project.Web.Base.Filters;

namespace Project.Web.Base
{
	[CompressFilter]
	[AdminFilter]
	public class BaseController : Controller
	{
		public ILog Logger
		{
			get
			{
				return Log4netHelper.Logger;
			}
		}

		public string Error
		{
			get
			{
				if (TempData["Error"] == null)
					return string.Empty;
				return TempData["Error"].ToString();
			}
			set
			{
				TempData["Error"] = value;
			}
		}

		public string Message
		{
			get
			{
				if (TempData["Message"] == null)
					return string.Empty;
				return TempData["Message"].ToString();
			}
			set
			{
				TempData["Message"] = value;
			}
		}

		public string RetUrl
		{
			get
			{
				return Request.QueryString["RetUrl"];
			}
		}

		public EnumPageState? PageState
		{
			get
			{
				if (string.IsNullOrEmpty(Request.QueryString["pagestate"]))
					return null;
				int state = int.Parse(Request.QueryString["pagestate"]);
				return (EnumPageState)state;
			}
		}



		/// <summary>
		/// 判断页面状态
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="pageState"></param>
		/// <returns></returns>
		public EnumPageState GetPageState(object obj, EnumPageState? pageState)
		{
            if (obj != null && obj.ToString() != Guid.Empty.ToString() && pageState == EnumPageState.新增)
                return EnumPageState.新增;
			if (obj != null && obj.ToString() != Guid.Empty.ToString() && pageState != EnumPageState.查看)
				return EnumPageState.编辑;
			if ((obj == null || (obj != null && obj.ToString() == Guid.Empty.ToString())))
				return EnumPageState.新增;
			return EnumPageState.查看;
		}
		protected ActionResult View()
		{
			if (Request.IsAjaxRequest())
			{
				return base.View(Request.RequestContext.RouteData.Values["action"] + "_Partial");
			}
			return base.View();
		}
		protected ViewResult View(object model)
		{
			if (Request.IsAjaxRequest())
			{
				return base.View(Request.RequestContext.RouteData.Values["action"] + "_Partial", model);
			}
			return base.View(model);
		}
	}
}