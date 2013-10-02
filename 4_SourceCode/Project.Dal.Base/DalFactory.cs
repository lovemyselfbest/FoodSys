using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Reflection;
using System.Reflection.Emit;
namespace Project.Dal.Base
{
	public class DalFactory
	{
		private static Dictionary<Type, object> objDalDictionary = new Dictionary<Type, object>();
		private static object asyn = new object();
		public static object Get(Type t)
		{
			Object o = null;
			Assembly asm = null;

			lock (asyn)
			{
				foreach (Type i in objDalDictionary.Keys)
				{
					if (i == t)
					{
						o = objDalDictionary[i];
						return o;
					}
				}
				asm = asm == null ? t.Assembly : asm;
				o = asm.CreateInstance(t.FullName,
					false,
					BindingFlags.Default | BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic,
					null,
					null,
					null,
					null);
				objDalDictionary.Add(t, o);
				//获得注入方法，并进行注入
				MethodInfo injectMethod = t.GetMethod("Inject");
				if(injectMethod!=null)
					injectMethod.Invoke(o, null);
				return o;
			}
		}

		public static T Get<T>()
		{
			return (T)Get( typeof(T));
		}
	}
}
