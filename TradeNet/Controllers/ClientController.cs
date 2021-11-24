using Microsoft.AspNetCore.Mvc;
using TradeNet.Application.Abstractions;

namespace TradeNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IExchangeClient _exchangeClient;

        public ClientController(IExchangeClient exchangeClient)
        {
            _exchangeClient = exchangeClient ?? throw new ArgumentNullException(nameof(exchangeClient));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTicker()
        {
            var res = await _exchangeClient.GetTickerAsync();
            return Ok(res);
        }
    }
}
