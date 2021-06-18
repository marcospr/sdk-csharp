﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiB2bLibrary.utils
{
    public class RequestUtil<IN, OUT>
    {
        static readonly string basePath = "http://api-integracao-extra.hlg-b2b.net";
        static readonly string token = "H9xO4+R8GUy+18nUCgPOlg==";

        private static readonly HttpClient client = new ();

        public RequestUtil()
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

        }
        public async Task<OUT> DoGetAsync(string path, string token, Dictionary<String, String> queryParams)
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            return await DoGetAsync( path,  queryParams);
        }
        public async Task<OUT> DoGetAsync(string path, Dictionary<String, String> queryParams)
        {
            string fullPath = basePath + path;

            if (queryParams != null)
            {
                fullPath += QueryParamStringBuilder(queryParams);
            }

            try
            {
            HttpResponseMessage response = await client.GetAsync(fullPath);
        //    response.EnsureSuccessStatusCode();
            string jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OUT>(jsonContent);
            return result;

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return default;
        }

        internal void DoGetAsync(object p)
        {
            throw new NotImplementedException();
        }

        public async Task<OUT> DoPostAsync(string path, string token, IN entityIn)
        {         
            string json = System.Text.Json.JsonSerializer.Serialize(entityIn);
            Console.WriteLine($"body entrada: {json}");
            StringContent data = new(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            HttpResponseMessage response = await client.PostAsync(path, data);
            string jsonContent = response.Content.ReadAsStringAsync().Result;   
            var result = JsonConvert.DeserializeObject<OUT>(jsonContent);
            return result;
        }

        public async Task<OUT> DoPatchPostAsync(string path, string token, IN entityIn)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(entityIn);
            Console.WriteLine($"body entrada: {json}");
            StringContent data = new(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            HttpResponseMessage response = await client.PatchAsync(path, data);
            string jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<OUT>(jsonContent);
            return result;
        }


        private static String QueryParamStringBuilder(Dictionary<String, String> queryParams)
        {
            StringBuilder b = new();
            foreach (var keyValuePair in queryParams)
            {
                if (keyValuePair.Value != null)
                {
                    if (b.Length == 0)
                    {
                        b.Append('?');
                    }
                    else
                    {
                        b.Append('&');
                    }
                    b.Append(keyValuePair.Key).Append('=').Append(keyValuePair.Value);
                }
            }
            return b.ToString();
        }

    }
}
