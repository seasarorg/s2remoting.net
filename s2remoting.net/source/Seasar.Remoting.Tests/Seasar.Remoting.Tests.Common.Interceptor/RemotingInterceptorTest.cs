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
using Seasar.Framework.Aop.Interceptors;

namespace Seasar.Remoting.Tests.Common.Interceptor
{
	[TestFixture]
	public class RemotingInterceptorTest : S2TestCase
	{
		private MockInterceptor mockInterceptor_ = null;
		private TestClient testClient_ = null;
		
		public RemotingInterceptorTest()
		{
		}

		public void  SetUpInvoke()
		{
			Include("RemotingInterceptorTest.dicon");
		}

		[Test, S2]
		public void  TestInvoke()
		{
			// call concrete method
//			IHoge hoge = (IHoge) GetComponent(typeof(IHoge));
//			hoge.Foo();
//			Assert.IsFalse(mockInterceptor_.IsInvoked("Invoke"));
			
			// call abstract method
			//hoge.Bar();
			testClient_.CallHoge();
			Assert.IsTrue(mockInterceptor_.IsInvoked("Invoke"));
		}

		public void  SetUpGetTargetName()
		{
			Include("RemotingInterceptorTest.dicon");
		}
		
		[Test, S2]
		public void TestGetTargetName()
		{
			testClient_.CallHoge();
			System.Object[] args = mockInterceptor_.GetArgs("Invoke");
			Assert.AreEqual("hoge", args[0]);
			
			testClient_.CallFoo();
			args = mockInterceptor_.GetArgs("Invoke");
			Assert.AreEqual("IFoo", args[0]);

			testClient_.CallBar();
			args = mockInterceptor_.GetArgs("Invoke");
			Assert.AreEqual("Bar", args[0]);
		}
	}
}