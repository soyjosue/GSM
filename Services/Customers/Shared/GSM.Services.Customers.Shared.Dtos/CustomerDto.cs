namespace GSM.Services.Customers.Shared.Dtos;

public class CustomerDto
{
    public CustomerDto(string names, string surnames, string identificationNumber)
    {
        Names = names;
        Surnames = surnames;
        IdentificationNumber = identificationNumber;
    }
    
    public CustomerDto(Guid id, string names, string surnames, string identificationNumber)
    {
        Id = id;
        Names = names;
        Surnames = surnames;
        IdentificationNumber = identificationNumber;
    }
    
    public Guid Id { get; set; }
    public string Names { get; set; }
    public string Surnames { get; set; }
    public string IdentificationNumber { get; set; }
}