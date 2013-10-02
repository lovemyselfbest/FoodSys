	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Project.Dal.Base
{
	public abstract class BaseSPDal
	{
		public ISession Session
		{
			get { return NHibernateHelper.SessionFactory.GetCurrentSession(); }
		}
	}
}
