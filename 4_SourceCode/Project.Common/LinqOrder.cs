using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Project.Common
{
	public class LinqOrder<T>
	{
		public Expression<Func<T, object>> Path{get;set;}
		public EnumOrder Direction { get; set; }
	}
}
