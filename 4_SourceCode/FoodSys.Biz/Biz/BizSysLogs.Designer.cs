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
	/// 日志表
	/// </summary>
	public partial class  BizSysLogs :BaseBiz<SysLogs,DalSysLogs>
	{
		private BizSysLogs()
			: base()
		{
			Initialize();
		}
	}
}
		