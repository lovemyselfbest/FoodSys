using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 组织机构表
	/// </summary>
	[MetadataType(typeof(UTOrganization_Validation ))]
	public partial class  UTOrganization 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 组织机构名称
			/// Length : 50
			/// </summary>
			public virtual System.String	Name{get;set;}
					 
			
			/// <summary>
			/// 排序
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	OrderIndex{get;set;}
					 
			
			/// <summary>
			/// 备注
			/// Length : 200
			/// </summary>
			public virtual System.String	Remark{get;set;}
					 
			
			/// <summary>
			/// 创建人
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	InputUser{get;set;}
					 
			
			/// <summary>
			/// 创建日期
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	InputDate{get;set;}
					 
			
			/// <summary>
			/// 修改人
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UpdateUser{get;set;}
					 
			
			/// <summary>
			/// 修改日期
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
