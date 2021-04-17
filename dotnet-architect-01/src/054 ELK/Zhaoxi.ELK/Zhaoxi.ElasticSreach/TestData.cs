using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.ElasticSearch
{
	public class TestData
	{
		#region 集群连接方式
		//var uris = new[]
		//        {
		//   	new Uri("http://localhost:9200"),
		//  new Uri("http://localhost:9201"),
		//  new Uri("http://localhost:9202"),
		//        };
		//var connectionPool = new SniffingConnectionPool(uris);
		//var settings = new ConnectionSettings(connectionPool)
		//	.DefaultIndex("people"); 
		//var client = new ElasticClient(settings);
		#endregion
		public void Trace(string message)
		{
			 
			var settings = new ConnectionSettings(new Uri(Url.url))
		.DefaultIndex("traceinfolog");
			var client = new ElasticClient(settings);
			TraceInfo traceInfo = new TraceInfo()
			{
				RpcID = Guid.NewGuid().ToString(),
				Message = message,
				Time = DateTime.Now
			};
			client.IndexDocument(traceInfo);

			//client.Index(traceInfo, i => i.Index("traceinfolog"));

			//client.Index(new IndexRequest<TraceInfo>(traceInfo, "TraceInfoLog"));
		}

		public void IndexMany()
		{
			//如果你会完linq ，或者list
			var settings = new ConnectionSettings(new Uri(Url.url))
			.DefaultIndex("people");
			var client = new ElasticClient(settings);
			List<Person> peoples = new List<Person>();
			for (int i = 0; i < 10; i++)
			{
				Person personinfo = new Person
				{
					Id = i,
					FirstName = "Martijn" + i,
					LastName = "Laarman" + i
				};
				peoples.Add(personinfo);
			}
			client.IndexMany<Person>(peoples);
		}

		public void Search()
		{
			var settings = new ConnectionSettings(new Uri(Url.url))
			   .DefaultIndex("people");
			var client = new ElasticClient(settings);
			var searchResponse = client.Search<Person>(s => s
				.From(0)
				.Size(10)
				.Query(q => q
					 .Match(m => m
						.Field(f => f.FirstName)
						.Query("Martijn1")
					 )
				)
			);
			var people = searchResponse.Documents;
			Console.WriteLine("查询结果");
			foreach (var item in people)
			{
				Console.WriteLine($"id:{item.Id},firstname:{item.FirstName},lastname:{item.LastName}");
			}
		}
		public class Url
		{
			public static string url = "http://localhost:9200/";
		}
	}
}
