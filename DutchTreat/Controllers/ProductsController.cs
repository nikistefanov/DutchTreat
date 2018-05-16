using DutchTreat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DutchTreat.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public IActionResult Get()
        {
            try
            {
                return Ok(this.repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failder to get products: {ex}");

                return BadRequest("Failed to get products");
            }
        }
    }
}
