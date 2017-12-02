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
using System.Reflection;

namespace Inyector.Configurations
{
    public class InyectorConfiguration
    {
        internal List<Assembly> Assemblies = new List<Assembly>();
        internal List<Rule> Rules { get; set; } = new List<Rule>();
        internal List<Mode> Modes { get; set; } = new List<Mode>();

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
        ///     Add New Rule with target assembly and a preconfigured mode
        /// </summary>
        /// <param name="assembly">Assembly to apply rule</param>
        /// <param name="criteria">
        ///     Criteria to is the func to check if a type match to other to be implemented the first param is
        ///     the implementation and the second is the interface
        /// </param>
        /// <param name="mode"> Mode to use to execute the inyection </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public InyectorConfiguration AddRule(Assembly assembly,
            Func<Type, Type, bool> criteria,
            string mode)
        {
            Rules.Add(new Rule
            {
                Assembly = assembly,
                Criteria = criteria,
                InyectorMode = mode
            });

            return this;
        }

        /// <summary>
        ///     Add New Rule with a preconfigured mode
        /// </summary>
        /// <param name="criteria">
        ///     Criteria to is the func to check if a type match to other to be implemented the first param is
        ///     the implementation and the second is the interface
        /// </param>
        /// <param name="mode"> Mode to use to execute the inyection </param>
        /// <returns>a instance of IInyectorConfiguration</returns>
        public InyectorConfiguration AddRule(Func<Type, Type, bool> criteria,
            string mode)
        {
            return AddRule(null, criteria, mode);
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