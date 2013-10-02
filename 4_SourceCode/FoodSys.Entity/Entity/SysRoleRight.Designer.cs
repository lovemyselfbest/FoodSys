using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 角色权限表
	/// </summary>
	[MetadataType(typeof(SysRoleRight_Validation ))]
	public partial class  SysRoleRight 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 角色ID
			/// Length : 
			/// </summary>
			public virtual System.Guid	RoleID{get;set;}
					 
			
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
