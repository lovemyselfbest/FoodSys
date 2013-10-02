using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping;
using NHibernate.Linq;
using NHibernate.Type;
using System.Text.RegularExpressions;
using NHibernate.Linq.Functions;
using System.Reflection;
using NHibernate.Hql.Ast;
using NHibernate.Linq.Visitors;
using System.Collections.ObjectModel;

namespace Project.Dal.Base
{

	public static class NHibernateLinqExtensions
	{
		public static bool IsLike(this string source, string pattern)
		{
			pattern = Regex.Escape(pattern);
			pattern = pattern.Replace("%", ".*?").Replace("_", ".");
			pattern = pattern.Replace(@"\[", "[").Replace(@"\]", "]").Replace(@"\^", "^");
			return Regex.IsMatch(source, pattern);
		}
	}

	public class IsLikeGenerator : BaseHqlGeneratorForMethod
	{
		public IsLikeGenerator()
		{
			SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NHibernateLinqExtensions.IsLike(null, null)) };
		}

		public override HqlTreeNode BuildHql(MethodInfo method, System.Linq.Expressions.Expression targetObject,
			ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
		{
			return treeBuilder.Like(visitor.Visit(arguments[0]).AsExpression(),
									visitor.Visit(arguments[1]).AsExpression());
		}
	}

	public class NHibernateLinqToHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
	{
		public NHibernateLinqToHqlGeneratorsRegistry()
		{
			RegisterGenerator(ReflectionHelper.GetMethodDefinition(
				() => NHibernateLinqExtensions.IsLike(null, null)), new IsLikeGenerator());
		}
	}

	public sealed class NHibernateHelper
	{
		private static ISessionFactory sessionFactory;
		public static Configuration NHConguration { get; set; }
		static NHibernateHelper()
		{

		}

		public static void Configure()
		{
			NHConguration = new Configuration().Configure();
			NHConguration.LinqToHqlGeneratorsRegistry<NHibernateLinqToHqlGeneratorsRegistry>();
			sessionFactory = NHConguration.BuildSessionFactory();
		}

		public static ISessionFactory SessionFactory
		{
			get
			{
				return sessionFactory;
			}
		}

		public static void CloseSessionFactory()
		{
			if (sessionFactory != null)
			{
				sessionFactory.Close();
			}
		}
	}
}


