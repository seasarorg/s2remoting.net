using System;
using MbUnit.Framework;
using Seasar.Remoting.DotNetRemoting.Adaptor;
using Seasar.Extension.Unit;
using Seasar.Framework.Beans;
using Seasar.Framework.Container;

namespace Seasar.Remoting.Tests.DotNetRemoting.Adaptor
{
	[TestFixture]
	public class DnrAdaptorTest : S2TestCase
	{
		private static System.String PATH = "test.dicon";
		
		private DnrAdaptor adaptor_ = null;

		public void SetUpInvoke()
		{
			Include(PATH);
		}
		
		[Test, S2]
		public void TestInvoke()
		{
			Assert.AreEqual("Hello!", adaptor_.Invoke("hello", "Say", null));
		}

		public void SetUpInvoke1()
		{
			Include(PATH);
		}
		
		[Test, S2]
		[ExpectedException(typeof(ComponentNotFoundRuntimeException))]
		public void TestInvoke1()
		{
			adaptor_.Invoke("hello1", "Say", null);
		}

		public void SetUpInvoke2()
		{
			Include(PATH);
		}

		[Test, S2]
		[ExpectedException(typeof(TooManyRegistrationRuntimeException))]
		public void TestInvoke2()
		{
			adaptor_.Invoke("hello2", "Say", null);
		}
		
		public void SetUpInvoke3()
		{
			Include(PATH);
		}

		[Test, S2]
		[ExpectedException(typeof(MethodNotFoundRuntimeException))]
		public void TestInvoke3()
		{
			adaptor_.Invoke("hello", "hello", null);
		}

		public void SetUpInvoke4()
		{
			Include(PATH);
		}

		[Test, S2]
		[ExpectedException(typeof(IllegalMethodRuntimeException))]
		public void TestInvoke4()
		{
			System.String[] args = new System.String[]{"hello"};
			adaptor_.Invoke("hello", "Say", args);
		}
	}
}