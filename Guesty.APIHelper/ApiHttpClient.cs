using Guesty.APIHelper.Models;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace Guesty.APIHelper
{
    public static class ApiHttpClient
    {
        public static ApiDetails ApiDetails { get; set; }
        public static IConfiguration Configuration { get; }

        public static HttpResponseMessage Get(string url)
        {
            HttpMessageHandler handler = new HttpClientHandler()
            {
            };
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(ApiDetails.Url),
            };
            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            //This is the key section you were missing    
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ApiDetails.Key + ":" + ApiDetails.Secret);
            string val = System.Convert.ToBase64String(plainTextBytes);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

            return httpClient.GetAsync(url).Result;

        }
        public static HttpResponseMessage Post(string url, object data)
        {
            HttpMessageHandler handler = new HttpClientHandler()
            {
            };
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(ApiDetails.Url),
            };
            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            //This is the key section you were missing    
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ApiDetails.Key + ":" + ApiDetails.Secret);
            string val = System.Convert.ToBase64String(plainTextBytes);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            return httpClient.PostAsync(url, byteContent).Result;

        }

    }
}
