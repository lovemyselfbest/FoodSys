using System;
using System.Collections.Generic;
using System.Linq;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using FoodSys.Enum;
using FoodSys.Web.Base;
using Project.Entity.Base;
using Project.Common;
using System.Web;

namespace FoodSys.Web.Areas.ModuleSys.Models
{
	/// <summary>
	/// 角色管理 分配权限(tab-1) molde Index
	/// 作者：黄剑锋
	/// 时间：2011-10-14
	/// </summary>
	public class ModelSysRoleManageIndex : Model
	{
		private BizSysRole bizSysRole;
		private BizSysMenu bizSysMenu;


		/// <summary>
		/// 用户类型菜单表
		/// </summary>
		private BizSysUserTypeMenu bizSysUserTypeMenu;

		private BaseSearch searchEntity;
		public BaseSearch SearchEntity
		{
			get
			{
				if (searchEntity == null)
					return new BaseSearch();
				return searchEntity;
			}
			set { searchEntity = value; }
		}

		private IList<SysMenu> nonAssignedRole = new List<SysMenu>();
		/// <summary>
		/// 未分配权限
		/// </summary>
		public IList<SysMenu> NonAssignedRole
		{
			get { return nonAssignedRole; }
			set { nonAssignedRole = value; }
		}

		private IList<SysMenu> nonAssignedRoleParent = new List<SysMenu>();
		/// <summary>
		/// 未分配权限父节点
		/// </summary>
		public IList<SysMenu> NonAssignedRoleParent
		{
			get { return nonAssignedRoleParent; }
			set { nonAssignedRoleParent = value; }
		}

		/// <summary>
		/// 已分配权限
		/// </summary>
		public IList<SysMenu> AssignedRole
		{
			get;
			set;
		}

		/// <summary>
		/// 选项卡选择的编号
		/// </summary>
		public int? SelectTabsId { get; set; }

		private IList<SysMenu> assignedRoleParent = new List<SysMenu>();
		/// <summary>
		/// 已分配权限父节点
		/// </summary>
		public IList<SysMenu> AssignedRoleParent
		{
			get { return assignedRoleParent; }
			set { assignedRoleParent = value; }
		}

		/// <summary>
		/// 用户角色ID
		/// </summary>
		public Guid? SysRoleID
		{
			get;
			set;
		}

		public void RetriveData()
		{
			//该角色实体对象
			SysRole sysRole = bizSysRole.GetFirst(x => x.ID == SysRoleID.Value);

			//角色对应的用户类型菜单列表
			IList<SysMenu> sysMenuList = bizSysMenu.ListSysMenuByUserTypeID(sysRole.UserTypeID.Value);

			//角色拥有的菜单
            AssignedRole = bizSysMenu.ListSysMenuBySysRoleID(sysRole.ID).Where(x => sysMenuList.Select(y=>y.ID).Contains(x.ID)).ToList();

			//角色拥有的菜单的ID
			IList<Guid> AssignedRoleGuid = AssignedRole.Select(x => x.ID).ToList();

			//未分配权限
			NonAssignedRole = sysMenuList.Where(x => !AssignedRoleGuid.Contains(x.ID)).ToList();

			//所有菜单
			IList<SysMenu> MenuList = bizSysMenu.ListBy(x => x.Level.Value == 1 || x.Level.Value == 2);


			// 未分配权限父节点
			NonAssignedRoleParent = GetSysMenuParentID(MenuList, NonAssignedRole);

			//已分配权限父节点
			AssignedRoleParent = GetSysMenuParentID(MenuList, AssignedRole);
		}

		/// <summary>
		/// 根据全部菜单和3级菜单列表返回3级菜单列表的所有父节点
		/// </summary>
		/// <param name="allMenuList"></param>
		/// <param name="menuList"></param>
		/// <returns></returns>
		public IList<SysMenu> GetSysMenuParentID(IList<SysMenu> allMenuList, IList<SysMenu> menuList)
		{
			IList<SysMenu> secondMenu = allMenuList.Where(x => menuList.Select(y => y.ParentID).Distinct().Contains(x.ID)).ToList();
			IList<SysMenu> firstMenu = allMenuList.Where(x => secondMenu.Select(y => y.ParentID).Distinct().Contains(x.ID)).ToList();
			IList<SysMenu> twoLevelsMenu = secondMenu.Union(firstMenu).ToList();
			return twoLevelsMenu;
		}
	}

