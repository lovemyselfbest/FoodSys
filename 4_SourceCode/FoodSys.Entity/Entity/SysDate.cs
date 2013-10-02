using System;

namespace FoodSys.Entity
{
    /// <summary>
    /// 系统日期表,记录某一天是否是节假日、周末。
    /// </summary>
    public class SysDate_Validation
    {
        /// <summary>
        ///
        /// Length :
        /// </summary>
        public virtual System.Guid ID { get; set; }

        /// <summary>
        ///
        /// Length :
        /// </summary>
        public virtual System.Nullable<DateTime> Date { get; set; }

        /// <summary>
        ///
        /// Length :
        /// </summary>
        public virtual System.Nullable<Boolean> IsHoliday { get; set; }
    }
}