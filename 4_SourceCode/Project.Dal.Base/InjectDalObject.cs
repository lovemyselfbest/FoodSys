using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using NHibernate;

namespace Project.Dal.Base
{
	public class InjectDalObject
	{
		private static Dictionary<Type, List<FieldInfo>> objBizFieldsDictionary = new Dictionary<Type, List<FieldInfo>>();
		private static object asyn = new object();
		public static void Inject(Object target)
		{
			Type t = target.GetType();
			lock (asyn)
			{
				foreach (Type i in objBizFieldsDictionary.Keys)
				{
					if (i == t)
					{
						var cacheFields  = objBizFieldsDictionary[i];
						foreach (var item in cacheFields)
						{
							item.SetValue(target, DalFactory.Get(item.FieldType));
						}
						return;
					}
				}
				var fields = t.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
				List<FieldInfo> targetFieldInfos = new List<FieldInfo>();
				foreach (var item in fields)
				{
					var baseType= item.FieldType.BaseType;
					if (baseType != null && (baseType.Name.StartsWith("Repository")
						|| baseType.Name.StartsWith("BaseSPDal"))
						)
					{
						targetFieldInfos.Add(item);
						item.SetValue(target, DalFactory.Get(item.FieldType));
					}
				}
				objBizFieldsDictionary.Add(t, targetFieldInfos);
			}
		}
	}
}
