using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Domain.Entities;

public class Order : BaseEntity
{
    public Guid RestaurantId { get; private set; }

    // Customer info
    public int OrderNumber { get; private set; }
    public string ClientSessionId { get; private set; } = string.Empty;
    public string CustomerName { get; private set; } = string.Empty;
    public string CustomerPhone { get; private set; } = string.Empty;

    //Status
    public OrderStatus Status { get; private set; }
    public CanceledBy? CanceledBy { get; private set; }
    public CancellationReasonType? CancellationReasonType { get; private set; }
    public string? CancellationDescription { get; private set; }
    public DateTime? CanceledAt { get; private set; }
    public DateTime? PreparingStartedAt { get; private set; }
    public DateTime? OnTheWayAt { get; private set; }
    public DateTime? ReadyAt { get; private set; }


    // Price
    public decimal? DeliveryFee { get; set; }
    public decimal SubTotal { get; private set; }
    public decimal Total { get; private set; }

    // Address
    public DeliveryMode DeliveryMode { get; set; }
    public string? Street { get; private set; }
    public string? Number { get; private set; }
    public string? Complement { get; private set; }
    public string? Reference { get; private set; }
    public int EstimatedDeliveryTimeInMinutes { get; private set; }
    public Guid? NeighborhoodId { get; private set; }
    public Neighborhood? Neighborhood { get; private set; }
    public string? City { get; private set; }
    public AddressName? AddressName { get; private set; }

    // Payment
    public Guid? PaymentId { get; private set; }
    public Payment? Payment { get; private set; }

    public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();

    protected Order()
    {
    }

    public Order(Guid restaurantId, string clientSessionId, string customerName, string customerPhone)
    {
        Id = Guid.NewGuid();
        RestaurantId = restaurantId;
        ClientSessionId = clientSessionId;
        CustomerName = customerName;
        CustomerPhone = customerPhone;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Draft;
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }

    public void CalculateTotals()
    {
        SubTotal = Items.Sum(i => i.TotalPrice);
        Total = SubTotal + (DeliveryFee ?? 0);
    }

    private void RecalculateTotal()
    {
        Total = SubTotal + (DeliveryFee ?? 0);
    }

    public void RemoveItemAndRecalculate(Guid orderItemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == orderItemId);
        if (item != null)
            Items.Remove(item);

        CalculateTotals();
    }

    public void RemoveAllOrderItems(Guid orderId)
    {
        var item = Items.Where(i => i.OrderId == orderId).ToList();
        item.ForEach(i => Items.Remove(i));
    }

    public void CompleteCheckout(string customerName, string customerPhone)
    {
        CustomerName = customerName;
        CustomerPhone = customerPhone;
    }

    public void OrderAddress(string street, string number, string complement, string reference, Guid? neighborhoodId,
        string city, AddressName? addressName)
    {
        Street = street;
        Number = number;
        Complement = complement;
        Reference = reference;
        NeighborhoodId = neighborhoodId;
        City = city;
        AddressName = addressName;
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }

    public void SetDeliveryFee(decimal deliveryFee)
    {
        if (deliveryFee < 0)
            throw new ArgumentException("Taxa de entrega inválida.");

        DeliveryFee = deliveryFee;
        RecalculateTotal();
    }

    public void SetPayment(Guid paymentId)
    {
        PaymentId = paymentId;
    }

    public void SetOrderNumber(int number)
    {
        OrderNumber = number;
    }

    public void SetEstimatedDeliveryTimeInMinutes(int minutes)
    {
        EstimatedDeliveryTimeInMinutes = minutes;
    }

    public void Cancel(CancellationReasonType reason, CanceledBy canceledBy, string? description = null)
    {
        if (!CanBeCanceled())
            throw new DomainException(["Pedido não pode ser cancelado."]);

        if (canceledBy == Enums.CanceledBy.Customer && !IsCustomerReason(reason))
            throw new DomainException(["Motivo inválido para cliente."]);

        if (canceledBy == Enums.CanceledBy.Restaurant && !IsRestaurantReason(reason))
            throw new DomainException(["Motivo inválido para restaurante."]);

        if (Status == OrderStatus.Canceled)
            throw new DomainException(["Pedido já está cancelado."]);

        if (reason == Enums.CancellationReasonType.DelayedOrder)
            if (!IsOrderReallyDelayed())
                throw new DomainException(["Tempo estimado de entrega ainda não foi ultrapassado."]);

        CancellationDescription = description;
        CancellationReasonType = reason;
        CanceledBy = canceledBy;
        CanceledAt = DateTime.UtcNow;
        Status = OrderStatus.Canceled;
    }

    private bool IsCustomerReason(CancellationReasonType reason)
    {
        return reason is Enums.CancellationReasonType.WrongAddress
            or Enums.CancellationReasonType.ChangedMind
            or Enums.CancellationReasonType.OrderMistake
            or Enums.CancellationReasonType.DelayTooLong
            or Enums.CancellationReasonType.PaymentIssue
            or Enums.CancellationReasonType.HighDeliveryFee
            or Enums.CancellationReasonType.DelayedOrder;
    }

    private bool CanBeCanceled()
    {
        return Status != OrderStatus.Ready
               && Status != OrderStatus.Canceled
               && Status != OrderStatus.OnTheWay;
    }

    private bool IsRestaurantReason(CancellationReasonType reason)
    {
        return reason is Enums.CancellationReasonType.OutOfStock
            or Enums.CancellationReasonType.IngredientUnavailable
            or Enums.CancellationReasonType.MenuError
            or Enums.CancellationReasonType.StoreClosing
            or Enums.CancellationReasonType.OutOfDeliveryArea
            or Enums.CancellationReasonType.NoCourierAvailable
            or Enums.CancellationReasonType.SystemError
            or Enums.CancellationReasonType.FraudSuspicion
            or Enums.CancellationReasonType.DuplicateOrder
            or Enums.CancellationReasonType.PaymentNotApproved;
    }

    private bool IsOrderReallyDelayed()
    {
        if (PreparingStartedAt is null)
            return false;

        var estimatedTimeDelivery = PreparingStartedAt.Value.AddMinutes(EstimatedDeliveryTimeInMinutes + 10);

        return DateTime.UtcNow > estimatedTimeDelivery;
    }

    public void SetPreparingStartedAt(DateTime? preparingStartedAt)
    {
        PreparingStartedAt = preparingStartedAt;
    }
    
    public void SetOnTheWayAt(DateTime? onTheWayAt)
    {
        OnTheWayAt = onTheWayAt;
    }

    public void ChangeOrderStatus(OrderStatus status)
    {
        Status = status;
    }
}