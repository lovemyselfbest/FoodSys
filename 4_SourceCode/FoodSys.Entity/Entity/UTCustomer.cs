using System.Collections.Generic; 
using System.Text; 
using System; 
namespace FoodSys.Entity {

	/// <summary>
	/// 客户表
	/// </summary>
	public class  UTCustomer_Validation
	{
		

					/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	ID{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 50
			/// </summary>
			public virtual System.String	Account{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 50
			/// </summary>
			public virtual System.String	Password{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 50
			/// </summary>
			public virtual System.String	ChineseName{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 50
			/// </summary>
			public virtual System.String	EnglishName{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 20
			/// </summary>
			public virtual System.String	Moblie{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 20
			/// </summary>
			public virtual System.String	Tel{get;set;}
					 
						/// <summary>
			/// 
			/// Length : 50
			/// </summary>
			public virtual System.String	Address{get;set;}
					 
				}
}
		