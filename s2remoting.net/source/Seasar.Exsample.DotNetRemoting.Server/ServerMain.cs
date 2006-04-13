using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Util;
using Seasar.Framework.Container.Factory;

namespace Seasar.Exsample.DotNetRemoting.Server
{
	class ServerMain
	{
		[STAThread]
		static void Main(string[] args)
		{
			try 
			{
				FileInfo info = new FileInfo(SystemInfo.AssemblyShortName(
					Assembly.GetExecutingAssembly()) + ".exe.config");
				XmlConfigurator.Configure(LogManager.GetRepository(), info);

				SingletonS2ContainerFactory.Init();
				Console.WriteLine("サーバーを起動しました、終了するには何かキーを押して下さい");
				Console.ReadLine();
			} 
			catch(Exception e) 
			{
				Console.WriteLine(e.Message);
			} 
			finally 
			{
				SingletonS2ContainerFactory.Destroy();
			}
		}
	}
}
