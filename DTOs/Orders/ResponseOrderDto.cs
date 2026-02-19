namespace BookStorePerfApi.DTOs.Orders
{
    public class ResponseOrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
