using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzFuncBlobTrigger
{
    public static class Function1
    {
        [FunctionName("MyFunction")]
        public static void Run([BlobTrigger("mail-attachment/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            try
            {
                log.LogInformation($"**************DOR Blog processing started: {DateTime.Now}**************");

                var environmentReaderApi = GetEnvironmentVariable("ReaderApi");

                log.LogInformation($"**************GetEnvironmentVariable: {environmentReaderApi}**************");

                string tableslist = GetEnvironmentVariable("TableList");

                log.LogInformation($"**************GetEnvironmentVariable tableslist: {tableslist}**************");

                tableslist = ConfigurationManager.AppSettings["TableList"];

                log.LogInformation($"**************ConfigurationManager.AppSettings: {tableslist}**************");
            }
            catch (Exception ex)
            {
                log.LogError("Error in processing for file name-" + name + " Error-" + ex.Message);
            }
            finally
            {
                log.LogInformation($"**************Processing completed: {DateTime.Now}**************");
            }

        }
        static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
