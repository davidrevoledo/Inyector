using System.Collections.Generic;
using AspNetWebAppSample.Models;

namespace AspNetWebAppSample.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> Get();
    }
}