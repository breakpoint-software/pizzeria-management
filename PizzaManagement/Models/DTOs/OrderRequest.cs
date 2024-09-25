namespace Models.Domain
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderDetailRequest
    {
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
    }

}
