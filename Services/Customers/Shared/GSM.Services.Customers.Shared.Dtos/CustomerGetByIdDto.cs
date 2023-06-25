namespace GSM.Services.Customers.Shared.Dtos;

public class CustomerGetByIdDto : CustomerDto
{
    public CustomerGetByIdDto(string names, string surnames, string identificationNumber) : base(names, surnames, identificationNumber) { }
}