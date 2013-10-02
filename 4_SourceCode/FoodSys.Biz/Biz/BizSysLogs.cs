using FoodSys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Project.Common;
using FoodSys.Enum;
namespace FoodSys.Biz.BizAccess
{
	/// <summary>
	/// 日志表
	/// </summary>
	public partial class BizSysLogs
	{
		private void Initialize()
		{

		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="user">当前系统用户</param>
		/// <param name="userHostAddress">远程客户IP</param>
		/// <param name="navigation">操作路径</param>
		/// <param name="name">操作目标字段的数据</param>
		/// <param name="id">操作的ID</param>
		/// <param name="actionName">操作方法(新增,修改,删除)</param>
		/// <param name="exceptionMsg">异常抛错信息</param>
		/// <param name="logType">日志类型</param>
		public void SaveOrUpdate(SysUser user, string userHostAddress, string navigation, string name, Guid id, string actionName, EnumLogType logType, string exceptionMsg)
		{
          
                try
                {
                    SysLogs syslogs = new SysLogs();
                    syslogs.OperationTime = DateTime.Now;
                    syslogs.OperatorName = user.UserName;
                    syslogs.MachineIP = userHostAddress;
                    syslogs.UserAccount = user.UserAccount;
                    syslogs.LogTypeID = (int)logType;
					syslogs.ExceptionMsg=exceptionMsg;
                    syslogs.LogContent = string.Format("【{0}】在【{1}】对【{2}】的【{3}】做了【{4}】操作", user.UserName, DateTime.Now, navigation, string.Format("{0}(ID:{1})", name, id), actionName);
                    SaveOrUpdate(syslogs);
                }
                catch (Exception ex)
                {
                    Log4netHelper.Logger.Fatal(ex);
                }
		}
	}
}
