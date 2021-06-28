using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidRapidApi.Domain.Services;

namespace CovidRapidApi.Controllers
{
    [Route("api/v1/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            try
            {
                var info = await _countryService.GetCountriesData();
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
