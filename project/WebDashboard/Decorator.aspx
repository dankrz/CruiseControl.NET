<%@ Register Namespace="SiteMesh.DecoratorControls" TagPrefix="decorator" Assembly="Sitemesh" %>
<%@ Page language="c#" Codebehind="Decorator.aspx.cs" AutoEventWireup="false" Inherits="ThoughtWorks.CruiseControl.WebDashboard.Decorator" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://localhost/NUnitAsp/dtd/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>
			<decorator:title runat="server" defaulttitle="CruiseControl.Net Build Results" ID="Title1" /></title>
		<meta name="vs_showGrid" content="True">
		<link type="text/css" rel="stylesheet" href="cruisecontrol.css">
	</HEAD>
	<body background="images/bg_blue_stripe.gif" topmargin="0" leftmargin="0" marginheight="0"
		marginwidth="0">
		<!-- head: logo, controls -->
		<table class="main-panel" border="0" align="center" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td valign="middle" align="left">
					<img src="images/shim.gif" width="6" border="0"> <a href="http://ccnet.thoughtworks.com">
						<img src="images/ccnet_logo.gif" border="0"></a>
				</td>
				<td valign="middle" align="right">
					<asp:Panel id="ProjectPanel1" runat="server"><A class="link" id="latestLog" href="." runat="server">
							latest</A> |&nbsp; <A class="link" id="nextLog" runat="server">next</A> |&nbsp; <A class="link" id="previousLog" runat="server">previous</A> <SPAN id="ProjectPluginLinks" runat="server" runAt="server">&nbsp;</SPAN>
					</asp:Panel>
				</td>
				<td><img src="images/shim.gif" width="6" border="0"></td>
			</tr>
		</table>
		<!-- body: main content panels -->
		<table border="0" align="center" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td width="196" valign="top">
					<!-- build results -->
					<asp:Panel id="ProjectPanel2" runat="server">
						<TABLE cellSpacing="0" cellPadding="0" width="196" align="center" border="0">
							<TR>
								<TD width="100%" bgColor="#333366"><IMG src="images/shim.gif" border="0"></TD>
								<TD><IMG src="images/corner_blue_ur.gif" border="0"></TD>
							</TR>
							<TR>
								<TD bgColor="#333366">
									<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<TR>
											<TD><IMG src="images/shim.gif" width="20"></TD>
											<TD noWrap><BR>
											</TD>
										<TR>
											<TD><IMG src="images/shim.gif" width="20"></TD>
											<TD noWrap>
												<asp:DataList id="ServerPluginsList" Runat="server"></asp:DataList></TD>
										</TR>
										<TR>
											<TD><IMG src="images/shim.gif" width="20"></TD>
											<TD noWrap><SPAN class="buildresults-header">BUILD 
                  RESULTS</SPAN>
												<BR>
												<SPAN class="buildresults-data" id="buildStats" runat="server" runAt="server"></SPAN></TD>
										</TR>
										<TR>
											<TD><IMG src="images/shim.gif" width="20"></TD>
											<TD noWrap>
												<asp:DataList id="menu" Runat="server"></asp:DataList></TD>
										</TR>
										<TR>
											<TD><IMG src="images/shim.gif" width="20"></TD>
											<TD noWrap>
												<asp:DataList id="BuildPluginsList" Runat="server"></asp:DataList></TD>
										</TR>
									</TABLE>
								</TD>
								<TD bgColor="#333366"><IMG src="images/shim.gif" border="0"></TD>
							</TR>
							<TR>
								<TD width="100%" bgColor="#333366"><IMG src="images/shim.gif" border="0"></TD>
								<TD><IMG src="images/corner_blue_lr.gif" border="0"></TD>
							</TR>
						</TABLE>
					</asp:Panel>
				</td>
				<td>
					<img src="images/shim.gif" width="12" border="0">
				</td>
				<td valign="top" width="100%">
					<!-- log details -->
					<table border="0" align="center" cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td><img src="images/corner_white_ul.gif" border="0"></td>
							<td bgcolor="#ffffff"><img src="images/shim.gif" border="0"></td>
						</tr>
						<tr>
							<td bgcolor="#ffffff"><img src="images/shim.gif" border="0"></td>
							<td id="contentCell" bgcolor="#ffffff" width="100%" valign="top" align="left" runat="server">
								<decorator:body id="Body1" runat="server"></decorator:body>
							</td>
						</tr>
						<tr>
							<td><img src="images/corner_white_ll.gif" border="0"></td>
							<td bgcolor="#ffffff"><img src="images/shim.gif" border="0"></td>
						</tr>
					</table>
					<!-- footer: twlogo -->
					<table width="100%">
						<tr>
							<td align="right">
								<a href="http://www.thoughtworks.com/" border="0"><img src="images/tw_dev_logo.gif" border="0"><img src="images/shim.gif" width="6" border="0">
								</a>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>
