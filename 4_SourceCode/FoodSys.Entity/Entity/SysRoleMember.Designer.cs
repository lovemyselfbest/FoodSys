using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 角色成员
	/// </summary>
	[MetadataType(typeof(SysRoleMember_Validation ))]
	public partial class  SysRoleMember 
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
			/// 用户ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UserID{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
