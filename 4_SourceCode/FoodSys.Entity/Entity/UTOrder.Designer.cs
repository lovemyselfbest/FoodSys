using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 
	/// </summary>
	[MetadataType(typeof(UTOrder_Validation ))]
	public partial class  UTOrder 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 客户ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	CustomerID{get;set;}
					 
			
			/// <summary>
			/// 确认时间
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	ConfirmDate{get;set;}
					 
			
			/// <summary>
			/// 配送方式
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	DistributionType{get;set;}
					 
			
			/// <summary>
			/// 配送时间
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	DeliveryTime{get;set;}
					 
			
			/// <summary>
			/// 状态1预定，2已提交，3出货中，4待确认，5下单已确认，6已作废
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
			/// 备注
			/// Length : 200
			/// </summary>
			public virtual System.String	Memo{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
