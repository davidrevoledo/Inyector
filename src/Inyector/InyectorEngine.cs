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
using System.Diagnostics;
using System.Linq;
using Inyector.Attributes;
using Inyector.Configurations;

namespace Inyector
{
    public static class InyectorEngine
    {
        /// <summary>
        ///     Proccess the inyector Engine
        /// </summary>
        /// <param name="configuration">inyector common configurations</param>
        internal static void Proccess(InyectorConfiguration configuration)
        {
            ProccessRules(configuration);

            ProccessAutoInyectedObjects(configuration);
        }

        /// <summary>
        ///     Proccess auto inyected object with attributes
        /// </summary>
        /// <param name="configuration">inyector common configurations</param>
        private static void ProccessAutoInyectedObjects(InyectorConfiguration configuration)
        {
            // first check if exist any declareted mode
            if (!InyectorContext.Modes.Any())
                return;

            var autoInyectedObjects = InyectorContext.ScanedTypes.Where(
                t => !t.IsInterface &&
                     !t.IsAbstract &&
                     t.GetCustomAttributes(
                             typeof(InyectAttribute),
                             true)
                         .Any());

            foreach (var candidate in autoInyectedObjects)
            {
                // get the attribute to get the meta data to inyect the object
                var attribute = candidate.GetCustomAttributes(typeof(InyectAttribute), true)
                    .FirstOrDefault();

                if (!(attribute is InyectAttribute))
                    continue;

                var inyectAttribute = (InyectAttribute)attribute;

                var mode = inyectAttribute.Mode ?? Mode.DefaultMode;

                // get the target mode 
                var targetMode = InyectorContext.Modes[mode];
                var target = inyectAttribute.AbstractType ?? candidate;

                // configure the target or the type itself
                targetMode?.InyectorMethod?.Invoke(candidate, target);

                TraceInyectOperation(configuration, candidate, target, mode);
            }
        }

        /// <summary>
        ///     Proccess Rules with configurations and scaned types from the assembly cache
        /// </summary>
        /// <param name="configuration">inyector common configurations</param>
        private static void ProccessRules(InyectorConfiguration configuration)
        {
            foreach (var rule in configuration.Rules.AsParallel())
            {
                // the target types to find
                var targets = new List<Type>();

                // add the assembly types
                if (rule.Assembly != null)
                    targets.AddRange(rule.Assembly.GetTypes());

                // add the scanned assemblies
                targets.AddRange(InyectorContext.ScanedTypes);

                // get interfaces to try match
                var interfaces = targets.Where(
                    t => t.IsInterface &&
                         !t.GetCustomAttributes(typeof(AvoidInyectorAttribute), true).Any());

                // get the candidates types to check the criteria logic
                var candidates = targets.Where(
                    t => !t.IsInterface &&
                         !t.IsAbstract &&
                         !t.GetCustomAttributes(typeof(AvoidInyectorAttribute), true).Any());

                foreach (var target in interfaces)
                {
                    var candidate = candidates.FirstOrDefault(c => rule.Criteria(c, target));

                    if (candidate == null)
                        continue;

                    // find method or mode to execute the inyection
                    var method = rule.InyectorMethod;
                    var methodName = string.Empty;

                    if (method == null)
                    {
                        InyectorContext.Modes.TryGetValue(rule.InyectorMode, out var mode);
                        methodName = mode?.ToString();
                        method = mode?.InyectorMethod;
                    }

                    method?.Invoke(candidate, target);
                    TraceInyectOperation(configuration, candidate, target, methodName);
                }
            }
        }

        /// <summary>
        ///     Trace inyect operations
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="type"></param>
        /// <param name="targetType"></param>
        /// <param name="mode"></param>
        private static void TraceInyectOperation(InyectorConfiguration configuration, Type type, Type targetType,
            string mode)
        {
            if (!configuration.EnableTracing)
                return;

            //  trace inyector activity
            var tracing = configuration.Log ??
                          ((from, target) =>
                          {
                              Trace.TraceInformation(
                                  $"Injector : registering from {type} to {targetType} with {mode} Mode");
                          });

            tracing.Invoke(type, targetType);
        }
    }
}