using System;
using Inyector.AspNetCore.Configurations;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions
{
    public static class InyectorExtensions
    {
        public static IServiceCollection UseInjector(this IServiceCollection services,
            Action<InyectorConfigurations> configurationAction)
        {
            var configurations = new InyectorConfigurations();

            configurationAction.Invoke(configurations);

            var context = InyectorContextFactory.Init(services);

            foreach (var rule in configurations.Rules)
                context.AddRule(rule);

            context.Proccess();

            return services;
        }
    }
}