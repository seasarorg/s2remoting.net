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
		
		/// <summary> dicon�t�@�C���Őݒ肳�ꂽbaseURL�v���p�e�B���g�p���āA
		/// ���W�X�g������DNRAdaptor�̃X�^�u�N���X���擾���܂�.
		/// </summary>
		public virtual void Lookup()
		{
			if (this.channel == null)
				throw new InvalidOperationException("Channel��ݒ肵�Ă�������");

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