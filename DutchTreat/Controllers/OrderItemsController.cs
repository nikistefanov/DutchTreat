using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Controllers
{
    [Route("api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(
            IDutchRepository repository,
            ILogger<OrderItemsController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IActionResult Get(int orderId)
        {
            var order = this.repository.GetOrderById(orderId);
            if (order != null) return Ok(this.mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));

            return NotFound();
        }

        // {id} is mapped by name with the second parameter
        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = this.repository.GetOrderById(orderId);

            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();

                if (item != null) return Ok(this.mapper.Map<OrderItem, OrderItemViewModel>(item));
            }

            return NotFound();
        }
    }
}
