using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Configurations;
using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Helpers;
using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Services
{
    class ReportTemplateService
    {
        private HostConfig hostConfig;
        private readonly string reportTemplateEndpoint = "/driver/reporttemplate";
        private AADTokenHelper AADTokenHelper;
        public ReportTemplateService(HostConfig hostConfig, ClientConfig clientConfig)
        {
            this.hostConfig = hostConfig;
            AADTokenHelper = new AADTokenHelper(clientConfig, hostConfig);
        }

        /// <summary>
        /// Create a new report template with a predefined payload
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<string> CreateReportTemplateAsync()
        {
            string payload = JsonConvert.SerializeObject(GenerateReportTemplate());
            string token = AADTokenHelper.GetAADTokenForDevCenterAsync().Result;
            var response = await HttpHelper.HttpPost(string.Format("{0}{1}", hostConfig.AsyncAPIServiceBaseUrl, reportTemplateEndpoint), payload, token);
            return response;
        }

        private ReportTemplate GenerateReportTemplate()
        {
            ReportTemplate reportTemplate = new ReportTemplate
            {
                Projection = new List<string>
                {
                    "EventType", "DriverName"
                },
                View = "IHVDriver",
                Limit = 10
            };
            reportTemplate.DatePeriod = new DateRange
            {
                ReportPeriod = ReportPeriod.Last3Days
            };


            // Condition
            reportTemplate.Condition = new Condition
            {
                And = new Expression[1]
            };
            reportTemplate.Condition.And[0] = new Expression
            {
                Attribute = "EventType",
                Operator = Operator.EQ,
                Value = "Kernel"
            };


            //Aggregation
            reportTemplate.Aggregation = new Aggregation
            {
                AggregatedColumns = new List<string>()
                {
                    "SUM(EventCount)"
                },
                Condition = new Condition()
                {
                    And = new Expression[1]
                }
            };
            reportTemplate.Aggregation.Condition.And[0] = new Expression
            {
                Attribute = "SUM(EventCount)",
                Operator = Operator.GT,
                Value = "4"
            };


            //Orderby
            reportTemplate.OrderBy = new List<OrderBy>
            {
                new OrderBy
                {
                    Attribute = "EventType",
                    Sort = "asc"
                }
            };

            return reportTemplate;
        }
    }
}
