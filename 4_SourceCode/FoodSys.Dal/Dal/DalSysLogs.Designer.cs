using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 日志表
	/// </summary>
	public partial class  DalSysLogs :Repository<SysLogs>
	{
		private DalSysLogs()
			: base()
		{
			Initialize();
		}
	}
}
		