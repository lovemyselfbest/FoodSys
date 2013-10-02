using System;
using System.Collections.Generic;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using FoodSys.Entity.EntitySearch;
using FoodSys.Enum;
using FoodSys.Web.Base;
using Project.Common;
using Project.Web.Base.Utility;
using Project.Entity.Base;
using System.Web;

namespace FoodSys.Web.Areas.ModuleSys.Models
{
    /// <summary>
    /// 组织机构管理Index Model
    /// 作者：尤啸
    /// 时间：2012-07-02
    /// </summary>
    public class ModelOrganizationManageIndex : Model
    {
        private BizUTOrganization bizUTOrganization;
        private BizSysLogs bizSysLogs;
        private BaseSearch searchEntity;
        private BizSysUser bizSysUser;
        /// <summary>
        /// 所有数据
        /// </summary>
        public IList<SysUser> treeDataSourcesAll;
        
        public BaseSearch SearchEntity
        {
            get
            {
                if (searchEntity == null)
                    searchEntity = new BaseSearch();
                if (string.IsNullOrEmpty(searchEntity._OrderName))
                {
                    searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<UTOrganization>(x => x.InputDate);
                    searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
                }
                return searchEntity;
            }
            set { searchEntity = value; }
        }

        private IList<UTOrganization> treeDataSources;

        /// <summary>
        /// 组织机构
        /// </summary>
        public IList<UTOrganization> TreeDataSources
        {
            get
            {
                if (treeDataSources != null)
                    return treeDataSources;
                treeDataSources = bizUTOrganization.List(x => x.OrderIndex, EnumOrder.ASC);
                return treeDataSources;
            }
        }

        /// <summary>
        /// 组织机构列表
        /// </summary>
        public IList<UTOrganization> GridDataSources { get; set; }

        public void RetriveData()
        {
            treeDataSourcesAll = bizSysUser.List();
            ExpressionCondition<UTOrganization> condition = ExpressionCondition<UTOrganization>.GetInstance();
            if (!string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
                condition.Or(x => x.Name, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
                    Or(x => x.UpdateUser, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
                    Or(x => x.UpdateDate, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
                    Or(x => x.Remark, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);
            GridDataSources = bizUTOrganization.ListBy(condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
        }

        /// <summary>
        /// 保存或新增组织机构
        /// </summary>
        /// <param name="departmentEntity"></param>
        public void Save(UTOrganization departmentEntity)
        {
            //新增
            if (departmentEntity.ID == Guid.Empty)
            {
                departmentEntity.InputDate = DateTime.Now;
                departmentEntity.InputUser = SessionManager.CurrentSysUser.ID;
                bizUTOrganization.SaveOrUpdate(departmentEntity);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    departmentEntity.Name,
                    departmentEntity.ID,
					"新增", EnumLogType.操作日志, 
					string.Empty);
            }//修改
            else
            {
                UTOrganization uTOrgDepartment = bizUTOrganization.GetFirst(x => x.ID == departmentEntity.ID);
                uTOrgDepartment.UpdateDate = DateTime.Now;
                uTOrgDepartment.UpdateUser = SessionManager.CurrentSysUser.ID;
                uTOrgDepartment.Name = departmentEntity.Name;
                uTOrgDepartment.OrderIndex = departmentEntity.OrderIndex;
                uTOrgDepartment.Remark = departmentEntity.Remark;

                uTOrgDepartment.UpdateDate = DateTime.Now;
                uTOrgDepartment.UpdateUser = SessionManager.CurrentSysUser.ID;
                bizUTOrganization.SaveOrUpdate(uTOrgDepartment);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    departmentEntity.Name,
                    departmentEntity.ID,
					"修改", 
					EnumLogType.操作日志, 
					string.Empty);
            }

        }


    }
}