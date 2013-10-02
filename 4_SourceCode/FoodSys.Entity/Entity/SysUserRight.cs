using System.Collections.Generic; 
using System.Text; 
using System; 
namespace FoodSys.Entity {

	/// <summary>
	/// 用户权限表（三类用户：内部用户、租户、商户）
	/// </summary>
	public class  SysUserRight_Validation
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
					 
				}
}
		