using System.Collections.Generic; 
using System.Text; 
using System; 
namespace FoodSys.Entity {

	/// <summary>
	/// 
	/// </summary>
	public class  UTOrderDetail_Validation
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
			public virtual System.Nullable<Guid>	OrderID{get;set;}
					 
						/// <summary>
			/// 产品ID
			/// Length : 
			/// </summary>
			public virtual System.Guid	ProductID{get;set;}
					 
						/// <summary>
			/// 数量
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	Number{get;set;}
					 
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
			/// 状态1预定，2已提交，3出货中，4待确认，5已确认，6已作废
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
		