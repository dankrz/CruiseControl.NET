using System;

namespace ThoughtWorks.CruiseControl.WebDashboard.Dashboard
{
	public class DefaultLinkFactory : ILinkFactory
	{
		private readonly IUrlBuilder urlBuilder;
		private readonly IBuildNameFormatter buildNameFormatter;

		public DefaultLinkFactory(IUrlBuilder urlBuilder, IBuildNameFormatter buildNameFormatter)
		{
			this.urlBuilder = urlBuilder;
			this.buildNameFormatter = buildNameFormatter;
		}

		public IAbsoluteLink CreateBuildLink(IBuildSpecifier buildSpecifier, string text, IActionSpecifier actionSpecifier)
		{
			return new BuildLink(urlBuilder, buildSpecifier, text, actionSpecifier);
		}

		public IAbsoluteLink CreateBuildLink(IBuildSpecifier buildSpecifier, IActionSpecifier actionSpecifier)
		{
			return new BuildLink(urlBuilder, buildSpecifier, buildNameFormatter.GetPrettyBuildName(buildSpecifier), actionSpecifier);
		}

		public IAbsoluteLink CreateBuildLinkWithFileName(IBuildSpecifier buildSpecifier, IActionSpecifier actionSpecifier, string fileName)
		{
			return new BuildLinkWithFileName(urlBuilder, buildSpecifier, buildNameFormatter.GetPrettyBuildName(buildSpecifier), actionSpecifier, fileName);
		}

		public IAbsoluteLink CreateProjectLink(IProjectSpecifier projectSpecifier, string text, IActionSpecifier actionSpecifier)
		{
			return new ProjectLink(urlBuilder, projectSpecifier, text, actionSpecifier);
		}

		public IAbsoluteLink CreateProjectLink(IProjectSpecifier projectSpecifier, IActionSpecifier actionSpecifier)
		{
			return new ProjectLink(urlBuilder, projectSpecifier, projectSpecifier.ProjectName, actionSpecifier);
		}

		public IAbsoluteLink CreateServerLink(IServerSpecifier serverSpecifier, string text, IActionSpecifier actionSpecifier)
		{
			return new ServerLink(urlBuilder, serverSpecifier, text, actionSpecifier);
		}

		public IAbsoluteLink CreateServerLink(IServerSpecifier serverSpecifier, IActionSpecifier actionSpecifier)
		{
			return new ServerLink(urlBuilder, serverSpecifier, serverSpecifier.ServerName, actionSpecifier);
		}

		public IAbsoluteLink CreateFarmLink(string text, IActionSpecifier actionSpecifier)
		{
			return new FarmLink(urlBuilder, text, actionSpecifier);
		}

		public IAbsoluteLink CreateStyledBuildLink(IBuildSpecifier specifier, IActionSpecifier actionSpecifier)
		{
			IAbsoluteLink link = CreateBuildLink(specifier, buildNameFormatter.GetPrettyBuildName(specifier), actionSpecifier);
			link.LinkClass = buildNameFormatter.GetCssClassForBuildLink(specifier);
			return link;
		}
	}
}
