using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Linq.Expressions;
using NHibernate.Type;
using Project.Common;
using System.Data;
using Project.Dal.Base;

namespace Project.Biz.Base
{
	public class BaseBiz<T, U> : IBiz<T>
		where T : class
		where U : Repository<T>
	{
		protected static U dbAccess;
		public BaseBiz()
		{
			dbAccess = DalFactory.Get<U>();
		}

		public void Inject()
		{
			InjectBizObject.Inject(this);
		}

		/// <summary>
		/// ����ID��ö���
		/// </summary>
		/// <typeparam name="TInput"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual T GetByID<TInput>(TInput id)
		{
			return dbAccess.GetByID(id);
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
		public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count, Expression<Func<T, object>> path, EnumOrder direction)
		{
			return dbAccess.PaginateList(pageSize, pageIndex, ref count, path, direction);
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
		public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count, string path, EnumOrder direction)
		{
			return dbAccess.PaginateList(pageSize, pageIndex, ref count, ReflectionTools.GetClassPropertyExpression<T>(path), direction);
		}

		/// <summary>
		///��ҳ��ѯ��T ����,���������� ��path,direction,����������ʹ��#���зָ
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="count"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count, string path, string direction)
		{
			return dbAccess.PaginateList(pageSize, pageIndex, ref count, ConvertPathToLinqOrderArray(path, direction));
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
			return dbAccess.PaginateList(pageSize, pageIndex, ref count);
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
			return dbAccess.PaginateList(pageSize, pageIndex, ref count, orderCollection);
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
		public virtual IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, EnumOrder direction )
		{
			return dbAccess.PaginateListBy(pageSize, pageIndex, ref count, criteria, path, direction);
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
		public virtual IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, string path, EnumOrder direction)
		{
			return dbAccess.PaginateListBy(pageSize, pageIndex, ref count, criteria, ReflectionTools.GetClassPropertyExpression<T>(path), direction);
		}

		/// <summary>
		/// ��ҳ��ѯ��T,������ɸѡ,����������path,direction,����������ʹ��#���зָ
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="count"></param>
		/// <param name="criteria"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> PaginateListBy(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, string path, string direction)
		{
			return dbAccess.PaginateListBy(pageSize, pageIndex, ref count, criteria, ConvertPathToLinqOrderArray(path, direction));
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
			return dbAccess.PaginateListBy(pageSize, pageIndex, ref count, criteria);
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
			return dbAccess.PaginateListBy(pageSize, pageIndex, ref count, criteria, orderCollection);
		}

		/// <summary>
		/// ���ر�T ��������
		/// </summary>
		/// <param name="path">�����ֶ�</param>
		/// <param name="direction">������</param>
		/// <returns></returns>
		public virtual IList<T> List(System.Linq.Expressions.Expression<Func<T, object>> path , EnumOrder direction)
		{
			return dbAccess.List(path, direction);
		}

		public virtual IList<T> List(string path, EnumOrder direction)
		{
			return dbAccess.List(ReflectionTools.GetClassPropertyExpression<T>(path), direction);
		}

		/// <summary>
		/// ���������򣬣�path,direction,����������ʹ��#���зָ
		/// </summary>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> List(string path, string direction)
		{
			return dbAccess.List(ConvertPathToLinqOrderArray(path, direction));
		}

		/// <summary>
		/// ���������򣬣�path,direction,����������ʹ��#���зָ
		/// </summary>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> List(params LinqOrder<T>[] orderCollection)
		{
			return dbAccess.List(orderCollection);
		}
		

		/// <summary>
		/// ���ر�T ��������
		/// </summary>
		/// <returns></returns>
		public virtual IList<T> List()
		{
			return dbAccess.List();
		}

		/// <summary>
		/// �г�In ������ ��¼����
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="collection"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		public virtual IList<T> ListIn<T1>(IList<T1> collection, Expression<Func<T, object>> property)
		{
			return dbAccess.ListIn(collection, property);
		}