	/// <summary>
	/// 角色管理 所属角色(tab-2) BelongsRole model
	/// 作者：黄剑锋
	/// 时间：2011-10-14
	/// </summary>
	public class ModelSysRoleManageBelongsRole : Model
	{
		private BizSysRole bizSysRole;
        private BizSysUser bizSysUser;
		private BizSysRoleMember bizSysRoleMember;
		private BizSysUserType bizSysUserType;


        private IList<SysUser> nonAssignedUser = new List<SysUser>();
		/// <summary>
		/// 不属于该角色的用户
		/// </summary>
        public IList<SysUser> NonAssignedUser
        {
            get { return nonAssignedUser; }
            set { nonAssignedUser = value; }
        }

        private IList<SysUser> assignedUser = new List<SysUser>();
        /// <summary>
        /// 所属该角色的用户
        /// </summary>
        public IList<SysUser> AssignedUser
        {
            get { return assignedUser; }
            set { assignedUser = value; }
        }

		/// <summary>
		/// 用户角色ID
		/// </summary>
		public Guid? SysRoleID { get; set; }

		public void RetriveData()
		{
			//用户类型ID
			Guid? userTypeID = bizSysRole.GetFirst(y => y.ID == SysRoleID).UserTypeID;
			//用户类型名称
			string sysUserTypeName = bizSysUserType.GetFirst(x => x.ID == userTypeID.Value).Name;

			//可访问用户ID
			IList<Guid> userGuids = new List<Guid>();

			
			userGuids = bizSysUser.List().Select(x => x.ID).ToList();


            //全部用户
            IList<SysUser> allUsers = bizSysUser.ListBy(x => userGuids.Contains(x.ID));

            //全部有角色的用户
            IList<Guid> hasRoleUser = bizSysRoleMember.List().Select(x => x.UserID.Value).Distinct().ToList();

            //根据角色查找所有用户id
            IList<Guid> sysRoleUserId = bizSysRoleMember.ListBy(x => x.RoleID == SysRoleID.Value).Select(x => x.UserID.Value).ToList();

            //所属该角色的用户
            AssignedUser = allUsers.Where(x => sysRoleUserId.Contains(x.ID)).ToList();

            //没有角色的用户
            NonAssignedUser = allUsers.Where(x => !hasRoleUser.Contains(x.ID)).ToList();

		}
	}

	/// <summary>
	/// 角色添加与删除
	/// 作者：黄剑锋
	/// 时间：2011-10-14
	/// </summary>
	public class ModelSysRoleManageMoveRoleMenu : Model
	{
		private BizSysRole bizSysRole;
		private BizSysRoleRight bizSysRoleRight;
        private BizSysLogs bizSysLogs;
        private BizSysMenu bizSysMenu;
		/// <summary>
		/// 用户角色ID
		/// </summary>
		public Guid? SysRoleID { get; set; }


