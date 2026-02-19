namespace BookStorePerfApi.DTOs.Orders
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        //      public IEnumerable<OrderItem>? OrderItems { get; set; }
    }
}
