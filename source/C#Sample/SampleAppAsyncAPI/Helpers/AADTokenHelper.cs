using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Helpers
{
    class AADTokenHelper
    {
        private ClientConfig clientConfig;
        private HostConfig hostConfig;
        private readonly string devcenterResource = "https://manage.devcenter.microsoft.com";
        private readonly string storageResource = "https://storage.azure.com";

        public AADTokenHelper(ClientConfig clientConfig, HostConfig hostConfig)
        {
            this.clientConfig = clientConfig;
            this.hostConfig = hostConfig;
        }

        public async System.Threading.Tasks.Task<string> GetAADTokenForDevCenterAsync()
        {
            return await GetTokenForResource(devcenterResource, hostConfig.TenantId);
        }

        public async System.Threading.Tasks.Task<string> GetAADTokenForStorageResourceAsync()
        {
            return await GetTokenForResource(storageResource, hostConfig.StorageTenantId);
        }

        private async System.Threading.Tasks.Task<string> GetTokenForResource(string resource, string tenantId)
        {
            dynamic result;
            using (HttpClient client = new HttpClient())
            {
                string tokenUrl = string.Format(hostConfig.AzureADTokenServiceUrl, tenantId);
                using (
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Post,
                        tokenUrl))
                {
                    string content =
                        string.Format(
                            "grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}",
                            clientConfig.ClientId,
                            clientConfig.ClientSecret,
                            resource);

                    request.Content = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject(responseContent);
                    }
                }
            }

            return result.access_token;

        }
    }
}
