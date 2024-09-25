using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Domain;
namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly PizzaManagementDbContext _context;
        private readonly IMapper mapper;

        public OrderService(PizzaManagementDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<OrderResponse> PlaceOrder(OrderRequest order)
        {
            if (order.OrderDetails.Count == 0)
                throw new ArgumentException("Send at least one pizza");

            var newOrder = mapper.Map<Order>(order);
            newOrder.CreatedDate = DateTime.Now;
            newOrder.OrderStatus = OrderStatus.Pending;

            newOrder.OrderDetails.ForEach(e =>
            {
                var pizza = _context.Pizzas.Where(p => p.Id == e.PizzaId).FirstOrDefault();

                if (pizza == null)
                {
                    throw new ArgumentException("Pizza not found");
                }
                e.Pizza = pizza;
                e.Quantity = e.Quantity;
                e.Price = e.Quantity * pizza.Price;
                e.CreatedDate = DateTime.Now;

                order.TotalPrice += e.Quantity * pizza.Price;
            });

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return mapper.Map<OrderResponse>(newOrder);
        }

        public async Task<OrderResponse> GetNextOrder()
        {
            var order = await _context.Orders.Include(e => e.OrderDetails).ThenInclude(e => e.Pizza).Where(e => e.OrderStatus == OrderStatus.Pending).OrderBy(e => e.CreatedDate).FirstOrDefaultAsync();

            return mapper.Map<OrderResponse>(order);
        }

        public async Task UpdateStatus(int orderId, OrderStatus status)
        {
            var order = _context.Orders.Where(e => e.Id == orderId).FirstOrDefault();
            if (order == null)
            {
                throw new ArgumentException("Invalid Id");
            }
            order.OrderStatus = status;
            await _context.SaveChangesAsync();
        }
    }

}
