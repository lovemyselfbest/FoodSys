using System.Collections.Generic; 
using System.Text; 
using System;
using System.ComponentModel.DataAnnotations; 
namespace FoodSys.Entity {

	/// <summary>
	/// 
	/// </summary>
	[MetadataType(typeof(UVProduct_Validation ))]
	public partial class  UVProduct 
	{
		
		
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	KeyID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Guid	ID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	SupplierID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 50
			/// </summary>
			public virtual System.String	Name{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	ProductType{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 20
			/// </summary>
			public virtual System.String	UnitID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Decimal>	PurchasePrice{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Decimal>	SellPrice{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	Status{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Int32>	MaxNumber{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	CreateDate{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	CreateID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<DateTime>	UpdateDate{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 
			/// </summary>
			public virtual System.Nullable<Guid>	UpdateID{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 100
			/// </summary>
			public virtual System.String	SupplierName{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 10
			/// </summary>
			public virtual System.String	TypeName{get;set;}
					 
			
			/// <summary>
			/// 
			/// Length : 100
			/// </summary>
			public virtual System.String	UnitName{get;set;}
					 
			}
	}
