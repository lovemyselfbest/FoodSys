using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualStudio.TextTemplating;
using T4WinHost.Base;
using System.CodeDom.Compiler;
using System.Runtime.Remoting.Messaging;
using System.Data.SqlClient;
using System.Threading;
using T4Common;
using T4Common.Domain;
using System.Threading.Tasks;

namespace T4WinHost
{
	public partial class Main : Form
	{
		private static object synObject = new object();
		private SqlServerProcMetaReader procMetaReader = null;
		private SqlServerTableMetaReader tableMetaReader = null;
		private IList<Table> tables = null;
		private int dealFileCount = 0;
		private int taskFinishedCount = 0;
		private static object synObj = new object();
		private DataGridViewCell contextCell;
		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			InitialTemplateInfo();
			InitialSettingInfo();
			if (TestConnection())
			{
				procMetaReader = new SqlServerProcMetaReader(txtCon.Text);
				tableMetaReader = new SqlServerTableMetaReader(txtCon.Text);
				InitialGvProcedures();

				tables = tableMetaReader.GetTables("dbo");
				InitialGvTables(tables);
				InitialGvViews(tables);
			}
		}

		private void InitialSettingInfo()
		{
			txtNameSpace.Text = T4Common.Properties.Settings.Default.NamespacePrefix;
			txtFolder.Text = T4Common.Properties.Settings.Default.ProjectRootPath;
			txtCon.Text = T4Common.Properties.Settings.Default.SqlConnectionString;
			txtUrl.Text = T4Common.Properties.Settings.Default.TFSUrl;
			txtName.Text = T4Common.Properties.Settings.Default.TFSName;
			txtPassword.Text = T4Common.Properties.Settings.Default.TFSPassword;
			chkTFS.Checked = Convert.ToBoolean(T4Common.Properties.Settings.Default.TFSChk);
			groupTFS.Visible = chkTFS.Checked;
			groupBox4.Height = groupTFS.Visible ? 168 : 168 - 85;
		}

		private void InitialTemplateInfo()
		{
			string templateFolder = string.Format(@"{0}\Templates", Environment.CurrentDirectory);
			string[] files = Directory.GetFiles(templateFolder);
			List<TemplateInfo> templateInfos = new List<TemplateInfo>();
			for (int i = 0; i < files.Count(); i++)
			{
				if (Path.GetExtension(Path.GetFileName(files[i])) != ".tt")
					continue;
				templateInfos.Add(new TemplateInfo() { Name = Path.GetFileName(files[i]), Description = "", FullPath = files[i] });
			}
			DataGridViewTextBoxColumn ColTemplateName = new DataGridViewTextBoxColumn();
			ColTemplateName.DataPropertyName = "name";
			ColTemplateName.Name = "name";
			ColTemplateName.HeaderText = "Template file";
			ColTemplateName.ReadOnly = true;
			ColTemplateName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			DataGridViewTextBoxColumn ColDescription = new DataGridViewTextBoxColumn();
			ColDescription.DataPropertyName = "description";
			ColDescription.HeaderText = "Description";
			ColDescription.Name = "Description";
			ColDescription.Width = 300;
			ColDescription.ReadOnly = true;

			DataGridViewTextBoxColumn ColTemplateFullPath = new DataGridViewTextBoxColumn();
			ColTemplateFullPath.DataPropertyName = "FullPath";
			ColTemplateFullPath.Name = "FullPath";
			ColTemplateFullPath.Visible = false;

			gvTemplate.Columns.Add(ColTemplateName);
			gvTemplate.Columns.Add(ColDescription);
			gvTemplate.Columns.Add(ColTemplateFullPath);

			gvTemplate.DataSource = templateInfos;
		}

