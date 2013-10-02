using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Common
{
	/// <summary>
	/// 实现IEqualityComparer 接口
	/// Func<int, int, bool> f = (x, y) => x == y;
	/// var comparer = new Comparer<int>(f);
	/// Console.WriteLine(comparer.Equals(1, 1));
	/// Console.WriteLine(comparer.Equals(1, 2));
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EqualityComparerComparer<T> : IEqualityComparer<T>
	{
		private readonly Func<T, T, bool> _comparer;

		public EqualityComparerComparer(Func<T, T, bool> comparer)
		{
			if (comparer == null)
				throw new ArgumentNullException("comparer");

			_comparer = comparer;
		}

		public bool Equals(T x, T y)
		{
			return _comparer(x, y);
		}

		public int GetHashCode(T obj)
		{
			return obj.ToString().ToLower().GetHashCode();
		}
	}
}
