using System;
using System.Collections.Generic;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using FoodSys.Web.Base;
using Project.Common;
using Project.Entity.Base;
using Project.Web.Base.Utility;
using FoodSys.Enum;
using System.Web;

namespace FoodSys.Web.Areas.ModuleSys.Models
{
	/// <summary>
	/// 部门管理Index Model
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public class ModelSysOrgDepartmentManageIndex : Model
	{
		private BizUTOrgDepartment bizUTOrgDepartment;
        private BizSysLogs bizSysLogs;
		private BaseSearch searchEntity;
		public BaseSearch SearchEntity
		{
			get
			{
				if (searchEntity == null)
					searchEntity = new BaseSearch();
				if (string.IsNullOrEmpty(searchEntity._OrderName))
				{
					searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<UTOrgDepartment>(x => x.InputDate);
					searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
				}
				return searchEntity;
			}
			set { searchEntity = value; }
		}
        private Guid? organizationId;
        /// <summary>
        /// 机构ID
        /// </summary>
        public Guid? OrganizationId
        {
            get { return organizationId; }
            set { organizationId = value; }
        }
		private IList<UTOrgDepartment> treeDataSources;

		/// <summary>
		/// 部门树
		/// </summary>
		public IList<UTOrgDepartment> TreeDataSources
		{
			get
			{
				if (treeDataSources != null)
					return treeDataSources;
				treeDataSources = bizUTOrgDepartment.List(x => x.OrderIndex, EnumOrder.ASC);
				return treeDataSources;
			}
		}
		/// <summary>
		/// 部门列表
		/// </summary>
		public IList<UTOrgDepartment> GridDataSources { get; set; }

		public void RetriveData()
		{
            ExpressionCondition<UTOrgDepartment> condition = ExpressionCondition<UTOrgDepartment>.GetInstance();
            if (!string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
            {
                condition.Or(x => x.DepartmentName, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
                     Or(x => x.DutyUser, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
                     Or(x => x.Email, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
                     Or(x => x.Mobile, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);
            }
            else if (OrganizationId != null && string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
            {
                condition.And(x => x.OrganizationID, OrganizationId, ExpressionValueRelation.Equal);

            }
            GridDataSources = bizUTOrgDepartment.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex.Value, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
            PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex.Value, SearchEntity, "SearchEntity");
        }

		/// <summary>
		/// 保存或新增部门
		/// </summary>
		/// <param name="departmentEntity"></param>
		public void Save(UTOrgDepartment departmentEntity)
		{
			//新增
			if (departmentEntity.ID == Guid.Empty)
			{
				departmentEntity.InputDate = DateTime.Now;
                departmentEntity.InputUser = SessionManager.CurrentSysUser.ID;

				bizUTOrgDepartment.SaveOrUpdate(departmentEntity);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    departmentEntity.DepartmentName,
                    departmentEntity.ID,
					"新增", EnumLogType.操作日志, 
					string.Empty);
			}//修改
			else
			{
				UTOrgDepartment uTOrgDepartment = bizUTOrgDepartment.GetFirst(x => x.ID == departmentEntity.ID);
				uTOrgDepartment.DepartmentName = departmentEntity.DepartmentName;
				uTOrgDepartment.DutyUser = departmentEntity.DutyUser;
				uTOrgDepartment.Mobile = departmentEntity.Mobile;
				uTOrgDepartment.Email = departmentEntity.Email;
				uTOrgDepartment.Remark = departmentEntity.Remark;

				uTOrgDepartment.UpdateDate = DateTime.Now;
                uTOrgDepartment.UpdateUser = SessionManager.CurrentSysUser.ID;
				bizUTOrgDepartment.SaveOrUpdate(uTOrgDepartment);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    departmentEntity.DepartmentName,
                    departmentEntity.ID,
					"修改", 
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
    public class ModelSysOrgDepartmentManageCreate : Model
    {
        private BizUTOrgDepartment bizUTOrgDepartment;
        private BizSysLogs bizSysLogs;
        private Guid? organizationId;
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public Guid? OrganizationId
        {
            get { return organizationId; }
            set { organizationId = value; }
        }

        /// <summary>
        /// 所有部门列表
        /// </summary>
        public IList<UTOrgDepartment> UTOrgDepartmentList { get; set; }

        private UTOrgDepartment uTOrgDepartmentEntity = new UTOrgDepartment();
        /// <summary>
        /// 部门用户实体
        /// </summary>
        public UTOrgDepartment UTOrgDepartmentEntity
        {
            get { return uTOrgDepartmentEntity; }
            set { uTOrgDepartmentEntity = value; }
        }
        /// <summary>
        /// 页面状态
        /// </summary>
        public EnumPageState? PageState { get; set; }

       

        public void RetriveData()
        {
            //查找所有部门
            UTOrgDepartmentList = bizUTOrgDepartment.List(x => x.ID, EnumOrder.ASC);
            if (organizationId != null)
            {
                uTOrgDepartmentEntity = bizUTOrgDepartment.GetFirst(x => x.ID == organizationId);
            }
            
        
        }

        /// <summary>
        /// 保存新增或修改用户信息
        /// </summary>
        public void Save()
        {
            //如果是新增
            if (UTOrgDepartmentEntity.ID == Guid.Empty && organizationId != Guid.Empty)
            {
                UTOrgDepartmentEntity.InputDate = DateTime.Now;
                UTOrgDepartmentEntity.InputUser = SessionManager.CurrentSysUser.ID;
                UTOrgDepartmentEntity.OrganizationID = organizationId;
                bizUTOrgDepartment.Save(UTOrgDepartmentEntity);
                bizSysLogs.SaveOrUpdate(new SysLogs()
                {
                    OperationTime = DateTime.Now,
                    OperatorName = SessionManager.CurrentSysUser.UserName,
                    MachineIP = HttpContext.Current.Request.UserHostAddress,
                    UserAccount = SessionManager.CurrentSysUser.UserAccount,
                    LogTypeID = 1,
                    LogContent = string.Format("【{0}】在【{1}】对【{2}】的【{3}】做了【{4}】操作", SessionManager.CurrentSysUser.UserName, DateTime.Now, Navigation, string.Format("{0}(ID:{1})", "采购单明细", UTOrgDepartmentEntity.ID), "新增")
                });

            }//修改 
            else if (UTOrgDepartmentEntity.ID != Guid.Empty)
            {
                //取出源数据 进行修改
                UTOrgDepartment uTOrgDepartment = bizUTOrgDepartment.GetFirst(x => x.ID == UTOrgDepartmentEntity.ID);
                uTOrgDepartment.DepartmentName = UTOrgDepartmentEntity.DepartmentName;
                uTOrgDepartment.DutyUser = UTOrgDepartmentEntity.DutyUser;
                uTOrgDepartment.Email = UTOrgDepartmentEntity.Email;
                uTOrgDepartment.Mobile = UTOrgDepartmentEntity.Mobile;
                uTOrgDepartment.OrderIndex = UTOrgDepartmentEntity.OrderIndex;
                uTOrgDepartment.UpdateUser = SessionManager.CurrentSysUser.ID;
                uTOrgDepartment.UpdateDate = DateTime.Now;
                uTOrgDepartment.Remark = UTOrgDepartmentEntity.Remark;
                bizUTOrgDepartment.Update(uTOrgDepartment);
                bizSysLogs.SaveOrUpdate(new SysLogs()
                {
                    OperationTime = DateTime.Now,
                    OperatorName = SessionManager.CurrentSysUser.UserName,
                    MachineIP = HttpContext.Current.Request.UserHostAddress,
                    UserAccount = SessionManager.CurrentSysUser.UserAccount,
                    LogTypeID = 1,
                    LogContent = string.Format("【{0}】在【{1}】对【{2}】的【{3}】做了【{4}】操作", SessionManager.CurrentSysUser.UserName, DateTime.Now, Navigation, string.Format("{0}(ID:{1})", "采购单明细", uTOrgDepartment.ID), "更新")
                });
            }

         
        }
    }


	/// <summary>
	/// 组织机构管理 部门信息管理
	/// 作者：黄剑锋
	/// 时间：2011-10-18
	/// </summary>
	public class ModelSysOrgDepartmentManageDetails : Model
	{
		private BizUTOrgDepartment bizUTOrgDepartment;

		/// <summary>
		/// 部门编号
		/// </summary>
		public Guid? UTOrgDepartmentID { get; set; }


		private UTOrgDepartment uTOrgDepartmentEntity;
		/// <summary>
		/// 部门表实体
		/// </summary>
		public UTOrgDepartment UTOrgDepartmentEntity
		{
			get { return uTOrgDepartmentEntity; }
			set { uTOrgDepartmentEntity = value; }
		}

		public void RetriveData()
		{
			UTOrgDepartmentEntity = bizUTOrgDepartment.GetFirst(x => x.ID == UTOrgDepartmentID);
		}

		/// <summary>
		/// 保存修改
		/// </summary>
		public void Save()
		{
			bizUTOrgDepartment.Update(UTOrgDepartmentEntity);
		}
	}


}