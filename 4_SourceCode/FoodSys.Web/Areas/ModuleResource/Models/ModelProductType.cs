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
using FoodSys.Enum;

namespace FoodSys.Web.Areas.ModuleResource.Models
{
	public class ModelProductTypeIndex : Model
	{
		private BizUTProductType bizUTProductType;

		private SearchProductType searchEntity;
		/// <summary>
		/// 搜索条件
		/// </summary>
		public SearchProductType SearchEntity
		{
			get
			{
				if (searchEntity == null)
					searchEntity = new SearchProductType();
				if (string.IsNullOrEmpty(searchEntity._OrderName))
				{
					searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<UTProductType>(x => x.SortIndex);
					searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
				}
				return searchEntity;
			}
			set
			{
				searchEntity = value;
			}
		}

		private IList<UTProductType> uTProductTypeCollection = new List<UTProductType>();
		/// <summary>
		///    集合
		/// </summary>
		public IList<UTProductType> UTProductTypeCollection
		{
			get { return uTProductTypeCollection; }
			set { uTProductTypeCollection = value; }
		}

		public void RetriveData()
		{
			ExpressionCondition<UTProductType> condition = ExpressionCondition<UTProductType>.GetInstance();
			if (string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
			{
				condition.And(x => x.TypeName, SearchEntity._TypeName, ExpressionValueRelation.Like)
					.And(x => x.Status, SearchEntity._Status, ExpressionValueRelation.Equal);


			}
			else
				condition
					.Or(x => x.TypeName, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

			//导出全部数据
			if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出全部)
			{
				return;
			}
			UTProductTypeCollection = bizUTProductType.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
			PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");

		}
	}



	public class ModelProductTypeCreate : Model
	{
		private BizUTProductType bizUTProductType;
		/// <summary>
		/// 页面状态
		/// </summary>
		public EnumPageState? PageState { get; set; }

		/// <summary>
		/// ID
		/// </summary>
		public Guid? ID { get; set; }

		private UTProductType uTProductTypeEntity = new UTProductType();
		/// <summary>
		/// 企业表 实体
		/// </summary>
		public UTProductType UTProductTypeEntity
		{
			get { return uTProductTypeEntity; }
			set { uTProductTypeEntity = value; }
		}



		public void RetriveData()
		{
			//如果编号不能NULL则查找该实体
			if (ID != null)
				UTProductTypeEntity = bizUTProductType.GetFirst(x => x.ID == ID);
		}

		/// <summary>
		/// 保存 新增或修改
		/// </summary>
		public void Save()
		{
			if (UTProductTypeEntity.ID == Guid.Empty)
			{
				UTProductTypeEntity.CreateDate = DateTime.Now;
				UTProductTypeEntity.CreateID = SessionManager.CurrentSysUser.ID;
			}
			else
			{
				UTProductTypeEntity.UpdateDate = DateTime.Now;
				UTProductTypeEntity.UpdateID = SessionManager.CurrentSysUser.ID;
			}

			bizUTProductType.SaveOrUpdate(UTProductTypeEntity);
		}
	}

}