using Microsoft.Extensions.DependencyInjection;

namespace Inyector.AspNetCore
{
    public class AspNetCoreInyectorContext
    {
        public IServiceCollection ServiceCollection { get; set; }
    }
}