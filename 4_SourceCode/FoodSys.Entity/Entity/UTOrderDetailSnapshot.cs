using System.Collections.Generic; 
using System.Text; 
using System; 
namespace FoodSys.Entity {

	/// <summary>
	/// 
	/// </summary>
	public class  UTOrderDetailSnapshot_Validation
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
			/// 产品名称
			/// Length : 100
			/// </summary>
			public virtual System.String	ProductName{get;set;}
					 
						/// <summary>
			/// 售价
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Decimal>	SellPrice{get;set;}
					 
						/// <summary>
			/// 单位ID
			/// Length : 20
			/// </summary>
			public virtual System.String	UnitID{get;set;}
					 
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
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
				}
}
		