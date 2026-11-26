using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoelGewicht.Api.Models;

// Eigen tabel in de eigen database "DoelGewicht" van de microservice.
[Table("DoelGewichten")]
public class DoelGewichtModel
{
    [Key]
    public int id { get; set; }

    [Required]
    public double doelgewicht { get; set; }

    [Required]
    public DateTime datumToegevoegd { get; set; } = DateTime.Now.Date;

    public DateTime? datumBehaald { get; set; }

    [Required]
    [MaxLength(255)]
    public string UserId { get; set; } = null!;
}
