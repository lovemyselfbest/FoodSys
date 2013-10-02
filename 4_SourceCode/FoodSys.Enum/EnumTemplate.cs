using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodSys.Enum
{
    /// <summary>
    /// 邮件/短信模板 类型
    /// </summary>
    public enum EnumTemplate
    {
        账户相关 = 0,
        预定相关 = 1,
        租房申请相关 = 2,
        租金相关 = 3,
        退租相关 = 4,
        其他提醒类 = 5,
        自定义 = 6
    }
}
