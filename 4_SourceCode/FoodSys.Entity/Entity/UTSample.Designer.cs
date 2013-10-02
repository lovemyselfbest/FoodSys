using System.Collections.Generic;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using Project.Common;
namespace FoodSys.Entity
{

	/// <summary>
	/// 示例表
	/// </summary>
	[MetadataType(typeof(UTSample_Validation))]
	public partial class UTSample
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
		public virtual System.String SupplierName { get; set; }


		/// <summary>
		/// 供货内容
		/// Length : 200
		/// </summary>
		public virtual System.String SupplyContent { get; set; }


		/// <summary>
		/// 联系人
		/// Length : 50
		/// </summary>
		public virtual System.String ContactName { get; set; }


		/// <summary>
		/// 移动电话
		/// Length : 20
		/// </summary>
		[Required(ErrorMessage = "请输入手机号码！")]
		[RegularExpression(RegexHelper.Mobile, ErrorMessage = "请输入正确的手机号码！")]
		public virtual System.String Mobile { get; set; }


		/// <summary>
		/// 固定电话
		/// Length : 20
		/// </summary>
		public virtual System.String Telephone { get; set; }


		/// <summary>
		/// 电子邮箱
		/// Length : 50
		/// </summary>
		public virtual System.String Email { get; set; }


		/// <summary>
		/// 传真
		/// Length : 20
		/// </summary>
		public virtual System.String Fax { get; set; }


		/// <summary>
		/// 地址
		/// Length : 200
		/// </summary>
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


		/// <summary>
		/// 
		/// Length : 
		/// </summary>
		public virtual System.Byte[] Version { get; set; }

	}
}
