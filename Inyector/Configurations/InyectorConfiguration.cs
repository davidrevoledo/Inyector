using System;
using System.Collections.Generic;
using System.Reflection;

namespace Inyector.Configurations
{
    public class InyectorConfiguration
    {
        internal List<Assembly> Assemblies = new List<Assembly>();
        internal List<Rule> Rules { get; set; } = new List<Rule>();

        /// <summary>
        ///     Add New Rule with target assembly
        /// </summary>
        /// <param name="assembly">Assembly to apply rule</param>
        /// <param name="criteria">
        ///     Criteria to is the func to check if a type match to other to be implemented the first param is
        ///     the implementation and the second is the interface
        /// </param>
        /// <param name="inyectorMethod">
        ///     is the action to apply the inyector method, the first param is the implementation and the
        ///     second is the interface
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public InyectorConfiguration AddRule(Assembly assembly,
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

        /// <summary>
        ///     Add New Rule
        /// </summary>
        /// <param name="criteria">
        ///     Criteria to is the func to check if a type match to other to be implemented the first param is
        ///     the implementation and the second is the interface
        /// </param>
        /// <param name="inyectorMethod">
        ///     is the action to apply the inyector method, the first param is the implementation and the
        ///     second is the interface
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public InyectorConfiguration AddRule(Func<Type, Type, bool> criteria,
            Action<Type, Type> inyectorMethod)
        {
            return AddRule(null, criteria, inyectorMethod);
        }

        /// <summary>
        ///     Scan Assembly to apply all rules
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public InyectorConfiguration Scan(Assembly assembly)
        {
            Assemblies.Add(assembly);
            return this;
        }
    }
}