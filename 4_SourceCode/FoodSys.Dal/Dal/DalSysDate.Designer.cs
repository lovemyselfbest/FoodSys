using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 系统日期表,记录某一天是否是节假日、周末。
	/// </summary>
	public partial class  DalSysDate :Repository<SysDate>
	{
		private DalSysDate()
			: base()
		{
			Initialize();
		}
	}
}
		