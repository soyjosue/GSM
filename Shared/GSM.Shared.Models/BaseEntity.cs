using System.ComponentModel.DataAnnotations;

namespace GSM.Shared.Models;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public DateTime CreatedTime { get; set; }
    
    public DateTime? UpdateTime { get; set; }
}