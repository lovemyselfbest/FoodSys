using System.Collections.Generic;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Project.Common;
namespace FoodSys.Entity
{

	/// <summary>
	/// 部门表
	/// </summary>
	public class UTOrgDepartment_Validation
	{


		/// <summary>
		/// 
		/// Length : 
		/// </summary>
		public virtual System.Guid ID { get; set; }

		/// <summary>
		/// 部门名称
		/// Length : 50
		/// </summary>
		[Required(ErrorMessage = "部门名称必填！")]
		[Remote("RemoteCheckDepartmentName", "SysOrgDepartmentManage", AdditionalFields = "ID", ErrorMessage = "此部门名称已经存在！")]
		public virtual System.String DepartmentName { get; set; }

		/// <summary>
		/// 负责人
		/// Length : 50
		/// </summary>
		[Required(ErrorMessage = "负责人必填！")]
		public virtual System.String DutyUser { get; set; }

		/// <summary>
		/// 电话
		/// Length : 20
		/// </summary>
		[Required(ErrorMessage = "联系电话必填！")]
		[RegularExpression(RegexHelper.Mobile, ErrorMessage = "请输入正确的联系电话！")]
		public virtual System.String Mobile { get; set; }

		/// <summary>
		/// 邮件地址
		/// Length : 50
		/// </summary>
		[Required(ErrorMessage = "Email必填！")]
		[RegularExpression(RegexHelper.Email,
			ErrorMessage = "请输入正确的Email格式！")]
		public virtual System.String Email { get; set; }

		/// <summary>
		/// 部门类型（扩展使用，可能以后会有多个物业部门）
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Int32> DepartmentType { get; set; }

		/// <summary>
		/// 排序
		/// Length : 
		/// </summary>
		[Required(ErrorMessage = "请输入排序！")]
		[RegularExpression(RegexHelper.Int, ErrorMessage = "请输入正整数！")]
		public virtual System.Nullable<Int32> OrderIndex { get; set; }

		/// <summary>
		/// 上级部门ID
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Guid> ParentID { get; set; }

		/// <summary>
		/// 备注
		/// Length : 200
		/// </summary>
		public virtual System.String Remark { get; set; }

		/// <summary>
		/// 创建人
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Guid> InputUser { get; set; }

		/// <summary>
		/// 创建日期
		/// Length : 
		/// </summary>
		public virtual System.Nullable<DateTime> InputDate { get; set; }

		/// <summary>
		/// 修改人
		/// Length : 
		/// </summary>
		public virtual System.Nullable<Guid> UpdateUser { get; set; }

		/// <summary>
		/// 修改日期
		/// Length : 
		/// </summary>
		public virtual System.Nullable<DateTime> UpdateDate { get; set; }

	}
}
