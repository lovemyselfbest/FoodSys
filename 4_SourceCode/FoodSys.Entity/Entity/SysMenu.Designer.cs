using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 菜单表
	/// </summary>
	[MetadataType(typeof(SysMenu_Validation ))]
	public partial class  SysMenu 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 名称
			/// Length : 50
			/// </summary>
			public virtual System.String	Name{get;set;}
					 
			
			/// <summary>
			/// 父级菜单ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	ParentID{get;set;}
					 
			
			/// <summary>
			/// 排序
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	OrderIndex{get;set;}
					 
			
			/// <summary>
			/// 链接地址
			/// Length : 255
			/// </summary>
			public virtual System.String	TargetURL{get;set;}
					 
			
			/// <summary>
			/// 图标
			/// Length : 255
			/// </summary>
			public virtual System.String	Icon{get;set;}
					 
			
			/// <summary>
			/// 菜单level
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	Level{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
