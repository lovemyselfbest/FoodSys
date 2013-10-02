using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations;
using Project.Common; 
namespace FoodSys.Entity {

	/// <summary>
	/// 用户表（内部）
	/// </summary>
	[MetadataType(typeof(SysUser_Validation ))]
	public partial class  SysUser 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 账户
			/// Length : 50
			/// </summary>
			public virtual System.String	UserAccount{get;set;}
					 
			
		 
					 
			
			/// <summary>
			/// 账户密码
			/// Length : 50
			/// </summary>
			public virtual System.String	Password{get;set;}
					 
			
			/// <summary>
			/// 用户姓名
			/// Length : 50
			/// </summary>
			public virtual System.String	UserName{get;set;}
					 
			
			/// <summary>
			/// 移动电话
			/// Length : 20
			/// </summary>
			[Required(ErrorMessage = "请输入手机号码！")]
			[RegularExpression(RegexHelper.Mobile, ErrorMessage = "请输入正确的手机号码！")]
			public virtual System.String	Mobile{get;set;}
					 
			
			/// <summary>
			/// 电子邮箱
			/// Length : 50
			/// </summary>
			public virtual System.String	Email{get;set;}
					 
			
			/// <summary>
			/// 备忘
			/// Length : 500
			/// </summary>
			public virtual System.String	Memo{get;set;}
					 
			
			/// <summary>
			/// 状态
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	Status{get;set;}
					 
			
			/// <summary>
			/// 部门ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	DepartmentID{get;set;}
					 
			
			/// <summary>
			/// 账户创建人ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	CreateUserID{get;set;}
					 
			
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
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
