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
	/// 企业用户表
	/// </summary>
	public partial class  BizSysCompanyUser :BaseBiz<SysCompanyUser,DalSysCompanyUser>
	{
		private BizSysCompanyUser()
			: base()
		{
			Initialize();
		}
	}
}
		