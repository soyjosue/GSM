using System.ComponentModel.DataAnnotations;
using GSM.Shared.Models;

namespace GSM.Services.Customers.Domain.Models;

public class Customer : BaseEntity
{
    public Customer(string identificationNumber, string names, string surnames)
    {
        this.IdentificationNumber = identificationNumber;
        this.Names = names;
        this.Surnames = surnames;
    }
    
    [Required(ErrorMessage = "Numero de identificación es obligatorio.")]
    public string IdentificationNumber { get; set; }
    
    [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
    public string Names { get; set; }
    
    [Required(ErrorMessage = "El apellido del cliente es obligatorio.")]
    public string Surnames { get; set; }
}