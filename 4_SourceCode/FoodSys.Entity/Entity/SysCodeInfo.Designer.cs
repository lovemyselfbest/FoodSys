using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 代码组信息表
	/// </summary>
	[MetadataType(typeof(SysCodeInfo_Validation ))]
	public partial class  SysCodeInfo 
	{
		
		
			/// <summary>
			/// ID
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 代码组类型
			/// Length : 100
			/// </summary>
			public virtual System.String	Type{get;set;}
					 
			
			/// <summary>
			/// 名称
			/// Length : 100
			/// </summary>
			public virtual System.String	Name{get;set;}
					 
			
			/// <summary>
			/// 代码
			/// Length : 20
			/// </summary>
			public virtual System.String	Code{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 20
			/// </summary>
			public virtual System.String	ParentCode{get;set;}
					 
			
			/// <summary>
			/// 顺序
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	SortOrder{get;set;}
					 
			
			/// <summary>
			/// 是否有效
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Boolean>	IsActive{get;set;}
					 
			
			/// <summary>
			/// 备注
			/// Length : 500
			/// </summary>
			public virtual System.String	Memo{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
