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