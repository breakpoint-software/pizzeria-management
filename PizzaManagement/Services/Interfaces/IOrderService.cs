using Models.Domain;

namespace Services.Services
{
    public interface IOrderService
    {

        Task<OrderResponse> PlaceOrder(OrderRequest order);

        Task<OrderResponse> GetNextOrder();
        Task UpdateStatus(int orderId, OrderStatus completed);
    }

}
