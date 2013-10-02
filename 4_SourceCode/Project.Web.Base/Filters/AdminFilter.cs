using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Base.Filters
{
	public class AdminFilterAttribute : ActionFilterAttribute
	{

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			/* 注释
			----------------------------------------------------------*/

			/*
		   var values = filterContext.RouteData.Values;
		   if ((values["Controller"].ToString().ToLower() == "sysloginmanage")
			   )
			   return;
			
		   filterContext.HttpContext.Response.Write("<script type='text/javascript'>window.location.href='www.bing.com'</script>&nbsp;");
		   filterContext.HttpContext.Response.End();
			
		   if (SysContext.CurrentSysUser == null)
		   {
			   if (values["Controller"].ToString().ToLower() == "exporthelper")
				   filterContext.HttpContext.Response.Write("<script type='text/javascript'>window.returnValue='401 ';window.close();</script>&nbsp;");
			   else if(values["Controller"].ToString().ToLower() == "home")
				   filterContext.HttpContext.Response.Write("<script type='text/javascript'>window.location.href='/sysloginmanage/index'</script>&nbsp;");
			   else
				   filterContext.HttpContext.Response.Write("<script type='text/javascript'>window.top.redirectToLogin();</script>&nbsp;");
			   filterContext.HttpContext.Response.End();
		   }*/
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{

		}
	}
}
