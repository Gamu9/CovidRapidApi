using CovidRapidApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CovidRapidApi.Controllers
{
    [Route("api/v1/continents")]
    [ApiController]
    public class ContinentsController : ControllerBase
    {
        private readonly IContinentsService _continentsService;
        public ContinentsController(IContinentsService continentsService)
        {
            _continentsService = continentsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var info = await _continentsService.GetContinentData();
                return Ok(info);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
