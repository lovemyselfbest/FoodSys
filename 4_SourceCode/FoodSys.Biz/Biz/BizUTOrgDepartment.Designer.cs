using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodSys.Dal.DataAccess;
using FoodSys.Entity;
using NHibernate;
using Project.Biz.Base;
using Project.Dal.Base;
namespace FoodSys.Biz.BizAccess {
	/// <summary>
	/// 部门表
	/// </summary>
	public partial class  BizUTOrgDepartment :BaseBiz<UTOrgDepartment,DalUTOrgDepartment>
	{
		private BizUTOrgDepartment()
			: base()
		{
			Initialize();
		}
	}
}
		