using System;
using System.Linq;
using Inyector.Attributes;
using Inyector.Configurations;

namespace Inyector
{
    public static class InyectorStartup
    {
        public static void Init(Action<InyectorConfiguration> action)
        {
            var configuration = new InyectorConfiguration();
            action.Invoke(configuration);

            Proccess(configuration);
        }

        private static void Proccess(InyectorConfiguration configuration)
        {
            foreach (var rule in configuration.Rules)
            {
                var types = rule.Assembly.GetTypes();

                var interfaces = types.Where(t => t.IsInterface &&
                                                  !t.GetCustomAttributes(typeof(AvoidInyectorAttribute), true).Any());

                var candidates = types.Where(t => !t.IsInterface &&
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