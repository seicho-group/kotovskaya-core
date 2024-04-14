namespace Kotovskaya.Order.Core.Interfaces
{
    public class OrderEntity
    {
        public required Guid Id { get; set; }
        public required string OrderNumber { get; set; }
        public int TotalPrice { get; set; } = 0;
        public required Guid OrdererId { get; set; }
    }
}
