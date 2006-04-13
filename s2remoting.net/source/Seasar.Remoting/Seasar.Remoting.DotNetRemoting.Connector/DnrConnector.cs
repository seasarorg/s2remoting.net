using System;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using Seasar.Remoting.DotNetRemoting.Adaptor;
using Seasar.Remoting.Common.Connector.Impl;

namespace Seasar.Remoting.DotNetRemoting.Connector
{
	public class DnrConnector : URLBasedConnector
	{
		private IDnrAdaptor adaptorStub;
		
		private IChannel channel;

		private bool isIISHost = false;

		public IChannel Channel
		{
			set { this.channel = value; }
		}
		
		public override object Invoke(string componentName, MethodInfo method, object[] args)
		{
			lock (this)
			{
				if (this.adaptorStub == null)
				{
					if (isIISHost)
						this.LookupForIISHost();
					else
						this.Lookup();
				}
			}
			return this.adaptorStub.Invoke(componentName, method.Name, args);
		}
		
		/// <summary> diconファイルで設定されたbaseURLプロパティを使用して、
		/// レジストリからDNRAdaptorのスタブクラスを取得します.
		/// </summary>
		public virtual void Lookup()
		{
			if (this.channel == null)
				throw new InvalidOperationException("Channelを設定してください");

			Uri targetURL = new Uri(base.BaseURL, DnrAdaptor.EXPORT_NAME);
			
			ChannelServices.RegisterChannel(this.channel);
			
			this.adaptorStub = (DnrAdaptor) Activator
				.GetObject(typeof(DnrAdaptor), targetURL.ToString());
		}

		public virtual void LookupForIISHost()
		{
			isIISHost = true;

			Uri targetURL = new Uri(base.BaseURL, DnrAdaptor.EXPORT_NAME);
			
			this.adaptorStub = (DnrAdaptor) Activator
				.GetObject(typeof(DnrAdaptor), targetURL.ToString());
		}
	}
}