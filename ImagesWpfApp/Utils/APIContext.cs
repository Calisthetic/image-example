using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImagesWpfApp.Utils
{
    public class APIContext
    {
        private static HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:5247/api/") };
        public static async Task<string> GetRequest(string path)
        {
            try
            {
                var result = await _httpClient.GetAsync(path);

                if (result.IsSuccessStatusCode)
                {
                    var resultString = await result.Content.ReadAsStringAsync();
                    return resultString;
                }
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }
        public static async Task<string> Post(string urlPath, string obj)
        {
            try
            {
                var result = await _httpClient.PostAsync(urlPath, new StringContent(obj, Encoding.UTF8, "application/json"));

                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return "Success";
                }
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }

        public static async Task<string> Put(string urlPath, string obj)
        {
            try
            {
                var result = await _httpClient.PutAsync(urlPath, new StringContent(obj, Encoding.UTF8, "application/json"));

                if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return "Success";
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }

        public static async Task<string> Delete(string urlPath)
        {
            try
            {
                var result = await _httpClient.DeleteAsync(urlPath);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    return "Success";
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }
    }
}