		/// <summary>
		/// ������������ѯ��T ����,�����е�����
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> ListBy(System.Linq.Expressions.Expression<Func<T, bool>> criteria, System.Linq.Expressions.Expression<Func<T, object>> path, EnumOrder direction)
		{
			return dbAccess.ListBy(criteria, path, direction);
		}

		/// <summary>
		/// ������������ѯ��T ����,�����е�����
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> ListBy(System.Linq.Expressions.Expression<Func<T, bool>> criteria, string path, string direction)
		{
			return dbAccess.ListBy(criteria, ConvertPathToLinqOrderArray(path, direction));
		}

		/// <summary>
		/// ������������ѯ��T ����,�����е�����
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual IList<T> ListBy(System.Linq.Expressions.Expression<Func<T, bool>> criteria, string path, EnumOrder direction)
		{
			return dbAccess.ListBy(criteria, ReflectionTools.GetClassPropertyExpression<T>(path), direction);
		}

		/// <summary>
		/// ������������ѯ��T ����
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public virtual IList<T> ListBy(System.Linq.Expressions.Expression<Func<T, bool>> criteria)
		{
			return dbAccess.ListBy(criteria);
		}

		/// <summary>
		/// ������������ѯ��T ����,�����ж�����
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public virtual IList<T> ListBy(System.Linq.Expressions.Expression<Func<T, bool>> criteria, params LinqOrder<T>[] orderCollection)
		{
			return dbAccess.ListBy(criteria, orderCollection);
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
			return dbAccess.ListTopX(criteria, count, path, direction);
		}

		/// <summary>
		/// ɸѡ���г�ǰTop��¼��
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public virtual IList<T> ListTopX(Expression<Func<T, bool>> criteria, int count)
		{
			return dbAccess.ListTopX(criteria, count);
		}

		/// <summary>
		/// ɸѡ���г�ǰTop��¼������������
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="count"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public virtual IList<T> ListTopX(Expression<Func<T, bool>> criteria, int count , params LinqOrder<T>[] orderCollection)
		{
			return dbAccess.ListTopX(criteria, count, orderCollection);
		}

		/// <summary>
		/// ��ñ��ܼ�¼��
		/// </summary>
		/// <returns></returns>
		public virtual int Count()
		{
			return dbAccess.Count();
		}

		/// <summary>
		/// ������������ܼ�¼��
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public virtual int CountBy(Expression<Func<T, bool>> criteria)
		{
			return dbAccess.CountBy(criteria);
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
			return dbAccess.GetFirst(criteria, path, direction);
		}

		/// <summary>
		/// ��õ�һ�����ݶ���
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public T GetFirst(Expression<Func<T, bool>> criteria)
		{
			return dbAccess.GetFirst(criteria);
		}

		/// <summary>
		/// ��õ�һ�����ݶ���,����������
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="orderCollection"></param>
		/// <returns></returns>
		public T GetFirst(Expression<Func<T, bool>> criteria, params LinqOrder<T>[] orderCollection)
		{
			return dbAccess.GetFirst(criteria, orderCollection);
		}

		/// <summary>
		/// ��ñ��һ����¼
		/// </summary>
		/// <returns></returns>
		public T GetFirst()
		{
			return dbAccess.GetFirst();
		}

		/// <summary>
		/// ���򣬻�õ�һ����¼
		/// </summary>
		/// <param name="path"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public T GetFirst(Expression<Func<T, object>> path, EnumOrder direction)
		{
			return dbAccess.GetFirst(path, direction);
		}

		/// <summary>
		/// �����¼
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Save(T entity)
		{
			ITransaction tx = dbAccess.Session.BeginTransaction();
			using (tx)
			{
				dbAccess.Save(entity);
				tx.Commit();
			}
		}

