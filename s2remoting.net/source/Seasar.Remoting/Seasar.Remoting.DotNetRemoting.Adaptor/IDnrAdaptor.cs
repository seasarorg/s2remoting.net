using System;
using Seasar.Extension.Component;

namespace Seasar.Remoting.DotNetRemoting.Adaptor
{
	/// <summary>
	/// IDnrAdaptor の概要の説明です。
	/// </summary>
	public interface IDnrAdaptor
	{
		IComponentInvoker Invoker { set; }
		object Invoke(string componentName, string methodName, object[] args);
	}
}
