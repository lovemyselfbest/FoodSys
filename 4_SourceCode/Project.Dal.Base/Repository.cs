using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using System.Linq.Expressions;
using Project.Common;
using NHibernate.Type;
using NHibernate.Linq;
using System.Data;

namespace Project.Dal.Base
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private static LinqOrder<T>[] emptyLinqOrderCollection;
		private Type type = typeof(T);

		#region IRepository<T> Members

		public Repository()
		{

		}

		public virtual void Inject()
		{
			InjectDalObject.Inject(this);
		}

		public virtual IList<T> FindByHQL(string hql)
		{
			IQuery query = Session.CreateQuery(hql);
			IList<T> list = query.Enumerable<T>().ToList();
			return list;
		}

		public virtual object FindUniqueResultBySQL(string sql)
		{
			ISQLQuery query = Session.CreateSQLQuery(sql);
			DataTable dt = new DataTable();
			object result = query.UniqueResult();
			return result;
		}

		public virtual void Save(T entity)
		{
			Session.Save(entity);
		}

		public virtual void Update(T entity)
		{
			Session.Update(entity);
		}

		public virtual void SaveOrUpdate(T entity)
		{
			Session.SaveOrUpdate(entity);
		}

		public virtual void Delete(T entity)
		{
			Session.Delete(entity);
		}

		public virtual T Merge(T entity)
		{
			return (T)Session.Merge(entity);
		}

		public virtual void Persist(T entity)
		{
			Session.Persist(entity);
		}

		public virtual T GetByID<TInput>(TInput id)
		{
			IList<T> list = Session.CreateCriteria<T>().Add(Restrictions.Eq("ID", id)).List<T>();
			if (list == null || list.Count <= 0)
				return null;
			return list[0];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id"></param>
		/// <param name="nullableType"></param>
		public virtual void DeleteByID<TInput>(TInput id, NullableType nullableType)
		{
			Session.Delete(string.Format("from {0} t where t.ID= ?", type.Name), id, nullableType);
		}

		/// <summary>
		/// �г�In ������ ��¼����
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="collection"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		public virtual IList<T> ListIn<U>(IList<U> collection, Expression<Func<T, object>> property)
		{
			MemberExpression me = property.Body as MemberExpression;
			string propertyName = "";
			if (me == null)
			{
				string body = property.Body.ToString();
				string strBody = body.Substring(8, body.Length - 8 - 1);
				propertyName = strBody.Substring(strBody.IndexOf(".") + 1, strBody.Length - strBody.IndexOf(".") - 1);
			}
			else
				propertyName = me.Member.Name;
			string strIds = string.Join(",", Array.ConvertAll<U, string>(collection.ToArray(), x => "'" + x.ToString() + "'"));
			IQuery query = Session.CreateQuery(string.Format("Select t from {0} t  where {2} in ({1})", NHibernateHelper.NHConguration.GetClassMapping(type).MappedClass.Name, strIds, propertyName));
			return query.List<T>();
		}

		/// <summary>
		/// ����������������ɾ��
		/// </summary>
		/// <param name="criteria"></param>
		public virtual void DeleteBy(DetachedCriteria criteria)
		{
			criteria.SetProjection(Projections.Property("ID"));
			Guid[] listGuids = criteria.GetExecutableCriteria(Session).List<Guid>().ToArray();
			string ids = string.Join(",", Array.ConvertAll<Guid, string>(listGuids, x => "'" + x.ToString() + "'"));
			ISQLQuery query = Session.CreateSQLQuery(string.Format("delete from {0} where id in ({1})", NHibernateHelper.NHConguration.GetClassMapping(type).Table.Name, ids));
			query.ExecuteUpdate();

		}

		/// <summary>
		/// ��ҳ��ѯ��T ����,����������
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="count"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count, Expression<Func<T, object>> path, EnumOrder? direction)
		{
			LinqOrder<T>[] orderCollection = null;
			orderCollection = path == null ? emptyLinqOrderCollection : (new LinqOrder<T>[] { new LinqOrder<T>() { Direction = direction ?? EnumOrder.ASC, Path = path } });
			return PaginateListBy(pageSize, pageIndex, ref count, null, orderCollection);
		}

		/// <summary>
		/// ��ҳ��ѯ��T ���ݣ�����������ѯ
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="count"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count, params LinqOrder<T>[] orderCollection)
		{
			return PaginateListBy(pageSize, pageIndex, ref count, null, orderCollection);
		}

		/// <summary>
		/// ��ҳ��ѯ��T ����
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count)
		{
			return PaginateListBy(pageSize, pageIndex, ref count, null);
		}

		/// <summary>
		/// ��ҳ��ѯ��T,������ɸѡ,����������
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="count"></param>
		/// <param name="criteria"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction)
		{
			LinqOrder<T>[] orderCollection = null;
			orderCollection = path == null ? emptyLinqOrderCollection : (new LinqOrder<T>[] { new LinqOrder<T>() { Direction = direction, Path = path } });
			return PaginateListBy(pageSize, pageIndex, ref count, criteria, orderCollection);
		}

		/// <summary>
		/// ��ҳ��ѯ��T,������ɸѡ��
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="count"></param>
		/// <param name="criteria"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria)
		{
			return PaginateListBy(pageSize, pageIndex, ref count, criteria, null);
		}


		/// <summary>
		/// ��ҳ��ѯ��T,������ɸѡ,����������
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="count"></param>
		/// <param name="criteria"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public virtual IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, params LinqOrder<T>[] orderCollection)
		{
			IList<T> list = null;
			IQueryable<T> query = Session.Query<T>();
			if (criteria != null)
				query = query.Where(criteria);

			/* �����������
			----------------------------------------------------------*/
			query = AttachOrders(query, orderCollection);

			/* ��ѯ����¼����
			----------------------------------------------------------*/
			count = criteria == null ? Session.Query<T>().Count() : Session.Query<T>().Where(criteria).Count();

			/* ���pageIndex������ҳ�����������һҳ��ѯ
			----------------------------------------------------------*/
			int pages = (int)Math.Ceiling(count / (double)pageSize);
			pageIndex = pageIndex > (pages - 1) ? 0 : pageIndex;


			/* ���з�ҳ��ѯ
			----------------------------------------------------------*/
			list = query.Skip((pageIndex) * pageSize).Take(pageSize).ToList();
			
			return list;
		}


		/// <summary>
		/// ���ر�T ��������
		/// </summary>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public virtual IList<T> List(params LinqOrder<T>[] orderCollection)
		{
			return ListBy(null, orderCollection);
		}

		/// <summary>
		/// ���ر�T ��������
		/// </summary>
		/// <param name="path">�����ֶ�</param>
		/// <param name="direction">������</param>
		/// <returns></returns>
		public virtual IList<T> List(Expression<Func<T, object>> path, EnumOrder direction)
		{
			LinqOrder<T>[] orderCollection = null;
			orderCollection = path == null ? emptyLinqOrderCollection : (new LinqOrder<T>[] { new LinqOrder<T>() { Direction = direction, Path = path } });
			return ListBy(null, orderCollection);
		}

		/// <summary>
		/// ���ر�T ��������
		/// </summary>
		/// <returns></returns>
		public IList<T> List()
		{
			return ListBy(null, null);
		}

		/// <summary>
		/// ������������ѯ��T ����
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public virtual IList<T> ListBy(Expression<Func<T, bool>> criteria)
		{
			return ListBy(criteria, null);
		}

		/// <summary>
		/// ������������ѯ��T ����,�����е�����
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> ListBy(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction)
		{
			LinqOrder<T>[] orderCollection = null;
			orderCollection = path == null ? emptyLinqOrderCollection : (new LinqOrder<T>[] { new LinqOrder<T>() { Direction = direction, Path = path } });
			return ListBy(criteria, orderCollection);
		}

		/// <summary>
		/// ������������ѯ��T ����,�����ж�����
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public virtual IList<T> ListBy(Expression<Func<T, bool>> criteria, params LinqOrder<T>[] orderCollection)
		{
			IQueryable<T> query = Session.Query<T>();
			if (criteria != null)
				query = query.Where(criteria);
			//�����������
			query = AttachOrders(query, orderCollection);
			return query.ToList();
		}

		/// <summary>
		/// ɸѡ���г�ǰTop��¼������������
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="count"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public virtual IList<T> ListTopX(Expression<Func<T, bool>> criteria, int count, params LinqOrder<T>[] orderCollection)
		{
			IQueryable<T> query = Session.Query<T>();
			if (criteria != null)
				query = query.Where(criteria);
			//�����������
			query = AttachOrders(query, orderCollection);
			return query.Take(count).ToList();
		}

		/// <summary>
		/// ɸѡ���г�ǰTop��¼������������
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="count"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> ListTopX(Expression<Func<T, bool>> criteria, int count, Expression<Func<T, object>> path, EnumOrder direction)
		{
			LinqOrder<T>[] orderCollection = null;
			orderCollection = path == null ? emptyLinqOrderCollection : (new LinqOrder<T>[] { new LinqOrder<T>() { Direction = direction, Path = path } });
			return ListTopX(criteria, count, orderCollection);
		}

		/// <summary>
		/// ɸѡ���г�ǰTop��¼��
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public virtual IList<T> ListTopX(Expression<Func<T, bool>> criteria, int count)
		{
			return ListTopX(criteria, count, null);
		}

		/// <summary>
		/// ��ñ��ܼ�¼��
		/// </summary>
		/// <returns></returns>
		public virtual int Count()
		{
			return Session.Query<T>().Count();
		}

		/// <summary>
		/// ������������ܼ�¼��
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public virtual int CountBy(Expression<Func<T, bool>> criteria)
		{
			return Session.Query<T>().Where(criteria).Count();
		}

		/// <summary>
		/// ��õ�һ�����ݶ���
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public T GetFirst(Expression<Func<T, bool>> criteria)
		{
			var list = ListTopX(criteria, 1, null);
			if (list.Count > 0)
				return list[0];
			return null;
		}

		/// <summary>
		/// ��õ�һ�����ݶ���,����������
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public T GetFirst(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction)
		{
			var list = ListTopX(criteria, 1, path, direction);
			if (list.Count > 0)
				return list[0];
			return null;
		}

		/// <summary>
		/// ��õ�һ�����ݶ���,����������
		/// </summary>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public T GetFirst(Expression<Func<T, object>> path, EnumOrder direction)
		{
			var list = ListTopX(null, 1, path, direction);
			if (list.Count > 0)
				return list[0];
			return null;
		}

		/// <summary>
		/// ��õ�һ�����ݶ���,����������
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public T GetFirst(Expression<Func<T, bool>> criteria, params LinqOrder<T>[] orderCollection)
		{
			var list = ListTopX(criteria, 1, orderCollection);
			if (list.Count > 0)
				return list[0];
			return null;
		}

		/// <summary>
		/// ��ñ��һ����¼
		/// </summary>
		/// <returns></returns>
		public T GetFirst()
		{
			var list = List();
			if (list.Count > 0)
				return list[0];
			return null;
		}

		#endregion


		private ISession staticSession;
		public ISession StaticSession
		{
			get
			{
				return staticSession;
			}
			set
			{
				staticSession = value;
			}
			
		}
		/// <summary>
		/// NHibernate Session ����
		/// </summary>

		private ISession session;
		public ISession Session
		{
			get
			{
				try
				{
					session = NHibernateHelper.SessionFactory.GetCurrentSession();
					if (session != null)
						return session;
				}
				catch
				{
					if (staticSession != null)
						return staticSession;
					staticSession = NHibernateHelper.SessionFactory.OpenSession();
					return staticSession;
				}
				return null;
			}
		}

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="query"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		private IQueryable<T> AttachOrders(IQueryable<T> query, LinqOrder<T>[] orderCollection)
		{
			if (orderCollection == null || orderCollection.Count() == 0)
				return query;
			for (int i = 0; i < orderCollection.Length; i++)
			{
				if (orderCollection[i].Direction == EnumOrder.ASC)
					query = query.OrderBy(orderCollection[i].Path);
				else
					query = query.OrderByDescending(orderCollection[i].Path);
			}
			return query;
		}

	}
}
