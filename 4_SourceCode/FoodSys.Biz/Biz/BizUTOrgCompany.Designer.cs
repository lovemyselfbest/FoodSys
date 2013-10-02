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
	/// 单位基本资料表
	/// </summary>
	public partial class  BizUTOrgCompany :BaseBiz<UTOrgCompany,DalUTOrgCompany>
	{
		private BizUTOrgCompany()
			: base()
		{
			Initialize();
		}
	}
}
		