namespace TemperoDaVovo.Communications.Responses;

public class OrderResponseJson
{
    public Guid OrderId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public int ItemsCount { get; set; }

    public OrderResponseJson(Guid orderId, decimal subTotal, decimal total, int itemsCount)
    {
        OrderId = orderId;
        SubTotal = subTotal;
        Total = total;
        ItemsCount = itemsCount;
    }
}