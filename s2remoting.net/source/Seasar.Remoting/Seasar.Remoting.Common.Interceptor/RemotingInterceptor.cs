using System;
using System.Reflection;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Interceptors;
using Seasar.Framework.Container;
using Seasar.Remoting.Common.Connector;
/*
* Copyright 2004-2006 the Seasar Foundation and the Others.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
* either express or implied. See the License for the specific language
* governing permissions and limitations under the License.
*/

namespace Seasar.Remoting.Common.Interceptor
{
	
	/// <summary> リモートオブジェクトのメソッド呼び出しを行うためのインターセプタです。</summary>
	public class RemotingInterceptor : AbstractInterceptor
	{
		protected internal IConnector connector;
		protected internal System.String remoteName;

		/// <summary> リモートオブジェクトの名前を設定します。このプロパティはオプションです。
		/// コンポーネント定義から取得できる名前を使うことが出来ない場合にのみ設定してください。
		/// </summary>
		public string RemoteName
		{
			set { remoteName = value; }
		}

		public virtual IConnector Connector
		{
			set
			{
				this.connector = value;
			}
		}
		
		/// <summary> ターゲットのメソッドが起動された時に呼び出されます。起動されたメソッドが抽象メソッドなら {@link Connector}に委譲します。
		/// 具象メソッドならターゲットのメソッドを呼び出します。
		/// </summary>
		public override object Invoke(IMethodInvocation invocation)
		{
			MethodInfo method = invocation.Method as MethodInfo;
			if (method.IsAbstract || invocation.Target.GetType().IsMarshalByRef)
			{
				return connector.Invoke(GetRemoteName(invocation), method, invocation.Arguments);
			}
			return invocation.Proceed();
		}
		
		/// <summary> リモートオブジェクトの名前を返します。リモートオブジェクトの名前は次の順で解決します。
		/// <ul>
		/// <li>プロパティ <code>remoteName</code> が設定されていればその値。</li>
		/// <li>コンポーネント定義に名前が設定されていればその値。</li>
		/// <li>コンポーネント定義に型が設定されていればその名前からパッケージ名を除いた値。</li>
		/// <li>ターゲットオブジェクトの型名からパッケージ名を除いた値。</li>
		/// </ul>
		protected internal virtual string GetRemoteName(IMethodInvocation invocation)
		{
			if (remoteName != null)
			{
				return remoteName;
			}
			
			IComponentDef componentDef = GetComponentDef(invocation);
			System.String componentName = componentDef.ComponentName;
			if (componentName != null)
			{
				return componentName;
			}
			
			System.Type componentClass = componentDef.ComponentType;
			if (componentClass != null)
			{
				return componentClass.Name;
			}
			
			return invocation.Target.GetType().Name;
		}
	}
}