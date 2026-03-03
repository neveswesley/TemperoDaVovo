using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid OrderId { get; private set; }
    public PaymentWay PaymentWay { get; private set; }
    public PaymentStatus Status { get; private set; }
    public decimal Total { get; private set; }
    public decimal AmountPaid { get; private set; }
    public decimal Change { get; private set; }

    public string? TransactionId { get; private set; }
    
    protected Payment() { }

    public Payment(Guid orderId, PaymentWay paymentWay, decimal total)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        PaymentWay = paymentWay;
        Total = total;
        Status = PaymentStatus.Pending;
    }

    public void ProcessCash(decimal amountGiven)
    {
        if (amountGiven < Total)
            throw new DomainException(["Valor insuficiente."]);

        PaymentWay = PaymentWay.Cash;
        AmountPaid = amountGiven;
        Change = amountGiven - Total;
        Status = PaymentStatus.PayOnDelivery;
    }

    public void ProcessCard()
    {
        PaymentWay = PaymentWay.Card;
        AmountPaid = Total;
        Change = 0;
        Status = PaymentStatus.PayOnDelivery;
    }

    public void StartPix(string transactionId)
    {
        PaymentWay = PaymentWay.Pix;
        TransactionId = transactionId;
        Status = PaymentStatus.Pending;
    }

    public void ConfirmPix()
    {
        Status = PaymentStatus.Paid;
        AmountPaid = Total;
    }
    
    public void MarkAsPaidManually()
    {
        if (Status == PaymentStatus.Paid)
            throw new DomainException(["Pagamento já foi confirmado."]);

        Status = PaymentStatus.Paid;
    }
    
    
}