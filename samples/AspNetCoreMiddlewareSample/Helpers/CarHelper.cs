using System.Collections.Generic;
using AspNetCoreMiddlewareSample.Models;
using Inyector.Attributes;

namespace AspNetCoreMiddlewareSample.Helpers
{
    [Inyect(typeof(IFooHelper))]
    public class CarHelper : IFooHelper
    {
        public IEnumerable<Car> Cars => new List<Car>
        {
            new Car
            {
                Color = "red",
                Model = "ak-1",
                Year = 2017
            },
            new Car
            {
                Color = "black",
                Model = "aj-1",
                Year = 2015
            }
        };
    }
}