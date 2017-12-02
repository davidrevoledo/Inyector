using System.Collections.Generic;
using AspNetCoreMiddlewareSample.Models;

namespace AspNetCoreMiddlewareSample.Helpers
{
    public interface IFooHelper
    {
        IEnumerable<Car> Cars { get; }
    }
}