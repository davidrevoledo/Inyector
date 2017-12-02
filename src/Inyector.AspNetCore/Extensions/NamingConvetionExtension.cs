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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Inyector.Configurations;
using Microsoft.Extensions.DependencyInjection;

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
        /// <param name="lifetime">AspNet Core mode to inyect </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForNamingConvention(this InyectorConfiguration configuration,
            Assembly assembly,
            ServiceLifetime lifetime)
        {
            configuration.AddRule(assembly, (t1, t2) => $"I{t1.Name}" == t2.Name, lifetime.ToString());

            return configuration;
        }

        /// <summary>
        ///     Apply a Naming convention rule
        ///     example : ICarRepository match with CarRepository
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="lifetime">AspNet Core mode to inyect </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForNamingConvention(this InyectorConfiguration configuration,
            ServiceLifetime lifetime)
        {
            return AddRuleForNamingConvention(configuration, null, lifetime);
        }

        /// <summary>
        ///     Apply a Naming convention rule with an assembly
        ///     with ends with names example only inyect classes that finish with [Repository, Helper, Services, Factory, and so
        ///     on...]
        ///     example : ICarRepository match with CarRepository
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="assembly">Assembly to apply rule</param>
        /// <param name="names">names to find is the type name finish with</param>
        /// <param name="lifetime">AspNet Core mode to inyect </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForEndsWithNamingConvention(this InyectorConfiguration configuration,
            Assembly assembly,
            IEnumerable<string> names,
            ServiceLifetime lifetime)
        {
            configuration.AddRule(assembly, (t1, t2) => names.Any(n => t1.Name.ToLower().EndsWith(n.ToLower())) &&
                                                        $"I{t1.Name}" == t2.Name,
                lifetime.ToString());

            return configuration;
        }

        /// <summary>
        ///     Apply a Naming convention rule
        ///     with ends with names example only inyect classes that finish with [Repository, Helper, Services, Factory, and so
        ///     on...]
        ///     example : ICarRepository match with CarRepository
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="names">names to find is the type name finish with</param>
        /// <param name="lifetime">AspNet Core mode to inyect </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public static InyectorConfiguration AddRuleForEndsWithNamingConvention(this InyectorConfiguration configuration,
            IEnumerable<string> names,
            ServiceLifetime lifetime)
        {
            return AddRuleForEndsWithNamingConvention(configuration, null, names, lifetime);
        }
    }
}