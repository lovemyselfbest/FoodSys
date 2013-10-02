using System.Collections.Generic; 
using System.Text; 
using System; 
namespace FoodSys.Entity {

	/// <summary>
	/// 角色权限表
	/// </summary>
	public class  SysRoleRight_Validation
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
					 
				}
}
		