using System.Collections.Generic;
using AspNetWebAppSample.Models;
using Inyector.Attributes;

namespace AspNetWebAppSample.Helpers
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