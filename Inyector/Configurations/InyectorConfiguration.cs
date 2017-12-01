using System;
using System.Collections.Generic;
using System.Reflection;

namespace Inyector.Configurations
{
    public class InyectorConfiguration
    {
        internal List<Rule> Rules { get; set; } = new List<Rule>();

        public InyectorConfiguration AddAssemblyRule(Assembly assembly,
            Func<Type, Type, bool> criteria,
            Action<Type, Type> inyectorMethod)
        {
            Rules.Add(new Rule
            {
                Assembly = assembly,
                Criteria = criteria,
                InyectorMethod = inyectorMethod
            });

            return this;
        }
    }
}
