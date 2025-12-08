using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleConsumer.Services.Interfaces;

namespace SaleConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ISaleService _saleService;

        public SaleController(ILogger<SaleController> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<ActionResult> SaleConsumer()
        {
            await _saleService.CreateSaleAsync();
            return Ok();
        }
    }
}
