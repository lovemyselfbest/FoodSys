using FoodSys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
namespace FoodSys.Biz.BizAccess 
{
	/// <summary>
	/// 角色表
	/// </summary>
	public partial class  BizSysRole 
	{
		private void Initialize()
		{
			
		}
        /// <summary>
        /// 根据用户ID获取角色
        /// </summary>
        /// <param name="userTypeID"></param>
        /// <returns></returns>
        public SysRole ListSysRoleByUserID(Guid userID)
        {
            return dbAccess.ListSysRoleByUserID(userID);
        }
	}
}
		