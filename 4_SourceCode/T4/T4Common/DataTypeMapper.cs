using System;
using System.Collections.Generic;

namespace T4Common
{
    public class DataTypeMapper
    {
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
		public static IList<string> CSharpKeywords
		{
			get
			{
				return _CSharpKeywords;
			}
		}
        public static Type MapFromDBType(string dataType)
        {
			dataType = dataType.ToLower();
            if (dataType == "date" || dataType == "datetime" ||
				dataType == "timestamp with time zone" || dataType == "timestamp with local time zone" ||
                dataType == "smalldatetime")
            {
                return typeof(DateTime);
            }
            if (dataType == "number" || dataType == "long" || dataType == "bigint")
            {
                return typeof(long);
            }
			if (dataType == "int" || dataType == "interval year to month" || dataType == "binary_integer")
            {
                return typeof(int);
            }
			if (dataType == "binary_double" || dataType == "float")
            {
                return typeof(double);
            }
			if (dataType == "binary_float" || dataType == "float")
            {
                return typeof(float);
            }
            if (dataType == "blob" || dataType == "bfile *" || dataType == "long raw" || dataType == "binary" ||
                dataType == "image" || dataType == "timestamp" || dataType == "varbinary")
            {
                return typeof(byte[]);
            }
			if (dataType == "interval day to second")
            {
                return typeof(TimeSpan);
            }
            if (dataType == "bit")
            {
                return typeof(Boolean);
            }
            if (dataType == "decimal" || dataType == "money" || dataType == "smallmoney" || dataType == "numeric")
            {
                return typeof(decimal);
            }
            if (dataType == "real")
            {
                return typeof(Single);
            }
            if (dataType == "smallint")
            {
                return typeof(Int16);
            }
            if (dataType == "uniqueidentifier")
            {
                return typeof(Guid);
            }
            if (dataType == "tinyint")
            {
                return typeof(Byte);
            }

            // CHAR, CLOB, NCLOB, NCHAR, XMLType, VARCHAR2, nchar, ntext
            return typeof(string);
        }
    }
}