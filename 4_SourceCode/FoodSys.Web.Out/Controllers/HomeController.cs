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
        private BizSysUser bizSysUser;
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Main(ModelHomeMain model)
		{
			return View();
		}
        #region 密码修改
        /// <summary>
        /// 弹出修改密码页面
        /// </summary>
        public ActionResult ChangePassword(Guid? id)
        {
            return View();
        }

        /// <summary>
        /// 确认修改密码操作
        /// </summary>
        [HttpPost]
        public ActionResult ChangePassword(string oldPW, string newPW)
        {
            try
            {
                if (SessionManager.CurrentSysUser.Password != Security.Encrypt(oldPW))
                {
                    //原密码错误
                    Error = Resources.Properties.Resources.M10013E;
                    return View();
                }

                SessionManager.CurrentSysUser.Password = Security.Encrypt(newPW);
                bizSysUser.Update(SessionManager.CurrentSysUser);
                return Content(WebTools.ScriptCloseDialog(DialogOption.GetDefaultInstance()));
            }
            catch
            {
                return View();
            }
        }
        #endregion
	}
}
