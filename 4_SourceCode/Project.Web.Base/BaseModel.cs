using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Web.Base.Utility;
using System.Web.Mvc;

namespace Project.Web.Base
{
	public class BaseModel
	{
		public BaseModel()
		{
			InjectBizObject.Inject(this);
		}

		/// <summary>
		/// 记录总数
		/// </summary>
		protected int count;

		/// <summary>
		/// 导出对象
		/// </summary>
		public ExportHelper ExportObject
		{
			get;
			set;
		}


		/// <summary>
		/// 分页对象
		/// </summary>
		private PaginateHelper paginateHelperObject;
		public PaginateHelper PaginateHelperObject
		{
			get
			{
				if (paginateHelperObject == null)
					paginateHelperObject = PaginateHelper.ConstructPaginate(0, 0, 0, null, null);
				return paginateHelperObject;
			}
			set
			{
				paginateHelperObject = value;
			}
		}

	}
}
