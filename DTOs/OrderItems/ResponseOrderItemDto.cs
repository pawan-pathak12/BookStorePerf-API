namespace BookStorePerfApi.DTOs.OrderItems
{
    public class ResponseOrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }


    }
}
