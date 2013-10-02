using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 用户类型菜单表
	/// </summary>
	[MetadataType(typeof(SysUserTypeMenu_Validation ))]
	public partial class  SysUserTypeMenu 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	UserTypeID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	MenuID{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
