using System.Collections.Generic; 
using System.Text; 
using System; 
namespace FoodSys.Entity {

	/// <summary>
	/// 用户与小区关联表
	/// </summary>
	public class  SysUserXCommuity_Validation
	{
		

					/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
						/// <summary>
			/// 用户ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UserID{get;set;}
					 
						/// <summary>
			/// 小区ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	CommunityID{get;set;}
					 
						/// <summary>
			/// 创建时间
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	CreateTime{get;set;}
					 
						/// <summary>
			/// 更新用户
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UpdateUser{get;set;}
					 
						/// <summary>
			/// 更新时间
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	UpdateDate{get;set;}
					 
				}
}
		