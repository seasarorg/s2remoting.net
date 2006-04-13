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
using MbUnit.Framework;
using Seasar.Extension.Unit;
using Seasar.Remoting.Common.Connector.Impl;

namespace Seasar.Remoting.Tests.Common.Connector.Impl
{
	
	[TestFixture]
	public class URLBasedConnectorTest : S2TestCase
	{
		
		public URLBasedConnectorTest()
		{
		}

		public void  SetUpBaseURL()
		{
			Include("URLBasedConnectorTest.dicon");
		}
		
		[Test, S2]
		public void  TestBaseURL()
		{
			URLBasedConnector connector = (URLBasedConnector) GetComponent(typeof(TestConnector));
			Assert.AreEqual(new System.Uri("http://localhost/path"), connector.BaseURL);
		}
	}
}