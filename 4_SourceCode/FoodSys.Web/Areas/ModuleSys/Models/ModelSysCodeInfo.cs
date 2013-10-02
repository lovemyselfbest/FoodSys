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
	public class ModelSysCodeInfo
	{
	}

	/// <summary>
	/// 参数设置管理 model
	/// 作者：尤啸
	/// 时间：2012-06-27
	/// </summary>
	public class ModelSysCodeInfoIndex : Model
	{
		private BizSysCodeInfo bizSysCodeInfo;
		private BizSysCodeType bizSysCodeType;

		private BaseSearch searchEntity;
		public BaseSearch SearchEntity
		{
			get
			{
				if (searchEntity == null)
					searchEntity = new BaseSearch();
				if (string.IsNullOrEmpty(searchEntity._OrderName))
				{
					searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<SysCodeInfo>(x => x.SortOrder);
					searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
				}
				return searchEntity;
			}
			set { searchEntity = value; }
		}

		/// <summary>
		/// 代码组类型
		/// </summary>
		public string SysCodeInfoType { get; set; }

		private IList<SysCodeInfo> sysCodeInfoCollection = new List<SysCodeInfo>();
		/// <summary>
		/// 代码组信息表 集合
		/// </summary>
		public IList<SysCodeInfo> SysCodeInfoCollection
		{
			get { return sysCodeInfoCollection; }
			set { sysCodeInfoCollection = value; }
		}

		private IList<SysCodeType> sysCodeTypeCollection = new List<SysCodeType>();
		/// <summary>
		/// 代码组类型表 集合
		/// </summary>
		public IList<SysCodeType> SysCodeTypeCollection
		{
			get { return sysCodeTypeCollection; }
			set { sysCodeTypeCollection = value; }
		}

		public void RetriveData()
		{
			ExpressionCondition<SysCodeInfo> condition = ExpressionCondition<SysCodeInfo>.GetInstance();
			if (!string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
				condition.Or(x => x.Code, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
					Or(x => x.Memo, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
					Or(x => x.Name, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);

			condition.And(x => x.Type, SysCodeInfoType, ExpressionValueRelation.Equal);

			if (SysCodeInfoType != null)
			{
				SysCodeInfoCollection = bizSysCodeInfo.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity.EnumOrderDirection);
				PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");
			}

			SysCodeTypeCollection = bizSysCodeType.List();
		}
	}

	/// <summary>
	/// 参数设置管理 Model Create
    /// 作者：尤啸
    /// 时间：2012-06-27
	/// </summary>
	public class ModelSysCodeInfoCreate : Model
	{
		private BizSysCodeInfo bizSysCodeInfo;
        private BizSysLogs bizSysLogs;

		/// <summary>
		/// 页面状态
		/// </summary>
		public EnumPageState? PageState { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string SysCodeInfoCode { get; set; }

		/// <summary>
		/// 代码组信息表ID
		/// </summary>
		public Guid? SysCodeInfoId { get; set; }

		private SysCodeInfo sysCodeInfoEntity = new SysCodeInfo();
		/// <summary>
		/// 代码组信息表 实体
		/// </summary>
		public SysCodeInfo SysCodeInfoEntity
		{
			get { return sysCodeInfoEntity; }
			set { sysCodeInfoEntity = value; }
		}

		public void RetriveData()
		{
			if (SysCodeInfoId != null)
				SysCodeInfoEntity = bizSysCodeInfo.GetFirst(x => x.ID == SysCodeInfoId);
		}

		/// <summary>
		/// 保存 新增或修改
		/// </summary>
		public void Save()
		{
			//如果是新增
			if (SysCodeInfoEntity.ID == Guid.Empty)
			{
				SysCodeInfoEntity.IsActive = true;
				bizSysCodeInfo.SaveOrUpdate(SysCodeInfoEntity);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    SysCodeInfoEntity.Name,
                    SysCodeInfoEntity.ID,
					"新增", EnumLogType.操作日志, 
					string.Empty);
			}//修改
			else if (SysCodeInfoEntity.ID != Guid.Empty)
			{
				//查找原实体
				SysCodeInfo sysCodeInfo = bizSysCodeInfo.GetFirst(x => x.ID == SysCodeInfoEntity.ID);
				//对页面字段进行更新
				sysCodeInfo.SortOrder = SysCodeInfoEntity.SortOrder;
				sysCodeInfo.Memo = SysCodeInfoEntity.Memo;
				sysCodeInfo.Name = SysCodeInfoEntity.Name;
				//更新
				bizSysCodeInfo.Update(sysCodeInfo);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    SysCodeInfoEntity.Name,
                    SysCodeInfoEntity.ID,
					"修改",
					EnumLogType.操作日志, 
					string.Empty);
               
			}


		}
	}

}