
using System;
using System.Web;
using System.Web.Mvc;
using Elmah;
using System.Data.SQLite;

namespace Project.Web.Base
{
	public class SQLiteErrorLogX
	{
		public static int ClearLog()
		{
			ErrorLog errLogB = ErrorLog.GetDefault(HttpContext.Current);
			SQLiteErrorLog errLog = (SQLiteErrorLog)errLogB;
			const string ds = @"delete from Error";
			HttpContext.Current.Trace.Write("query=" + ds);
			using (SQLiteConnection connection = new SQLiteConnection(errLog.ConnectionString))
			{
				using (SQLiteCommand command = new SQLiteCommand(ds,connection))
				{
					connection.Open();
					int rowsDeleted = command.ExecuteNonQuery();
					HttpContext.Current.Trace.Write("Error Log cleared. " +rowsDeleted + " entries were deleted.");
					return rowsDeleted;
				}
			}
		}
	}

	public class ElmahHandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			base.OnException(context);

			var e = context.Exception;
			if (context.ExceptionHandled   // if unhandled, will be logged anyhow 
				|| RaiseErrorSignal(e)      // prefer signaling, if possible 
				|| IsFiltered(context))     // filtered? 
			{
				context.ExceptionHandled = true;
				context.HttpContext.Response.Redirect("/error/Code500");
				return;
			}
			LogException(e);
		}

		private static bool RaiseErrorSignal(Exception e)
		{
			var context = HttpContext.Current;
			if (context == null)
				return false;
			var signal = ErrorSignal.FromContext(context);
			if (signal == null)
				return false;
			signal.Raise(e, context);
			return true;
		}

		private static bool IsFiltered(ExceptionContext context)
		{
			var config = context.HttpContext.GetSection("elmah/errorFilter")
						 as ErrorFilterConfiguration;

			if (config == null)
				return false;

			var testContext = new ErrorFilterModule.AssertionHelperContext(
									  context.Exception, HttpContext.Current);

			return config.Assertion.Test(testContext);
		}

		private static void LogException(Exception e)
		{
			var context = HttpContext.Current;
			ErrorLog.GetDefault(context).Log(new Error(e, context));
		}
	}
}