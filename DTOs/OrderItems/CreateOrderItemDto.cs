namespace BookStorePerfApi.DTOs.OrderItems
{
    public class CreateOrderItemDto
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

    }
}
