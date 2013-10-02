using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity.Base
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ExportAttribute:System.Attribute
	{
		/// <summary>
		/// 导出Excel，头部显示的名称。
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// excel列宽
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// 被标记属性的名称,反射时自动赋值,无须进行赋值
		/// </summary>
		public string PropertyName { get; set; }
		/// <summary>
		/// 被标记属性的类型,反射时自动赋值,无须进行赋值
		/// </summary>
		public Type PropertyType { get; set; }
	}
}
