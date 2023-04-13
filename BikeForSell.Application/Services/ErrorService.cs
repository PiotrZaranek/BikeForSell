using BikeForSell.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.Services
{
    public class ErrorService : IErrorService
    {
        private readonly ILogger<ErrorService> _logger;

        public ErrorService(ILogger<ErrorService> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception ex, string controller, string action, string userId)
        {
            using FileStream fileStrem = File.Open(@$"Logs\Error-{DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss")}.txt", FileMode.Create, FileAccess.Write);
            using StreamWriter streamWriter = new StreamWriter(fileStrem);

            streamWriter.WriteLine(DateTime.Now);
            streamWriter.WriteLine("------------------------------");
            streamWriter.WriteLine("Source: " + ex.Source + "\n");
            streamWriter.WriteLine("Message: " + ex.Message + "\n");
            streamWriter.WriteLine("Trace:" + ex.StackTrace + "\n");
            streamWriter.WriteLine("Target: " + ex.TargetSite);
            streamWriter.WriteLine("------------------------------");
            streamWriter.WriteLine("UserId: " + userId + "\n");
            streamWriter.WriteLine("Controller: " + controller + "\n");
            streamWriter.WriteLine("Action: " + action);            
        }
    }
}
