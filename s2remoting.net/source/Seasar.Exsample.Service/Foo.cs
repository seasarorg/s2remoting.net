using System;

namespace Seasar.Exsample.Service
{
	/// <summary>
	/// Foo の概要の説明です。
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
