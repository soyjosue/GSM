using System.ComponentModel.DataAnnotations;

namespace GSM.Shared.Models;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedTime { get; set; }
    
    public DateTime? UpdateTime { get; set; }
}