using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Compression;
using System.Web.Mvc;
using Project.Common;
using Project.Web.Base.Utility;

namespace Project.Web.Base.Filters
{
	/// <summary>
	/// Gzip压缩HttpStream
	/// </summary>
	public class CompressFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			HttpRequestBase request = filterContext.HttpContext.Request;
			HttpResponseBase response = filterContext.HttpContext.Response;
			if (response.IsRequestBeingRedirected || request["ExportType"] != null || filterContext.ActionDescriptor.GetCustomAttributes(false).Count(x=>x.GetType().Equals(typeof(AsposeExportFileAttribute)))>0)
				return;

			string acceptEncoding = request.Headers["Accept-Encoding"];
			if (string.IsNullOrEmpty(acceptEncoding)) 
				return;

			try
			{
				acceptEncoding = acceptEncoding.ToUpperInvariant();
				if (acceptEncoding.Contains("GZIP"))
				{
					response.AppendHeader("Content-encoding", "gzip");
					response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
				}
				else if (acceptEncoding.Contains("DEFLATE"))
				{
					response.AppendHeader("Content-encoding", "deflate");
					response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
				}
			}
			catch (Exception ex)
			{
				Log4netHelper.Logger.Fatal(ex);
			}

		}
	}
}