		private IList<Guid> menuGuid = new List<Guid>();
		/// <summary>
		/// 所要操作的MenuGuid
		/// </summary>
		public IList<Guid> MenuGuid
		{
			get { return menuGuid; }
			set { menuGuid = value; }
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
					SysRoleRight sysRoleRight = new SysRoleRight()
					{
						MenuID = guid,
						RoleID = SysRoleID.Value
					};
					bizSysRoleRight.Save(sysRoleRight);
                    //此操作记录到日志
                    bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                        HttpContext.Current.Request.UserHostAddress,
                        Navigation,
                        bizSysMenu.GetFirst(x => x.ID == sysRoleRight.MenuID).Name,
                        sysRoleRight.ID,
						"新增", EnumLogType.操作日志, 
						string.Empty);
				}
			}
			else if (OperateType == EnumDirection.Right)
			{
				foreach (Guid guid in MenuGuid)
				{
					SysRoleRight sysRoleRight = bizSysRoleRight.GetFirst(x => x.MenuID == guid && x.RoleID == SysRoleID.Value);
					bizSysRoleRight.Delete(sysRoleRight);
                    //此操作记录到日志
                    bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                        HttpContext.Current.Request.UserHostAddress,
                        Navigation,
                        bizSysMenu.GetFirst(x => x.ID == sysRoleRight.MenuID).Name,
                        sysRoleRight.ID,
						"删除", 
						EnumLogType.操作日志, 
						string.Empty);
				}
			}
		}
	}

	/// <summary>
	/// 用户添加到角色与从角色中删除
	/// 作者：黄剑锋
	/// 时间：2011-10-14
	/// </summary>
	public class ModelSysRoleManageMoveUsers : Model
	{
		private BizSysRole bizSysRole;
		private BizSysRoleMember bizSysRoleMember;
        private BizSysLogs bizSysLogs;
		/// <summary>
		/// 用户角色ID
		/// </summary>
		public Guid? SysRoleID { get; set; }

		private IList<Guid> nonAssignedUserList = new List<Guid>();
		/// <summary>
		/// 未分配该角色的用户的ID
		/// </summary>
		public IList<Guid> NonAssignedUserList
		{
			get { return nonAssignedUserList; }
			set { nonAssignedUserList = value; }
		}

		private IList<Guid> assignedUserList = new List<Guid>();
		/// <summary>
		/// 所属该角色用户的ID
		/// </summary>
		public IList<Guid> AssignedUserList
		{
			get { return assignedUserList; }
			set { assignedUserList = value; }
		}

		/// <summary>
		/// 类型 left添加 right删除
		/// </summary>
		public EnumDirection OperateType { get; set; }

		public void RetriveData()
		{
			if (OperateType == EnumDirection.Left)
			{
				foreach (Guid guid in NonAssignedUserList)
				{
					SysRoleMember sysRoleMember = new SysRoleMember()
					{
						RoleID = SysRoleID.Value,
						UserID = guid
					};
					bizSysRoleMember.Save(sysRoleMember);
                    bizSysLogs.SaveOrUpdate(new SysLogs()
                    {
                        OperationTime = DateTime.Now,
                        OperatorName = SessionManager.CurrentSysUser.UserName,
                        MachineIP = HttpContext.Current.Request.UserHostAddress,
                        UserAccount = SessionManager.CurrentSysUser.UserAccount,
                        LogTypeID = 1,
                        LogContent = string.Format("【{0}】在【{1}】对【{2}】的【{3}】做了【{4}】操作", SessionManager.CurrentSysUser.UserName, DateTime.Now, Navigation, string.Format("{0}(ID:{1})", bizSysRole.GetFirst(x => x.ID == sysRoleMember.RoleID).RoleName, sysRoleMember.ID), "添加")
                    });
				}
			}
			else if (OperateType == EnumDirection.Right)
			{
				foreach (Guid guid in AssignedUserList)
				{
					SysRoleMember sysRoleMember = bizSysRoleMember.GetFirst(x => x.UserID == guid && x.RoleID == SysRoleID.Value);
					bizSysRoleMember.Delete(sysRoleMember);
                    bizSysLogs.SaveOrUpdate(new SysLogs()
                    {
                        OperationTime = DateTime.Now,
                        OperatorName = SessionManager.CurrentSysUser.UserName,
                        MachineIP = HttpContext.Current.Request.UserHostAddress,
                        UserAccount = SessionManager.CurrentSysUser.UserAccount,
                        LogTypeID = 1,
                        LogContent = string.Format("【{0}】在【{1}】对【{2}】的【{3}】做了【{4}】操作", SessionManager.CurrentSysUser.UserName, DateTime.Now, Navigation, string.Format("{0}(ID:{1})", bizSysRole.GetFirst(x => x.ID == sysRoleMember.RoleID).RoleName, sysRoleMember.ID), "移除")
                    });
				}
			}
		}
	}

	/// <summary>
	/// 根据页面状态 创建与编辑用户角色
	/// 作者：黄剑锋
	/// 时间：2011-10-14
	/// </summary>
	public class ModelSysRoleManageCreate : Model
	{
		private BizSysRole bizSysRole;
		private BizSysUserType bizSysUserType;

		public SysRole SysRole { get; set; }


		private IList<SysUserType> sysUserTypeList = new List<SysUserType>();
		/// <summary>
		/// 所有用户类型列表
		/// </summary>
		public IList<SysUserType> SysUserTypeList
		{
			get { return sysUserTypeList; }
			set { sysUserTypeList = value; }
		}

		/// <summary>
		/// 用户角色ID
		/// </summary>
		public Guid? sysRoleID { get; set; }

		/// <summary>
		/// 页面类型
		/// </summary>
		public EnumPageState? pageState { get; set; }

		public void RetriveData()
		{
			//查找所有用户类型
			SysUserTypeList = bizSysUserType.List();

			if (pageState == EnumPageState.编辑)
				SysRole = bizSysRole.GetFirst(x => x.ID == sysRoleID.Value);
		}

		public void Save()
		{
			//新增
			if (SysRole.ID == Guid.Empty)
			{
				SysRole.CreateTime = DateTime.Now;
				SysRole.CreateUserID = SessionManager.CurrentSysUser.ID;
				bizSysRole.Save(SysRole);
			}
			else
			{
				//修改
				bizSysRole.Update(SysRole);
			}

		}
	}
}