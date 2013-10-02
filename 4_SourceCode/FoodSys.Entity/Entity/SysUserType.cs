using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc; 
namespace FoodSys.Entity {

	/// <summary>
	/// 用户类型
	/// </summary>
	public class  SysUserType_Validation
	{
		

					/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
						/// <summary>
			/// 类型名称
			/// Length : 50
			/// </summary>
            [Required(ErrorMessage = "请输入用户类型名称！")]
            [Remote("RemoteCheckUserTypeName", "SysUserTypeManage", AdditionalFields = "ID", ErrorMessage = "此用户类型已经存在！")]
			public virtual System.String	Name{get;set;}
					 
				}
}
		