using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace invoice.Models;

public class Fee
{
    [Key]
    public ulong Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [Precision(18, 2)]
    public decimal Amount { get; set; }

    [Required]
    [Display(Name = "Is this a flat fee?")]
    public bool IsFlatFee { get; set; }
}
