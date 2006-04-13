using System;

namespace Seasar.Exsample.Service
{
	/// <summary>
	/// Foo ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class Foo : MarshalByRefObject
	{
		public Foo()
		{
		
		}
		
		public string Bar()
		{
			return "Bar";
		}
		
		public void Add(int a, int b , ref int total)
		{
			total = a + b;
		}
	}
}
