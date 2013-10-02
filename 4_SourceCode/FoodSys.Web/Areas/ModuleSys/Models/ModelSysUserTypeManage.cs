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
using System.Web.Mvc;

namespace FoodSys.Web.Areas.ModuleSys.Models
{

	/// <summary>
	/// 用户类型管理 分配用户类型 molde Index
	/// 作者：尤啸
	/// 时间：2012-06-28
	/// </summary>
	public class ModelSysUserTypeManageIndex : Model
	{
		private BizSysUserType bizSysUserType;
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



		private IList<SysMenu> nonAssignedUserType = new List<SysMenu>();
		/// <summary>
		/// 未分配用户类型
		/// </summary>
		public IList<SysMenu> NonAssignedUserType
		{
			get { return nonAssignedUserType; }
			set { nonAssignedUserType = value; }
		}


		private IList<SysMenu> nonAssignedUserTypeParent = new List<SysMenu>();
		/// <summary>
		/// 未分配用户类型父节点
		/// </summary>
		public IList<SysMenu> NonAssignedUserTypeParent
		{
			get { return nonAssignedUserTypeParent; }
			set { nonAssignedUserTypeParent = value; }
		}

		/// <summary>
		/// 已分配用户类型
		/// </summary>
		public IList<SysMenu> AssignedUserType { get; set; }

		/// <summary>
		///所有菜单
		/// </summary>
		public IList<SysMenu> AllMenu { get; set; }

		/// <summary>
		/// 选项卡选择的编号
		/// </summary>
		public int? SelectTabsId { get; set; }


		private IList<SysMenu> assignedUserTypeParent = new List<SysMenu>();
		/// <summary>
		/// 已分配用户类型父节点
		/// </summary>
		public IList<SysMenu> AssignedUserTypeParent
		{
			get { return assignedUserTypeParent; }
			set { assignedUserTypeParent = value; }
		}

		/// <summary>
		/// 用户类型ID
		/// </summary>
		public Guid? SysUserTypeID { get; set; }

		public void RetriveData()
		{
			//该角色实体对象
			SysUserType sysUserType = bizSysUserType.GetFirst(x => x.ID == SysUserTypeID.Value);

			//已经分配用户类型对应的菜单列表
			AssignedUserType = bizSysMenu.ListSysMenuByUserTypeID(sysUserType.ID);

			//用户类型拥有的菜单的ID
			IList<Guid> AssignedUserTypeGuid = AssignedUserType.Select(x => x.ID).ToList();

			//所有菜单列表
			AllMenu = bizSysMenu.ListBy(x => x.Level != 1 && x.Level != 2);
			//未分配用户类型
			NonAssignedUserType = AllMenu.Where(x => !AssignedUserTypeGuid.Contains(x.ID)).ToList();

			//2级以上菜单
			IList<SysMenu> MenuList = bizSysMenu.ListBy(x => x.Level.Value == 1 || x.Level.Value == 2);


			// 未分配权限父节点
			NonAssignedUserTypeParent = GetSysMenuParentID(MenuList, NonAssignedUserType);

			//已分配权限父节点
			AssignedUserTypeParent = GetSysMenuParentID(MenuList, AssignedUserType);

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
	/// 用户类型添加与删除
    /// 作者：尤啸
    /// 时间：2012-06-28
	/// </summary>
    public class ModelSysUserTypeManageMoveUserTypeMenu : Model
    {
        private BizSysUserType bizSysUserType;
        private BizSysUserTypeMenu bizSysUserTypeMenu;
        private BizSysUserRight bizSysUserRight;
        private BizSysRoleRight bizSysRoleRight;
        private BizSysLogs bizSysLogs;
        private BizSysMenu bizSysMenu;
        /// <summary>
        /// 用户类型ID
        /// </summary>
        public Guid? SysUserTypeID { get; set; }

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

        public string MoveMenu()
        {
            if (OperateType == EnumDirection.Left)
            {
                foreach (Guid guid in MenuGuid)
                {
                    SysUserTypeMenu sysUserTypeMenu = new SysUserTypeMenu()
                    {
                        MenuID = guid,
                        UserTypeID = SysUserTypeID.Value
                    };

                    bizSysUserTypeMenu.Save(sysUserTypeMenu);
                    //此操作记录到日志
                    bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                        HttpContext.Current.Request.UserHostAddress,
                        Navigation,
                        bizSysMenu.GetFirst(x => x.ID == sysUserTypeMenu.MenuID).Name,
                        sysUserTypeMenu.ID,
						"新增", EnumLogType.操作日志, 
						string.Empty);
                }              

            }
            else if (OperateType == EnumDirection.Right)
            {
                IList<SysUserTypeMenu> lisSysUserTypeMenu = bizSysUserTypeMenu.ListIn(MenuGuid, x => x.MenuID).Where(x => x.UserTypeID == SysUserTypeID.Value).ToList();
                IList<SysUserRight> listUserRight = bizSysUserRight.ListIn(MenuGuid, x => x.MenuID);
                IList<SysRoleRight> listRoleRight = bizSysRoleRight.ListIn(MenuGuid, x => x.MenuID);
                if (listUserRight.Count == 0 && listRoleRight.Count == 0)
                {
                    for (int i = 0; i < lisSysUserTypeMenu.Count; i++)
                    {
                        bizSysUserTypeMenu.DeleteMenu(lisSysUserTypeMenu[i], 
                            SessionManager.CurrentSysUser,
                            HttpContext.Current.Request.UserHostAddress,
                            Navigation,
                            bizSysMenu.GetFirst(x => x.ID == lisSysUserTypeMenu[i].MenuID).Name,
                            lisSysUserTypeMenu[i].ID,
                            "删除", EnumLogType.操作日志);
                      
                    }
                }
                else
                {
                    return FoodSys.Resources.Properties.Resources.M10014E;
                }
               
            }
            return string.Empty;
        }
    }

	/// <summary>
	/// 根据页面状态 创建与编辑用户类型
    /// 作者：尤啸
    /// 时间：2012-06-28
	/// </summary>
	public class ModelSysUserTypeManageCreate : Model
	{
		private BizSysUserType bizSysUserType;
		public SysUserType SysUserType { get; set; }
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
		/// 用户类型ID
		/// </summary>
		public Guid? sysUserTypeID { get; set; }

		/// <summary>
		/// 页面类型
		/// </summary>
		public EnumPageState? pageState { get; set; }

		public void RetriveData()
		{
			sysUserTypeList = bizSysUserType.List();
			if (pageState == EnumPageState.编辑)
				SysUserType = bizSysUserType.GetFirst(x => x.ID == sysUserTypeID.Value);
		}

		public void Save()
		{
			//新增
			if (SysUserType.ID == Guid.Empty)
			{
				bizSysUserType.Save(SysUserType);
			}
			else
			{
				//修改
				bizSysUserType.Update(SysUserType);
			}

		}
	}
}