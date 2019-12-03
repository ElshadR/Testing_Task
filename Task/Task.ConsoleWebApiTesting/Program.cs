using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.RequestLibrary.Core;
using Task.RequestLibrary.Core.Interfaces;
using Task.RequestLibrary.Core.Services;
using Task.RequestLibrary.Models;
using k = System.Threading.Tasks;

namespace Task.ConsoleWebApiTesting
{
    class Program
    {
        static async k.Task Main(string[] args)
        {
            var request = new RequestSender();
            IWorkerService worker = new WorkerService(request);
            IProcessService process = new ProcessService(request);

            Console.WriteLine("Mesgul isciler");
            var busyWorkers = await process.BusyWorkers();

            if(busyWorkers.Data!=null)
                foreach (var item in busyWorkers.Data)
                {
                    Console.WriteLine($"FullName: {item.FullName}, NickName: {item.NickName}, IsBusy: {item.IsBusy}, Position: {item.Position}, Count Compleated Query: {item.Queries.Count}");
                }
            Console.WriteLine("------------------");

            var isCreated = await process.AddQuery(new QueryModel
            {
                Question = "Senin nece yasin var?",
            });
            Console.ReadLine();
           
        }
    }
}
