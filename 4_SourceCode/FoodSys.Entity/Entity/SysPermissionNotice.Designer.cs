using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 首页提醒权限控制
	/// </summary>
	[MetadataType(typeof(SysPermissionNotice_Validation ))]
	public partial class  SysPermissionNotice 
	{
		
		
			/// <summary>
			/// ID
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 名称
			/// Length : 50
			/// </summary>
			public virtual System.String	Name{get;set;}
					 
			
			/// <summary>
			/// 菜单ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	MenuID{get;set;}
					 
			
			/// <summary>
			/// 目标页面
			/// Length : 500
			/// </summary>
			public virtual System.String	TargetURL{get;set;}
					 
			
			/// <summary>
			/// 查询语句
			/// Length : -1
			/// </summary>
			public virtual System.String	Sql{get;set;}
					 
			
			/// <summary>
			/// 排序
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	SortOrder{get;set;}
					 
			
			/// <summary>
			/// 图片
			/// Length : 50
			/// </summary>
			public virtual System.String	Icon{get;set;}
					 
			
			/// <summary>
			/// 备注
			/// Length : 500
			/// </summary>
			public virtual System.String	Remark{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
