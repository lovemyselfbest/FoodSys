using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodSys.Enum;
using System.Text;

namespace FoodSys.Web.Out.Base
{
	public class Widget
	{
		public static string RenderStyle(int? auditResult)
		{
			if (auditResult == null)
				return "audit-waiting";
			var enumAuditResult = (EnumAuditResult)auditResult;
			switch (enumAuditResult)
			{
				case EnumAuditResult.待审核:
					return "audit-waiting";
				case EnumAuditResult.通过:
					return "audit-pass";
				case EnumAuditResult.未通过:
					return "audit-pass-not";
				default:
					break;
			}
			return "audit-waiting";
		}

		/// <summary>
		/// 获得租金有效日期范围
		/// </summary>
		/// <param name="contractBeginDate"></param>
		/// <param name="validBeginDate"></param>
		/// <param name="validEndDate"></param>
		public static void GetRentalValidDate(DateTime contractBeginDate, ref DateTime validBeginDate, ref DateTime validEndDate)
		{
			if (contractBeginDate.Year % 2 == 0 && contractBeginDate.Month < 7)
			{
				validBeginDate = new DateTime(contractBeginDate.Year - 2, 7, 1);
				validEndDate = validBeginDate.AddYears(2).AddDays(-1);
			}
			else if (contractBeginDate.Year % 2 == 0 && contractBeginDate.Month >= 7)
			{
				validBeginDate = new DateTime(contractBeginDate.Year, 7, 1);
				validEndDate = validBeginDate.AddYears(2).AddDays(-1);
			}
			else if (contractBeginDate.Year % 2 != 0)
			{
				validBeginDate = new DateTime(contractBeginDate.Year - 1, 7, 1);
				validEndDate = validBeginDate.AddYears(2).AddDays(-1);
			}
		}

		public static string ConvertBytesToString(byte[] bytesData)
		{
			return Encoding.UTF8.GetString(bytesData);
		}

	}
}