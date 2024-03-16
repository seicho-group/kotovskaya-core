namespace Kotovskaya.Order.Core.Interfaces
{
    public class OrderEntity
    {
        public required int Id { get; set; }
        public required string OrderNumber { get; set; }
        public int TotalPrice { get; set; } = 0;
        public required string Orderer { get; set; }
    }
}
