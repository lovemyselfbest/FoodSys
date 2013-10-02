using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodSys.Biz.BizAccess;
using FoodSys.Web.Base;
using FoodSys.Web.Models;
using FoodSys.Entity;
using FoodSys.Enum;
using Project.Common;
using Project.Web.Base;
using Project.Web.Base.Utility;

namespace FoodSys.Web.Controllers
{
	public class TreePageController : BaseController
	{
		private BizUTOrgDepartment bizUTOrgDepartment;
		private BizSysRole bizSysRole;
		private BizSysCodeType bizSysCodeType;
		private BizSysMenu bizSysMenu;
		private BizSysUserType bizSysUserType;
		private BizUTOrganization bizUTOrganization;

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult SysUserTypeManageIndex()
		{
			var list = bizSysUserType.List(x => x.Name, EnumOrder.ASC);
			SelectList sysUserTypeCollenction = SelectListHelper.ComposeSelectListFromCollect(list, x => x.ID, x => x.Name);
			return View(sysUserTypeCollenction);
		}

		public ActionResult SysMenuManageIndex()
		{
			ViewBag.FrameUrl = "/ModuleSys/SysMenuManage/Index";
			var treeDataSources = bizSysMenu.List(x => x.OrderIndex, EnumOrder.ASC);
			return View(treeDataSources);
		}

		public ActionResult SysOrgDepartmentManageIndex()
		{
			ViewBag.FrameUrl = "/ModuleSys/SysOrgDepartmentManage/Index";
			ModelTreePageSysOrganizationManageIndex modelObj = new ModelTreePageSysOrganizationManageIndex();
			modelObj.RetriveData();
			return View(modelObj);
		}

		public ActionResult SysUserManageIndex(ModelTreePageUTOrgDepartmentManageIndex model)
		{
			ViewBag.FrameUrl = "/ModuleSys/SysUserManage/Index";
			model.RetriveData();
			return View(model);
		}

		public ActionResult SysRoleManageIndex()
		{
			var list = bizSysRole.List(x => x.CreateTime, EnumOrder.ASC);
			SelectList sysRoleCollection = SelectListHelper.ComposeSelectListFromCollect(list, x => x.ID, x => x.RoleName);
			return View(sysRoleCollection);
		}

		public ActionResult SysUserRightIndex(ModelTreePageSysUserRightIndex model)
		{
			model.RetriveData();
			return View(model);
		}

		public ActionResult ParameterSettingsIndex()
		{
			IList<SysCodeType> sysCodeTypeCollection = bizSysCodeType.ListBy(x => x.Class == (int)EnumCodeTypeClass.可维护, x => x.SortOrder, EnumOrder.ASC);
			return View(sysCodeTypeCollection);
		}
	}
}
