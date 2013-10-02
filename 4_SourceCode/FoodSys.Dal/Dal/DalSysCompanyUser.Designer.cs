using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 企业用户表
	/// </summary>
	public partial class  DalSysCompanyUser :Repository<SysCompanyUser>
	{
		private DalSysCompanyUser()
			: base()
		{
			Initialize();
		}
	}
}
		