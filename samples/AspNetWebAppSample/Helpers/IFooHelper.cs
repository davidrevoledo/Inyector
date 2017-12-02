using System.Collections.Generic;
using AspNetWebAppSample.Models;

namespace AspNetWebAppSample.Helpers
{
    public interface IFooHelper
    {
        IEnumerable<Car> Cars { get; }
    }
}