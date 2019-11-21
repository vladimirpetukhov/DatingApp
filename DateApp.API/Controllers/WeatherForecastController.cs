namespace DateApp.API.Controllers {
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System;
    using Data;
    using DateApp.API.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    [Authorize]
    [ApiController]
    [Route ("[controller]")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new [] {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DataContext _context;
        public WeatherForecastController (
            ILogger<WeatherForecastController> logger,
            DataContext context) {
            _logger = logger;
            this._context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Value>> Get () {
            var values = await this._context.Values.ToListAsync ();
            return values;

        }

    }
}