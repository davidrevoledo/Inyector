using AspNetWebAppSample.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWebAppSample.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            return Json(_carRepository.Get());
        }
    }
}