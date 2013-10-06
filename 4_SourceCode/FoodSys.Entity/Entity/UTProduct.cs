using System.Collections.Generic;
using System.Text;
using System;
namespace FoodSys.Entity
{

	/// <summary>
	/// 
	/// </summary>
	public class UTProduct_Validation
	{


		/// <summary>
		/// 主键
		/// Length : 
		/// </summary>
		public virtual System.Guid ID { get; set; }

		/// <summary>
		/// 供应商ID
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Guid> SupplierID { get; set; }

		/// <summary>
		/// 名称
		/// Length : 50
		/// </summary>
		public virtual System.String Name { get; set; }

		/// <summary>
		/// 类型
		/// Length : 
		/// </summary>
		public virtual System.String ProductType { get; set; }

		/// <summary>
		/// 描述
		/// Length : 500
		/// </summary>
		public virtual System.String Description { get; set; }

		/// <summary>
		/// 单位ID
		/// Length : 20
		/// </summary>
		public virtual System.String UnitID { get; set; }

		/// <summary>
		/// 进价
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Decimal> PurchasePrice { get; set; }

		/// <summary>
		/// 售价
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Decimal> SellPrice { get; set; }

		/// <summary>
		/// 最大购买数量
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Int32> MaxNumber { get; set; }

		/// <summary>
		/// 创建日期
		/// Length : 
		/// </summary>
		public virtual System.Nullable<DateTime> CreateDate { get; set; }

		/// <summary>
		/// 创建人ID
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Guid> CreateID { get; set; }

		/// <summary>
		/// 修改日期
		/// Length : 
		/// </summary>
		public virtual System.Nullable<DateTime> UpdateDate { get; set; }

		/// <summary>
		/// 修改人ID
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Guid> UpdateID { get; set; }

		/// <summary>
		/// 版本号
		/// Length : 
		/// </summary>
		public virtual System.Byte[] Version { get; set; }

	}
}
