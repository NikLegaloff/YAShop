using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAShop.BusinessLogic.Bus;

namespace YAShop.JobExecutor
{
    public class JobExecutorService
    {
        private static readonly int NumberOfThreads = 4;
        private bool _stopAll;

        private Task[] _tasks;

        public void Start()
        {
            var tasks = new List<Task>();
            for (var i = 0; i < NumberOfThreads; i++)
            {
                var task = new Task(ProcessJobs, i);
                tasks.Add(task);
                task.Start();
                Thread.Sleep(2000); // for uniform calls of threads
            }
            _tasks = tasks.ToArray();
        }

        private void ProcessJobs<T>(T i)
        {
            Console.WriteLine("Thread #" + i + " started");
            do
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                var result =
                    new CommandExecutor(s => Console.WriteLine(threadId + "\t" + DateTime.Now + "\t" + s))
                        .TakeOneAndExecute().Result;
                ProgrCommonInfrProvider.ResetIdentityMap(threadId);
                if (!result)
                    Thread.Sleep(NumberOfThreads * 2000); // if nothing processed then sleep 2 sec * # of threads
            } while (!_stopAll);
        }

        public void Stop()
        {
            _stopAll = true;
            Console.WriteLine("Wating 5 min for all tasks");
            Task.WaitAll(_tasks, 5 * 60 * 1000);
        }
    }
}