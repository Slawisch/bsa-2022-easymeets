﻿using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace EasyMeets.Core.BLL.Services.Quartz
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (_serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob)!;
        }

        public void ReturnJob(IJob job) { }
    }
}
