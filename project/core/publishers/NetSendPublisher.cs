using System;
using System.Diagnostics;
using Exortech.NetReflector;
using tw.ccnet.remote;
using tw.ccnet.core.util;

namespace tw.ccnet.core.publishers
{
	[ReflectorType("netsend")]
	public class NetSendPublisher : PublisherBase
	{
		[ReflectorProperty("names")]
		public string Names;

		[ReflectorProperty("failedMessage", Required=false)]
		public string FailedMessage = "BUILD FAILED!";

		[ReflectorProperty("fixedMessage", Required=false)]
		public string FixedMessage = "BUILD FIXED!";

		public override void Publish(object source, IntegrationResult result)
		{
			if (ShouldSendMessage(result))
			{
				string[] names = Names.Split(',');
				foreach (string name in names)
				{
					Send(name, GetMessage(result));
				}
			}
		}

		internal bool ShouldSendMessage(IntegrationResult result)
		{
			return result.Failed || result.Fixed;
		}

		internal string GetMessage(IntegrationResult result)
		{
			if (result.Failed)
			{
				string comment = result.Modifications.Length == 0 ? "Unknown" : result.Modifications[0].Comment;
				string committer = result.Modifications.Length == 0 ? "Unknown" : result.Modifications[0].UserName;
				return String.Format("{2}\nLast comment: {0}\nLast committer: {1}", comment, committer, FailedMessage);
			}
			else
			{
				return FixedMessage;
			}
		}

		internal int Send(string name, string message)
		{
			Process process = ProcessUtil.CreateProcess("net", String.Format("send \"{0}\" \"{1}\"", name, message));
			process.StartInfo.UseShellExecute = false;	
			return ExecuteProcess(process);
		}

		protected virtual int ExecuteProcess(Process process)
		{
			try
			{
				process.Start();
				process.WaitForExit();
				return process.ExitCode;
			}
			finally
			{
				process.Close();
			}
		}
	}
}
