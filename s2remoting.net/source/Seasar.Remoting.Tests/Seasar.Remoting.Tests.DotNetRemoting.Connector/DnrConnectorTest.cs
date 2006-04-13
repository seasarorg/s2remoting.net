using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using MbUnit.Framework;
using Seasar.Remoting.DotNetRemoting.Connector;
using Seasar.Extension.Unit;

namespace Seasar.Remoting.Tests.DotNetRemoting.Connector
{
	[TestFixture]
	public class DnrConnectorTest : S2TestCase
	{
		private static System.String PATH = "test.dicon";
		
		private DnrConnector connector_ = null;

		public void SetUpLookup()
		{
			Include(PATH);
		}

		[Test, S2]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestLookup()
		{
			connector_.Lookup();
		}

//		public void SetUpLookup2()
//		{
//			Include(PATH);
//		}
//
//		[Test, S2]
//		[ExpectedException(typeof(RemotingException))]
//		public void TestLookup2()
//		{
//			connector_.Channel = new TcpChannel(8000);
//			connector_.Lookup();
//		}
	}
}