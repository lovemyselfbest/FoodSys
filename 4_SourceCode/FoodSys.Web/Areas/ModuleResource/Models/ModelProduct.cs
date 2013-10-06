using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodSys.Web.Base;
using FoodSys.Biz.BizAccess;
using Project.Common;
using FoodSys.Entity;
using Project.Web.Base.Utility;
using FoodSys.Entity.EntitySearch;

namespace FoodSys.Web.Areas.ModuleResource.Models
{
	public class ModelProductIndex : Model
	{
		private BizUTProduct bizUTProduct;

		private SearchProduct searchEntity;
		/// <summary>
		/// 搜索条件
		/// </summary>
		public SearchProduct SearchEntity
		{
			get
			{
				if (searchEntity == null)
					searchEntity = new SearchProduct();
				if (string.IsNullOrEmpty(searchEntity._OrderName))
				{
					searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<UTProduct>(x => x.Name);
					searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
				}
				return searchEntity;
			}
			set
			{
				searchEntity = value;
			}
		}

		private IList<UTProduct> uTProductCollection = new List<UTProduct>();
		/// <summary>
		///    集合
		/// </summary>
		public IList<UTProduct> UTProductCollection
		{
			get { return uTProductCollection; }
			set { uTProductCollection = value; }
		}

		public void RetriveData()
		{
			ExpressionCondition<UTProduct> condition = ExpressionCondition<UTProduct>.GetInstance();
			if (string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
				condition.And(x => x.Name, SearchEntity._Name, ExpressionValueRelation.Like)
					.And(x => x.ProductType, SearchEntity.PurchasePriceS, ExpressionValueRelation.Like);
			else
				condition
					.Or(x => x.Name, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
					.Or(x => x.ProductType, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
					.Or(x => x.SupplierID, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

			//导出全部数据
			if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出全部)
			{
				 
				return;
			}
			UTProductCollection = bizUTProduct.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
			PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");
 
		}
	}



	public class ModelProductCreate : Model
	{
		private BizUTProduct bizUProduct;

		/// <summary>
		/// 页面状态
		/// </summary>
		public EnumPageState? PageState { get; set; }

		/// <summary>
		/// ID
		/// </summary>
		public Guid? ID { get; set; }

		private UTProduct uTProductEntity = new UTProduct();
		/// <summary>
		/// 企业表 实体
		/// </summary>
		public UTProduct UTProductEntity
		{
			get { return uTProductEntity; }
			set { uTProductEntity = value; }
		}


		public void RetriveData()
		{
			//如果编号不能NULL  则查找该实体
			if (ID != null)
				UTProductEntity = bizUProduct.GetFirst(x => x.ID == ID);
		}

		/// <summary>
		/// 保存 新增或修改
		/// </summary>
		public void Save()
		{
			if (UTProductEntity.ID == Guid.Empty)
			{
				UTProductEntity.CreateDate = DateTime.Now;
				UTProductEntity.CreateID = SessionManager.CurrentSysUser.ID;
			}
			else
			{
				UTProductEntity.UpdateDate = DateTime.Now;
				UTProductEntity.UpdateID = SessionManager.CurrentSysUser.ID;
			}

			bizUProduct.SaveOrUpdate(UTProductEntity);
		}
	}

}