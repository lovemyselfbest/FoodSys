using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace T4WinHost
{
	public delegate void DelegateSetLabelText(string text,Label label);
	public class ThreadStateObject
	{
		public string TemplateName { get; set; }
		public Label LblTip { get; set; }
	}
}
