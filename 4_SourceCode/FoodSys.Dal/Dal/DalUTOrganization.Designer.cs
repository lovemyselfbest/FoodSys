using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 组织机构表
	/// </summary>
	public partial class  DalUTOrganization :Repository<UTOrganization>
	{
		private DalUTOrganization()
			: base()
		{
			Initialize();
		}
	}
}
		