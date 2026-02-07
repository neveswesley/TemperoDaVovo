using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Domain.Entities;

public class SideDish : BaseEntity
{
    public Guid SideDishGroupId { get; set; }
    public SideDishGroup SideDishGroup { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public void Update(string newName, int newQuantity, decimal newUnitPrice)
    {

        if (string.IsNullOrWhiteSpace(newName))
            throw new DomainException(["O nome do complemento é obrigatório"]);

        if (newQuantity < 0)
            throw new DomainException(["Quantidade inválida."]);
        
        if (newUnitPrice < 0)
            throw new DomainException(["Preço inválido."]);
            
            
        Name = newName;
        Quantity = newQuantity;
        UnitPrice = newUnitPrice;
    }

    public void Pause()
    {
        
    }
}