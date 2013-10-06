using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodSys.Web.Base;
using FoodSys.Entity.EntitySearch;
using Project.Common;
using FoodSys.Entity;
using Project.Web.Base.Utility;
using FoodSys.Biz.BizAccess;

namespace FoodSys.Web.Areas.ModuleResource.Models
{
	public class ModelSupplierIndex : Model
	{
		private BizUTSupplier bizUTSupplier;

		private SearchSupplier searchEntity;
		/// <summary>
		/// 搜索条件
		/// </summary>
		public SearchSupplier SearchEntity
		{
			get
			{
				if (searchEntity == null)
					searchEntity = new SearchSupplier();
				if (string.IsNullOrEmpty(searchEntity._OrderName))
				{
					searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<UTSupplier>(x => x.Name);
					searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
				}
				return searchEntity;
			}
			set
			{
				searchEntity = value;
			}
		}

		private IList<UTSupplier> uTSupplierCollection = new List<UTSupplier>();
		/// <summary>
		///    集合
		/// </summary>
		public IList<UTSupplier> UTSupplierCollection
		{
			get { return uTSupplierCollection; }
			set { uTSupplierCollection = value; }
		}

		public void RetriveData()
		{
			ExpressionCondition<UTSupplier> condition = ExpressionCondition<UTSupplier>.GetInstance();
			if (string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
				condition.And(x => x.Name, SearchEntity._Name, ExpressionValueRelation.Like)
					.And(x => x.Mobile, SearchEntity._Mobile, ExpressionValueRelation.Like);
			else
				condition
					.Or(x => x.Name, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
					.Or(x => x.Mobile, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
					.Or(x => x.Address, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

			//导出全部数据
			if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出全部)
			{
				//UTOrgCompanyCollection = bizUTOrgCompany.ListBy(condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
				//ExportExcel(UTOrgCompanyCollection);
				return;
			}
			UTSupplierCollection = bizUTSupplier.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
			PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");

			//导出部分
			//if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出本页)
			//{
			//    ExportExcel(UTOrgCompanyCollection);
			//    return;
			//}
		}
	}


	public class ModelSupplierCreate : Model
	{
		private BizUTSupplier bizUSupplier;

		/// <summary>
		/// 页面状态
		/// </summary>
		public EnumPageState? PageState { get; set; }

		/// <summary>
		/// ID
		/// </summary>
		public Guid? ID { get; set; }

		private UTSupplier uTSupplierEntity = new UTSupplier();
		/// <summary>
		/// 企业表 实体
		/// </summary>
		public UTSupplier UTSupplierEntity
		{
			get { return uTSupplierEntity; }
			set { uTSupplierEntity = value; }
		}


		public void RetriveData()
		{
			//如果编号不能NULL  则查找该实体
			if (ID != null)
				UTSupplierEntity = bizUSupplier.GetFirst(x => x.ID == ID);
		}

		/// <summary>
		/// 保存 新增或修改
		/// </summary>
		public void Save()
		{
			if (UTSupplierEntity.ID == Guid.Empty)
			{
				UTSupplierEntity.CreateDate = DateTime.Now;
				UTSupplierEntity.CreateID = SessionManager.CurrentSysUser.ID;
			}
			else
			{
				UTSupplierEntity.UpdateDate = DateTime.Now;
				UTSupplierEntity.UpdateID = SessionManager.CurrentSysUser.ID;
			}

			bizUSupplier.SaveOrUpdate(UTSupplierEntity);
		}
	}
}