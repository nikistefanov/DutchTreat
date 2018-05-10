using DutchTreat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public IActionResult Get()
        {
            try
            {
                return Ok(this.repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failder to get orders: {ex}");

                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = this.repository.GetOrderById(id);

                if (order != null) return Ok(order);
                else return NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failder to get orders: {ex}");

                return BadRequest("Failed to get orders");
            }
        }
    }
}
