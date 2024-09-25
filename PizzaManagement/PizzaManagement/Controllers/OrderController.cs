using Microsoft.AspNetCore.Mvc;
using Models.Domain;
using Services.Services;

namespace PizzaManagement.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            try
            {
                var order = await _orderService.PlaceOrder(request);
                return Ok(new
                {
                    OrderId = order.Id,
                    Message = "Order created successfully",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("next")]
        public async Task<IActionResult> GetNextOrder()
        {
            var order = await _orderService.GetNextOrder();
            if (order == null)
            {
                return NotFound("No orders found");
            }

            return Ok(order);
        }

        [HttpGet("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                await _orderService.UpdateStatus(id, OrderStatus.Completed);
                return Ok("Order completed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
