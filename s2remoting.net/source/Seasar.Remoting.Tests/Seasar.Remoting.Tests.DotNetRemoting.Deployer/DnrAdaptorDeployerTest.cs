using System;
using System.Runtime.Remoting;
using MbUnit.Framework;
using Seasar.Remoting.DotNetRemoting.Adaptor;
using Seasar.Remoting.DotNetRemoting.Deployer;
using Seasar.Extension.Unit;

namespace Seasar.Remoting.Tests.DotNetRemoting.Deployer
{
	[TestFixture]
	public class DnrAdaptorDeployerTest : S2TestCase
	{
		private DnrAdaptorDeployer deployer_ = null;

		public void  SetUpDeploy()
		{
			Include("test.dicon");
		}
		
		[Test, S2]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestDeploy()
		{
			deployer_.Adaptor = null;
			deployer_.Deploy();
		}

		public void  SetUpDeploy2()
		{
			Include("test2.dicon");
		}
		
		[Test, S2]
		public void TestDeploy2()
		{
			deployer_.Deploy();
			
			WellKnownServiceTypeEntry[] serviceEntrys = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
			WellKnownServiceTypeEntry entry = serviceEntrys[0] as WellKnownServiceTypeEntry;
			Assert.AreEqual(entry.ObjectType, typeof(DnrAdaptor));
			Assert.AreEqual(entry.ObjectUri, DnrAdaptor.EXPORT_NAME);
			Assert.AreEqual(entry.Mode, WellKnownObjectMode.SingleCall);
		}
	}
}