using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodSys.Entity;
using FoodSys.Biz.BizAccess;
using Project.Web.Base.Utility;
using FoodSys.Entity.EntitySearch;
using FoodSys.Web.Out.Base;

namespace FoodSys.Web.Out.Models
{
	public class ModelSampleIndex : Model
	{
		private BizUTSample bizUTSupplier;
		/// <summary>
		/// 查询实体
		/// </summary>
		private SearchUTSupplier searchEntity;
		public SearchUTSupplier SearchEntity
		{
			get
			{
				if (searchEntity == null)
					return new SearchUTSupplier();
				return searchEntity;
			}
			set
			{
				searchEntity = value;
			}
		}

		/// <summary>
		/// 供应商列表
		/// </summary>
		public IList<UTSample> GridDataSources
		{
			get;
			set;
		}

		/// <summary>
		/// 支持多条件查询
		/// </summary>
		public void RetriveData()
		{
			ExpressionCondition<UTSample> condition = ExpressionCondition<UTSample>.GetInstance();
			if (string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
				condition.And(x => x.SupplierName, SearchEntity._SupplierName, ExpressionValueRelation.Like)
					.And(x => x.SupplyContent, SearchEntity._SupplyContent, ExpressionValueRelation.Like)
					.And(x => x.ContactName, SearchEntity._ContactName, ExpressionValueRelation.Like);
			else
				condition.Or(x => x.SupplierName, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
						.Or(x => x.SupplyContent, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
						.Or(x => x.ContactName, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
						.Or(x => x.Mobile, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
						.Or(x => x.Telephone, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
						.Or(x => x.Email, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
						.Or(x => x.Fax, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
						.Or(x => x.Address, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

			GridDataSources = bizUTSupplier.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
			//导出本页数据或者不导出逻辑            
			if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出本页)
			{
				ExportExcel(GridDataSources);
				return;
			}

			//导出全部数据
			if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出全部)
			{
				GridDataSources = bizUTSupplier.ListBy(condition.ConditionExpression,
				   SearchEntity._OrderName,
				   SearchEntity.EnumOrderDirection);

				ExportExcel(GridDataSources);
				return;
			}

			//分页加载
			PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");
		
		}

		/// <summary>
		/// 导出成Excel格式
		/// 作者：李方磊
		/// 时间：2011-12-01
		/// </summary>
		/// <param name="datasource">租户合同台账grid数据源</param>
		public void ExportExcel(IList<UTSample> datasource)
		{
			if (ExportObject.ExportType == null)
				return;
			var exportInstance = ExportHelper.GetInstance();
			exportInstance.ExportExcel<UTSample>(datasource, ExportObject, "Sample");
		}

	}


	public class ModelSampleCreate : Model
	{
		private BizUTSample bizUTSample;
		/// <summary>
		/// 供应商实体
		/// </summary>
		public UTSample UTSample
		{
			get;
			set;
		}


		/// <summary>
		/// 新增或者修改
		/// </summary>
		/// <param name="sampleCreateModel"></param>
		public void Save()
		{
			bizUTSample.SaveOrUpdate(UTSample);
		}
	}
}