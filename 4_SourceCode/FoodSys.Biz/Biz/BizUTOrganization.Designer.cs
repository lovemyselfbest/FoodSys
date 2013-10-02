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
	/// 组织机构表
	/// </summary>
	public partial class  BizUTOrganization :BaseBiz<UTOrganization,DalUTOrganization>
	{
		private BizUTOrganization()
			: base()
		{
			Initialize();
		}
	}
}
		