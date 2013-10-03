using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace T4Common
{
	public class StoreProcedure
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<SpColumn> Parameters { get; set; }
	}
}
