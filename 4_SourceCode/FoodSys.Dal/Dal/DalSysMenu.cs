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
	/// 菜单表
	/// </summary>
    public partial class DalSysMenu
    {
        private void Initialize()
        {

        }

        /// <summary>
        /// 根据用户类型查询对应的菜单列表
        /// </summary>
        /// <param name="userTypeID"></param>
        /// <returns></returns>
        public IList<SysMenu> ListSysMenuByUserTypeID(Guid userTypeID)
        {
            var query = from t1 in Session.Query<SysUserTypeMenu>()
                        join t2 in Session.Query<SysMenu>()
                        on t1.MenuID equals t2.ID
                        where t1.UserTypeID == userTypeID
                        select t2;
            return query.ToList();
        }

        /// <summary>
        /// 根据用户角色ID查询对应的菜单列表
        /// </summary>
        /// <param name="sysRoleId"></param>
        /// <returns></returns>
        public IList<SysMenu> ListSysMenuBySysRoleID(Guid sysRoleId)
        {
            var query = from t1 in Session.Query<SysRoleRight>()
                        join t2 in Session.Query<SysMenu>()
                        on t1.MenuID equals t2.ID
                        where t1.RoleID == sysRoleId
                        select t2;
            return query.ToList();
        }

        /// <summary>
        /// 根据用户ID查询对应的菜单列表
        /// </summary>
        /// <param name="sysRoleId"></param>
        /// <returns></returns>
        public IList<SysMenu> ListSysMenuByUserId(Guid userId)
        {
            var query = from t1 in Session.Query<SysUserRight>()
                        join t2 in Session.Query<SysMenu>()
                        on t1.MenuID equals t2.ID
                        where t1.UserID == userId
                        select t2;
            return query.ToList();
        }
    }
}
		