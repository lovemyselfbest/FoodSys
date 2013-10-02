using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodSys.Entity;
using FoodSys.Web.Base;
using FoodSys.Biz.BizAccess;
using FoodSys.Dal;
using FoodSys.Enum;
using Project.Common;
using Project.Web.Base.Utility;

namespace FoodSys.Web.Models
{
	public class ModelTreePage
	{

	}


	/// <summary>
	/// 菜单管理 树信息
	/// 作者：尤啸
	/// 时间：2010-06-029
	/// </summary>
	public class ModelTreePageSysMenuManageIndex : Model
	{
		private BizSysMenu bizSysMenu;
		//private BizSysUserTypeMenu bizSysUserTypeMenu;

		/// <summary>
		/// 菜单列表
		/// </summary>
		public IList<SysMenu> SysMenuCollection
		{
			get;
			set;
		}

		public void RetriveData()
		{
			SysMenuCollection = bizSysMenu.List(x => x.OrderIndex, EnumOrder.ASC);
		}

	}

	/// 组织机构 树信息
	/// 作者：尤啸
	/// 时间：2010-07-03
	public class ModelTreePageSysOrganizationManageIndex : Model
	{
		private BizUTOrganization bizUTOrganization;
		/// <summary>
		/// 所有组织机构信息
		/// </summary>
		public IList<UTOrganization> UTOrganizationCollection
		{
			get;
			set;
		}
		public void RetriveData()
		{
			UTOrganizationCollection = bizUTOrganization.List(x => x.OrderIndex, EnumOrder.ASC);
		}
	}

	/// <summary>
	/// 用户类型管理 树信息
	/// 作者: 李方磊
	/// 时间: 2011/11/26
	/// </summary>
	public class ModelTreePageSysUserTypeManageIndex : Model
	{
		private BizSysUserType bizSysUserType;

		public IList<SysUserType> SysUserTypeCollection
		{
			get;
			set;
		}
		public void RetriveData()
		{
			SysUserTypeCollection = bizSysUserType.List(x => x.ID, EnumOrder.ASC);
		}
	}

	/// <summary>
	/// 权限管理 树信息
	/// 作者：尤啸
	/// 时间：2011-10-17
	/// </summary>
	public class ModelTreePageSysUserRightIndex : Model
	{
        private BizUTOrganization bizUTOrganization;
		private BizUTOrgDepartment bizUTOrgDepartment;
		private BizSysUser bizSysUser;
        /// <summary>
        /// 所有组织机构
        /// </summary>
        public IList<UTOrganization> UTOrganizationCollection { get; set; }
		/// <summary>
		/// 所有部门信息
		/// </summary>
		public IList<UTOrgDepartment> UTOrgDepartmentList { get; set; }

		/// <summary>
		/// 所有员工信息
		/// </summary>
		public IList<SysUser> SysUserList { get; set; }
		/// <summary>
		/// 员工姓名
		/// </summary>
		public string UserName { get; set; }

		public void RetriveData()
		{
			//查询所有部门信息
			UTOrgDepartmentList = bizUTOrgDepartment.List(x => x.InputDate, EnumOrder.ASC);
            //查询所有组织机构
            UTOrganizationCollection = bizUTOrganization.List(x => x.InputDate, EnumOrder.ASC);
			//所有员工信息 模糊查询
			ExpressionCondition<SysUser> condition = ExpressionCondition<SysUser>.GetInstance();
			condition.Or(x => x.UserName, UserName, ExpressionValueRelation.Like).Or(x => x.UserAccount, UserName, ExpressionValueRelation.Like);

			//查询所有员工信息
			SysUserList = bizSysUser.ListBy(condition.ConditionExpression);
			//根据员工信息查询员工所属部门
			UTOrgDepartmentList = UTOrgDepartmentList.Where(x => SysUserList.Select(s => s.DepartmentID).Contains(x.ID)).ToList();
		}
	}


	/// <summary>
	/// 部门管理 树信息
	/// 作者: 尤啸
	/// 时间: 2012-07-06
	/// </summary>
	public class ModelTreePageUTOrgDepartmentManageIndex : Model
	{
		private BizUTOrgDepartment bizUTOrgDepartment;
		private BizUTOrganization bizUTOrganization;

		public IList<UTOrgDepartment> UTOrgDepartmentCollection
		{
			get;
			set;
		}
		public IList<UTOrganization> UTOrganizationCollection
		{
			get;
			set;
		}
		public void RetriveData()
		{
			UTOrgDepartmentCollection = bizUTOrgDepartment.List(x => x.ID, EnumOrder.ASC);
			UTOrganizationCollection = bizUTOrganization.List(x => x.InputDate, EnumOrder.ASC);
		}
	}






}