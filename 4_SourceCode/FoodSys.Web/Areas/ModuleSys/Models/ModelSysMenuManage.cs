using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodSys.Web.Base;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using FoodSys.Enum;
using Project.Common;
using FoodSys.Entity.EntitySearch;
using Project.Web.Base.Utility;

namespace FoodSys.Web.Areas.ModuleSys.Models
{
	/// <summary>
	/// 菜单管理 model index
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public class ModelSysMenuManageIndex : Model
	{
		private BizSysMenu bizSysMenu;
		private SearchSysMenuManageIndex searchEntity;
		public SearchSysMenuManageIndex SearchEntity
		{
			get
			{
				if (searchEntity == null)
					searchEntity = new SearchSysMenuManageIndex();
				if (string.IsNullOrEmpty(searchEntity._OrderName))
				{
					searchEntity._OrderName = ReflectionTools.GetPropertyNameFromExpression<SysMenu>(x => x.OrderIndex);
					searchEntity._OrderDirection = ((int)EnumOrder.ASC).ToString();
				}
				return searchEntity;
			}
			set { searchEntity = value; }
		}

		private Guid? menuId;
		/// <summary>
		/// 新增菜单父类ID
		/// </summary>
		public Guid? MenuId
		{
			get { return menuId; }
			set { menuId = value; }
		}
		/// <summary>
		/// 查询数据
		/// </summary>
		private IList<SysMenu> treeDataSources;
		public IList<SysMenu> TreeDataSources
		{
			get
			{
				if (treeDataSources != null)
					return treeDataSources;
				treeDataSources = bizSysMenu.List();
				return treeDataSources;
			}
		}
		/// <summary>
		/// 所有数据
		/// </summary>
		public IList<SysMenu> treeDataSourcesAll;
		/// <summary>
		/// 加载数据
		/// </summary>
		public void RetriveData()
		{
            treeDataSourcesAll = bizSysMenu.ListBy(x => x.Level == 2, x => x.OrderIndex, EnumOrder.ASC);
			ExpressionCondition<SysMenu> condition = ExpressionCondition<SysMenu>.GetInstance();
			if (!string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
			{
				condition.Or(x => x.Name, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like).
					Or(x => x.Icon, SearchEntity._CommonSearchCondition, ExpressionValueRelation.Like);
			}
			else if (MenuId != null && string.IsNullOrEmpty(SearchEntity._CommonSearchCondition))
			{
				condition.And(x => x.ParentID, MenuId, ExpressionValueRelation.Equal);

			}
			else
			{
                condition.And(x => x.Level, 1, ExpressionValueRelation.Equal);

			}

			treeDataSources = bizSysMenu.PaginateListBy(SearchEntity._PageSize.Value, SearchEntity._PageIndex ?? 0, ref count, condition.ConditionExpression, SearchEntity._OrderName, SearchEntity._OrderDirection);
			PaginateHelperObject = PaginateHelper.ConstructPaginate(SearchEntity._PageSize.Value, count, SearchEntity._PageIndex ?? 0, SearchEntity, "SearchEntity");
		}

	}

	/// <summary> 
	/// 菜单管理 新增 修改  
	/// 作者：李方磊
	/// 时间：2011-11-25
	/// </summary>
	public class ModelSysMenuManageCreate : Model
	{
		private BizSysMenu bizSysMenu;
        private BizSysLogs bizSysLogs;
		private Guid? menuId;
		/// <summary>
		/// 菜单父类ID
		/// </summary>
		public Guid? MenuId
		{
			get { return menuId; }
			set { menuId = value; }
		}

		private Guid? sysMenuId;
		/// <summary>
		/// 菜单ID
		/// </summary>
		public Guid? SysMenuId
		{
			get { return sysMenuId; }
			set { sysMenuId = value; }
		}
		/// <summary>
		/// 所有菜单列表
		/// </summary>
		public IList<SysMenu> SysMenuList { get; set; }

		/// <summary>
		/// 页面状态
		/// </summary>
		public EnumPageState? PageState { get; set; }

		private SysMenu sysMenuEntity = new SysMenu();
		/// <summary>
		/// 菜单实体
		/// </summary>
		public SysMenu SysMenuEntity
		{
			get { return sysMenuEntity; }
			set { sysMenuEntity = value; }
		}

		public void RetriveData()
		{
			//查找所有菜单
			SysMenuList = bizSysMenu.List(x => x.ID, EnumOrder.ASC);
			if (sysMenuId != null)
			{
				sysMenuEntity = bizSysMenu.GetFirst(x => x.ID == sysMenuId);
			}
		}

		/// <summary>
		/// 保存新增或修改菜单信息
		/// </summary>
		public void Save()
		{
			//如果是新增
			if (SysMenuEntity.ID == Guid.Empty && menuId == null)
			{
                SysMenuEntity.Level = 1;
				sysMenuEntity.ParentID = null;
				bizSysMenu.Save(SysMenuEntity);
                //此操作记录到日志
                bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                    HttpContext.Current.Request.UserHostAddress,
                    Navigation,
                    SysMenuEntity.Name,
                    SysMenuEntity.ID,
					"新增", EnumLogType.操作日志, 
					string.Empty);
			}
			else
			{
				if (SysMenuEntity.ID == Guid.Empty && menuId != null)
				{
					SysMenu sysMenu = bizSysMenu.GetFirst(x => x.ID == menuId);
                    SysMenuEntity.Level = sysMenu.Level + 1;
					sysMenuEntity.ParentID = menuId;
					bizSysMenu.Save(SysMenuEntity);
                    //此操作记录到日志
                    bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                        HttpContext.Current.Request.UserHostAddress,
                        Navigation,
                        SysMenuEntity.Name,
                        SysMenuEntity.ID,
						"更新", 
						EnumLogType.操作日志, 
						string.Empty);
				}//修改 
				else
				{
					//取出源数据 进行修改
					SysMenu sysMenu = bizSysMenu.GetFirst(x => x.ID == SysMenuEntity.ID);
					sysMenu.Name = SysMenuEntity.Name;
					sysMenu.TargetURL = SysMenuEntity.TargetURL;
					sysMenu.Icon = SysMenuEntity.Icon;
					sysMenu.OrderIndex = SysMenuEntity.OrderIndex;
					bizSysMenu.Update(sysMenu);
                    //此操作记录到日志
                    bizSysLogs.SaveOrUpdate(SessionManager.CurrentSysUser,
                        HttpContext.Current.Request.UserHostAddress,
                        Navigation,
                        sysMenu.Name,
                        sysMenu.ID,
						"更新", EnumLogType.操作日志, string.Empty);
				}
			}
		}
	}
}
