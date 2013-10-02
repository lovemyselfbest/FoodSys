using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 企业用户表
	/// </summary>
	[MetadataType(typeof(SysCompanyUser_Validation ))]
	public partial class  SysCompanyUser 
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
			/// 企业全称
			/// Length : 100
			/// </summary>
			public virtual System.String	CompanyName{get;set;}
					 
			
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
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
