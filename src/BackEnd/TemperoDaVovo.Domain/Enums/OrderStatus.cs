namespace TemperoDaVovo.Domain.Enums;

public enum OrderStatus
{
    Draft = 0,
    PendingConfirmation = 1,
    Preparing = 2,
    OnTheWay = 3,
    Ready = 4,
    Canceled = 5
}