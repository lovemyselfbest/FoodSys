using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 部门表
	/// </summary>
	public partial class  DalUTOrgDepartment :Repository<UTOrgDepartment>
	{
		private DalUTOrgDepartment()
			: base()
		{
			Initialize();
		}
	}
}
		