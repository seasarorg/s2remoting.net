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
using System;
using System.Reflection;
using MbUnit.Framework;
using Seasar.Extension.Unit;
using Seasar.Remoting.Common.Connector.Impl;

namespace Seasar.Remoting.Tests.Common.Connector.Impl
{
	[TestFixture]
	public class TargetSpecificURLBasedConnectorTest : S2TestCase
	{
	
		public TargetSpecificURLBasedConnectorTest()
		{
		}
		
		[Test]
		public void TestGetTargetURL()
		{
			TargetSpecificURLBasedConnector connector = new AnonymousClassTargetSpecificURLBasedConnector(this);
			
			connector.BaseURL = new Uri("http://localhost/context/");
			Assert.AreEqual(new Uri("http://localhost/context/Foo"), connector.GetTargetURL("Foo"));
			
			connector.BaseURL = new Uri("http://localhost/context");
			Assert.AreEqual(new Uri("http://localhost/Bar"), connector.GetTargetURL("Bar"));

			connector.BaseURL = new Uri("http://localhost/context/");
			Assert.AreEqual(new Uri("http://localhost/context/Foo"), connector.GetTargetURL("Foo"));

		}
		
		[Test]
		[ExpectedArgumentException]
		public void TestLRUMap()
		{
			TargetSpecificURLBasedConnector.LruCache cache = new TargetSpecificURLBasedConnector.LruCache(1);
			
			Assert.AreEqual(0, cache.Count);
			
			cache["1"] = "1";
			Assert.AreEqual(1, cache.Count);
			
			cache["2"] = "2";
			Assert.AreEqual(1, cache.Count);
			
			cache.CacheSize = 2;
			cache["3"] = "3";
			Assert.AreEqual(2, cache.Count);
			
			cache["4"] = "4";
			Assert.AreEqual(2, cache.Count);
			
			cache.CacheSize = 0;
			cache["5"] = "5";
			Assert.AreEqual(3, cache.Count);
		}

		private class AnonymousClassTargetSpecificURLBasedConnector : TargetSpecificURLBasedConnector
		{
			private TargetSpecificURLBasedConnectorTest enclosingInstance;

			public AnonymousClassTargetSpecificURLBasedConnector(TargetSpecificURLBasedConnectorTest enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TargetSpecificURLBasedConnectorTest enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			
			public TargetSpecificURLBasedConnectorTest Enclosing_Instance
			{
				get { return enclosingInstance; }
			}
			
			public override System.Object Invoke(Uri url, MethodInfo method, object[] params_Renamed)
			{
				return null;
			}
		}
	}
}