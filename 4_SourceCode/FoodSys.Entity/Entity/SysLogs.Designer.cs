using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 日志表
	/// </summary>
	[MetadataType(typeof(SysLogs_Validation ))]
	public partial class  SysLogs 
	{
		
		
			/// <summary>
			/// ID
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 日志类型
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	LogTypeID{get;set;}
					 
			
			/// <summary>
			/// 用户类型
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UserTypeID{get;set;}
					 
			
			/// <summary>
			/// 所属企业
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	CompID{get;set;}
					 
			
			/// <summary>
			/// 操作内容
			/// Length : 2000
			/// </summary>
			public virtual System.String	LogContent{get;set;}
					 
			
			/// <summary>
			/// 操作帐号
			/// Length : 50
			/// </summary>
			public virtual System.String	UserAccount{get;set;}
					 
			
			/// <summary>
			/// 操作人姓名
			/// Length : 50
			/// </summary>
			public virtual System.String	OperatorName{get;set;}
					 
			
			/// <summary>
			/// 操作时间
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	OperationTime{get;set;}
					 
			
			/// <summary>
			/// 操作机器名
			/// Length : 50
			/// </summary>
			public virtual System.String	MachineName{get;set;}
					 
			
			/// <summary>
			/// 操作机器IP
			/// Length : 50
			/// </summary>
			public virtual System.String	MachineIP{get;set;}
					 
			
			/// <summary>
			/// 异常消息
			/// Length : 4000
			/// </summary>
			public virtual System.String	ExceptionMsg{get;set;}
					 
			
			/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
			}
	}
