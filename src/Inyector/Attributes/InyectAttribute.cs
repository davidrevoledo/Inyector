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

namespace Inyector.Attributes
{
    /// <summary>
    ///     Attribute to define the Abstract class to be auto implemented
    ///     How to implement the Inyection is defined by Inyector Modes, "Default" is the default mode.
    ///     If no mode is implemented then the Engine won't do nothing
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InyectAttribute : Attribute
    {
        /// <summary>
        ///     Constructor with default mode
        /// </summary>
        /// <param name="abstractType">the type of the interface</param>
        public InyectAttribute(Type abstractType)
        {
            AbstractType = abstractType;
        }

        /// <summary>
        ///     Default constuctor where the target it the same class
        /// </summary>
        public InyectAttribute()
        {
        }

        /// <summary>
        ///     Constructor with declared mode
        /// </summary>
        /// <param name="abstractType">the type of the interface</param>
        /// <param name="mode">the inyector mode</param>
        public InyectAttribute(Type abstractType, string mode)
            : this(abstractType)
        {
            Mode = mode;
        }

        public string Mode { get; }

        public Type AbstractType { get; }
    }
}