using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Inyector.Configurations;

namespace Inyector
{
    public static class NamingConvetionExtension
    {
        /// <summary>
        ///     Apply a Naming convention rule with an Assembly
        ///     example : ICarRepository match with CarRepository
        /// </summary>
        /// <param name="inyector"></param>
        /// <param name="assembly">Assembly to apply rule</param>
        /// <param name="inyectorMethod">
        ///     is the action to apply the inyector method, the first param is the implementation and the
        ///     second is the interface
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForNamingConvention(this InyectorConfiguration inyector,
            Assembly assembly,
            Action<Type, Type> inyectorMethod)
        {
            inyector.Rules.Add(new Rule
            {
                Assembly = assembly,
                Criteria = (t1, t2) => $"I{t1.Name}" == t2.Name,
                InyectorMethod = inyectorMethod
            });

            return inyector;
        }

        /// <summary>
        ///     Apply a Naming convention rule
        ///     example : ICarRepository match with CarRepository
        /// </summary>
        /// <param name="inyector"></param>
        /// <param name="inyectorMethod">
        ///     is the action to apply the inyector method, the first param is the implementation and the
        ///     second is the interface
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForNamingConvention(this InyectorConfiguration inyector,
            Action<Type, Type> inyectorMethod)
        {
            return AddRuleForNamingConvention(inyector, null, inyectorMethod);
        }

        /// <summary>
        ///     Apply a Naming convention rule with an assembly
        ///     with ends with names example only inyect classes that finish with [Repository, Helper, Services, Factory, and so
        ///     on...]
        ///     example : ICarRepository match with CarRepository
        /// </summary>
        /// <param name="inyector"></param>
        /// <param name="assembly">Assembly to apply rule</param>
        /// <param name="names">names to find is the type name finish with</param>
        /// <param name="inyectorMethod">
        ///     is the action to apply the inyector method, the first param is the implementation and the
        ///     second is the interface
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForEndsWithNamingConvention(this InyectorConfiguration inyector,
            Assembly assembly,
            IEnumerable<string> names,
            Action<Type, Type> inyectorMethod)
        {
            inyector.Rules.Add(new Rule
            {
                Assembly = assembly,
                Criteria = (t1, t2) => names.Any(n => t1.Name.ToLower().EndsWith(n.ToLower())) &&
                                       $"I{t1.Name}" == t2.Name,
                InyectorMethod = inyectorMethod
            });

            return inyector;
        }

        /// <summary>
        ///     Apply a Naming convention rule
        ///     with ends with names example only inyect classes that finish with [Repository, Helper, Services, Factory, and so
        ///     on...]
        ///     example : ICarRepository match with CarRepository
        /// </summary>
        /// <param name="inyector"></param>
        /// <param name="names">names to find is the type name finish with</param>
        /// <param name="inyectorMethod">
        ///     is the action to apply the inyector method, the first param is the implementation and the
        ///     second is the interface
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForEndsWithNamingConvention(this InyectorConfiguration inyector,
            IEnumerable<string> names,
            Action<Type, Type> inyectorMethod)
        {
            return AddRuleForEndsWithNamingConvention(inyector, null, names, inyectorMethod);
        }
    }
}