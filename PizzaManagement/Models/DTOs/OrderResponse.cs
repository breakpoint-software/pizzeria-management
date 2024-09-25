namespace Models.Domain
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; }
    }
    public class OrderDetailResponse
    {

        public string Pizza { get; set; }
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
    }
}
