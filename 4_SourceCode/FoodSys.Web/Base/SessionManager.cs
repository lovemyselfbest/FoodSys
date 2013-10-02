using FoodSys.Entity;
using System.Web;
using Project.Common;
using FoodSys.Biz.BizAccess;
using Project.Biz.Base;
using System;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Web.Routing;
using System.Linq;

namespace FoodSys.Web.Base
{
	public class SessionManager
	{
		public const string SYS_USER_COOKIE_KEY = "SYS_USER_COOKIE_KEY";
		public const string SYS_USER_SESSION_KEY = "SYS_USER_SESSION_KEY";
		public const string IS_MUTIPLE_USER_LOGINED = "IS_MUTIPLE_USER_LOGINED";
		public const string SYS_USER_RIGHT_SESSION_KEY = "SYS_USER_RIGHT_SESSION_KEY";
		public const string DEFFAULTBBSPASSWORD = "a6AbVxECqKE=";

		public const string NONASSET_COMMUNITY_SESSION_KEY = "NONASSET_COMMUNITY_SESSION_KEY";

		/// <summary>
		/// 当前系统用户
		/// </summary>
		public static SysUser CurrentSysUser
		{
			get
			{
				if (HttpContext.Current.Session[SYS_USER_SESSION_KEY] != null)
					return HttpContext.Current.Session[SYS_USER_SESSION_KEY] as SysUser;

				HttpCookie cookie = HttpContext.Current.Request.Cookies[SYS_USER_COOKIE_KEY];
				if (cookie != null &&
					cookie[SYS_USER_COOKIE_KEY] != null &&
					cookie[SYS_USER_COOKIE_KEY].ToString() != string.Empty)
				{
					BizSysUser bizSysUser = BizFactory.Get<BizSysUser>();
					HttpContext.Current.Session[SYS_USER_SESSION_KEY] = bizSysUser.GetFirst(x => x.UserAccount == cookie[SYS_USER_COOKIE_KEY].ToString());
					return HttpContext.Current.Session[SYS_USER_SESSION_KEY] as SysUser;
				}
				return null;
			}
			set
			{
				HttpContext.Current.Session[SYS_USER_SESSION_KEY] = value;
			}
		}

		public static bool? IsMutipleUserLogined
		{
			get
			{
				if (HttpContext.Current.Session[IS_MUTIPLE_USER_LOGINED] != null)
					return HttpContext.Current.Session[IS_MUTIPLE_USER_LOGINED] as bool?;
				return false;
			}
			set
			{
				HttpContext.Current.Session[IS_MUTIPLE_USER_LOGINED] = value;
			}
		}


		/// <summary>
		/// 对用户登录进行验证，如果未登录，则跳转到登录页面
		/// </summary>
		public static void Authorize()
		{
			var currentContext = new HttpContextWrapper(HttpContext.Current);
			var routeData = RouteTable.Routes.GetRouteData(currentContext);
			if (routeData == null)
				return;
			string area = routeData.DataTokens["area"] == null ? "" : routeData.DataTokens["area"].ToString();
			string controller = routeData.Values["Controller"].ToString();
			string action = routeData.Values["Action"].ToString();
			string url = string.Format("/{0}/{1}/{2}", area, controller, action);
			if (url.Equals("/ModuleLogin/Login/Index", StringComparison.OrdinalIgnoreCase))
				return;
			if (CurrentSysUser == null)
			{
				if (controller.Equals("exporthelper", StringComparison.OrdinalIgnoreCase))
					HttpContext.Current.Response.Write("<script type='text/javascript'>window.returnValue='401';window.close();</script>&nbsp;");
				else
					HttpContext.Current.Response.Write("<script type='text/javascript'>window.top.location.href='/modulelogin/login/index'</script>&nbsp;");
				HttpContext.Current.Response.End();
			}
		}

		/// <summary>
		/// 默认密码
		/// </summary>
		public static String DefaultPassword
		{
			get
			{
				return WebConfigurationManager.AppSettings["DefaultPassword"];
			}
		}

		public static IList<SysMenu> CurrentSysUserMenus
		{
			get
			{
				if (HttpContext.Current.Session[SYS_USER_RIGHT_SESSION_KEY] != null)
					return HttpContext.Current.Session[SYS_USER_RIGHT_SESSION_KEY] as IList<SysMenu>;
				BizSysRole bizSysRole = BizFactory.Get<BizSysRole>();
				BizSysMenu bizSysMenu = BizFactory.Get<BizSysMenu>();


				List<SysMenu> menuCollection = new List<SysMenu>();

				/* 获取角色相关菜单
				----------------------------------------------------------*/
				SysRole sysRole = bizSysRole.ListSysRoleByUserID(CurrentSysUser.ID);
				if (sysRole != null)
				{
					List<SysMenu> userRoleMenus = bizSysMenu.ListSysMenuBySysRoleID(sysRole.ID) as List<SysMenu>;
					menuCollection.AddRange(userRoleMenus);
				}


				/* 获取对用户额外分配的权限
				----------------------------------------------------------*/
				List<SysMenu> userExtraMenus = bizSysMenu.ListSysMenuByUserId(CurrentSysUser.ID) as List<SysMenu>;
                for (int i = 0; i <menuCollection.Count; i++)
                {
                    for (int j = 0; j <= userExtraMenus.Count; j++)
                    {
                        if (j <userExtraMenus.Count)
                        {
                            if (menuCollection[i].Name == userExtraMenus[j].Name)
                            {
                                userExtraMenus.RemoveAt(j);
                            }
                        }
                    }
                }
                    /* 两者权限合并返回
                    ----------------------------------------------------------*/
                    menuCollection.AddRange(userExtraMenus);

				/* 获取叶子权限的上级
				----------------------------------------------------------*/
				List<Guid> level2Ids = menuCollection.Select(x => x.ParentID.Value).Distinct().ToList();
				List<SysMenu> level2 = bizSysMenu.ListBy(x => level2Ids.Contains(x.ID)) as List<SysMenu>;

				List<Guid> level1Ids = level2.Select(x => x.ParentID.Value).Distinct().ToList();
				List<SysMenu> level1 = bizSysMenu.ListBy(x => level1Ids.Contains(x.ID)) as List<SysMenu>;

				menuCollection.AddRange(level1);
				menuCollection.AddRange(level2);

				return menuCollection;
			}
		}

	}
}