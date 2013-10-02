using System;
using System.Collections.Generic;
using System.Linq;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using FoodSys.Enum;
using FoodSys.Web.Base;
using Project.Common;
using System.Web;

namespace FoodSys.Web.Areas.ModuleSys.Models
{
	public class ModelSysUserRightManage
	{
	}

	/// <summary>
	/// 权限管理 index
	/// 作者：黄剑锋
	/// 时间：2011-10-17
	/// </summary>
	public class ModelSysUserRightManageIndex : Model
	{
		private BizSysRole bizSysRole;
		private BizSysMenu bizSysMenu;

		/// <summary>
		/// 获取用户点击ID
		/// </summary>
		public Guid? UserId { get; set; }

		/// <summary>
		/// 未分配权限菜单
		/// </summary>
		public IList<SysMenu> NonAssignedRoleMenu { get; set; }

		/// <summary>
		/// 已分配权限菜单
		/// </summary>
		public IList<SysMenu> AssignedRoleMenu { get; set; }

		/// <summary>
		/// 未分配权限父节点
		/// </summary>
		public IList<SysMenu> NonAssignedRoleMenuParent { get; set; }

		/// <summary>
		/// 已分配权限父节点
		/// </summary>
		public IList<SysMenu> AssignedRoleMenuParent { get; set; }

		/// <summary>
		/// 该用户可以访问的菜单
		/// </summary>
		public IList<SysMenu> UserMenu { get; set; }

		public void RetriveData()
		{
			//该用户所属角色
			SysRole sysRole = bizSysRole.ListSysRoleByUserID(UserId.Value);

			//该用户可以访问的所有菜单
			IList<SysMenu> userTypeVisitMenu = bizSysMenu.ListSysMenuByUserTypeID(sysRole == null ? Guid.Empty : sysRole.UserTypeID.Value);

			//该用户角色可以访问的所有菜单
			IList<SysMenu> userRoleMenu = bizSysMenu.ListSysMenuBySysRoleID(sysRole == null ? Guid.Empty : sysRole.ID).Where(x=>userTypeVisitMenu.Select(y=>y.ID).Contains(x.ID)).ToList();

			//该用户可以访问的菜单
			UserMenu = bizSysMenu.ListSysMenuByUserId(UserId.Value).Distinct().ToList().Where(x=>userTypeVisitMenu.Select(y=>y.ID).Contains(x.ID)).ToList();

			//用户已分配权限菜单
			AssignedRoleMenu = UserMenu.Union(userRoleMenu).ToList();

			//用户未分配权限菜单
			NonAssignedRoleMenu = userTypeVisitMenu.Where(x => !AssignedRoleMenu.Select(s => s.ID).Contains(x.ID)).ToList();

			//所有菜单
			IList<SysMenu> MenuList = bizSysMenu.ListBy(x => x.Level.Value == 1 || x.Level.Value == 2);
            
			// 未分配权限父节点
			NonAssignedRoleMenuParent = GetSysMenuParentID(MenuList, NonAssignedRoleMenu);

			//已分配权限父节点
			AssignedRoleMenuParent = GetSysMenuParentID(MenuList, AssignedRoleMenu);
		}

		/// <summary>
		/// 根据全部菜单和3级菜单列表返回3级菜单列表的所有父节点
		/// </summary>
		/// <param name="allMenuList"></param>
		/// <param name="menuList"></param>
		/// <returns></returns>
		public IList<SysMenu> GetSysMenuParentID(IList<SysMenu> allMenuList, IList<SysMenu> menuList)
		{
			IList<SysMenu> sencondMenu = allMenuList.Where(x => menuList.Select(y => y.ParentID).Distinct().Contains(x.ID)).ToList();
			IList<SysMenu> firstMenu = allMenuList.Where(x => sencondMenu.Select(y => y.ParentID).Distinct().Contains(x.ID)).ToList();
			IList<SysMenu> twoLevelsMenu = sencondMenu.Union(firstMenu).ToList();
			return twoLevelsMenu;
		}
	}

	/// <summary>
	/// 权限管理 操作用户权限
	/// 作者：黄剑锋
	/// 时间：2011-10-17
	/// </summary>
	public class ModelSysUserRightManageMoveRoleMenu : Model
	{
		private BizSysRole bizSysRole;
		private BizSysUserRight bizSysUserRight;
        private BizSysLogs bizSysLogs;
        private BizSysMenu bizSysMenu;
		/// <summary>
		/// 用户角色ID
		/// </summary>
		public Guid? UserID
		{
			get;
			set;
		}

		/// <summary>
		/// 所要操作的MenuGuid
		/// </summary>
		/// <returns></returns>
		public IList<Guid> MenuGuid
		{
			get;
			set;
		}

		/// <summary>
		/// 类型 left添加 right删除
		/// </summary>
		public EnumDirection OperateType { get; set; }

		public void RetriveData()
		{
			if (OperateType == EnumDirection.Left)
			{
				foreach (Guid guid in MenuGuid)
				{
					SysUserRight sysUserRight = new SysUserRight()
					{
						MenuID = guid,
						UserID = UserID.Value
					};

					bizSysUserRight.Save(sysUserRight);
                    //此操作记录到日志
                    bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                        HttpContext.Current.Request.UserHostAddress,
                        Navigation,
                        bizSysMenu.GetFirst(x => x.ID == sysUserRight.MenuID).Name,
                        sysUserRight.ID,
						"新增", EnumLogType.操作日志, 
						string.Empty);
				}
			}
			else if (OperateType == EnumDirection.Right)
			{
				foreach (Guid guid in MenuGuid)
				{
					SysUserRight sysUserRight = bizSysUserRight.GetFirst(x => x.MenuID == guid && x.UserID == UserID.Value);
					bizSysUserRight.Delete(sysUserRight);
                    //此操作记录到日志
                    bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                        HttpContext.Current.Request.UserHostAddress,
                        Navigation,
                        bizSysMenu.GetFirst(x => x.ID == sysUserRight.MenuID).Name,
                        sysUserRight.ID,
						"删除", 
						EnumLogType.操作日志, 
						string.Empty);
				}
			}
		}
	}
}