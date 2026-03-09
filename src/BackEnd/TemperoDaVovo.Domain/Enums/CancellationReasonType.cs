namespace TemperoDaVovo.Domain.Enums;

public enum CancellationReasonType
{
    
    // Client
    ChangedMind, // mudou de ideia
    WrongAddress, // endereço errado
    OrderMistake, // pedido feito por engano
    DelayTooLong, // tempo de espera mt longo
    PaymentIssue, // problema no pagamento
    HighDeliveryFee, // valor do frete mt alto
    DelayedOrder, // pedido atrasado
    Other, // outro
    
    // Restaurant
    OutOfStock, // produto indisponível
    IngredientUnavailable, // falta de ingrediente
    MenuError, // erro no cardápio
    StoreClosing, // restaurante fechado
    OutOfDeliveryArea, // area fora de entrega
    NoCourierAvailable, // sem entregador disponível
    SystemError, // erro no sistema
    FraudSuspicion, // suspeita de fraude
    DuplicateOrder, // pedido duplicado
    PaymentNotApproved, // problema no pagamento
    
    //System
    NotConfirmedByRestaurant
}