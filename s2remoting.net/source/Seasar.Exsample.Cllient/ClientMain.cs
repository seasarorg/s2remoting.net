using System;
using System.IO;
using System.Reflection;
using System.Threading;
using log4net;
using log4net.Config;
using log4net.Util;
using Seasar.Exsample.Service;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Exceptions;
using Seasar.Framework.Util;
using System.Data;
using System.Diagnostics;

namespace Seasar.Exsample.Client
{
	class ClientMain
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Thread.Sleep(new TimeSpan(0,0,0,10,0));
			try 
			{
				FileInfo info = new FileInfo(SystemInfo.AssemblyShortName(
					Assembly.GetExecutingAssembly()) + ".exe.config");
				XmlConfigurator.Configure(LogManager.GetRepository(), info);

				SingletonS2ContainerFactory.Init();
				IS2Container container = SingletonS2ContainerFactory.Container;
				IHello hello = (IHello) container.GetComponent(typeof(IHello));
				Console.WriteLine(hello.Say());	

				Foo foo = (Foo) container.GetComponent(typeof(Foo));
				Console.WriteLine(foo.Bar());


                Stopwatch watch = new Stopwatch();
                Stopwatch transferWatch = new Stopwatch();
                Stopwatch loopWatch = new Stopwatch();

				for (int i = 1; i < 4 ; i++)
				{
                    watch.Reset();
                    watch.Start();
                    
					for(int j=1; j < 6; j++)
					{
                        
                        transferWatch.Reset();
                        transferWatch.Start();
                        DataSet ds = hello.GetDataSet();
                        //DataSet ds = hello.GetBigDataSet();
                        transferWatch.Stop();
                        Console.WriteLine("Transfer {0} Times 経過時間:{1}[ms]", new object[] { j, transferWatch.ElapsedMilliseconds });

                        loopWatch.Reset();
                        loopWatch.Start();
                        foreach (DataTable tbl in ds.Tables)
                        {
                            foreach (DataRow row in tbl.Rows)
                            {
                                string s = row["Text1"] as string;
                            }
                        }
                        loopWatch.Stop();
                        Console.WriteLine("Loop Call {0} Times 経過時間:{1}[ms]", new object[] { j, loopWatch.ElapsedMilliseconds });
					}
                    watch.Stop();
                    Console.WriteLine("GetDataSet Call {0} Times 経過時間:{1}[ms]", new object[] { i, watch.ElapsedMilliseconds });
				}
				try
				{
					hello.Bar();
				}
				catch (IllegalMethodRuntimeException e)
				{
					Console.WriteLine("例外のテスト " + e.MethodName);
					//Console.WriteLine(e.Message);
				}
				Console.WriteLine("終了するには何かキーを押して下さい");
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
