using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Project.Common
{
	public enum EnumAttachDataKey
	{
		String,
		Int,
		Decimal
	}

	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public class AttachDataAttribute : Attribute
	{
		public AttachDataAttribute(object key, object value)
		{
			this.Key = key;
			this.Value = value;
		}

		public object Key { get; private set; }

		public object Value { get; private set; }

		public static string GetAttachedData<T>(string memeberName, EnumAttachDataKey key)
		{
			var type = typeof(T);
			var memInfos = type.GetMember(memeberName);
			var attributes = memInfos[0].GetCustomAttributes(typeof(AttachDataAttribute), false);
			if (attributes.Length == 0)
				return null;
			foreach (var item in attributes)
			{
				if (((AttachDataAttribute)item).Key.ToString() == key.ToString())
				{
					return ((AttachDataAttribute)item).Value.ToString();
				}
			}
			return ((AttachDataAttribute)attributes[0]).Value.ToString();
		}
	}

	public static class AttachDataExtensions
	{
		public static object GetAttachedData(
			this ICustomAttributeProvider provider, object key)
		{
			var attributes = (AttachDataAttribute[])provider.GetCustomAttributes(
				typeof(AttachDataAttribute), false);
			return attributes.First(a => a.Key.Equals(key)).Value;
		}

		public static T GetAttachedData<T>(
			this ICustomAttributeProvider provider, object key)
		{
			return (T)provider.GetAttachedData(key);
		}

		public static object GetAttachedData(this Enum value, object key)
		{
			return value.GetType().GetField(value.ToString()).GetAttachedData(key);
		}

		public static T GetAttachedData<T>(this Enum value, object key)
		{
			return (T)value.GetAttachedData(key);
		}

	}
}
