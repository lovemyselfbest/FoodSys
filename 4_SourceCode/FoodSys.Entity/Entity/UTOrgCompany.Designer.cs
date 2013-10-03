using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 单位基本资料表
	/// </summary>
	[MetadataType(typeof(UTOrgCompany_Validation ))]
	public partial class  UTOrgCompany 
	{
		
		
			/// <summary>
			/// ID
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 单位全称
			/// Length : 50
			/// </summary>
			public virtual System.String	Name{get;set;}
					 
			
			/// <summary>
			/// 注册地址
			/// Length : 100
			/// </summary>
			public virtual System.String	Address{get;set;}
					 
			
			/// <summary>
			/// 法人登记证号
			/// Length : 50
			/// </summary>
			public virtual System.String	LegalPersonRegNO{get;set;}
					 
			
			/// <summary>
			/// 组织机构代码证号
			/// Length : 10
			/// </summary>
			public virtual System.String	OrganizationCode{get;set;}
					 
			
			/// <summary>
			/// 税务登记证号
			/// Length : 50
			/// </summary>
			public virtual System.String	TaxRegNO{get;set;}
					 
			
			/// <summary>
			/// 营业执照号码
			/// Length : 50
			/// </summary>
			public virtual System.String	LicenseNo{get;set;}
					 
			
			/// <summary>
			/// 社会保险登记证号
			/// Length : 50
			/// </summary>
			public virtual System.String	SocialInsuranceRegNO{get;set;}
					 
			
			/// <summary>
			/// 住房公积金账户
			/// Length : 50
			/// </summary>
			public virtual System.String	HousingFundNO{get;set;}
					 
			
			/// <summary>
			/// 联系人
			/// Length : 50
			/// </summary>
			public virtual System.String	LinkMan{get;set;}
					 
			
			/// <summary>
			/// 手机
			/// Length : 20
			/// </summary>
			public virtual System.String	Mobile{get;set;}
					 
			
			/// <summary>
			/// 座机
			/// Length : 20
			/// </summary>
			public virtual System.String	Tel{get;set;}
					 
			
			/// <summary>
			/// 传真
			/// Length : 20
			/// </summary>
			public virtual System.String	Fax{get;set;}
					 
			
			/// <summary>
			/// Email
			/// Length : 50
			/// </summary>
			public virtual System.String	Email{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
