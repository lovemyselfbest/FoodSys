using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 代码组信息表
	/// </summary>
	public partial class  DalSysCodeInfo :Repository<SysCodeInfo>
	{
		private DalSysCodeInfo()
			: base()
		{
			Initialize();
		}
	}
}
		