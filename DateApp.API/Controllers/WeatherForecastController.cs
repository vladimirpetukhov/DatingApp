namespace DateApp.API.Controllers
{
    using Data;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using DateApp.API.Models;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DataContext _context;
        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            DataContext context)
        {
            _logger = logger;
            this._context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Value>> Get()
        {
            var values = await this._context.Values.ToListAsync();
            return values;

        }

        
    }
}
