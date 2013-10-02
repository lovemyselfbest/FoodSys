	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Collections;
using System.Reflection;
using NHibernate.Properties;

namespace NHibernate.Transform
{
	/// <summary> 
	/// Result transformer that allows to transform a result to  
	/// a user specified class which will be populated via setter   
	/// methods or fields matching the alias names.  
	/// </summary> 
	/// <example> 
	/// <code> 
	/// IList resultWithAliasedBean = s.CreateCriteria(typeof(Enrollment)) 
	///             .CreateAlias("Student", "st") 
	///             .CreateAlias("Course", "co") 
	///             .SetProjection( Projections.ProjectionList() 
	///                     .Add( Projections.Property("co.Description"), "CourseDescription" ) 
	///             ) 
	///             .SetResultTransformer( new AliasToBeanResultTransformer(typeof(StudentDTO)) ) 
	///             .List(); 
	///  
	/// StudentDTO dto = (StudentDTO)resultWithAliasedBean[0]; 
	/// </code> 
	/// </example> 
	[Serializable]
	public class CustomAliasToBeanResultTransformer : IResultTransformer
	{
		// Fields
		private readonly ConstructorInfo constructor;
		private const BindingFlags flags = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		private readonly IPropertyAccessor propertyAccessor;
		private readonly System.Type resultClass;
		private ISetter[] setters;

		// Methods
		public CustomAliasToBeanResultTransformer(System.Type resultClass)
		{
			if (resultClass == null)
			{
				throw new ArgumentNullException("resultClass");
			}
			this.resultClass = resultClass;
			this.constructor = resultClass.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, System.Type.EmptyTypes, null);
			if ((this.constructor == null) && resultClass.IsClass)
			{
				throw new ArgumentException("The target class of a AliasToBeanResultTransformer need a parameter-less constructor", "resultClass");
			}
			this.propertyAccessor = new ChainedPropertyAccessor(new IPropertyAccessor[] { PropertyAccessorFactory.GetPropertyAccessor(null), PropertyAccessorFactory.GetPropertyAccessor("field") });
		}

		public bool Equals(CustomAliasToBeanResultTransformer other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			return (object.ReferenceEquals(this, other) || object.Equals(other.resultClass, this.resultClass));
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as AliasToBeanResultTransformer);
		}

		public override int GetHashCode()
		{
			return this.resultClass.GetHashCode();
		}

		public IList TransformList(IList collection)
		{
			return collection;
		}

		public object TransformTuple(object[] tuple, string[] aliases)
		{
			ProcessCSharpKeywords(aliases);
			object obj2;
			if (aliases == null)
			{
				throw new ArgumentNullException("aliases");
			}
			try
			{
				if (this.setters == null)
				{
					this.setters = new ISetter[aliases.Length];
					for (int j = 0; j < aliases.Length; j++)
					{
						string propertyName = aliases[j];
						if (propertyName != null)
						{
							this.setters[j] = this.propertyAccessor.GetSetter(this.resultClass, propertyName);
						}
					}
				}
				obj2 = this.resultClass.IsClass ? this.constructor.Invoke(null) : NHibernate.Cfg.Environment.BytecodeProvider.ObjectsFactory.CreateInstance(this.resultClass, true);
				for (int i = 0; i < aliases.Length; i++)
				{
					if (this.setters[i] != null)
					{
						this.setters[i].Set(obj2, tuple[i]);
					}
				}
			}
			catch (InstantiationException exception)
			{
				throw new HibernateException("Could not instantiate result class: " + this.resultClass.FullName, exception);
			}
			catch (MethodAccessException exception2)
			{
				throw new HibernateException("Could not instantiate result class: " + this.resultClass.FullName, exception2);
			}
			return obj2;
		}
		private string[] ProcessCSharpKeywords(string[] strArray)
		{
			for (int i = 0; i < strArray.Length; i++)
			{
				if (_CSharpKeywords.Contains(strArray[i]))
					strArray[i] = strArray[i] + "_Keyword";
			}
			return strArray;
		}

		private static IList<string> _CSharpKeywords = new List<string>()
		{
			"abstract",
			"as",
			"base",
			"bool",
			"break",
			"byte",
			"case",
			"catch",
			"char",
			"checked",
			"class",
			"const",
			"continue",
			"decimal",
			"default",
			"delegate",
			"do",
			"double",
			"else",
			"enum",
			"event",
			"explicit",
			"extern",
			"false",
			"finally",
			"fixed",
			"float",
			"for",
			"foreach",
			"goto",
			"if",
			"implicit",
			"in",
			"int",
			"interface",
			"internal",
			"is",
			"lock",
			"long",
			"namespace",
			"new",
			"null",
			"object",
			"operator",
			"out",
			"override",
			"params",
			"private",
			"protected",
			"public",
			"readonly",
			"ref",
			"return",
			"sbyte",
			"sealed",
			"short",
			"sizeof",
			"stackalloc",
			"static",
			"string",
			"struct",
			"switch",
			"this",
			"throw",
			"true",
			"try",
			"typeof",
			"uint",
			"ulong",
			"unchecked",
			"unsafe",
			"ushort",
			"using",
			"virtual",
			"void",
			"volatile",
			"while"
		};

	}

}

