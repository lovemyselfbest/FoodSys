using FoodSys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using FoodSys.Dal.DataAccess;
using Project.Common;
namespace FoodSys.Biz.BizAccess 
{
	/// <summary>
	/// 菜单表
	/// </summary>
    public partial class BizSysMenu
    {
        private DalSysRoleRight dalSysRoleRight;
        private DalSysUserTypeMenu dalSysUserTypeMenu;
        private DalSysUserRight dalSysUserRight;

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
            return dbAccess.ListSysMenuByUserTypeID(userTypeID);
        }

        /// <summary>
        /// 根据用户角色ID查询对应的菜单列表
        /// </summary>
        /// <param name="sysRoleId"></param>
        /// <returns></returns>
        public IList<SysMenu> ListSysMenuBySysRoleID(Guid sysRoleId)
        {
            return dbAccess.ListSysMenuBySysRoleID(sysRoleId);
        }

        /// <summary>
        /// 根据用户ID查询对应的菜单列表
        /// </summary>
        /// <param name="sysRoleId"></param>
        /// <returns></returns>
        public IList<SysMenu> ListSysMenuByUserId(Guid userId)
        {
            return dbAccess.ListSysMenuByUserId(userId);
        }

        public IList<SysMenu> PaginateListBy(int p, int p_2, ref int count, System.Linq.Expressions.Expression<Func<SysMenu, bool>> expression, int? nullable, Project.Common.EnumOrder enumOrder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string DeleteSysMenuByID(Guid menuID)
        {
            using (ITransaction transaction = dbAccess.Session.BeginTransaction())
            {
                try
                {
                    IList<SysRoleRight> sysRoleRightCollection = dalSysRoleRight.ListBy(x => x.MenuID == menuID);
                    foreach (var sysRoleRight in sysRoleRightCollection)
                    {
                        dalSysRoleRight.Delete(sysRoleRight);
                    }

                    IList<SysUserTypeMenu> sysUserTypeMenuCollection = dalSysUserTypeMenu.ListBy(x => x.MenuID == menuID);
                    foreach (var sysUserTypeMenu in sysUserTypeMenuCollection)
                    {
                        dalSysUserTypeMenu.Delete(sysUserTypeMenu);
                    }

                    IList<SysUserRight> sysUserRightCollection = dalSysUserRight.ListBy(x => x.MenuID == menuID);
                    foreach (var sysUserRight in sysUserRightCollection)
                    {
                        dalSysUserRight.Delete(sysUserRight);
                    }

                    dbAccess.DeleteByID(menuID, NHibernate.NHibernateUtil.Guid);
                    transaction.Commit();
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Log4netHelper.Logger.Fatal(ex);
                }

                return Resources.Properties.Resources.M00002E;
            }
        }
    }
}
		