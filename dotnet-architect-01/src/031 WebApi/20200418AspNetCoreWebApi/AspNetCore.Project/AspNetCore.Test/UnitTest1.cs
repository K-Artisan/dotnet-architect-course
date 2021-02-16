using NUnit.Framework;
using System;
using System.Net.Http;

namespace AspNetCore.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        { 
            string url = "http://localhost:2021/api/First/GetString"; 
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var result = httpClient.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;

            }

            //Assert.Pass();
        }
    }
}