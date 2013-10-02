using System.Collections.Generic;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
namespace FoodSys.Entity
{

	/// <summary>
	/// 个人用户表
	/// </summary>
	[MetadataType(typeof(SysRenterUser_Validation))]
	public partial class SysRenterUser
	{


		/// <summary>
		/// 
		/// Length : 
		/// </summary>
		public virtual System.Guid ID { get; set; }


		/// <summary>
		/// 账户
		/// Length : 50
		/// </summary>
		public virtual System.String UserAccount { get; set; }


		/// <summary>
		/// 账户密码
		/// Length : 50
		/// </summary>
		public virtual System.String Password { get; set; }


		/// <summary>
		/// 用户姓名
		/// Length : 50
		/// </summary>
		public virtual System.String UserName { get; set; }


		/// <summary>
		/// 证件类型
		/// Length : 10
		/// </summary>
		public virtual System.String CertType { get; set; }


		/// <summary>
		/// 证件号码
		/// Length : 18
		/// </summary>
		public virtual System.String CertNo { get; set; }


		/// <summary>
		/// 备忘
		/// Length : 500
		/// </summary>
		public virtual System.String Memo { get; set; }


		/// <summary>
		/// 状态
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Int32> Status { get; set; }


		/// <summary>
		/// 版本号
		/// Length : 
		/// </summary>
		public virtual System.Byte[] Version { get; set; }

	}
}
