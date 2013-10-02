using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 角色成员
	/// </summary>
	public class  SysRoleMember_Validation
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
            [Required(ErrorMessage = "请选择角色！")]
			public virtual System.Guid	RoleID{get;set;}
					 
						/// <summary>
			/// 用户ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UserID{get;set;}
					 
				}
}
		