using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Services;

namespace PizzaManagement.Controllers
{
    [ApiController]
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private IOrderService _orderService;
        private readonly PizzaManagementDbContext context;

        public MenuController(IOrderService orderService, PizzaManagementDbContext context)
        {
            _orderService = orderService;
            this.context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetMenu()
        {
            var pizzas = context.Pizzas.ToList();
            return Ok(new
            {
                Pizzas = pizzas
            });
        }

    }

}
