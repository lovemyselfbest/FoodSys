using NHibernate.Linq;
using System.Linq;
using Project.Common;
using System;
using System.Collections.Generic;
using NHibernate;
using FoodSys.Entity;
namespace FoodSys.Dal.DataAccess 
{
	/// <summary>
	/// 角色表
	/// </summary>
	public partial class DalSysRole 
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
            var query = from t1 in Session.Query<SysRoleMember>()
                        join t2 in Session.Query<SysRole>()
                        on t1.RoleID equals t2.ID
                        where t1.UserID == userID
                        select t2;
            return query.FirstOrDefault();
        }
	}
}
		