using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SiteMesh.DecoratorControls;
using ThoughtWorks.CruiseControl.WebDashboard.Dashboard;
using ThoughtWorks.CruiseControl.WebDashboard.IO;
using ThoughtWorks.CruiseControl.WebDashboard.Plugins;
using ThoughtWorks.CruiseControl.WebDashboard.Plugins.SiteTemplatePlugin;

namespace ThoughtWorks.CruiseControl.WebDashboard
{
	public class Decorator : Page
	{
		protected DataList menu;
		protected HtmlGenericControl buildStats;
		protected HtmlGenericControl ProjectPluginLinks;
		protected HtmlAnchor latestLog;
		protected HtmlAnchor nextLog;
		protected HtmlAnchor previousLog;
		protected Title Title1;
		protected Body Body1;
		protected GetProperty prop1;
		protected HtmlTableCell Td2;
		protected Title Title3;
		protected HtmlTableCell contentCell;
		protected System.Web.UI.WebControls.Panel ProjectPanel1;
		protected System.Web.UI.WebControls.DataList BuildPluginsList;
		protected System.Web.UI.WebControls.DataList ServerPluginsList;
		protected System.Web.UI.WebControls.Panel ProjectPanel2;

		private void Page_Load(object sender, EventArgs e)
		{
			SiteTemplateResults results = new PluginPageRendererFactory(new DashboardComponentFactory(Request, Context, this)).SiteTemplate.Do();

			if (results.ProjectMode)
			{
				buildStats.InnerHtml = results.BuildStatsHtml;
				buildStats.Attributes["class"] = results.BuildStatsClass;

				ServerPluginsList.DataSource = results.ServerPluginsList;
				ServerPluginsList.DataBind();

				menu.DataSource = results.BuildLinkList;
				menu.DataBind();

				BuildPluginsList.DataSource = results.BuildPluginsList;
				BuildPluginsList.DataBind();

				latestLog.HRef = results.LatestLogLink;
				previousLog.HRef = results.PreviousLogLink;
				nextLog.HRef = results.NextLogLink;
			}

			ProjectPanel1.Visible = results.ProjectMode;
			ProjectPanel2.Visible = results.ProjectMode;
		}

		// This binds the HRef control that is each data item into the Controls container of the list
		private void DataList_BindItem(object sender, DataListItemEventArgs e)
		{
			if (e.Item.DataItem != null)
				e.Item.Controls.Add((Control)e.Item.DataItem);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.menu.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.DataList_BindItem);
			this.BuildPluginsList.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.DataList_BindItem);
			this.ServerPluginsList.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.DataList_BindItem);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
