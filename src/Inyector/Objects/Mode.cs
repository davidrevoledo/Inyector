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

namespace Inyector
{
    /// <summary>
    ///     Inyector Mode
    /// </summary>
    public class Mode
    {
        /// <summary>
        ///     Default Mode Key
        /// </summary>
        public const string DefaultMode = "Default";

        /// <summary>
        ///     Mode Key
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        ///     Action Delegate to execute the inyection engine
        ///     the first param is the implementation and the
        ///     second is the interface
        /// </summary>
        public Action<Type, Type> InyectorMethod { get; set; }
    }
}