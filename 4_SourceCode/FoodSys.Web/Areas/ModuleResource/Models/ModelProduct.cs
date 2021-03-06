﻿using System;
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
	public class ModelProductIndex : Model
	{
		private BizUVProduct bizUVProduct;
		private BizUTProductType bizUTProductType;
		private BizUTSupplier bizTUSupplier;

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
					searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<UVProduct>(x => x.Name);
					searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
				}
				return searchEntity;
			}
			set
			{
				searchEntity = value;
			}
		}

		private IList<UVProduct> uVProductCollection = new List<UVProduct>();
		/// <summary>
		///    集合
		/// </summary>
		public IList<UVProduct> UVProductCollection
		{
			get { return uVProductCollection; }
			set { uVProductCollection = value; }
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
			UTSupplierCollection = bizTUSupplier.ListBy(x => x.Status == (int)EnumSupplierStatus.正常).OrderBy(y => y.Name).ToList();
			UTProductTypeCollection = bizUTProductType.ListBy(x => x.Status == (int)EnumProductStatus.正常, "SortIndex", EnumOrder.ASC);
			ExpressionCondition<UVProduct> condition = ExpressionCondition<UVProduct>.GetInstance();
			if (string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
			{
				condition.And(x => x.Name, SearchEntity._Name, ExpressionValueRelation.Like)
					.And(x => x.ProductType, SearchEntity._ProductType, ExpressionValueRelation.Equal)
					.And(x => x.PurchasePrice, SearchEntity._PurchasePriceS, ExpressionValueRelation.GreaterThanOrEqual)
					.And(x => x.PurchasePrice, SearchEntity._PurchasePriceE, ExpressionValueRelation.LessThanOrEqual)
					.And(x => x.SellPrice, SearchEntity._SellPriceS, ExpressionValueRelation.GreaterThanOrEqual)
					.And(x => x.SellPrice, SearchEntity._SellPriceE, ExpressionValueRelation.LessThanOrEqual)
					.And(x => x.Status, SearchEntity._Status, ExpressionValueRelation.Equal)
					.And(x => x.SupplierID, SearchEntity._SupplierID, ExpressionValueRelation.Equal);


			}
			else
				condition
					.Or(x => x.Name, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
					.Or(x => x.PurchasePrice, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
					.Or(x => x.SellPrice, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
					.Or(x => x.SupplierName, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like)
					.Or(x => x.TypeName, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

			//导出全部数据
			if (ExportObject.ExportType != null && ExportObject.ExportType == ExportType.导出全部)
			{
				return;
			}
			UVProductCollection = bizUVProduct.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
			PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");

		}
	}



	public class ModelProductCreate : Model
	{
		private BizUTProduct bizUTProduct;
		private BizUTProductType bizUTProductType;
		private BizUTSupplier bizTUSupplier;
		private BizSysCodeInfo bizSysCodeInfo;

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

		private IList<UTSupplier> uTSupplierCollection = new List<UTSupplier>();
		/// <summary>
		///    集合
		/// </summary>
		public IList<UTSupplier> UTSupplierCollection
		{
			get { return uTSupplierCollection; }
			set { uTSupplierCollection = value; }
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

		private IList<SysCodeInfo> sysCodeInfoCollection = new List<SysCodeInfo>();
		/// <summary>
		///    集合
		/// </summary>
		public IList<SysCodeInfo> SysCodeInfoCollection
		{
			get { return sysCodeInfoCollection; }
			set { sysCodeInfoCollection = value; }
		}

		/// <summary>
		/// 图片
		/// </summary>
		public HttpPostedFileBase ImgAddress
		{
			get;
			set;
		}

		public void RetriveData()
		{
			UTSupplierCollection = bizTUSupplier.ListBy(x => x.Status == (int)EnumSupplierStatus.正常).OrderBy(y => y.Name).ToList();
			UTProductTypeCollection = bizUTProductType.ListBy(x => x.Status == (int)EnumProductStatus.正常, "SortIndex", EnumOrder.ASC);
			SysCodeInfoCollection = bizSysCodeInfo.ListBy(x => x.Type == "ProductUnit" && x.IsActive == true, "SortOrder", EnumOrder.ASC);

			//如果编号不能NULL则查找该实体
			if (ID != null)
				UTProductEntity = bizUTProduct.GetFirst(x => x.ID == ID);
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
			UTProductEntity.ImgAddress = ImgAddress == null ? UTProductEntity.ImgAddress : UploadHelper.SaveOriginalImgFile(ImgAddress);
			UploadHelper.CutImg(ImgAddress, UTProductEntity.ImgAddress, 300, 300);

			bizUTProduct.SaveOrUpdate(UTProductEntity);
		}
	}

}