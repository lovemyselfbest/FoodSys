using System;
using System.Collections.Generic;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using FoodSys.Enum;
using FoodSys.Web.Base;
using Project.Common;
using Project.Entity.Base;
using Project.Web.Base.Utility;
using System.Web;

namespace FoodSys.Web.Areas.ModuleSys.Models
{
	/// <summary>
	/// 员工用户管理 model index
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public class ModelSysUserManageIndex : Model
	{
		private BizSysUser bizSysUser;
		private BizUTOrgDepartment bizUTOrgDepartment;
        private BizSysLogs bizSysLogs;
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

		private Guid? departmentId;
		/// <summary>
		/// 新增的部门ID
		/// </summary>
		public Guid? DepartmentId
		{
			get { return departmentId; }
			set { departmentId = value; }
		}
        /// <summary>
        /// 根据组织机构ID得到部门
        /// </summary>
        public IList<UTOrgDepartment> UTOrgDepartmentCollection { get; set; }

        public IList<SysUser> SysUserCollection { get; set; }
        public IList<SysUser> SysUser { get; set; }
        private Guid? organizationId;
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public Guid? OrganizationId
        {
            get { return organizationId; }
            set { organizationId = value; }
        }

		private IList<UTOrgDepartment> treeDataSources;
		public IList<UTOrgDepartment> TreeDataSources
		{
			get
			{
				if (treeDataSources != null)
					return treeDataSources;
				treeDataSources = bizUTOrgDepartment.List(x => x.DepartmentName, EnumOrder.ASC);
				return treeDataSources;
			}
		}

		public IList<SysUser> GridDataSources { get; set; }

		public void RetriveData()
		{
			ExpressionCondition<SysUser> condition = ExpressionCondition<SysUser>.GetInstance();
			if (!string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
				condition.Or(x => x.UserName, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
					Or(x => x.Email, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

            if (DepartmentId != null)
            {
                condition.And(x => x.DepartmentID, DepartmentId, ExpressionValueRelation.Equal);
            }
            else if (OrganizationId != null)
            {
                UTOrgDepartmentCollection = bizUTOrgDepartment.ListBy(x => x.OrganizationID == organizationId, x => x.InputDate, EnumOrder.ASC);
                  foreach (UTOrgDepartment temp in UTOrgDepartmentCollection)
                  {
                      condition.Or(x => x.DepartmentID, temp.ID, ExpressionValueRelation.Equal);
                  }
            }		
			GridDataSources = bizSysUser.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
			PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");
		}

		/// <summary>
		/// 保存新增或修改用户信息
		/// </summary>
		/// <param name="sysUserEntity">用户实体</param>
		/// <param name="departmentId">部门ID</param>
		public void Save(SysUser sysUserEntity, Guid? departmentId)
		{
			//如果是新增
			if (sysUserEntity.ID == Guid.Empty && departmentId != Guid.Empty)
			{
				sysUserEntity.DepartmentID = departmentId;
				sysUserEntity.CreateTime = DateTime.Now;
				sysUserEntity.Status = 0;
				sysUserEntity.CreateUserID = SessionManager.CurrentSysUser.ID;
				sysUserEntity.CreateTime = DateTime.Now;
                bizSysUser.SaveOrUpdate(sysUserEntity);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    sysUserEntity.UserName,
                    sysUserEntity.ID,
					"新增", EnumLogType.操作日志, 
					string.Empty);
			}
			else if (sysUserEntity.ID != Guid.Empty)
			{
				sysUserEntity.UpdateDate = DateTime.Now;
				sysUserEntity.UpdateUser = SessionManager.CurrentSysUser.ID;
				sysUserEntity.UpdateDate = DateTime.Now;
                bizSysUser.SaveOrUpdate(sysUserEntity);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    sysUserEntity.UserName,
                    sysUserEntity.ID,
					"删除", 
					EnumLogType.操作日志, 
					string.Empty);
			}
			
		}

	}

	/// <summary> 
	/// 用户 新增 修改  model create
    /// 作者：尤啸
    /// 时间：2012-06-26
	/// </summary>
	public class ModelSysUserManageCreate : Model
	{
		private BizSysUser bizSysUser;
		private BizUTOrgDepartment bizUTOrgDepartment;
		private BizSysRole bizSysRole;
		private BizSysRoleMember bizSysRoleMember;
		private BizSysUserType bizSysUserType;
        private BizSysLogs bizSysLogs;
		private Guid? departmentId;
		/// <summary>
		/// 部门ID
		/// </summary>
		public Guid? DepartmentId
		{
			get { return departmentId; }
			set { departmentId = value; }
		}

		/// <summary>
		/// 所有部门列表
		/// </summary>
		public IList<UTOrgDepartment> UTOrgDepartmentList { get; set; }

		private SysUser sysUserEntity = new SysUser();
		/// <summary>
		/// 部门用户实体
		/// </summary>
		public SysUser SysUserEntity
		{
			get { return sysUserEntity; }
			set { sysUserEntity = value; }
		}

		/// <summary>
		/// 部门用户ID
		/// </summary>
		public Guid? SysUserID { get; set; }

		/// <summary>
		/// 页面状态
		/// </summary>
		public EnumPageState? PageState { get; set; }

		/// <summary>
		/// 所有角色列表
		/// </summary>
		public IList<SysRole> SysRoleList { get; set; }

		private SysRoleMember sysRoleMemberEntity = new SysRoleMember();
		/// <summary>
		/// 角色成员实体类
		/// </summary>
		public SysRoleMember SysRoleMemberEntity
		{
			get { return sysRoleMemberEntity; }
			set { sysRoleMemberEntity = value; }
		}
        private Guid? userTypeID;
        /// <summary>
        /// 用户类型ID
        /// </summary>
        public Guid? UserTypeID
        {
            get { return userTypeID; }
            set { userTypeID = value; }
        }

		public void RetriveData()
		{
			//查找所有部门
			UTOrgDepartmentList = bizUTOrgDepartment.List(x => x.DepartmentName, EnumOrder.ASC);
           
            SysRoleList = bizSysRole.List(x => x.RoleName, EnumOrder.ASC);

			//如果用户编号为空，则不查找该用户 否则查找该用户实体和所属角色
			if (SysUserID != null)
			{
				SysUserEntity = bizSysUser.GetFirst(x => x.ID == SysUserID);
				SysRoleMember sysRoleMember = bizSysRoleMember.GetFirst(x => x.UserID == SysUserEntity.ID);
				SysRoleMemberEntity.RoleID = sysRoleMember != null ? sysRoleMember.RoleID : Guid.Empty;
			}
		}

		/// <summary>
		/// 保存新增或修改用户信息
		/// </summary>
		public void Save()
		{
			//如果是新增
			if (SysUserEntity.ID == Guid.Empty && departmentId != Guid.Empty)
			{
				SysUserEntity.DepartmentID = DepartmentId == null ? SysUserEntity.DepartmentID : DepartmentId;
				SysUserEntity.CreateTime = DateTime.Now;
				SysUserEntity.CreateUserID = SessionManager.CurrentSysUser.ID;
				SysUserEntity.Status = (int)EnumUserStatus.已激活;
				SysUserEntity.Password = StringUtility.Encrypt(SysUserEntity.Password);
				bizSysUser.Save(SysUserEntity);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    SysUserEntity.UserName,
                    SysUserEntity.ID,
					"新增", 
					EnumLogType.操作日志, 
					string.Empty);
			}//修改 
			else if (SysUserEntity.ID != Guid.Empty)
			{
				//取出源数据 进行修改
				SysUser sysUser = bizSysUser.GetFirst(x => x.ID == SysUserEntity.ID);
				sysUser.UserAccount = SysUserEntity.UserAccount;
				sysUser.UserName = SysUserEntity.UserName;
				sysUser.DepartmentID = SysUserEntity.DepartmentID;
				sysUser.Mobile = SysUserEntity.Mobile;
				sysUser.Email = SysUserEntity.Email;
				sysUser.Memo = SysUserEntity.Memo;
				sysUser.UpdateUser = SessionManager.CurrentSysUser.ID;
				//sysUser.UserType = SysUserEntity.UserType;

				SysUserEntity.UpdateDate = DateTime.Now;
				bizSysUser.Update(sysUser);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    SysUserEntity.UserName,
                    SysUserEntity.ID,
					"修改",
					EnumLogType.操作日志, 
					string.Empty);
			}

			//查找该用户所属角色
			SysRoleMember sysRoleMember = bizSysRoleMember.GetFirst(x => x.UserID == SysUserEntity.ID);
			if (sysRoleMember == null)
				sysRoleMember = new SysRoleMember();

			//如果角色编号为NULL则返回
			if (SysRoleMemberEntity == null)
			{
				bizSysRoleMember.Delete(sysRoleMember);
				return;
			}
			sysRoleMember.RoleID = SysRoleMemberEntity.RoleID;
			sysRoleMember.UserID = SysUserEntity.ID;
			bizSysRoleMember.SaveOrUpdate(sysRoleMember);
		}
	}
}