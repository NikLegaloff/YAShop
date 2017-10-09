using Topshelf;
using YAShop.BusinessLogic;

namespace YAShop.JobExecutor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Registry.Init(new ProgrCommonInfrProvider());
            HostFactory.Run(x =>
            {
                x.Service<JobExecutorService>(s =>
                {
                    s.ConstructUsing(name => new JobExecutorService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsNetworkService(); // you can setup any other credentials here
                x.SetServiceName("YAShopJobExec");
                x.SetDisplayName("YAShopJobExec");
            });
        }
    }
}