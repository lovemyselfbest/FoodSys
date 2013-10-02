using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 用户权限表（三类用户：内部用户、租户、商户）
	/// </summary>
	[MetadataType(typeof(SysUserRight_Validation ))]
	public partial class  SysUserRight 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 用户ID(存在三类用户，内部用户、租户、商户)
			/// Length : 
			/// </summary>
			public virtual System.Guid	UserID{get;set;}
					 
			
			/// <summary>
			/// 菜单ID
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
