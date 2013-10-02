using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 代码组类型表
	/// </summary>
	[MetadataType(typeof(SysCodeType_Validation ))]
	public partial class  SysCodeType 
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
			public virtual System.String	TypeName{get;set;}
					 
			
			/// <summary>
			/// 顺序
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	SortOrder{get;set;}
					 
			
			/// <summary>
			/// 0、不可维护（资产） 1、可维护
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	Class{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
