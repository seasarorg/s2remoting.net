using System;

namespace Seasar.Remoting.Tests.Common.Interceptor
{
	public class TestClient
	{
		IHoge hoge = null;
		IFoo foo = null;
		IBar bar = null;

		public TestClient(IHoge hoge, IFoo foo, IBar bar)
		{
			this.hoge = hoge;
			this.foo = foo;
			this.bar = bar;
		}

		public void CallHoge()
		{
			hoge.Hoge();
		}
		public void CallFoo()
		{
			foo.Foo();
		}
		public void CallBar()
		{
			bar.Bar();
		}
	}
	
	public class HogeImpl : MarshalByRefObject, IHoge
	{
		public void  Hoge()
		{
		}

		public void Hoge2()
		{
			
		}
	}

	public interface IBar
	{
		void  Bar();
	}

	public interface IFoo
	{
		void  Foo();
	}
	
	public interface IHoge
	{
			
		void  Hoge();
			
		//void  Bar();
	}
}
