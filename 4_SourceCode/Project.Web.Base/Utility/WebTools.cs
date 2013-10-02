using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
using System.Reflection;
using Project.Common;
using System.Runtime.CompilerServices;

namespace Project.Web.Base.Utility
{
	public class WebTools
	{
		/// <summary>
		/// Main页面刷新脚本
		/// </summary>
		public static string ScriptCloseDialog(DialogOption option)
		{
			var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
			string json = oSerializer.Serialize(option);
			return string.Format("<script>window.top.closeDialog({0});</script>", json);
		}
		/// <summary>
		/// PageRightFrame页面刷新脚本
		/// </summary>
		public static string ScriptCloseEmbeddedFrameDialog(DialogOption option)
		{
			var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
			string json = oSerializer.Serialize(option);
			return string.Format("<script>window.top.closeEmbeddedFrameDialog({0});</script>", json);

		}

		public static string ScriptRefreshDialog(int index)
		{
			return string.Format("<script>window.top.refreshDialog({0});</script>", index);
		}

		public static string GetScript(string script)
		{
			return string.Format("<script>{0}</script>", script);
		}

		/// <summary>
		/// 判断执行类型
		/// </summary>
		/// <param name="extension"></param>
		/// <returns></returns>
		public static bool IsNotExcuteAspx(string extension)
		{
			return
				extension.Equals(".css", StringComparison.OrdinalIgnoreCase)
				|| extension.Equals(".js", StringComparison.OrdinalIgnoreCase)
				|| extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
				|| extension.Equals(".gif", StringComparison.OrdinalIgnoreCase)
				|| extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
				|| extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
				|| extension.Equals(".ico", StringComparison.OrdinalIgnoreCase)
				|| extension.Equals(".swf", StringComparison.OrdinalIgnoreCase)
				;
		}

	}



	public class DialogOption
	{

		public static void MergeWith<T>(T primary, T secondary)
		{
			foreach (var pi in typeof(T).GetProperties())
			{
				var priValue = pi.GetGetMethod().Invoke(primary, null);
				var secValue = pi.GetGetMethod().Invoke(secondary, null);
				//if (priValue == null || (pi.PropertyType.IsValueType && priValue == Activator.CreateInstance(pi.PropertyType)))
				if ((priValue == null || pi.PropertyType.IsValueType) && secValue != null)
				{
					pi.GetSetMethod().Invoke(primary, new object[] { secValue });
				}
			}
		}

		public DialogOption()
		{

		}

		public static DialogOption GetDefaultInstance(DialogOption mergeOption = null)
		{
			var primary = new DialogOption()
			{
				IsShowMessage = true,
				Message = "操作成功",
				Form = "form",
				AutoClose = true,
				RefreshOpenerAsynchronous = true,
				RefreshOpener = true
			};
			if (mergeOption != null)
				MergeWith<DialogOption>(primary, mergeOption);

			return primary;
		}

		public int? Index
		{
			get;
			set;
		}

		public int? Width
		{
			get;
			set;
		}

		public int? Height
		{
			get;
			set;
		}

		public bool? IsShowMessage
		{
			get;
			set;
		}

		public bool? AutoClose
		{
			get;
			set;
		}

		public bool? RefreshOpenerAsynchronous
		{
			get;
			set;
		}

		public bool? RefreshOpener
		{
			get;
			set;
		}

		public int? RefreshDialogIndex
		{
			get;
			set;
		}

		public string Callback
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public string Form
		{
			get;
			set;
		}

		public object HighlightData
		{
			get;
			set;
		}

		

	}
}