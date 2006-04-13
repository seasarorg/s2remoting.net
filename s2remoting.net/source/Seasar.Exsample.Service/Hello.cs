using System;
using System.Data;

namespace Seasar.Exsample.Service
{
	public class Hello : IHello
	{
		private static readonly DataSet TestDs;
        private static readonly DataSet TestBigDs;
		
		static Hello()
		{
			 TestDs = GetTestDataSet(3000);
             TestBigDs = GetTestDataSet(100000);
		}
		
		public string Say() 
		{
			return "Hello";
		}

		public string Bar() 
		{
			throw new ApplicationException("例外のテスト");
			return "Bar";
		}

		public DataSet GetDataSet()
		{
			return TestDs;
		}

        public DataSet GetBigDataSet()
        {
            return TestBigDs;
        }
        
		public void Add(int a, int b , ref int total)
		{
			total = a + b;
		}

		private static DataSet GetTestDataSet(int size)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			dt.Columns.Add("Id", typeof (int));
			dt.Columns.Add("Text1", typeof (string));
            dt.Columns.Add("Text2", typeof(string));
            dt.Columns.Add("Text3", typeof(string));
			dt.PrimaryKey = new DataColumn[]{ dt.Columns[0] };
            for (int i = 0; i < size; i++)
			{
				DataRow dr = dt.NewRow();
				dr["Id"] = i;
				dr["Text1"] = "文字列あああいいいうううえええおおお";
                dr["Text2"] = "文字列かかかきききくくくけけけこここ";
                dr["Text3"] = "文字列さささしししすすすせせせそそそ";
				dt.Rows.Add(dr);
			}
			ds.Tables.Add(dt);
			return ds;
		}
	}
	
	public interface IHello 
	{
		string Say();
		string Bar();
		DataSet GetDataSet();
        DataSet GetBigDataSet();
		void Add(int a, int b , ref int total);
	}
}
