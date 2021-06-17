using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiB2bLibrary.utils
{
    class RequestUtil<IN, OUT>
    {
        static string basePath = "http://api-integracao-extra.hlg-b2b.net";
        static string token = "H9xO4+R8GUy+18nUCgPOlg==";

        private static HttpClient client = new HttpClient();

        public async Task<OUT> DoGetAsync(string path, Dictionary<String, String> queryParams)
        {
            string fullPath = basePath + path;

            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            if (queryParams != null)
            {
                fullPath = fullPath + QueryParamStringBuilder(queryParams);
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

            return default(OUT);
        }

        internal void DoGetAsync(object p)
        {
            throw new NotImplementedException();
        }

        public async Task<OUT> DoPostAsync(string path, IN entityIn)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(entityIn);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(path, data);
            string jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<OUT>(jsonContent);
            return result;
        }


        private String QueryParamStringBuilder(Dictionary<String, String> queryParams)
        {
            StringBuilder b = new StringBuilder();
            foreach (var keyValuePair in queryParams)
            {
                if (keyValuePair.Value != null)
                {
                    if (b.Length == 0)
                    {
                        b.Append("?");
                    }
                    else
                    {
                        b.Append("&");
                    }
                    b.Append(keyValuePair.Key).Append("=").Append(keyValuePair.Value);
                }
            }
            return b.ToString();
        }

    }
}
