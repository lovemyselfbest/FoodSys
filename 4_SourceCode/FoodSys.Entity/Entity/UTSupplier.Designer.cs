using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 
	/// </summary>
	[MetadataType(typeof(UTSupplier_Validation ))]
	public partial class  UTSupplier 
	{
		
		
			/// <summary>
			/// 主键
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 名称
			/// Length : 100
			/// </summary>
			public virtual System.String	Name{get;set;}
					 
			
			/// <summary>
			/// 手机
			/// Length : 30
			/// </summary>
			public virtual System.String	Mobile{get;set;}
					 
			
			/// <summary>
			/// 电话
			/// Length : 30
			/// </summary>
			public virtual System.String	Phone{get;set;}
					 
			
			/// <summary>
			/// 备注
			/// Length : 300
			/// </summary>
			public virtual System.String	Memo{get;set;}
					 
			
			/// <summary>
			/// 状态 0不可用，1可用
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	Status{get;set;}
					 
			
			/// <summary>
			/// 创建日期
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	CreateDate{get;set;}
					 
			
			/// <summary>
			/// 创建人ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	CreateID{get;set;}
					 
			
			/// <summary>
			/// 修改日期
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	UpdateDate{get;set;}
					 
			
			/// <summary>
			/// 修改人ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UpdateID{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			
			/// <summary>
			/// 地址
			/// Length : 50
			/// </summary>
			public virtual System.String	Address{get;set;}
					 
			}
	}
