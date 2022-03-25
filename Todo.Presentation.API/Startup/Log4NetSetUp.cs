using System.IO;
using System.Reflection;
using System.Xml;

namespace Todo.Presentation
{
	public static class Log4NetSetUp
	{
		private static readonly string log4netConfigFile = "log4net.config"; 

		public static void SetUpLog4Net()
		{
			var log4netConfig = new XmlDocument();
			log4netConfig.Load(File.OpenRead(log4netConfigFile));

			var repository = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

			log4net.Config.XmlConfigurator.Configure(repository, log4netConfig["log4net"]);
		}
	}
}
