﻿using System.Collections.Generic;
using AspNetCoreMiddlewareSample.Models;

namespace AspNetCoreMiddlewareSample.Repositories
{
    public class CarRepository : ICarRepository
    {
        public IEnumerable<Car> Get()
        {
            return new List<Car>
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
}