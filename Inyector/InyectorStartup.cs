using System;
using System.Collections.Generic;
using System.Linq;
using Inyector.Attributes;
using Inyector.Configurations;

namespace Inyector
{
    /// <summary>
    ///     Inyector Startup
    /// </summary>
    public static class InyectorStartup
    {
        /// <summary>
        ///     Init Inyector
        /// </summary>
        /// <param name="action">the method to configure the inyector </param>
        public static void Init(Action<InyectorConfiguration> action)
        {
            var configuration = new InyectorConfiguration();

            action.Invoke(configuration);

            Proccess(configuration);
        }

        /// <summary>
        ///     Proccess the rules
        /// </summary>
        /// <param name="configuration">the pre configured configuration to run Inyector Engine</param>
        private static void Proccess(InyectorConfiguration configuration)
        {
            //cached assemblies for all rules
            var scanedAssemblies = configuration.Assemblies.SelectMany(t => t.GetTypes());

            foreach (var rule in configuration.Rules)
            {
                // the target types to find
                var target = new List<Type>();

                // add the assembly types
                if (rule.Assembly != null)
                    target.AddRange(rule.Assembly.GetTypes());

                // add the scanned assemblies
                target.AddRange(scanedAssemblies);

                // get interfaces to try match
                var interfaces = target.Where(t => t.IsInterface &&
                                                   !t.GetCustomAttributes(typeof(AvoidInyectorAttribute), true).Any());

                // get the candidates types to check the criteria logic
                var candidates = target.Where(t => !t.IsInterface &&
                                                   !t.IsAbstract &&
                                                   !t.GetCustomAttributes(typeof(AvoidInyectorAttribute), true).Any())
                    .ToList();

                foreach (var @interface in interfaces)
                {
                    var candidate = candidates.FirstOrDefault(c => rule.Criteria(c, @interface));

                    if (candidate != null)
                        rule.InyectorMethod.Invoke(candidate, @interface);
                }
            }
        }
    }
}