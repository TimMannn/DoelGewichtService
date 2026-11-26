using System.ComponentModel.DataAnnotations;

namespace DoelGewicht.Api.Models;

public class AddDoelGewichtModel
{
    [Required]
    [Range(0, 200, ErrorMessage = "Doel gewicht is niet realistisch (onder 0 of boven 200kg)")]
    public double Doelgewicht { get; set; }
}
