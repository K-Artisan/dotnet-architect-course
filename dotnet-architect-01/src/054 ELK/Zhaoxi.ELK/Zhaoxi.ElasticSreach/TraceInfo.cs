using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.ElasticSearch
{
	public class TraceInfo
	{
		// RpcID 这个就是当前请求的唯一标识

		/// <summary>
		/// 唯一的请求的标识
		/// </summary>
		public string RpcID { get; set; }

		public DateTime Time { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Content { get; set; }



		public string Message { get; set; }



	}
}
