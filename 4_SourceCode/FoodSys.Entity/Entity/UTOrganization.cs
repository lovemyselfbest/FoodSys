using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 组织机构表
	/// </summary>
	public class  UTOrganization_Validation
	{
		

					/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
						/// <summary>
			/// 机构名称
			/// Length : 50
			/// </summary>
            [Required(ErrorMessage = "请输机构名称！")]
            [StringLength(50, ErrorMessage = "机构名称不能超过50个字符！")]
			public virtual System.String	Name{get;set;}
					 
						/// <summary>
			/// 排序
			/// Length : 
			/// </summary>
            [Required(ErrorMessage = "请输入数字")]
			public virtual System.Nullable<Int32>	OrderIndex{get;set;}
					 
						/// <summary>
			/// 备注
			/// Length : 200
			/// </summary>
			public virtual System.String	Remark{get;set;}
					 
						/// <summary>
			/// 创建人
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	InputUser{get;set;}
					 
						/// <summary>
			/// 创建日期
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	InputDate{get;set;}
					 
						/// <summary>
			/// 修改人
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UpdateUser{get;set;}
					 
						/// <summary>
			/// 修改日期
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	UpdateDate{get;set;}
					 
				}
}
		