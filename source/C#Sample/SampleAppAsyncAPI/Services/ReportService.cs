using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Configurations;
using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Helpers;
using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Services
{
    class ReportService
    {
        private HostConfig hostConfig;
        private readonly string reportEndpoint = "/driver/report";
        private AADTokenHelper AADTokenHelper;

        public ReportService(HostConfig hostConfig, ClientConfig clientConfig)
        {
            this.hostConfig = hostConfig;
            AADTokenHelper = new AADTokenHelper(clientConfig, hostConfig);
        }

        /// <summary>
        /// Create a new report with a schedule and a given report template Id
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<string> CreateReportAsync(int templateId)
        {
            string payload = JsonConvert.SerializeObject(GenerateReport(templateId));
            string token = AADTokenHelper.GetAADTokenForDevCenterAsync().Result;
            var response = await HttpHelper.HttpPost(string.Format("{0}{1}", hostConfig.AsyncAPIServiceBaseUrl, reportEndpoint), payload, token);
            return response;
        }

        private Report GenerateReport(int templateId)
        {
            Report report = new Report
            {
                TemplateId = templateId,
                Schedule = new Schedule
                {
                    StartTime = DateTime.UtcNow.AddMinutes(5).ToString("yyyy-MM-ddTHH\\:mm\\:ssZ"),
                    Recurrence = 1,
                    RecurrenceInterval = 12
                },
                CallbackUrl = "https://jsonplaceholder.typicode.com/posts"
            };

            return report;
        }
    }
}
