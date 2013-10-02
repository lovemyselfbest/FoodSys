using FoodSys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Project.Common;
using FoodSys.Dal.DataAccess;
using Project.Dal.Base;
namespace FoodSys.Biz.BizAccess 
{
	/// <summary>
	/// 系统日期表,记录某一天是否是节假日、周末。
	/// </summary>
	public partial class  BizSysDate 
	{
		private void Initialize()
		{
			
		}
        /// <summary>
        /// 根据日期获取该日期后十年Day
        /// 作者：尤啸
        /// 日期：2012-06-27
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public string GenerateDay(DateTime currentDate)
        {
            using (ITransaction transaction = dbAccess.Session.BeginTransaction())
            {
                try
                {
                    int year = currentDate.Year;
                    for (int month = 1; month <= 12; month++)
                    {
                        for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                        {
                            DateTime date = Convert.ToDateTime(string.Format("{0}-{1}-{2}", year, month, day));
                            Boolean isHoliday = (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) ? true : false;
                            SysDate sysDateEntity = new SysDate()
                            {
                                Date = date,
                                IsHoliday = isHoliday
                            };
                            dbAccess.Save(sysDateEntity);
                        }
                    }


                    transaction.Commit();
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Log4netHelper.Logger.Fatal(ex);
                }

                //操作失败
                return Resources.Properties.Resources.M00002E;
            }
        }

        /// <summary>
        /// 计算工作日
        /// 作者：尤啸
        /// 日期：2012-06-27
        /// </summary>
        /// <param name="fromDay"></param>
        /// <param name="toDay"></param>
        /// <returns></returns>
        public static int CalculateWorkDays(DateTime? fromDay, DateTime toDay)
        {
            DalSysDate dalSysDate = DalFactory.Get<DalSysDate>();

            return dalSysDate.CountBy(x => x.Date >= fromDay &&
                x.Date < toDay.AddDays(1) &&
                x.IsHoliday == false);
        }
	}
}
		