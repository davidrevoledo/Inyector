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
using System.Reflection;
using Inyector.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Inyector
{
    /// <summary>
    ///     Rule Extensions
    /// </summary>
    public static class RulesExtensions
    {
        /// <summary>
        ///     Add Rule from LifeTime using the default models and a target Assembly
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="assembly">Assembly to apply the rule</param>
        /// <param name="criteria">Criteria to find the types</param>
        /// <param name="lifetime">lifetime</param>
        /// <returns></returns>
        public static InyectorConfiguration AddRule(this InyectorConfiguration configuration,
            Assembly assembly,
            Func<Type, Type, bool> criteria,
            ServiceLifetime lifetime)
        {
            configuration.AddRule(assembly, criteria, lifetime.ToString());

            return configuration;
        }

        /// <summary>
        ///     Add Rule from LifeTime using the default models
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="criteria">Criteria to find the types</param>
        /// <param name="lifetime">lifetime</param>
        /// <returns></returns>
        public static InyectorConfiguration AddRule(this InyectorConfiguration configuration,
            Func<Type, Type, bool> criteria,
            ServiceLifetime lifetime)
        {
            return AddRule(configuration, null, criteria, lifetime);
        }
    }
}