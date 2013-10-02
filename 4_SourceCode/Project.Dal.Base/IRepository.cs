using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;
using Project.Common;

namespace Project.Dal.Base
{
	public interface IRepository<T> where T : class
	{
		ISession Session { get; }
		#region Basic Operation

		IList<T> FindByHQL(string hql);
		void Save(T entity);
		void Update(T entity);
		void Delete(T entity);

		#endregion

		#region Select Operation

		T GetByID<TInput>(TInput id);
		T GetFirst(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction);
		IList<T> List();
		IList<T> List(params LinqOrder<T>[] orderCollection);
		IList<T> List(Expression<Func<T, object>> path, EnumOrder direction);

		IList<T> ListBy(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path , EnumOrder direction );
		IList<T> ListIn<U>(IList<U> collection, Expression<Func<T, object>> property);
		IList<T> ListTopX(Expression<Func<T, bool>> criteria, int count, Expression<Func<T, object>> path, EnumOrder direction);
		IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction);
		int Count();
		int CountBy(Expression<Func<T, bool>> criteria);
		#endregion


		#region Delete Operation

		void DeleteByID<TInput>(TInput id, NullableType nullableType);
		void DeleteBy(DetachedCriteria criteria);

		#endregion

		#region Update Operation

		void SaveOrUpdate(T model);
		T Merge(T entity);
		void Persist(T entity);

		#endregion
	}
}

