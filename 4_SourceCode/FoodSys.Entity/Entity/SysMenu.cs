using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc; 
namespace FoodSys.Entity {

	/// <summary>
	/// 菜单表
	/// </summary>
	public class  SysMenu_Validation
	{
		

					/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
						/// <summary>
			/// 名称
			/// Length : 50
			/// </summary>
            [Required(ErrorMessage = "请输入菜单名！")]
            [Remote("RemoteCheckMenuName", "SysMenuManage", AdditionalFields = "ID", ErrorMessage = "此菜单名已经存在！")]
			public virtual System.String	Name{get;set;}
					 
						/// <summary>
			/// 父级菜单ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	ParentID{get;set;}
					 
						/// <summary>
			/// 排序
			/// Length : 
			/// </summary>
            [Required(ErrorMessage = "请输入顺序号！")]
			public virtual System.Nullable<Int32>	OrderIndex{get;set;}
					 
						/// <summary>
			/// 链接地址
			/// Length : 255
			/// </summary>
            [Required(ErrorMessage = "请输入指向地址！")]
			public virtual System.String	TargetURL{get;set;}
					 
						/// <summary>
			/// 图标
			/// Length : 255
			/// </summary>
            [Required(ErrorMessage = "请输入标名！")]
			public virtual System.String	Icon{get;set;}
					 
						/// <summary>
			/// 菜单level
			/// Length : 
			/// </summary>
            public virtual System.Nullable<Int32> Level { get; set; }
					 
				}
}
		