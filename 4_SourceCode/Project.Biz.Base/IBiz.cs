using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NHibernate.Criterion;
using NHibernate.Type;
using Project.Common;
using System.Data;

namespace Project.Biz.Base
{
	public interface IBiz<T>
	{
		#region Select Operation
		T GetByID<TInput>(TInput id);
		T GetFirst(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction);
		IList<T> List(Expression<Func<T, object>> path, EnumOrder direction);
		IList<T> ListBy(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction);
        IList<T> ListBy(Expression<Func<T, bool>> criteria, string path, EnumOrder direction );
        IList<T> ListBy(Expression<Func<T, bool>> criteria);
		IList<T> ListIn<T1>(IList<T1> collection, Expression<Func<T, object>> property);
		IList<T> ListTopX(Expression<Func<T, bool>> criteria, int count, Expression<Func<T, object>> path, EnumOrder direction);
		IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction);
		IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, string path, EnumOrder direction);
		IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria);
        int Count();
		int CountBy(Expression<Func<T, bool>> criteria);
		#endregion

		#region Basic Operation

		void Save(T entity);
		void Update(T entity);
		void Delete(T entity);
		void DeleteByID<TInput>(TInput id, NullableType nullableType);
		#endregion

		#region Update Operation

		void SaveOrUpdate(T model);
		T Merge(T entity);
		void Persist(T entity);

		#endregion

	}
}

