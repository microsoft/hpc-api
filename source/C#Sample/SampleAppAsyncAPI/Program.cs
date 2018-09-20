using Microsoft.Extensions.Configuration;
using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Configurations;
using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models;
using Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Services;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp
{
    public class Program
    {
        static readonly ClientConfig clientConfig = new ClientConfig();
        static readonly HostConfig hostConfig = new HostConfig();
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            PopulateConfigurations(configuration);

            
            //Create a new Report Template
            int templateId = CreateReportTemplateAsync().Result;


            //Using the Template Created, Create a new Report
            int reportId = 0;
            if (templateId > 0)
            {
                reportId = CreateReportAsync(templateId).Result;
            }

            if (reportId > 0)
            {
                await GetReportDataAsync(reportId);
            }
            

            //await GetReportDataBlobAsync("URL To Blob");


            Console.WriteLine("Bye Bye World!");
        }


        private static async System.Threading.Tasks.Task GetReportDataAsync(int reportId)
        {
            Console.WriteLine($"----------Initiating Get Report Data API Call with ReportId: {reportId}");
            ReportDataService reportDataService = new ReportDataService(hostConfig, clientConfig);
            var responseString = await reportDataService.GetReportDataAsync(reportId);
            Console.WriteLine($"----------Get Report Data Initiated, response received: {responseString}");
            Console.WriteLine("**********************************");
        }

        private static async System.Threading.Tasks.Task<int> CreateReportAsync(int templateId)
        {
            Console.WriteLine($"----------Initiating Create Report API Call with TemplateId: {templateId}");
            ReportService reportService = new ReportService(hostConfig, clientConfig);
            var responseString = await reportService.CreateReportAsync(templateId);
            Console.WriteLine($"----------Create Report Initiated, response received: {responseString}");
            Console.WriteLine("**********************************");

            if (!string.IsNullOrEmpty(responseString))
            {
                try
                {
                    var reportResponse = JsonConvert.DeserializeObject<APIResponse<ReportResponse>>(responseString);
                    if (reportResponse != null && reportResponse.Data != null)
                    {
                        return reportResponse.Data.ReportId;
                    }
                }
                catch (Exception e)
                {
                    Console.Write($"Exception Occured: {e.Message}");
                    return -1;
                }
            }
            return -1;
        }

        private static async System.Threading.Tasks.Task<int> CreateReportTemplateAsync()
        {
            Console.WriteLine("----------Initiating Create ReportTemplate API Call");
            ReportTemplateService reportTemplateService = new ReportTemplateService(hostConfig, clientConfig);
            var responseString = await reportTemplateService.CreateReportTemplateAsync();
            Console.WriteLine($"----------Create ReportTemplate Initiated, response received: {responseString}");
            Console.WriteLine("**********************************");

            if (!string.IsNullOrEmpty(responseString))
            {
                try
                {
                    var reportTemplateResponse = JsonConvert.DeserializeObject<APIResponse<ReportTemplateResponse>>(responseString);
                    if (reportTemplateResponse != null && reportTemplateResponse.Data != null)
                    {
                        return reportTemplateResponse.Data.TemplateId;
                    }
                }
                catch(Exception e)
                {
                    Console.Write($"Exception Occured: {e.Message}");
                    return -1;
                }
            }
            return -1;
        }

        private static void PopulateConfigurations(IConfigurationRoot configuration)
        {
            var clientDetails = configuration.GetSection("client_details");
            if (clientDetails != null)
            {
                clientConfig.ClientId = clientDetails["client_id"];
                clientConfig.ClientSecret = clientDetails["client_secret"];
            }

            hostConfig.TenantId = configuration["tenant_id"];
            hostConfig.StorageTenantId = configuration["storage_tenant_id"];

            if (!string.IsNullOrWhiteSpace(hostConfig.TenantId) && !string.IsNullOrWhiteSpace(hostConfig.StorageTenantId))
            {
                hostConfig.AzureADTokenServiceUrl = configuration["aad_token_service_url"];
            }

            hostConfig.AsyncAPIServiceBaseUrl = configuration["analytics_api_service_url"];
        }

        private static async System.Threading.Tasks.Task GetReportDataBlobAsync(string url)
        {
            Console.WriteLine($"----------Initiating Get Report Data Blob with Blob Url: {url}");
            ReportDataService reportDataService = new ReportDataService(hostConfig, clientConfig);
            await reportDataService.GetReportDataBlobAsync(url);
            Console.WriteLine("**********************************");
        }
    }
}
