using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.Runtime.Remoting;
using System.Web.UI.WebControls;

using ThoughtWorks.CruiseControl.Remote;

namespace ThoughtWorks.CruiseControl.WebDashboard
{
	public class Default : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label ExceptionTitleLabel;
		protected System.Web.UI.WebControls.DataGrid ExceptionGrid;
		protected System.Web.UI.WebControls.Label StatusLabel;
		protected System.Web.UI.WebControls.DataGrid StatusGrid;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			IList urls = (IList) ConfigurationSettings.GetConfig("projectURLs");
			ArrayList projectDetailsList = new ArrayList();
			ArrayList connectionExceptions = new ArrayList();
			Hashtable urlsForProjects = new Hashtable();

			foreach (string url in urls)
			{
				try
				{
					ICruiseManager remoteCC = (ICruiseManager) RemotingServices.Connect(typeof(ICruiseManager), url);
					foreach (ProjectStatus status in remoteCC.GetProjectStatus())
					{
						projectDetailsList.Add(new ProjectDetails(status, GenerateForceBuildURL(status)));
						urlsForProjects.Add(status.Name, url);
					}
				}
				catch (Exception f)
				{
					connectionExceptions.Add(new ConnectionException(url, f));
				}
			}

			if (this.Request.QueryString.Count > 0)
			{
				HandleQueryString(urlsForProjects);
			}

			if (projectDetailsList.Count > 0)
			{
				StatusGrid.DataSource = projectDetailsList;
				StatusGrid.DataBind();
				StatusGrid.Visible = true;
			}
			else
			{
				StatusGrid.Visible = false;
			}

			if (connectionExceptions.Count > 0)
			{
				ExceptionGrid.DataSource = connectionExceptions;
				ExceptionGrid.DataBind();
				ExceptionGrid.Visible = true;
				ExceptionTitleLabel.Visible = true;
			}
			else
			{
				ExceptionGrid.Visible = false;
				ExceptionTitleLabel.Visible = false;
			}
		}

		private string GenerateForceBuildURL(ProjectStatus status)
		{
			return "Default.aspx?forceBuildFor=" + status.Name;
		}

		private void HandleQueryString(Hashtable urlsForProjects)
		{
			string forceBuildProjectName = this.Request.QueryString["forceBuildFor"];
			if (forceBuildProjectName != null && forceBuildProjectName.Length > 0)
			{
				ForceBuild(forceBuildProjectName, urlsForProjects);
			}
			else
			{
				StatusLabel.Text = "Warning - unknown query string parameter(s) found";
				StatusLabel.Visible = true;
			}
		}

		private void ForceBuild(string projectName, Hashtable urlsForProjects)
		{
			try
			{
				ICruiseManager remoteCC = (ICruiseManager) RemotingServices.Connect(typeof(ICruiseManager), (string) urlsForProjects[projectName]);
				remoteCC.ForceBuild(projectName);
				StatusLabel.Text = "Build Successfully Forced for project [ " + projectName + " ]";
			}
			catch (Exception e)
			{
				StatusLabel.Text = "Build could not be forced for project [ " + projectName + " ], exception was: " + e.ToString();
			}
			StatusLabel.Visible = true;
		}

		private void StatusGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if((e.Item.ItemType == ListItemType.Item) ||
				(e.Item.ItemType == ListItemType.AlternatingItem))
			{
				ProjectStatus thisProjectsStatus = (ProjectStatus) e.Item.DataItem;
				TableCell buildStatusCell = (TableCell)e.Item.Controls[1];

				if (thisProjectsStatus.BuildStatus == IntegrationStatus.Success)
				{
					buildStatusCell.ForeColor = Color.Green;
				}
				else if (thisProjectsStatus.BuildStatus == IntegrationStatus.Unknown)
				{
					buildStatusCell.ForeColor = Color.Yellow;
				}
				else
				{
					buildStatusCell.ForeColor = Color.Red;
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.StatusGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.StatusGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}

	public struct ConnectionException
	{
		private string _url;
		private Exception _exception;

		public ConnectionException(string URL, Exception exception)
		{
			this._url = URL;
			this._exception = exception;
		}

		public string URL
		{
			get { return _url; }
		}

		public string Message
		{
			get { return _exception.Message; }
		}

		public Exception Exception
		{
			get { return _exception; }
		}
	}
}
