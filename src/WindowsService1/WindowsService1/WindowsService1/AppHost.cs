using System.ServiceProcess;
using Funq;
using ServiceStack;
using WindowsService1.ServiceInterface;
using WindowsService1.ServiceModel;
using ServiceStack.Caching;
using ServiceStack.Logging;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace WindowsService1
{
    public class AppHost : AppHostHttpListenerBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("WindowsService1", typeof(MyServices).Assembly)
        {
            LogManager.LogFactory = new ConsoleLogFactory();
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            string[] sentinelHosts = new[] { "127.0.0.1:21001", "127.0.0.1:21002", "127.0.0.1:21003" };
            var sentinel = new RedisSentinel(sentinelHosts, "mymaster");
            var redisManager = sentinel.Start();
            container.Register(redisManager);

            var cacheClient = container.Resolve<IRedisClientsManager>().GetCacheClient();
            container.Register<ICacheClient>(cacheClient);

            QueueNames.SetQueuePrefix("localhost.");
            var mqService = new RedisMqServer(container.Resolve<IRedisClientsManager>())
            {
                DisablePriorityQueues = true
            };
            container.Register<IMessageService>(mqService);
            container.Register(mqService.MessageFactory);

            mqService.RegisterHandler<Hello>(ServiceController.ExecuteMessage);
            
            mqService.Start();
        }
    }
}
