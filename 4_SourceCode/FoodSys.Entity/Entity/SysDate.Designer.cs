using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 系统日期表,记录某一天是否是节假日、周末。
	/// </summary>
	[MetadataType(typeof(SysDate_Validation ))]
	public partial class  SysDate 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	Date{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Boolean>	IsHoliday{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
