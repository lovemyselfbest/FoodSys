using FoodSys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using FoodSys.Enum;
using Project.Common;
using FoodSys.Dal.DataAccess;
namespace FoodSys.Biz.BizAccess
{
	/// <summary>
	/// 用户类型菜单表
	/// </summary>
	public partial class BizSysUserTypeMenu
	{
		DalSysUserTypeMenu dalSysUserTypeMenu;
		BizSysUserTypeMenu bizSysUserTypeMenu;
		BizSysLogs bizSysLogs;
		BizSysMenu bizSysMenu;
		private void Initialize()
		{

		}

		/// <summary>
		/// 删除菜单
		/// </summary>
		/// <param name="contractID"></param>
		/// <param name="applyID"></param>
		/// <param name="user"></param>
		/// <returns></returns>
		public string DeleteMenu(SysUserTypeMenu sysUserTypeMenu,
			SysUser user,
			string userHostAddress,
			string navigation,
			string name,
			Guid id,
			string actionName,
			EnumLogType logType)
		{
			using (ITransaction transaction = dbAccess.Session.BeginTransaction())
			{
				try
				{
					dalSysUserTypeMenu.Delete(sysUserTypeMenu);
					bizSysLogs.SaveOrUpdate(user, userHostAddress, navigation, name, id, actionName, logType, string.Empty);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					Log4netHelper.Logger.Fatal(ex);
					return FoodSys.Resources.Properties.Resources.M00002E;
				}
			}
			return string.Empty;
		}
	}
}
