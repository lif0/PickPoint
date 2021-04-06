using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.DataLayer.Models;
using Shop.DataLayer.Repositories.Abstracts;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly IOrderRepository _orderRepository;
        private readonly IPostamatRepository _postamatRepository;
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IOrderRepository orderRepository, IPostamatRepository postamatRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _postamatRepository = postamatRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpPost]
        public IActionResult Create(Order model)
        {
            _orderRepository.Create(model);
            return this.Ok();
        }
        
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            return this.Ok(_orderRepository.FindById(id));
        }
    }
}