		/// <summary>
		/// ���¼�¼
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Update(T entity)
		{
			ITransaction tx = dbAccess.Session.BeginTransaction();
			using (tx)
			{
				dbAccess.Update(entity);
				tx.Commit();
			}
		}

		/// <summary>
		/// ɾ����¼
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Delete(T entity)
		{
			ITransaction tx = dbAccess.Session.BeginTransaction();
			using (tx)
			{
				dbAccess.Delete(entity);
				tx.Commit();
			}
		}

		/// <summary>
		/// ������߸������ݼ�¼�����T��������Ϊ��Guid���򱣴�,Ϊ�գ������
		/// </summary>
		/// <param name="entity"></param>
		public virtual void SaveOrUpdate(T entity)
		{
			ITransaction tx = dbAccess.Session.BeginTransaction();
			using (tx)
			{
				dbAccess.SaveOrUpdate(entity);
				tx.Commit();
			}
		}

		public virtual T Merge(T entity)
		{
			ITransaction tx = dbAccess.Session.BeginTransaction();
			T t = default(T);
			using (tx)
			{
				t = dbAccess.Merge(entity);
				tx.Commit();
			}
			return t;
		}

		public virtual void Persist(T entity)
		{
			ITransaction tx = dbAccess.Session.BeginTransaction();
			using (tx)
			{
				dbAccess.Persist(entity);
				tx.Commit();
			}
		}

		/// <summary>
		/// ����ID����ɾ��
		/// </summary>
		/// <typeparam name="TInput"></typeparam>
		/// <param name="id"></param>
		/// <param name="nullableType"></param>
		public void DeleteByID<TInput>(TInput id, NullableType nullableType)
		{
			ITransaction tx = dbAccess.Session.BeginTransaction();
			using (tx)
			{
				dbAccess.DeleteByID(id, nullableType);
				tx.Commit();
			}
		}


		private LinqOrder<T>[] ConvertPathToLinqOrderArray(string path,string direction)
		{
			var pathCollection = path.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
			var directionCollection = direction.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
			IList<LinqOrder<T>> linqOrderCollection = new List<LinqOrder<T>>();
			for (int i = 0; i < pathCollection.Count(); i++)
			{
				var tempExpression = ReflectionTools.GetClassPropertyExpression<T>(pathCollection[i]);
				if (tempExpression == null)
					continue;
				EnumOrder tempOrder;
				if ((directionCollection.Length - 1) >= i)
				{
					if (directionCollection[i].Trim() != ((int)EnumOrder.DESC).ToString() && directionCollection[i].Trim().ToUpper() != ((int)EnumOrder.ASC).ToString())
						tempOrder = EnumOrder.ASC;
					else
						tempOrder = (EnumOrder)Convert.ToInt32(directionCollection[i]);
				}
				else
					tempOrder = EnumOrder.ASC;
				LinqOrder<T> tempLinqOrder = new LinqOrder<T>();
				tempLinqOrder.Path = tempExpression;
				tempLinqOrder.Direction = tempOrder;
				linqOrderCollection.Add(tempLinqOrder);
			}
			return linqOrderCollection.ToArray();
		}

		//public static TIn InvokeConstructor<TIn>()
		//{
		//    Type t = typeof(TIn);
		//    Type st = typeof(ISession);
		//    ConstructorInfo constructInfo = t.GetConstructor(new Type[] { st });
		//    DynamicMethod dynamicmethod = new DynamicMethod("", t, new Type[] { st });
		//    ILGenerator il = dynamicmethod.GetILGenerator();
		//    il.Emit(OpCodes.Ldarg_0);
		//    il.Emit(OpCodes.Newobj, constructInfo);
		//    il.Emit(OpCodes.Ret);
		//    Func<ISession, TIn> fun = dynamicmethod.CreateDelegate(typeof(Func<ISession, TIn>)) as Func<ISession, TIn>;
		//    TIn o = fun(session);
		//    return o;
		//}

	}
}
