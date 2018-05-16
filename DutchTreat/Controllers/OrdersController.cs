using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DutchTreat.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;

        public OrdersController(
            IDutchRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var results = this.repository.GetAllOrders(includeItems);
                return Ok(this.mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
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

                if (order != null) return Ok(this.mapper.Map<Order, OrderViewModel>(order));
                else return NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failder to get orders: {ex}");

                return BadRequest("Failed to get orders");
            }
        }

        // Using [FromBody] will take options not from the query string
        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = this.mapper.Map<OrderViewModel, Order>(model);

                    // If the user is not specified the date, then use current time
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    this.repository.AddEntity(newOrder);

                    if (this.repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", this.mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to save a new order: {ex}");
            }

            return BadRequest("Failed to save new order");
        }
    }
}
