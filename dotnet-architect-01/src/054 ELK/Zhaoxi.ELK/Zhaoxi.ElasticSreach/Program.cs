using System;
using Zhaoxi.ElasticSearch;

namespace Zhaoxi.ElasticSreach
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//Install-Package NEST 
				{

					var data=SendHttp.GetDataBySql("select * from people limit 5");

					TestData testData = new TestData();
					testData.IndexMany();
					Console.WriteLine("ok");
					testData.Search();
					//Console.ReadKey();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
