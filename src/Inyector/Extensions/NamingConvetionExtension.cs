/*
 *  MIT License
    Copyright (c) 2017 David Revoledo

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    // Project Lead - David Revoledo davidrevoledo@d-genix.com
 */

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
        /// <param name="configuration"></param>
        /// <param name="assembly">Assembly to apply rule</param>
        /// <param name="inyectorMethod">
        ///     is the action to apply the inyector method, the first param is the implementation and the
        ///     second is the interface
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForNamingConvention(this InyectorConfiguration configuration,
            Assembly assembly,
            Action<Type, Type> inyectorMethod)
        {
            configuration.Rules.Add(new Rule
            {
                Assembly = assembly,
                Criteria = (t1, t2) => $"I{t1.Name}" == t2.Name,
                InyectorMethod = inyectorMethod
            });

            return configuration;
        }

        /// <summary>
        ///     Apply a Naming convention rule
        ///     example : ICarRepository match with CarRepository
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="inyectorMethod">
        ///     is the action to apply the inyector method, the first param is the implementation and the
        ///     second is the interface
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForNamingConvention(this InyectorConfiguration configuration,
            Action<Type, Type> inyectorMethod)
        {
            return AddRuleForNamingConvention(configuration, null, inyectorMethod);
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

        /// <summary>
        ///     Apply a Naming convention rule with an Assembly
        ///     example : ICarRepository match with CarRepository
        ///     and a pre configurated Inyector Mode
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="assembly">Assembly to apply rule</param>
        /// <param name="mode">
        ///     Mode to use to execute the inyection
        /// </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForNamingConvention(this InyectorConfiguration configuration,
            Assembly assembly,
            string mode)
        {
            configuration.Rules.Add(new Rule
            {
                Assembly = assembly,
                Criteria = (t1, t2) => $"I{t1.Name}" == t2.Name,
                InyectorMode = mode
            });

            return configuration;
        }

        /// <summary>
        ///     Apply a Naming convention rule
        ///     example : ICarRepository match with CarRepository
        ///     and a pre configurated Inyector Mode
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="mode"> Mode to use to execute the inyection </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForNamingConvention(this InyectorConfiguration configuration,
            string mode)
        {
            return AddRuleForNamingConvention(configuration, null, mode);
        }

        /// <summary>
        ///     Apply a Naming convention rule with an assembly
        ///     with ends with names example only inyect classes that finish with [Repository, Helper, Services, Factory, and so
        ///     on...]
        ///     example : ICarRepository match with CarRepository
        ///     and a pre configurated mode
        /// </summary>
        /// <param name="inyector"></param>
        /// <param name="assembly">Assembly to apply rule</param>
        /// <param name="names">names to find is the type name finish with</param>
        /// <param name="mode"> Mode to use to execute the inyection </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForEndsWithNamingConvention(this InyectorConfiguration inyector,
            Assembly assembly,
            IEnumerable<string> names,
            string mode)
        {
            inyector.Rules.Add(new Rule
            {
                Assembly = assembly,
                Criteria = (t1, t2) => names.Any(n => t1.Name.ToLower().EndsWith(n.ToLower())) &&
                                       $"I{t1.Name}" == t2.Name,
                InyectorMode = mode
            });

            return inyector;
        }

        /// <summary>
        ///     Apply a Naming convention rule
        ///     with ends with names example only inyect classes that finish with [Repository, Helper, Services, Factory, and so
        ///     on...]
        ///     example : ICarRepository match with CarRepository
        ///     and a pre configurated mode
        /// </summary>
        /// <param name="inyector"></param>
        /// <param name="names">names to find is the type name finish with</param>
        /// <param name="mode"> Mode to use to execute the inyection </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForEndsWithNamingConvention(this InyectorConfiguration inyector,
            IEnumerable<string> names,
            string mode)
        {
            return AddRuleForEndsWithNamingConvention(inyector, null, names, mode);
        }
    }
}