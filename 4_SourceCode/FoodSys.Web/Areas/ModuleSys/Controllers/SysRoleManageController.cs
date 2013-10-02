using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FoodSys.Biz.BizAccess;
using FoodSys.Enum;
using FoodSys.Web.Areas.ModuleSys.Models;
using FoodSys.Web.Base;
using Project.Common;
using Project.Web.Base;
using Project.Web.Base.Utility;

namespace FoodSys.Web.Areas.ModuleSys.Controllers
{
	/// <summary>
	/// 角色管理
	/// 作者：尤啸
	/// 时间：2012-06-27
	/// </summary>
	public class SysRoleManageController : BaseController
	{
		private BizSysRole bizSysRole;

		public ActionResult Index(ModelSysRoleManageIndex model, Guid? id, int? selectTabsId)
		{
			model.SelectTabsId = selectTabsId;
			model.SysRoleID = id;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 所属角色
		/// </summary>
		public ActionResult BelongsRole(ModelSysRoleManageBelongsRole model, Guid? id)
		{
			model.SysRoleID = id;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 移动角色菜单
		/// </summary>
		/// <param name="menuGuid"></param>
		/// <param name="operateType"></param>
		/// <param name="sysRoleID"></param>
		/// <returns></returns>
		public ActionResult MoveRoleMenu(IList<Guid> menuGuid, EnumDirection operateType, Guid? sysRoleID)
		{
			ModelSysRoleManageMoveRoleMenu model = new ModelSysRoleManageMoveRoleMenu();
			model.SysRoleID = sysRoleID;
			model.MenuGuid = menuGuid;
			model.OperateType = operateType;
			model.RetriveData();

			return RedirectToAction("Index", "SysRoleManage", new { id = sysRoleID });
		}

		/// <summary>
		/// 移动用户所属角色
		/// </summary>
		/// <param name="nonAssignedUserID"></param>
		/// <param name="assignedUserList"></param>
		/// <param name="operateType"></param>
		/// <param name="sysRoleID"></param>
		/// <returns></returns>
		public ActionResult MoveUsers(IList<Guid> nonAssignedUserList, IList<Guid> assignedUserList, EnumDirection operateType, Guid? sysRoleID)
		{
			ModelSysRoleManageMoveUsers model = new ModelSysRoleManageMoveUsers();
			model.SysRoleID = sysRoleID;
			model.OperateType = operateType;
			model.NonAssignedUserList = nonAssignedUserList;
			model.AssignedUserList = assignedUserList;
			model.RetriveData();
			return RedirectToAction("BelongsRole", "SysRoleManage", new { id = sysRoleID });
		}

		/// <summary>
		/// 新增或修改角色
		/// </summary>
		/// <returns></returns>
		public ActionResult Create(Guid? id, EnumPageState? pageState)
		{
			ModelSysRoleManageCreate model = new ModelSysRoleManageCreate();
			model.sysRoleID = id;
			model.pageState = pageState;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 新增或修改角色
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Create(ModelSysRoleManageCreate model)
		{
			try
			{
				model.Save();
                var option = DialogOption.GetDefaultInstance();
                option.RefreshOpenerAsynchronous = false;
                return Content(WebTools.ScriptCloseDialog(option));
			}
			catch
			{
				Error = Resources.Properties.Resources.M00002E;
				model.RetriveData();
				return View(model);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		[HttpPost]
		public ActionResult Delete(Guid id)
		{
			JsonResult jsresult = new JsonResult();
            jsresult.ContentType = Consts.CONTENT_TYPE;
			jsresult.Data = new { result = string.Empty };
			try
			{
				bizSysRole.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
				jsresult.Data = new { result = string.Empty };
			}
			catch
			{
                //删除失败，请检查此角色是否已经被使用！
                jsresult.Data = new { result = Resources.Properties.Resources.M10010E };
			}
			return jsresult;
		}

		/// <summary>
		/// 验证角色名称是否已经存在
		/// </summary>
		public ActionResult RemoteCheckRoleName(ModelSysRoleManageCreate model)
		{
			if (model.SysRole.ID != null)
				return bizSysRole.CountBy(x => x.ID != model.SysRole.ID && x.RoleName == model.SysRole.RoleName) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
			else
				return bizSysRole.CountBy(x => x.RoleName == model.SysRole.RoleName) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
		}
	}
}
