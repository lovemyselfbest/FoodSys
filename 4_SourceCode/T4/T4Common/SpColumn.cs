using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4Common
{
	[Serializable]
	public class SpColumn
	{
		/// <summary>
		/// 数据库中的字段名称，有可能是C#关键字
		/// </summary>
		public string CName
		{
			get;
			set;
		}

		public int Length
		{
			get;
			set;
		}

		/// <summary>
		/// 如果CName是数据库关键字，则对其进行处理，加_Keyword
		/// </summary>
		public string CLegalName
		{
			get
			{
				if (DataTypeMapper.CSharpKeywords.Contains(CName))
					return CName + "_Keyword";
				return CName;
			}
		}

		public bool IsNullable
		{
			get;
			set;
		}

		public Type CType
		{
			get;
			set;
		}

	}
}
