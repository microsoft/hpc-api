using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Configurations;
using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Services
{
    class ReportDataService
    {
        private HostConfig hostConfig;
        private readonly string reportDataEndpoint = "/driver/reportdata";
        private AADTokenHelper AADTokenHelper;

        public ReportDataService(HostConfig hostConfig, ClientConfig clientConfig)
        {
            this.hostConfig = hostConfig;
            AADTokenHelper = new AADTokenHelper(clientConfig, hostConfig);
        }

        /// <summary>
        /// Get report data details like report download link, report execution status, report execution date
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<string> GetReportDataAsync(int reportId)
        {
            string token = AADTokenHelper.GetAADTokenForDevCenterAsync().Result;
            string url = string.Format("{0}{1}/{2}?startDate={3}&endDate={4}", hostConfig.AsyncAPIServiceBaseUrl, 
                reportDataEndpoint, reportId, DateTime.UtcNow.AddDays(-5).ToString("yyyy-MM-dd"), DateTime.UtcNow.ToString("yyyy-MM-dd"));
            var response = await HttpHelper.HttpGet(url, token);
            return response;
        }

        /// <summary>
        /// Download the blob corresponding to the provided blob url. Note this will only work if your AAD App has been onboarded to blob storage
        /// </summary>
        /// <param name="blobLocation"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task GetReportDataBlobAsync(string blobLocation)
        {
            string accessToken = AADTokenHelper.GetAADTokenForStorageResourceAsync().Result;
            TokenCredential tokenCredential = new TokenCredential(accessToken);
            StorageCredentials storageCredentials = new StorageCredentials(tokenCredential);
            CloudBlockBlob blob = new CloudBlockBlob(new Uri(blobLocation), storageCredentials);

            try
            {
                await blob.DownloadToFileAsync(blob.Name, FileMode.Create);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
