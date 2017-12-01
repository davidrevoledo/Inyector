using System;
using System.Reflection;
using Inyector.Abstractions;

namespace Inyector.AspNetCore.Rules
{
    public class NamingConventionRule : IInyectorRule
    {
        private readonly InyectorType _inyectorType;
        private Assembly _assembly;
        private AspNetCoreInyectorContext _context;
        private string[] _conventions;

        public NamingConventionRule(Assembly assembly, InyectorType inyectorType, params string[] conventions)
        {
            _assembly = assembly;

            _inyectorType = inyectorType;
            _conventions = conventions;
        }

        public AspNetCoreInyectorContext Context
        {
            set => _context = value;
        }

        public virtual void PerformInyect(Type a)
        {
            var requestedType = a.Name; // example FooRepository then find IFooRepository
            var target = _work.Assembly.GetTypes()
                .FirstOrDefault(t => t.Name.ToLower() == $"i{requestedType.ToLower()}");

            if (target == null)
                return;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_inyectorType)
            {
                case InyectorType.Singleton:
                    _context.ServiceCollection.AddSingleton(target, a);
                    break;

                case InyectorType.Scoped:
                    _context.ServiceCollection.AddScoped(target, a);
                    break;

                case InyectorType.Transient:
                    _context.ServiceCollection.AddTransient(target, a);
                    break;
            }
        }
    }
}