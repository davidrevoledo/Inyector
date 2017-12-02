using AspNetWebAppSample.Helpers;
using AspNetWebAppSample.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWebAppSample.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IFooHelper _fooHelper;

        public CarsController(ICarRepository carRepository,
            IFooHelper fooHelper)
        {
            _carRepository = carRepository;
            _fooHelper = fooHelper;
        }

        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            return Json(_carRepository.Get());
        }
    }
}