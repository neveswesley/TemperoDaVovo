using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Communications.Requests;

public class AddressRequest
{
    public string ZipCode { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Neighborhood { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public string? Complement { get; private set; }
}