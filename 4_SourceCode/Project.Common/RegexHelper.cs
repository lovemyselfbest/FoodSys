using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Common
{
	public static class RegexHelper
	{
		/// <summary>
		/// 浮点型数字@"^\$?\d+(\.(\d{1}))?$"
		/// </summary>
		public const string Decimal = @"^([0-9]*|\d*\.\d{1}?\d*)$";

		/// <summary>
		/// 大于0的浮点数
		/// </summary>
		public const string PositiveDecimal = @"(^\d+\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.?\d*$)";

		public const string Int = @"^[0-9]*$";

		public const string Email = @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$";

		public const string Mobile = @"^(1(([35][0-9])|(47)|[8][01236789]))\d{8}$";

		public const string Telephone = @"^\d{7,15}$";



	}
}
