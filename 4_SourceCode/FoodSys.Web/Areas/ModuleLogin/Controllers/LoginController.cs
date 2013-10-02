using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Web.Base;
using FoodSys.Web.Areas.ModuleLogin.Models;
using FoodSys.Biz.BizAccess;
using FoodSys.Enum;
using Project.Common;
using FoodSys.Web.Base;

namespace FoodSys.Web.Areas.ModuleLogin.Controllers
{
	public class LoginController : BaseController
	{
		private BizSysLogs bizSysLogs;
		private BizSysUser bizSysUser;
		public ActionResult Index()
		{
			HttpCookie cookie = Request.Cookies[SessionManager.SYS_USER_COOKIE_KEY];
			if (cookie != null
				&& cookie[SessionManager.SYS_USER_COOKIE_KEY] != null 
				&& !string.IsNullOrEmpty(cookie[SessionManager.SYS_USER_COOKIE_KEY].ToString()))
			{
				var userAccount = cookie[SessionManager.SYS_USER_COOKIE_KEY];
				var user = bizSysUser.GetFirst(x => x.UserAccount == userAccount);
				if (user != null && user.Status == (int)EnumUserStatus.已激活)
				{
					SessionManager.CurrentSysUser = user;
					return Redirect("/home");
				}
			}

			return View();
		}

		[HttpPost]
		public ActionResult Index(ModelLoginIndex model)
		{
			JsonResult jsresult = new JsonResult();
			jsresult.ContentType = Consts.CONTENT_TYPE;
			jsresult.Data = new { result = string.Empty };

			var user = bizSysUser.GetFirst(x => x.UserAccount == model.SysUser.UserAccount);
			if (user == null)
			{
				//用户名不存在！
				jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M10001E };
				return jsresult;
			}
			else if (user.Password != Security.Encrypt(model.SysUser.Password))
			{
				//密码错误！
				jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M10002E };
				return jsresult;
			}
			else if (user.Status != (int)EnumUserStatus.已激活)
			{
				//该用户当前状态不可登录！
				jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M10003E };
				return jsresult;
			}
			SessionManager.CurrentSysUser = user;

			/* 设置记录Cookie状态
			----------------------------------------------------------*/
			if (model.RememberLoginStatus)
			{
				HttpCookie cookie = Request.Cookies[SessionManager.SYS_USER_COOKIE_KEY];
				if (cookie == null)
				{
					cookie = new HttpCookie(SessionManager.SYS_USER_COOKIE_KEY);
					cookie.Expires = DateTime.Now.AddDays(14);
				}
				cookie.Values.Set(SessionManager.SYS_USER_COOKIE_KEY, model.SysUser.UserAccount);
				Response.SetCookie(cookie);
			}
			/* 登录日志记录
			----------------------------------------------------------*/
			bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser, Request.UserHostAddress, "登录", SessionManager.CurrentSysUser.UserName, SessionManager.CurrentSysUser.ID, "登录", EnumLogType.登录日志, string.Empty);
			return jsresult;
		}

		public ActionResult LoginOut()
		{
			JsonResult jsresult = new JsonResult();
			jsresult.ContentType = Consts.CONTENT_TYPE;
			jsresult.Data = new { result = string.Empty };

			int count = Request.Cookies.Count;
			for (int i = 0; i < count; i++)
			{
				HttpCookie tempCookie;
				string cookieName = Request.Cookies[i].Name;
				tempCookie = new HttpCookie(cookieName);
				tempCookie.Expires = DateTime.Now.AddDays(-1);
				Response.Cookies.Add(tempCookie);
			}
			Session.Clear();
			return jsresult;
		}

	}
}
