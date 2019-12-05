using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Task.RequestLibrary.Core;
using Task.RequestLibrary.Core.Interfaces;
using Task.RequestLibrary.Core.Services;
using Task.RequestLibrary.Models;
using k = System.Threading.Tasks;

namespace Task.ConsoleWebApiTesting
{
    class LoginProcess
    {
        static object locker = new object();
        private System.Timers.Timer aTimer;
        public Login Login { get; set; }
        public QueryModel Query { get; set; }
        public IProcessService process { get; set; }
        public LoginProcess(Login login)
        {
            Login = login;
            var request = new RequestSender();
            process = new ProcessService(request);
            SetTimerStartProcess();
        }

        private void SetTimerStartProcess()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        } 
       
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Login.Role == Role.Worker)
            {
                var query = process.GetQuery(Login.Id).GetAwaiter().GetResult();

                Console.WriteLine($"-----Get query {Login.NickName}-------");
                if (query.IsSucced)
                {
                    var t = Query == null;
                    Console.WriteLine("yes");
                    Query = query.Data;
                    Console.WriteLine(query.Data.Question);
                    if(t)
                        SetAnswerQueryTimer();

                }
                else
                {
                    Console.WriteLine("no");
                }
                Console.WriteLine("------------");
            }
            //else
            //{
            //    var query = process.AddQuery(new QueryModel()
            //    {
            //        Question = "new query" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
            //        UserId = Login.Id
            //    }).GetAwaiter().GetResult();

            //    Console.WriteLine($"------Add query {Login.NickName}------");
            //    if (query.IsSucced)
            //    {
            //        var count = process.CountPendingByQuery(query.Data.Id).GetAwaiter().GetResult();
            //        Console.WriteLine("yes");
            //        Console.WriteLine(query.Data.Question + ". Count Pending: "+count.Data);
            //    }
            //    else
            //    {
            //        Console.WriteLine("no");
            //    }


            //    Console.WriteLine("------------");
            //}
        }
        private void SetAnswerQueryTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(120000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEventAnswerQuery;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private void OnTimedEventAnswerQuery(Object source, ElapsedEventArgs e)
        {
            lock (locker)
            {
                Console.WriteLine("sdasdasd");
                var query = process.AnswerQuery(new AnswerModel
                {
                    Text = "answer " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                    Id = Query.Id
                }).GetAwaiter().GetResult();
                Console.WriteLine($"------Answer {Login.NickName}------");
                if (query.IsSucced)
                {
                    Console.WriteLine("Answer query");
                    Console.WriteLine(query.Data.Text);

                }
                else
                {
                    Console.WriteLine("No answer query");
                }
                Console.WriteLine("------------");
            }
        }
    }
    class Program
    {

        public static List<Thread> threads = new List<Thread>();
        static async k.Task Main(string[] args)
        {

            

            var request = new RequestSender();
            IWorkerService worker = new WorkerService(request);
            IProcessService process = new ProcessService(request);

            var q = await process.GetQuery(2);

            Console.WriteLine("isBusy Workers");
            var busyWorkers = await process.BusyWorkers();

            if(busyWorkers.Data!=null)
                foreach (var item in busyWorkers.Data)
                {
                    Console.WriteLine($"FullName: {item.FullName}, NickName: {item.NickName}, IsBusy: {item.IsBusy}, Position: {item.Position}, Count Compleated Query: {item.Queries.Count}");
                }

            Console.WriteLine("------------------");
         

            var login =await process.Login(new Login
            {
                NickName = "cavid1993",
                Password = "cavid@1234567",
            });
            var login1 =await process.Login(new Login
            {
                NickName = "mikayil1998",
                Password = "mikayil@12345",
            });
            var login2 = await process.Login(new Login
            {
                NickName = "senan1995",
                Password = "senan@123456",
            });
            var login3 =await process.Login(new Login
            {
                NickName = "penah1997",
                Password = "penah@12345678",
            });
            var login4 =await process.Login(new Login
            {
                NickName = "elshad1997",
                Password = "elshad@123",
            });
            List<Login> logins = new List<Login>();
            logins.Add(login.Data);
            logins.Add(login1.Data);
            logins.Add(login2.Data);
            logins.Add(login3.Data);
            logins.Add(login4.Data);

            Thread queriesGenerated = new Thread(x =>
            {
                for (int i = 0; i < 50; i++)
                {
                    var query = process.AddQuery(new QueryModel()
                    {
                        Question = "new query " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                        UserId = login.Data.Id
                    }).GetAwaiter().GetResult();
                    Thread.Sleep(5000);
                }
            });
            queriesGenerated.Start();

            foreach (var item in logins)
            {

                Thread thread = new Thread( x =>
                {
                    var newItem = new LoginProcess(item);
                });
                threads.Add(thread);
            }
            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }


            Thread thread1 = new Thread(async x =>
              {
                  Thread.Sleep(240000);
                  Console.WriteLine("-------Cancel query-------");
                  var queries = await process.PendingQuery();

                  if (queries.Data.Count != 0)
                  {
                      var response = await process.CancelQuery(queries.Data[queries.Data.Count / 2].Id);

                      if (response.IsSucced)
                      {
                          Console.WriteLine("yes");
                          Console.WriteLine(response.Data.Question + " => cancel");
                      }
                      else
                      {
                          Console.WriteLine("no");
                      }
                  }
                  return;

              });
            thread1.Start();
            thread1.Join();
            Thread thread2 = new Thread(async x =>
            {
                Thread.Sleep(300000);
                Console.WriteLine("-------Setting td and tm-------");

                var settings = await process.Settings(new SettingModel
                {
                    Td = 25,
                    Tm = 15
                });

            });
            thread2.Start();
            thread2.Join();

            Console.ReadLine();

        }

      
    }
}
