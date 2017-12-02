using System.Collections.Generic;
using AspNetCoreMiddlewareSample.Models;

namespace AspNetCoreMiddlewareSample.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> Get();
    }
}