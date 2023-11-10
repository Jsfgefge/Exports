using ExchangeRate;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ExchangeRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase 
    {
        public decimal ExchangeRate;
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            TipoCambioSoapClient client = new TipoCambioSoapClient(TipoCambioSoapClient.EndpointConfiguration.TipoCambioSoap12);

            await client.OpenAsync();

            var vars = await client.VariablesAsync(2);

            ExchangeRate = (decimal)vars.Body.VariablesResult.CambioDolar.ToList()[0].referencia;

            await client.CloseAsync();

            return Ok(vars.Body.VariablesResult.CambioDolar.ToList()[0].referencia);
        }
    }
}
