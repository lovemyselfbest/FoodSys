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
	/// 系统日期表,记录某一天是否是节假日、周末。
	/// </summary>
	public partial class  BizSysDate :BaseBiz<SysDate,DalSysDate>
	{
		private BizSysDate()
			: base()
		{
			Initialize();
		}
	}
}
		