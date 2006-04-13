using System;
using Seasar.Extension.Component;

namespace Seasar.Remoting.DotNetRemoting.Adaptor
{
	/// <summary>
	/// IDnrAdaptor ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public interface IDnrAdaptor
	{
		IComponentInvoker Invoker { set; }
		object Invoke(string componentName, string methodName, object[] args);
	}
}
