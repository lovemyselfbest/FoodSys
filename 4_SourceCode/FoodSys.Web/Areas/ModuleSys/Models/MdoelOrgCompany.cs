using System;
using System.Collections.Generic;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using FoodSys.Entity.EntitySearch;
using FoodSys.Enum;
using FoodSys.Web.Base;
using Project.Common;
using Project.Web.Base.Utility;
using System.Web;

namespace FoodSys.Web.Areas.ModuleSys.Models
{
	/// <summary>
	/// 企业信息维护管理
	/// 作者：尤啸
	/// 时间：2012-06-27
	/// </summary>
    public class MdoelOrgCompanyIndex : Model
    {
        private BizUTOrgCompany bizUTOrgCompany;

        private SearchOrgCompany searchEntity;
        /// <summary>
        /// 搜索条件
        /// </summary>
        public SearchOrgCompany SearchEntity
        {
            get
            {
                if (searchEntity == null)
                    searchEntity = new SearchOrgCompany();
                if (string.IsNullOrEmpty(searchEntity._OrderName))
                {
                    searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<UTOrgCompany>(x => x.Name);
                    searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
                }
                return searchEntity;
            }
            set
            {
                searchEntity = value;
            }
        }

        private IList<UTOrgCompany> uTOrgCompanyCollection = new List<UTOrgCompany>();
        /// <summary>
        ///  企业信息 集合
        /// </summary>
        public IList<UTOrgCompany> UTOrgCompanyCollection
        {
            get { return uTOrgCompanyCollection; }
            set { uTOrgCompanyCollection = value; }
        }

        public void RetriveData()
        {
            ExpressionCondition<UTOrgCompany> condition = ExpressionCondition<UTOrgCompany>.GetInstance();
            if (string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
                condition.And(x => x.Name, SearchEntity._CompanyName, ExpressionValueRelation.Like)
                    .And(x => x.OrganizationCode, SearchEntity._CompanyCode, ExpressionValueRelation.Like);
            else
                condition.Or(x => x.OrganizationCode, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
                    .Or(x => x.Name, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
                    .Or(x => x.LinkMan, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
                    .Or(x => x.LicenseNo, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
                    .Or(x => x.TaxRegNO, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
                    .Or(x => x.Address, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
                    .Or(x => x.Tel, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
                    .Or(x => x.Email, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

            //导出全部数据
            if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出全部)
            {
                UTOrgCompanyCollection = bizUTOrgCompany.ListBy(condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
                ExportExcel(UTOrgCompanyCollection);
                return;
            }
            UTOrgCompanyCollection = bizUTOrgCompany.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
            PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");

            //导出部分
            if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出本页)
            {
                ExportExcel(UTOrgCompanyCollection);
                return;
            }
        }

        public void ExportExcel(IList<UTOrgCompany> datasource)
        {
            if (ExportObject.ExportType == null)
                return;
            var exportInstance = ExportHelper.GetInstance();
            exportInstance.ExportExcel<UTOrgCompany>(datasource, ExportObject, "企业信息");
        }
    }

	/// <summary>
	/// 新增、修改、查看 企业信息
    /// 作者：尤啸
    /// 时间：2012-06-27
	/// </summary>
    public class MdoelOrgCompanyCreate : Model
    {
        private BizUTOrgCompany bizUTOrgCompany;

        /// <summary>
        /// 页面状态
        /// </summary>
        public EnumPageState? PageState { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public Guid? UTOrgCompanyID { get; set; }

        private UTOrgCompany uTOrgCompanyEntity = new UTOrgCompany();
        /// <summary>
        /// 企业表 实体
        /// </summary>
        public UTOrgCompany UTOrgCompanyEntity
        {
            get { return uTOrgCompanyEntity; }
            set { uTOrgCompanyEntity = value; }
        }

        public void RetriveData()
        {
            //如果企业编号不能NULL  则查找该实体
            if (UTOrgCompanyID != null)
                UTOrgCompanyEntity = bizUTOrgCompany.GetFirst(x => x.ID == UTOrgCompanyID);
        }

        /// <summary>
        /// 保存 新增或修改
        /// </summary>
        public void Save()
        {
            bizUTOrgCompany.SaveOrUpdate(UTOrgCompanyEntity);
        }
    }

	public class ModelOrgCompanySelect : Model
	{
		private BizUTOrgCompany bizUTOrgCompany;
		private SearchOrgCompany searchEntity;
		/// <summary>
		/// 搜索条件
		/// </summary>
		public SearchOrgCompany SearchEntity
		{
			get
			{
				if (searchEntity == null)
					searchEntity = new SearchOrgCompany();
				if (string.IsNullOrEmpty(searchEntity._OrderName))
				{
					searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<UTOrgCompany>(x => x.Name);
					searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
				}
				return searchEntity;
			}
			set
			{
				searchEntity = value;
			}
		}

		/// <summary>
		///  企业信息 集合
		/// </summary>
		public IList<UTOrgCompany> GridDatasource { get; set; }

		public void RetriveData()
		{
			ExpressionCondition<UTOrgCompany> condition = ExpressionCondition<UTOrgCompany>.GetInstance();
			if (string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
				condition.And(x => x.OrganizationCode, SearchEntity._CompanyCode, ExpressionValueRelation.Like).
					And(x => x.Name, SearchEntity._CompanyName, ExpressionValueRelation.Like);
			else
				condition.Or(x => x.OrganizationCode, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
					Or(x => x.Name, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
					Or(x => x.LinkMan, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
					Or(x => x.Email, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

			GridDatasource = bizUTOrgCompany.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
			PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");

		}


	}
}
