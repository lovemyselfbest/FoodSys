using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T4Common;
using T4Common.Domain;

namespace T4WinHost.Base
{
	[Serializable]
	public class T4Parameters
	{
		#region Initial info
		public string SqlConnectionString { get; set; }
		public string NamespacePrefix { get; set; }
		public string ProjectRootPath { get; set; }
		public string DalFolder { get; set; }
		public string BizFolder { get; set; }
		public string EntityFolder { get; set; }
		public string EnumFolder { get; set; }
		public bool IsGenerateSingle { get; set; }
		public string TFSUrl { get; set; }
		public string TFSPassword { get; set; }
		public string TFSUserName { get; set; }
		public bool TFSChk { get; set; }
		#endregion


		#region StoreProcedure
		public Dictionary<string, List<SpColumn>> StoreProcedureParametersMeta { get; set; }
		public Dictionary<string, List<SpColumn>> StoreProcedureResultMeta { get; set; }
		#endregion

		#region Tables
		public IList<Table> Tables { get; set; }
		#endregion


		private static T4Parameters _default;
		public static T4Parameters Default
		{
			get
			{
				if (_default != null)
					return _default;
				_default = new T4Parameters();
				return _default;
			}
		}

		private T4Parameters()
		{

		}
	}
}
