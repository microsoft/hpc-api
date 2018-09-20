using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Helpers
{
    public class HttpHelper
    {
        private const string incorrectUrl = "The url format provided is incorrect";
        public static async Task<string> HttpPost(string url, string payload, string token)
        {
            if (url == null || !Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                return incorrectUrl;
            }

            HttpClient client = new HttpClient();
            
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsync(uri, new StringContent(payload, Encoding.UTF8, "application/json"));
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public static async Task<string> HttpGet(string url, string token)
        {
            if (url == null || !Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                return incorrectUrl;
            }

            HttpClient client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
