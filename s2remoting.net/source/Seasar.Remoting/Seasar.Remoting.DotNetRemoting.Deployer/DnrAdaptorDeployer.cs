using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Seasar.Remoting.DotNetRemoting.Adaptor;
using Seasar.Framework.Log;
using Seasar.Remoting.Common.Deployer;

namespace Seasar.Remoting.DotNetRemoting.Deployer
{
	
	public class DnrAdaptorDeployer : IDeployer
	{
		private static readonly Logger logger = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		
		private IDnrAdaptor adaptor;
		
		private IChannel channel;
		
		private WellKnownObjectMode wellKnownObjectMode;

		public IDnrAdaptor Adaptor
		{
			set { this.adaptor = value; }
		}

		public IChannel Channel
		{
			set { this.channel = value; }
		}

		public WellKnownObjectMode WellKnownObjectMode
		{
			set { wellKnownObjectMode = value; }
		}

		public virtual void  Deploy()
		{  
			this.isSetAdaptor();
			try
			{
				ChannelServices.RegisterChannel(this.channel);
				ChannelDataStore channelDataStore = null;
				string[] messages = new string[3];
				if (logger.IsDebugEnabled)
				{
					messages[0] = this.channel.ChannelName;
					messages[1] = Convert.ToString(this.channel.ChannelPriority);

					if (this.channel is TcpChannel)
					{
						TcpChannel tcpChannel = this.channel as TcpChannel;
						channelDataStore = tcpChannel.ChannelData as ChannelDataStore;
						
					}
					if (this.channel is HttpChannel)
					{
						HttpChannel httpChannel = this.channel as HttpChannel;
						channelDataStore = httpChannel.ChannelData as ChannelDataStore;
					}
					messages[2] = channelDataStore.ChannelUris[0];
					logger.Log("DS2R1001", messages);
				}
				RegisterWellKnownServiceType();
			}
			catch (System.Exception e)
			{
				logger.Log(e);
                throw;
			}
		}

		public void RegisterWellKnownServiceType()
		{
			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(DnrAdaptor), DnrAdaptor.EXPORT_NAME, this.wellKnownObjectMode);
			if (logger.IsDebugEnabled)
			{
				logger.Log("DS2R1002", new object[] {DnrAdaptor.EXPORT_NAME, this.wellKnownObjectMode});	
			}
		}
		
		internal void  isSetAdaptor()
		{
			if (this.adaptor == null)
			{
				throw new InvalidOperationException("DNRAdaptorÇê›íËÇµÇƒÇ≠ÇæÇ≥Ç¢");
			}
		}
	}
}