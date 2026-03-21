namespace TemperoDaVovo.Domain.Entities;

public class Address
{
    public string ZipCode { get; private set; }
    public string State { get; private set; }
    public string City { get; private set; }
    public string Neighborhood { get; private set; }
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string? Complement { get; private set; }

    protected Address()
    {
        
    }
    
    public Address(
        string zipCode,
        string state,
        string city,
        string neighborhood,
        string street,
        string number,
        string? complement)
    {
        ZipCode = zipCode;
        State = state;
        City = city;
        Neighborhood = neighborhood;
        Street = street;
        Number = number;
        Complement = complement;
    }
}