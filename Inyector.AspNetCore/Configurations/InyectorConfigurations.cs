using System.Collections.Generic;
using Inyector.Abstractions;

namespace Inyector.AspNetCore.Configurations
{
    public class InyectorConfigurations
    {
        public IList<IInyectorRule> Rules { get; set; } = new List<IInyectorRule>();
    }
}