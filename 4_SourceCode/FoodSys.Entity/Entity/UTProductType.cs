using System.Collections.Generic; 
using System.Text; 
using System; 
namespace FoodSys.Entity {

	/// <summary>
	/// 
	/// </summary>
	public class  UTProductType_Validation
	{
		

					/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
						/// <summary>
			/// 类型那个名称
			/// Length : 10
			/// </summary>
			public virtual System.String	TypeName{get;set;}
					 
						/// <summary>
			/// 父级ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	ParentID{get;set;}
					 
						/// <summary>
			/// 排序
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	SortIndex{get;set;}
					 
						/// <summary>
			/// 是否叶子
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Boolean>	IsLeaf{get;set;}
					 
						/// <summary>
			/// 是否有效
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Boolean>	Status{get;set;}
					 
						/// <summary>
			/// 创建日期
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	CreateDate{get;set;}
					 
						/// <summary>
			/// 创建人ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	CreateID{get;set;}
					 
						/// <summary>
			/// 修改日期
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	UpdateDate{get;set;}
					 
						/// <summary>
			/// 修改人ID
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UpdateID{get;set;}
					 
						/// <summary>
			/// 版本号
			/// Length : 
			/// </summary>
			public virtual System.Byte[]	Version{get;set;}
					 
				}
}
		