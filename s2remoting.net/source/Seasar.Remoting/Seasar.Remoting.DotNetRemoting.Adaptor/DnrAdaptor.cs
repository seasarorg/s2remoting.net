using System;
using System.Reflection;
using Seasar.Extension.Component;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Log;

namespace Seasar.Remoting.DotNetRemoting.Adaptor
{
	public class DnrAdaptor : MarshalByRefObject, IDnrAdaptor
	{
		private static readonly Logger logger = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		
		public const string EXPORT_NAME = "DNRAdaptor.rem";

		private IComponentInvoker invoker;

		public IComponentInvoker Invoker
		{
			set { this.invoker = value; }
		}
		
		public object Invoke(string componetName, string methodName, object[] args)
		{
			invoker = SingletonS2ContainerFactory.Container
				.GetComponent(typeof(IComponentInvoker)) as IComponentInvoker;
			try
			{
				return this.invoker.Invoke(componetName, methodName, args);	
			}
			catch (Exception e)
			{
				logger.Log(e);
				throw;
			}
		}
	}
}