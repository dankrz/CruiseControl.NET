using ThoughtWorks.CruiseControl.Remote;

namespace ThoughtWorks.CruiseControl.Core
{
	public interface IIntegrationRunnerTarget : ITask
	{
		ISourceControl SourceControl { get; }
		void Prebuild(IIntegrationResult result);

		void PublishResults(IIntegrationResult result);

		// Would like to have this somewhere else really
        ProjectActivity Activity { set; get;}

        void CreateLabel(IIntegrationResult result);

	}
}
