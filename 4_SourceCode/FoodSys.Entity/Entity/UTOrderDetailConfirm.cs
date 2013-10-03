using System.Collections.Generic; 
using System.Text; 
using System; 
namespace FoodSys.Entity {

	/// <summary>
	/// 
	/// </summary>
	public class  UTOrderDetailConfirm_Validation
	{
		

					/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
						/// <summary>
			/// 订单ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	OrderDetailID{get;set;}
					 
						/// <summary>
			/// 实际售价
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Decimal>	RealSellPrice{get;set;}
					 
						/// <summary>
			/// 实际数量
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	RealNumber{get;set;}
					 
						/// <summary>
			/// 确认人ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	ConfirmUserID{get;set;}
					 
						/// <summary>
			/// 确认时间
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	ConfirmDate{get;set;}
					 
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
			/// 状态1未确认，2已确认
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
		