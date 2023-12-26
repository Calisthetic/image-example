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
                var responseTask = await _httpClient.GetAsync(path);

                if (responseTask.IsSuccessStatusCode)
                {
                    var resultString = await responseTask.Content.ReadAsStringAsync();
                    return resultString;
                }
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }
    }
}
