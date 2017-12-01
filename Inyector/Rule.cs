using System;
using System.Reflection;

namespace Inyector
{
    internal class Rule
    {
        public Assembly Assembly { get; set; }

        public Func<Type, Type, bool> Criteria { get; set; }

        public Action<Type, Type> InyectorMethod { get; set; }
    }
}