		private void ProcessTemplate(params string[] args)
		{
			string templateFileName = null;

			if (args.Length == 0)
			{
				throw new System.Exception("you must provide a text template file path");
			}

			templateFileName = args[0];

			if (templateFileName == null)
			{
				throw new ArgumentNullException("the file name cannot be null");
			}

			if (!File.Exists(templateFileName))
			{
				throw new FileNotFoundException("the file cannot be found");
			}

			OnShowWorkInfoEventHandler(this, new WorkEventArgs(WorkStage.InitializeWork, string.Format("processing template: {0}", Path.GetFileName(templateFileName))));

			TextTemplatingSession session = new TextTemplatingSession();
			session.Add("t4Parameter", T4Parameters.Default);
			CustomCmdLineHost host = new CustomCmdLineHost();
			host.TemplateFileValue = templateFileName;
			host.Session = session;
			Engine engine = new Engine();
			//Read the text template.
			string input = File.ReadAllText(templateFileName);
			input = input.Replace(@"$(ProjectDir)\", "");
			//Transform the text template.
			string output = engine.ProcessTemplate(input, host);
			foreach (CompilerError error in host.Errors)
			{
				Console.WriteLine(error.ToString());
			}

			lock (synObject)
			{
				int prograssBarValue = OnGetValueEventHandler(this, null);
				prograssBarValue++;
				OnDoWorkEventHandler(this, new WorkEventArgs(WorkStage.DoWork, string.Format("{0} has been processed...", templateFileName), prograssBarValue) { TemplateName = Path.GetFileName(templateFileName) });
			}
		}

		private void BtnGenerateAll_Click(object sender, EventArgs e)
		{
			this.progressBar.Value = 0;
			this.BtnGenerateAll.Enabled = false;
			this.lblInfo.Text = "";
			RecoverBackground();
			if (!CheckIntegrity())
				return;
			if (!TestConnection())
				MessageBox.Show("Connection failed!");
			T4Parameters.Default.IsGenerateSingle = false;
			SaveSettings();
			ThreadPool.QueueUserWorkItem(InitialStoreProcedures, null);
		}

		private void Process()
		{
			List<string> selectFiles = RetriveGridViewSelect(gvTemplate, "FullPath", "ColCheck");
			OnStartWorkEventHandler(this, new WorkEventArgs(WorkStage.BeginWork, "Start to work...", selectFiles.Count));
			for (int i = 0; i < selectFiles.Count(); i++)
			{
				ThreadPool.QueueUserWorkItem(MutipleThreadDo, selectFiles[i]);
			}
			dealFileCount = selectFiles.Count;
			Thread monitor = new Thread(Monitor);
			monitor.Start();
		}

		private void InitialStoreProcedures(object state)
		{
			List<string> selectFiles = RetriveGridViewSelect(gvTemplate, "FullPath", "ColCheck");
			int i = 0;
			for (i = 0; i < selectFiles.Count; i++)
			{
				if (Path.GetFileName(selectFiles[i]) != "Biz.tt" && Path.GetFileName(selectFiles[i]) != "Dal.tt" &&
					Path.GetFileName(selectFiles[i]) != "CSProj.Enum.tt" && Path.GetFileName(selectFiles[i]) != "CSProj.Solution.tt" &&
					Path.GetFileName(selectFiles[i]) != "Entity.tt" && Path.GetFileName(selectFiles[i]) != "EntityMapping.tt" &&
					Path.GetFileName(selectFiles[i]) != "Enum.tt")
					break;
			}
			if (i == selectFiles.Count)
			{
				InitailTables();
				Process();
				return;
			}

			SqlServerProcMetaReader procMetaReader = new SqlServerProcMetaReader(T4Parameters.Default.SqlConnectionString);
			T4Parameters.Default.StoreProcedureParametersMeta = procMetaReader.SpParaMeta;
			T4Parameters.Default.StoreProcedureResultMeta = procMetaReader.SpResultMeta;
			if (procMetaReader.SpFailed.Count > 0)
			{
				StringBuilder spErrorBuilder = new StringBuilder();
				spErrorBuilder.AppendLine(string.Format("{0} store procedure failed", procMetaReader.SpFailed.Count));
				spErrorBuilder.AppendLine("**********************************************************************");
				foreach (var item in procMetaReader.SpFailed)
				{
					spErrorBuilder.AppendLine(string.Format("Procedure name : {0}", item.Key));
				}
				spErrorBuilder.AppendLine("**********************************************************************");
				OnSetLogHandler(this, new WorkEventArgs(WorkStage.DoWork, "") { ErrorInfo = spErrorBuilder.ToString() });
			}

			InitailTables();
			Process();
		}

		private void InitailTables()
		{
			List<string> selectFiles = RetriveGridViewSelect(gvTemplate, "FullPath", "ColCheck");
			int i = 0;
			for (i = 0; i < selectFiles.Count; i++)
			{
				if (Path.GetFileName(selectFiles[i]) != "CSProj.Solution.tt")
					break;
			}
			if (i == selectFiles.Count)
				return;
			OnShowWorkInfoEventHandler(this, new WorkEventArgs(WorkStage.InitializeWork, "Initialize Tables.."));
			SqlServerTableMetaReader tableReader = new SqlServerTableMetaReader(T4Parameters.Default.SqlConnectionString);
			T4Parameters.Default.Tables = tableReader.RetriveTableDetails();

			//table no primary key and view no keyID
			List<string> errorTables = new List<string>();
			List<string> errorViews = new List<string>();
			for (int j = 0; j < T4Parameters.Default.Tables.Count; j++)
			{
				if (T4Parameters.Default.Tables[j].TableType == T4Common.Domain.EnumTableType.Table && T4Parameters.Default.Tables[j].PrimaryKey.Columns.Count == 0)
					errorTables.Add(T4Parameters.Default.Tables[j].Name);
				else if ((T4Parameters.Default.Tables[j].TableType == T4Common.Domain.EnumTableType.View && T4Parameters.Default.Tables[j].Columns.Count(x => x.Name.Equals("KeyID", StringComparison.OrdinalIgnoreCase)) == 0))
					errorViews.Add(T4Parameters.Default.Tables[j].Name);
			}
			SetTableErrorlog(errorTables, EnumTableType.Table);
			SetTableErrorlog(errorViews, EnumTableType.View);

		}

		private void SetTableErrorlog(List<string> errors, EnumTableType tableType)
		{
			if (errors.Count() > 0)
			{
				StringBuilder tableErrorBuilder = new StringBuilder();
				tableErrorBuilder.AppendLine(string.Format("{0} {1} failed", errors.Count(), tableType.ToString()));
				tableErrorBuilder.AppendLine("**********************************************************************");
				foreach (var item in errors)
				{
					tableErrorBuilder.AppendLine(string.Format("{1} name : {0} , has no {2} .", item, tableType.ToString(), tableType == EnumTableType.Table ? "primary key" : "keyID"));
				}
				tableErrorBuilder.AppendLine("**********************************************************************");
				OnSetLogHandler(this, new WorkEventArgs(WorkStage.DoWork, "") { ErrorInfo = tableErrorBuilder.ToString() });
			}
		}

		private List<string> RetriveGridViewSelect(DataGridView gridView, string dataColName, string gridColName)
		{
			List<string> selects = new List<string>();
			for (int i = 0; i < gridView.Rows.Count; i++)
			{
				DataGridViewCheckBoxCell colCheck = gridView.Rows[i].Cells[gridColName] as DataGridViewCheckBoxCell;
				if (colCheck.Value != null && Convert.ToBoolean(colCheck.Value))
				{
					DataGridViewTextBoxCell colIdentity = gridView.Rows[i].Cells[dataColName] as DataGridViewTextBoxCell;
					selects.Add(colIdentity.Value.ToString());
				}
			}
			return selects;
		}

		public void Monitor()
		{
			while (true)
			{
				Thread.Sleep(1000);
				int prograssBarValue = OnGetValueEventHandler(this, null);
				if (prograssBarValue == dealFileCount)
				{
					OnEndWorkEventHandler(this, new WorkEventArgs(WorkStage.EndWork, "Process is finished!", dealFileCount));
					return;
				}
			}
		}

		private void MutipleThreadDo(object o)
		{
			ProcessTemplate(o as string);
		}

		private void SaveSettings()
		{
			T4Common.Properties.Settings.Default.NamespacePrefix = txtNameSpace.Text.Trim();
			T4Common.Properties.Settings.Default.ProjectRootPath = txtFolder.Text.EndsWith("\\") ? txtFolder.Text : txtFolder.Text + "\\";
			T4Common.Properties.Settings.Default.SqlConnectionString = txtCon.Text;
			T4Common.Properties.Settings.Default.TFSName = txtName.Text.Trim();
			T4Common.Properties.Settings.Default.TFSPassword = txtPassword.Text.Trim();
			T4Common.Properties.Settings.Default.TFSUrl = txtUrl.Text.Trim();
			T4Common.Properties.Settings.Default.TFSChk = chkTFS.Checked.ToString();
			T4Common.Properties.Settings.Default.Save();
			T4Parameters.Default.SqlConnectionString = T4Common.Properties.Settings.Default.SqlConnectionString;
			T4Parameters.Default.NamespacePrefix = T4Common.Properties.Settings.Default.NamespacePrefix;
			T4Parameters.Default.ProjectRootPath = T4Common.Properties.Settings.Default.ProjectRootPath;
			T4Parameters.Default.DalFolder = string.Format("{0}.Dal", T4Parameters.Default.NamespacePrefix);
			T4Parameters.Default.BizFolder = string.Format("{0}.Biz", T4Parameters.Default.NamespacePrefix);
			T4Parameters.Default.EntityFolder = string.Format("{0}.Entity", T4Parameters.Default.NamespacePrefix);
			T4Parameters.Default.EnumFolder = string.Format("{0}.Enum", T4Parameters.Default.NamespacePrefix);
			T4Parameters.Default.TFSUrl = txtUrl.Text.Trim();
			T4Parameters.Default.TFSUserName = txtName.Text.Trim();
			T4Parameters.Default.TFSPassword = txtPassword.Text.Trim();
			T4Parameters.Default.TFSChk = chkTFS.Checked;
			
		}

		private void BtnCon_Click(object sender, EventArgs e)
		{
			if (TestConnection())
			{
				MessageBox.Show("Successful connection!", "Connection information");
				procMetaReader = new SqlServerProcMetaReader(txtCon.Text);
				tableMetaReader = new SqlServerTableMetaReader(txtCon.Text);
				tables = tableMetaReader.GetTables("dbo");
				InitialGvProcedures();
				InitialGvTables(tables);
				InitialGvViews(tables);
			}
			else
				MessageBox.Show("Connection failed!");
		}

		private bool CheckIntegrity()
		{
			string message = "";
			if (string.IsNullOrEmpty(txtCon.Text))
				message += "DB Connection String can't be empty!\n";
			if (string.IsNullOrEmpty(txtFolder.Text))
				message += "Destination folder can't be emty!\n";
			if (string.IsNullOrEmpty(txtNameSpace.Text))
				message += "Namespace can't be emty!\n";

			if (message == "")
				return true;
			return false;
		}

		private bool TestConnection()
		{
			SqlConnection connection = new SqlConnection();
			try
			{
				connection.ConnectionString = txtCon.Text.Trim();
				connection.Open();
				this.Text = string.Format("{0} - {1}", "T4 Engine", connection.Database);
				return true;

			}
			catch
			{
				return false;
			}

		}

		private void btnSelectFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog openDialog = new FolderBrowserDialog();
			DialogResult result = openDialog.ShowDialog();
			if (result.ToString() == "OK")
				txtFolder.Text = openDialog.SelectedPath;
		}

		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
			this.Dispose();
		}

		private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxSelectAll(gvTemplate, cbSelectAll, "ColCheck");
		}

		private void CheckBoxSelectAll(DataGridView gridView, CheckBox checkbox, string colName)
		{
			for (int i = 0; i < gridView.Rows.Count; i++)
			{
				DataGridViewCheckBoxCell colCheck = gridView.Rows[i].Cells[colName] as DataGridViewCheckBoxCell;
				colCheck.Value = checkbox.Checked;
			}
		}

		private void cbReverse_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxReverse(gvTemplate, "ColCheck");
		}

		private void CheckBoxReverse(DataGridView gridView, string colName)
		{
			for (int i = 0; i < gridView.Rows.Count; i++)
			{
				DataGridViewCheckBoxCell colCheck = gridView.Rows[i].Cells[colName] as DataGridViewCheckBoxCell;
				if (colCheck.Value != null && Convert.ToBoolean(colCheck.Value))
					colCheck.Value = false;
				else
					colCheck.Value = true;
			}
		}

		private void SetGridRecordBackground(string templateName)
		{
			for (int i = 0; i < gvTemplate.Rows.Count; i++)
			{
				DataGridViewTextBoxCell colName = gvTemplate.Rows[i].Cells["name"] as DataGridViewTextBoxCell;
				if (!colName.Value.Equals(templateName))
					continue;
				gvTemplate.Rows[i].DefaultCellStyle.BackColor = Color.Green;
			}
		}

		private void RecoverBackground()
		{
			for (int i = 0; i < gvTemplate.Rows.Count; i++)
			{
				gvTemplate.Rows[i].DefaultCellStyle.BackColor = Color.White;
			}
		}

		#region Tab Tables

		public void InitialGvTables(IList<Table> tables)
		{
			CommonInitialGridView(tables, gvTables, EnumTableType.Table);
		}

		private void FilterTables()
		{
			if (string.IsNullOrEmpty(txtTableName.Text.Trim()))
			{
				gvTables.DataSource = tables.Where(x => x.TableType == EnumTableType.Table).ToList();
				return;
			}
			gvTables.DataSource = tables.Where(x => (x.Name.ToLower().Contains(txtTableName.Text.Trim().ToLower()) || x.Remark.Contains(txtTableName.Text.Trim())) && x.TableType == EnumTableType.Table).ToList();
		}

		private void btnSearchTable_Click(object sender, EventArgs e)
		{
			FilterTables();
		}

		private void txtTableName_TextChanged(object sender, EventArgs e)
		{
			FilterTables();
		}

		private void CommonInitialGridView(IList<Table> tables, DataGridView gridView, EnumTableType tableType)
		{
			gridView.AutoGenerateColumns = false;
			if (gridView.ColumnCount <= 1)
			{
				DataGridViewTextBoxColumn ColTemplateName = new DataGridViewTextBoxColumn();
				ColTemplateName.DataPropertyName = "Name";
				ColTemplateName.Name = "name";
				ColTemplateName.HeaderText = "Name";
				ColTemplateName.ReadOnly = true;
				ColTemplateName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

				DataGridViewTextBoxColumn ColRemark = new DataGridViewTextBoxColumn();
				ColRemark.DataPropertyName = "Remark";
				ColRemark.Name = "Remark";
				ColRemark.HeaderText = "Remark";
				ColRemark.ReadOnly = true;
				ColRemark.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				gridView.Columns.Add(ColTemplateName);
				gridView.Columns.Add(ColRemark);
				gridView.AllowUserToResizeRows = false;
			}
			gridView.DataSource = tables.Where(x => x.TableType == tableType).ToList();
		}

		private void CommonGenerateTable(IList<string> selectObjectNames, Label lblTip, Button btnGenerate)
		{
			IList<Table> tableDetails = new List<Table>();
			for (int i = 0; i < selectObjectNames.Count; i++)
			{
				var tempTable = tables.First(x => x.Name == selectObjectNames[i]);
				tempTable.Columns = tableMetaReader.GetTableDetails(tempTable, "dbo");
				tableDetails.Add(tempTable);
			}
			if (!CheckIntegrity())
			{
				MessageBox.Show("设置不正确,请确认!");
				return;
			}
			SaveSettings();
			T4Parameters.Default.Tables = tableDetails;
			T4Parameters.Default.IsGenerateSingle = true;
			var taskEntity = Task.Factory.StartNew(
					() => GenerateFile(new ThreadStateObject() { TemplateName = "Entity.tt", LblTip = lblTip })
				);
			var taskMapping = Task.Factory.StartNew(
					() => GenerateFile(new ThreadStateObject() { TemplateName = "EntityMapping.tt", LblTip = lblTip })
				);
			var taskBiz = Task.Factory.StartNew(
					() => GenerateFile(new ThreadStateObject() { TemplateName = "Biz.tt", LblTip = lblTip })
				);
			var taskDal = Task.Factory.StartNew(
					() => GenerateFile(new ThreadStateObject() { TemplateName = "Dal.tt", LblTip = lblTip })
				);

			var taskSyn = Task.Factory.StartNew(
					() => MonitorTask(lblTip, btnGenerate)
				);
		}

		private void btnGenerateSelectTable_Click(object sender, EventArgs e)
		{
			var selectTableNames = RetriveGridViewSelect(gvTables, "name", "ColTableCheck");
			if (selectTableNames.Count == 0)
			{
				lblGenerateTable.Text = "请选择表.";
				return;
			}
			btnGenerateSelectTable.Enabled = false;
			CommonGenerateTable(selectTableNames, lblGenerateTable, btnGenerateSelectTable);
		}

		private void GenerateFile(ThreadStateObject threadStateObject)
		{
			try
			{
				SetLabelText(string.Format("正在生成 {0} ", threadStateObject.TemplateName.ToString()), threadStateObject.LblTip);
				string templateFolder = string.Format(@"{0}\Templates\", Environment.CurrentDirectory);
				TextTemplatingSession session = new TextTemplatingSession();
				session.Add("t4Parameter", T4Parameters.Default);

				CustomCmdLineHost host = new CustomCmdLineHost();
				host.TemplateFileValue = threadStateObject.TemplateName.ToString();
				host.Session = session;
				Engine engine = new Engine();
				//Read the text template.
				string input = File.ReadAllText(templateFolder + threadStateObject.TemplateName);
				input = input.Replace(@"$(ProjectDir)\", "");
				//Transform the text template.
				string output = engine.ProcessTemplate(input, host);
				foreach (CompilerError error in host.Errors)
				{
					Console.WriteLine(error.ToString());
				}
			}
			catch
			{

			}
			lock (synObj)
			{
				taskFinishedCount = taskFinishedCount + 1;
			}
		}

		private void MonitorTask(Label lblTip, Button btnGenerate)
		{
			while (true)
			{
				if (taskFinishedCount == 4)
				{
					T4Parameters.Default.Tables = null;
					MethodInvoker methodInvoker = new MethodInvoker(
						() => btnGenerate.Enabled = true
						);
					Invoke(methodInvoker);
					taskFinishedCount = 0;
					SetLabelText("生成结束", lblTip);
					return;
				}
				Thread.Sleep(1000);
			}
		}

		public void SetLabelText(string text, Label label)
		{
			DelegateSetLabelText method = new DelegateSetLabelText((x, l) => l.Text = x);
			this.Invoke(method, text, label);
		}

		private void cbTableSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxSelectAll(gvTables, cbTableSelectAll, "ColTableCheck");
		}

		private void cbTableReverse_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxReverse(gvTables, "ColTableCheck");
		}

		private void gvTables_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex == -1)
				return;
			if (e.ColumnIndex == 1)
			{
				contextCell = gvTables.Rows[e.RowIndex].Cells[e.ColumnIndex];
				contextCell.ContextMenuStrip = contextMenuStrip;
			}

			gvTables.Rows[e.RowIndex].Selected = true;

			if (gvTables.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && !string.IsNullOrEmpty(gvTables.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
				Clipboard.SetText(gvTables.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

			DataGridViewTextBoxCell colIdentity = gvTables.Rows[e.RowIndex].Cells["name"] as DataGridViewTextBoxCell;
			var tempTable = tables.First(x => x.Name == colIdentity.Value);
			tempTable.Columns = tableMetaReader.GetTableDetails(tempTable, "dbo");
			gvTableDetail.DataSource = tempTable.Columns;
		}


		private void gvTableDetail_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex <= 0 || e.ColumnIndex > 1 || e.ColumnIndex < 0)
				return;
			if (gvTableDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || string.IsNullOrEmpty(gvTableDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
				return;
			Clipboard.SetText(gvTableDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
		}

		#endregion

		#region Tab Views

		public void InitialGvViews(IList<Table> tables)
		{
			CommonInitialGridView(tables, gvViews, EnumTableType.View);
		}

		private void FilterViews()
		{
			if (string.IsNullOrEmpty(txtViewName.Text.Trim()))
			{
				gvViews.DataSource = tables.Where(x => x.TableType == EnumTableType.View).ToList();
				return;
			}
			gvViews.DataSource = tables.Where(x => (x.Name.ToLower().Contains(txtViewName.Text.Trim().ToLower()) || x.Remark.Contains(txtViewName.Text.Trim())) && x.TableType == EnumTableType.View).ToList();
		}

		private void btnSearchView_Click(object sender, EventArgs e)
		{
			FilterViews();
		}

		private void txtViewName_TextChanged(object sender, EventArgs e)
		{
			FilterViews();
		}

		private void btnGenerateView_Click(object sender, EventArgs e)
		{
			var selectViewNames = RetriveGridViewSelect(gvViews, "name", "ColViewCheck");
			if (selectViewNames.Count == 0)
			{
				lblGenerateView.Text = "请选择视图.";
				return;
			}
			btnGenerateView.Enabled = false;
			CommonGenerateTable(selectViewNames, lblGenerateView, btnGenerateView);
		}
		private void cbViewSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxSelectAll(gvViews, cbViewSelectAll, "ColViewCheck");
		}

		private void cbViewReverse_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxReverse(gvViews, "ColViewCheck");
		}

		private void gvViews_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex == -1)
				return;
			if (e.ColumnIndex == 1)
			{
				contextCell = gvViews.Rows[e.RowIndex].Cells[e.ColumnIndex];
				contextCell.ContextMenuStrip = contextMenuStrip;
			}
			gvViews.Rows[e.RowIndex].Selected = true;

			if (gvViews.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && !string.IsNullOrEmpty(gvViews.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
				Clipboard.SetText(gvViews.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

			DataGridViewTextBoxCell colIdentity = gvViews.Rows[e.RowIndex].Cells["name"] as DataGridViewTextBoxCell;
			var tempTable = tables.First(x => x.Name == colIdentity.Value);
			tempTable.Columns = tableMetaReader.GetTableDetails(tempTable, "dbo");
			gvViewDetail.DataSource = tempTable.Columns;
		}

		private void gvViewDetail_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex > 1 || e.ColumnIndex < 0)
				return;
			if (gvViewDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || string.IsNullOrEmpty(gvViewDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
				return;
			Clipboard.SetText(gvViewDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
		}

		#endregion

		#region Tab Procedure

		public StoreProcedure StoreProcedure { get; set; }
		public SqlParameterCollection ParameterCollection { get; set; }
		public bool IsCanGenerateFile { get; set; }

		public void InitialGvProcedures()
		{
			List<StoreProcedure> procedures = procMetaReader.StoreProcedures;
			if (gvProcedures.ColumnCount == 1)
			{
				DataGridViewTextBoxColumn ColTemplateName = new DataGridViewTextBoxColumn();
				ColTemplateName.DataPropertyName = "name";
				ColTemplateName.Name = "name";
				ColTemplateName.HeaderText = "Name";
				ColTemplateName.ReadOnly = true;
				ColTemplateName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				gvProcedures.Columns.Add(ColTemplateName);
				gvProcedures.AutoGenerateColumns = false;
			}
			gvProcedures.DataSource = procedures;
		}

		private void InitialGvProcedureParameters()
		{
			DataGridViewTextBoxColumn ColTemplateName = new DataGridViewTextBoxColumn();
			ColTemplateName.DataPropertyName = "CName";
			ColTemplateName.Name = "CName";
			ColTemplateName.HeaderText = "Name";
			ColTemplateName.ReadOnly = true;
			ColTemplateName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			DataGridViewCheckBoxColumn ColNullable = new DataGridViewCheckBoxColumn();
			ColNullable.DataPropertyName = "IsNullable";
			ColNullable.HeaderText = "IsNullable";
			ColNullable.Name = "IsNullable";
			ColNullable.Width = 100;
			ColNullable.ReadOnly = true;
			ColNullable.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			DataGridViewTextBoxColumn ColType = new DataGridViewTextBoxColumn();
			ColType.DataPropertyName = "CType";
			ColType.Name = "CType";
			ColType.HeaderText = "Type";
			ColType.ReadOnly = true;
			ColType.Width = 150;
			ColType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			gvParameters.AutoGenerateColumns = false;
			if (gvParameters.ColumnCount == 1)
			{
				gvParameters.Columns.Insert(0, ColTemplateName);
				gvParameters.Columns.Insert(1, ColNullable);
				gvParameters.Columns.Insert(2, ColType);
			}

			gvParameters.DataSource = StoreProcedure.Parameters;
		}

		private void gvProcedures_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
				return;
			if (gvProcedures.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && !string.IsNullOrEmpty(gvProcedures.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
				Clipboard.SetText(gvProcedures.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

			var procedureName = gvProcedures.Rows[e.RowIndex].Cells["Name"] as DataGridViewTextBoxCell;
			StoreProcedure = procMetaReader.StoreProcedures.First(x => x.Name == procedureName.Value.ToString());
			ParameterCollection = procMetaReader.GetParameterCollectionByProcedure(procedureName.Value.ToString());
			IsCanGenerateFile = CheckIntegrity();
			if (IsCanGenerateFile)
				SaveSettings();
			procMetaReader.FillParameter(StoreProcedure);
			InitialGvProcedureParameters();
		}

		private void btnExecuteProcedure_Click(object sender, EventArgs e)
		{
			SqlConnection connection = new SqlConnection(txtCon.Text);
			SqlCommand innerCommand = new SqlCommand() { Connection = connection };
			SqlDataAdapter adapter = new SqlDataAdapter(innerCommand);
			innerCommand.CommandType = CommandType.StoredProcedure;
			innerCommand.CommandText = StoreProcedure.Name;
			var parameters = RetriveCommandParametersFromUI();
			if (parameters != null)
				innerCommand.Parameters.AddRange(parameters);
			else
			{
				innerCommand.Parameters.AddRange(procMetaReader.RetriveParametersWithDefaultValue(StoreProcedure.Name));
			}
			DataSet ds = new DataSet();
			try
			{
				adapter.Fill(ds);
				gvResult.DataSource = ds.Tables[0];
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		private SqlParameter[] RetriveCommandParametersFromUI()
		{
			bool isAllNull = true;
			List<SqlParameter> parameters = new List<SqlParameter>();
			for (int i = 0; i < gvParameters.RowCount; i++)
			{
				var parameterName = gvParameters.Rows[i].Cells["CName"].Value.ToString();
				var parameterValue = gvParameters.Rows[i].Cells["ColParameterValue"].Value;
				if (parameterValue == null)
					continue;
				isAllNull = false;
				SqlParameter newParameter = new SqlParameter(parameterName, parameterValue);
				parameters.Add(newParameter);
			}
			if (isAllNull)
				return null;
			return parameters.ToArray();
		}

		private void gvParameters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 0)
				return;
		}

		private void btnGenerateProcedure_Click(object sender, EventArgs e)
		{
			btnGenerateProcedure.Enabled = false;
			Dictionary<string, List<SpColumn>> storeProcedureResultMeta = null;
			Dictionary<string, List<SpColumn>> storeProcedureParameterMeta = null;
			try
			{
				storeProcedureParameterMeta = procMetaReader.RetriveStoreProcedureParameterMeta(new string[] { StoreProcedure.Name });
				Dictionary<string, SqlParameter[]> storeProcedureParameters = null;
				var parameters = RetriveCommandParametersFromUI();
				if (parameters != null)
				{
					if (storeProcedureParameters == null)
						storeProcedureParameters = new Dictionary<string, SqlParameter[]>();
					storeProcedureParameters.Add(StoreProcedure.Name, parameters);
				}
				Dictionary<string, string> failedProcedures = new Dictionary<string, string>();
				storeProcedureResultMeta = procMetaReader.RetriveStoreProcedureResultMeta(failedProcedures, new string[] { StoreProcedure.Name }, storeProcedureParameters);

				if (failedProcedures.Count != 0)
				{
					MessageBox.Show(failedProcedures.First().Value);
					btnGenerateProcedure.Enabled = true;
					return;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				btnGenerateProcedure.Enabled = true;
				return;
			}
			T4Parameters.Default.StoreProcedureParametersMeta = storeProcedureParameterMeta;
			T4Parameters.Default.StoreProcedureResultMeta = storeProcedureResultMeta;

			var taskEntity = Task.Factory.StartNew(
					() => GenerateFile(new ThreadStateObject() { TemplateName = "SPEntity.tt", LblTip = lblGenerateProcedure })
				);

			var taskEntityMapping = Task.Factory.StartNew(
					() => GenerateFile(new ThreadStateObject() { TemplateName = "SPEntityMapping.tt", LblTip = lblGenerateProcedure })
				);

			var taskDal = Task.Factory.StartNew(
					() => GenerateFile(new ThreadStateObject() { TemplateName = "SPDal.tt", LblTip = lblGenerateProcedure })
				);
			var taskBiz = Task.Factory.StartNew(
					() => GenerateFile(new ThreadStateObject() { TemplateName = "SPBiz.tt", LblTip = lblGenerateProcedure })
				);
			var taskSyn = Task.Factory.StartNew(
					() => MonitorProcedureTask()
				);
		}

		private void MonitorProcedureTask()
		{
			while (true)
			{
				if (taskFinishedCount == 4)
				{
					T4Parameters.Default.StoreProcedureParametersMeta = null;
					T4Parameters.Default.StoreProcedureResultMeta = null;
					MethodInvoker methodInvoker = new MethodInvoker(
						() => btnGenerateProcedure.Enabled = true
						);
					Invoke(methodInvoker);
					taskFinishedCount = 0;
					SetLabelText("生成结束", lblGenerateProcedure);
					return;
				}
				Thread.Sleep(1000);
			}
		}

		private void txtProcedureName_TextChanged(object sender, EventArgs e)
		{
			gvProcedures.DataSource = procMetaReader.StoreProcedures.Where(x => x.Name.ToLower().Contains(txtProcedureName.Text.Trim().ToLower())).ToList();
		}

		#endregion

		#region Progress bar

		//线程开始的时候调用的委托
		private delegate void MaxValueDelegate(WorkEventArgs e);
		//线程执行中调用的委托
		private delegate void NowValueDelegate(WorkEventArgs e);
		//线程执行结束调用的委托
		private delegate void EndValueDelegate(WorkEventArgs e);
		//获得Progress bar value
		private delegate int GetValueDelegate(EventArgs e);
		//线程初始化设置
		private delegate void InitialDelegate(WorkEventArgs e);
		//线程设置log
		private delegate void SetLogDelegate(WorkEventArgs e);

		void OnShowWorkInfoEventHandler(object sender, WorkEventArgs e)
		{
			InitialDelegate initial = new InitialDelegate(Initialize);
			this.Invoke(initial, e);
		}

		void OnSetLogHandler(object sender, WorkEventArgs e)
		{
			SetLogDelegate setLog = new SetLogDelegate(SetLog);
			this.Invoke(setLog, e);
		}

		/// <summary>
		/// 线程开始事件,设置进度条最大值,需要一个委托来替我完成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnStartWorkEventHandler(object sender, WorkEventArgs e)
		{
			MaxValueDelegate max = new MaxValueDelegate(SetMaxValue);
			this.Invoke(max, e);
		}

		/// <summary>
		/// 线程执行中的事件,设置进度条当前进度,需要一个委托来替我完成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnDoWorkEventHandler(object sender, WorkEventArgs e)
		{
			NowValueDelegate now = new NowValueDelegate(SetNowValue);
			this.Invoke(now, e);
		}

		int OnGetValueEventHandler(object sender, EventArgs e)
		{
			GetValueDelegate getvalue = new GetValueDelegate(GetValue);
			return Convert.ToInt32(this.Invoke(getvalue, e));
		}

		/// <summary>
		/// 线程完成事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnEndWorkEventHandler(object sender, WorkEventArgs e)
		{
			EndValueDelegate end = new EndValueDelegate(SetEndValue);
			this.Invoke(end, e);
		}

		/// <summary>
		/// 被委托调用,专门设置进度条最大值的
		/// </summary>
		/// <param name="maxValue"></param>
		private void SetMaxValue(WorkEventArgs e)
		{
			this.progressBar.Maximum = e.Position;
			this.progressBar.Value = 0;
			this.lblInfo.Text = e.Info;
		}

		private int GetValue(EventArgs e)
		{
			return this.progressBar.Value;
		}

		private void Initialize(WorkEventArgs e)
		{
			this.lblInfo.Text = e.Info;
			//this.progressBar.Value = 0;
		}

		private void SetLog(WorkEventArgs e)
		{
			txtLog.Text = txtLog.Text + e.ErrorInfo + "\n";
		}

		/// <summary>
		/// 被委托调用,专门设置进度条当前值的
		/// </summary>
		/// <param name="nowValue"></param>
		private void SetNowValue(WorkEventArgs e)
		{
			this.progressBar.Value = e.Position;
			lblInfo.Text = e.Info;
			SetGridRecordBackground(e.TemplateName);
		}

		/// <summary>
		/// 被委托调用,专门设置进度条结束后的处理
		/// </summary>
		/// <param name="nowValue"></param>
		private void SetEndValue(WorkEventArgs e)
		{
			this.progressBar.Value = e.Position;
			lblInfo.Text = e.Info;
			BtnGenerateAll.Enabled = true;
			RecoverBackground();
		}

		class WorkEventArgs : EventArgs
		{
			//主要是用来往外传递信息的
			public WorkStage Stage;
			public string Info = "";
			public int Position = 0;
			public string TemplateName { get; set; }
			public string ErrorInfo { get; set; }
			public WorkEventArgs(WorkStage Stage, string Info, int Position)
			{
				this.Stage = Stage;
				this.Info = Info;
				this.Position = Position;
			}

			public WorkEventArgs(WorkStage Stage, string Info)
			{
				this.Stage = Stage;
				this.Info = Info;
			}


		}
		public enum WorkStage
		{
			InitializeWork,
			BeginWork,  //准备工作
			DoWork,     //正在工作  
			EndWork,    //工作结束
		}
		#endregion

		private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			switch (e.ClickedItem.Name)
			{
				case "entity":
					Clipboard.SetText(contextCell.Value.ToString().Replace("_", ""));
					break;
				case "dal":
					Clipboard.SetText(string.Format("Dal{0}", contextCell.Value.ToString().Replace("_", "")));
					break;
				case "biz":
					Clipboard.SetText(string.Format("Biz{0}", contextCell.Value.ToString().Replace("_", "")));
					break;
				case "defEntityProperty":
					Clipboard.SetText(string.Format("public {0} {0}Entity {{ get; set; }}", contextCell.Value.ToString().Replace("_", "")));
					break;
				case "defEntityVariable":
					Clipboard.SetText(string.Format("{0} {1}Entity;", contextCell.Value.ToString().Replace("_", ""), contextCell.Value.ToString().Substring(0, 1).ToLower() + contextCell.Value.ToString().Substring(1, contextCell.Value.ToString().Length - 1).Replace("_", "")));
					break;
				case "defBizVariable":
					Clipboard.SetText(string.Format("private {0} {1};", string.Format("Biz{0}", contextCell.Value.ToString().Replace("_", "")), string.Format("biz{0}", contextCell.Value.ToString().Replace("_", ""))));
					break;
				case "defDalVariable":
					Clipboard.SetText(string.Format("private {0} {1};", string.Format("Dal{0}", contextCell.Value.ToString().Replace("_", "")), string.Format("dal{0}", contextCell.Value.ToString().Replace("_", ""))));
					break;
				case "defIListVariable":
					Clipboard.SetText(string.Format("IList<{0}> {1}Collection;", string.Format("{0}", contextCell.Value.ToString().Replace("_", "")), string.Format("{0}", contextCell.Value.ToString().Replace("_", ""))));
					break;
				case "defIListProperty":
					Clipboard.SetText(string.Format("public IList<{0}> {0}Collection {{ get; set; }}", contextCell.Value.ToString().Replace("_", "")));
					break;
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData.ToString() == "F5" && tabCommand.Visible)
			{
				var conn = new SqlConnection(txtCon.Text);
				conn.Open();
				using (conn)
				{
					using (var command = new SqlCommand())
					{
						try
						{
							command.Connection = conn;
							command.CommandText = string.IsNullOrEmpty(richTbCommand.SelectedText) ? richTbCommand.Text : richTbCommand.SelectedText;
							SqlDataAdapter adapter = new SqlDataAdapter(command);
							DataTable dataTable = new DataTable();
							adapter.Fill(dataTable);
							gvCommandResult.DataSource = dataTable;

						}
						catch (Exception ex)
						{

						}
					}
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void btnTestTFS_Click(object sender, EventArgs e)
		{
			Microsoft.TeamFoundation.Client.TfsTeamProjectCollection tpc = null;
			Microsoft.TeamFoundation.VersionControl.Client.VersionControlServer sc = null;
			Microsoft.TeamFoundation.VersionControl.Client.Workspace lstWS = null;
			try
			{
				System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential(txtName.Text.Trim(), txtPassword.Text.Trim());
				System.Net.ICredentials credential = (System.Net.ICredentials)networkCredential;
				tpc = new Microsoft.TeamFoundation.Client.TfsTeamProjectCollection(new Uri(txtUrl.Text.Trim()), credential);
				sc = (Microsoft.TeamFoundation.VersionControl.Client.VersionControlServer)tpc.GetService(typeof(Microsoft.TeamFoundation.VersionControl.Client.VersionControlServer));
				lstWS = sc.QueryWorkspaces(null, txtName.Text.Trim().Substring(0,txtName.Text.Trim().IndexOf("@")), null).FirstOrDefault(x => x.IsLocal);
				if (lstWS != null)
					MessageBox.Show("TFS Successful connection!");
			}
			catch (Exception ex)
			{
				MessageBox.Show("TFS Connection Failed!");
			}
		}

		private void chkTFS_CheckedChanged(object sender, EventArgs e)
		{
			groupTFS.Visible = chkTFS.Checked;
			groupBox4.Height = groupTFS.Visible ? 168 : 168 - 85;
		}

	}
}
