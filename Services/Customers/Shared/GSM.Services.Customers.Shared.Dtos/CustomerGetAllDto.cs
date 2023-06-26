namespace GSM.Services.Customers.Shared.Dtos;

public class CustomerGetAllDto : CustomerDto
{
    public CustomerGetAllDto(string names, string surnames, string identificationNumber) : base(names, surnames, identificationNumber) { }
}