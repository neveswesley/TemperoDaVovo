namespace TemperoDaVovo.Domain.Enums;

public enum OrderStatus
{
    Draft = 0,
    PendingConfirmation = 1,
    Preparing = 2,
    OnTheWay = 3,
    Ready = 4,
    CancellationRequested = 5,
    Canceled = 6,
    Abandoned = 99
}