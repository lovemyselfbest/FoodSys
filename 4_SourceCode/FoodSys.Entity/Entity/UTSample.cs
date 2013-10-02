using System.Collections.Generic;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using Project.Entity.Base;
namespace FoodSys.Entity
{

	/// <summary>
	/// 供应商表
	/// </summary>
	public class UTSample_Validation
	{


		/// <summary>
		/// 
		/// Length : 
		/// </summary>
		public virtual System.Guid ID { get; set; }

		/// <summary>
		/// 供应商名称
		/// Length : 50
		/// </summary>
		[Required(ErrorMessage = "请填写供应商名称")]
		[Export(DisplayName = "供应商名称")]
		public virtual System.String SupplierName { get; set; }

		/// <summary>
		/// 供货内容
		/// Length : 200
		/// </summary>
		[Export(DisplayName = "供货内容")]
		public virtual System.String SupplyContent { get; set; }

		/// <summary>
		/// 联系人
		/// Length : 50
		/// </summary>
		[Required(ErrorMessage = "请填写联系人")]
		[Export(DisplayName = "联系人")]
		public virtual System.String ContactName { get; set; }

		/// <summary>
		/// 移动电话
		/// Length : 20
		/// </summary>
		[Export(DisplayName = "移动电话")]
		public virtual System.String Mobile { get; set; }

		/// <summary>
		/// 固定电话
		/// Length : 20
		/// </summary>
		[Export(DisplayName = "固定电话")]
		public virtual System.String Telephone { get; set; }

		/// <summary>
		/// 电子邮箱
		/// Length : 50
		/// </summary>
		[Export(DisplayName = "电子邮箱")]
		public virtual System.String Email { get; set; }

		/// <summary>
		/// 传真
		/// Length : 20
		/// </summary>
		[Export(DisplayName = "传真")]
		public virtual System.String Fax { get; set; }

		/// <summary>
		/// 地址
		/// Length : 200
		/// </summary>
		[Required(ErrorMessage = "请填写地址")]
		[Export(DisplayName = "地址")]
		public virtual System.String Address { get; set; }

		/// <summary>
		/// 录入人
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Guid> InputUser { get; set; }

		/// <summary>
		/// 录入时间
		/// Length : 
		/// </summary>
		public virtual System.Nullable<DateTime> InputDate { get; set; }

		/// <summary>
		/// 更新人
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Guid> UpdateUser { get; set; }

		/// <summary>
		/// 更新时间
		/// Length : 
		/// </summary>
		public virtual System.Nullable<DateTime> UpdateDate { get; set; }

	}
}
