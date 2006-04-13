using Seasar.Remoting.Common.Connector.Impl;

namespace Seasar.Remoting.Tests.Common.Connector.Impl
{
	public class TestConnector : URLBasedConnector
	{
		public override System.Object Invoke(System.String name, System.Reflection.MethodInfo method, System.Object[] args)
		{
			return null;
		}
	}
}