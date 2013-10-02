using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 单位基本资料表
	/// </summary>
	public partial class  DalUTOrgCompany :Repository<UTOrgCompany>
	{
		private DalUTOrgCompany()
			: base()
		{
			Initialize();
		}
	}
}
		