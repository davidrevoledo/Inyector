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
using Microsoft.Extensions.DependencyInjection;

namespace Inyector.AspNetCore
{
    /// <summary>
    ///     AspNetCore Mode Factory
    /// </summary>
    public abstract class AspNetCoreModeFactory
    {
        /// <summary>
        ///     Create an Inyector Mode from a AspNetCore ServiceLifetime
        /// </summary>
        /// <param name="lifetime">lifetime to create the mode</param>
        /// <param name="services">Asp.Net Core Service inyection</param>
        /// <returns>An Instance of Inyector Mode</returns>
        public static Mode Create(ServiceLifetime lifetime, IServiceCollection services)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    return new Mode
                    {
                        InyectorMethod =
                            (type, inter) => services.AddScoped(inter, type),
                        Name = lifetime.ToString()
                    };

                case ServiceLifetime.Singleton:
                    return new Mode
                    {
                        InyectorMethod =
                            (type, inter) => services.AddSingleton(inter, type),
                        Name = lifetime.ToString()
                    };

                case ServiceLifetime.Transient:
                    return new Mode
                    {
                        InyectorMethod =
                            (type, inter) => services.AddTransient(inter, type),
                        Name = lifetime.ToString()
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }
    }
}