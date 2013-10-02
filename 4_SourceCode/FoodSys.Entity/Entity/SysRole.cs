using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc; 
namespace FoodSys.Entity {

	/// <summary>
	/// 角色表
	/// </summary>
	public class  SysRole_Validation
	{
		

					/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
						/// <summary>
			/// 角色名
			/// Length : 50
			/// </summary>
            [Required(ErrorMessage = "请输入角色名！")]
            [Remote("RemoteCheckRoleName", "SysRoleManage", AdditionalFields = "ID", ErrorMessage = "此角色名称已经存在！")]
			public virtual System.String	RoleName{get;set;}
					 
						/// <summary>
			/// 备注
			/// Length : 200
			/// </summary>
			public virtual System.String	Remark{get;set;}
					 
						/// <summary>
			/// 用户类型
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UserTypeID{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	CreateUserID{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	CreateTime{get;set;}
					 
				}
}
		