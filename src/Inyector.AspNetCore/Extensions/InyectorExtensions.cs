using System;
using Microsoft.Extensions.DependencyInjection;
using Inyector.Configurations;
using Inyector;
using Inyector.AspNetCore;

namespace Microsoft.Extensions
{
    /// <summary>
    /// AspNet Core Service Extension
    /// </summary>
    public static class InyectorExtensions
    {
        public static IServiceCollection UseInjector(this IServiceCollection services,
            Action<InyectorConfiguration> configurationAction)
        {

            // Call Inyector Startup
            InyectorStartup.Init((c) =>
            {
                // add default modes
                c.AddMode(AspNetCoreModeFactory.Create(ServiceLifetime.Scoped, services))
                    .AddMode(AspNetCoreModeFactory.Create(ServiceLifetime.Singleton, services))
                    .AddMode(AspNetCoreModeFactory.Create(ServiceLifetime.Transient, services));

                configurationAction.Invoke(c);
            });

            return services;
        }
    }
}