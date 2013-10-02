using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FoodSys.Enum;
using FoodSys.Web.Areas.ModuleSys.Models;
using FoodSys.Web.Base;
using Project.Web.Base;
using FoodSys.Web.Areas.ModuleSys.Models;
using Project.Common;

namespace FoodSys.Web.Areas.ModuleSys.Controllers
{
	/// <summary>
	/// 权限管理
	/// 作者：尤啸
	/// 时间：2012-06-27
	/// </summary>
	public class SysUserRightManageController : BaseController
	{
		/// <summary>
		/// 权限管理 index
		/// </summary>
		/// <param name="model">权限管理模型Index</param>
		/// <param name="id">用户ID</param>
		public ActionResult Index(ModelSysUserRightManageIndex model, Guid? id)
		{
			model.UserId = id;
			model.RetriveData();
			return View(model);
		}


		/// <summary>
		/// 移动角色菜单
		/// </summary>
		public ActionResult MoveRoleMenu(IList<Guid> menuGuid, EnumDirection operateType, Guid? userID)
		{
			ModelSysUserRightManageMoveRoleMenu model = new ModelSysUserRightManageMoveRoleMenu();
			model.UserID = userID;
			model.MenuGuid = menuGuid;
			model.OperateType = operateType;
			model.RetriveData();

			return RedirectToAction("Index", "SysUserRightManage", new { id = userID });
		}

	}
}
