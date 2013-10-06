using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Project.Web.Base.Utility;
using Project.Dal.Base;
using Project.Web.Base;
using Project.Common;
using NHibernate;
using NHibernate.Context;
using FoodSys.Web.Out.Base;
using System.Configuration;

namespace FoodSys.Web.Out
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			//filters.Add(new ElmahHandleErrorAttribute()); 
		}
		public static ISessionFactory SessionFactory
		{
			get;
			private set;
		}
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("favicon.ico");
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*allpng}", new { allpng = @".*\.png(/.*)?" });
			routes.IgnoreRoute("{*allgif}", new { allgif = @".*\.gif(/.*)?" });
			routes.IgnoreRoute("{*allcss}", new { allcss = @".*\.png(/.*)?" });
			routes.IgnoreRoute("{*alljpg}", new { alljpg = @".*\.jpg(/.*)?" });
			routes.IgnoreRoute("{*alljs}", new { alljs = @".*\.js(/.*)?" });

			//Custom route for reports
			routes.MapPageRoute(
			 "ReportRoute",                         // Route name
			 "Reports/{reportname}",                // URL
			 "~/Reports/{reportname}.aspx"   // File
			 );

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			//clear elmah log
			//SQLiteErrorLogX.ClearLog();
			#region configure nhibernate
			NHibernateHelper.Configure();
			#endregion

			#region configure log4net
			Log4netHelper.Configure();
			#endregion

			AreaRegistration.RegisterAllAreas();
			ControllerBuilder.Current.SetControllerFactory(typeof(BaseControllerFactory));
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
			DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			if (WebTools.IsNotExcuteAspx(Context.Request.CurrentExecutionFilePathExtension))
				return;
			var session = NHibernateHelper.SessionFactory.OpenSession();
			CurrentSessionContext.Bind(session);
		}

		protected void Application_AcquireRequestState(object sender, EventArgs e)
		{
			if (WebTools.IsNotExcuteAspx(Context.Request.CurrentExecutionFilePathExtension))
				return;
			//SessionManager.Authorize();
		}


		protected void Application_EndRequest(object sender, EventArgs e)
		{
			if (WebTools.IsNotExcuteAspx(Context.Request.CurrentExecutionFilePathExtension))
				return;
			var session = CurrentSessionContext.Unbind(NHibernateHelper.SessionFactory);
			session.Dispose();
		}
	}
}
