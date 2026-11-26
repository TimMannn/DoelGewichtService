using DoelGewicht.Api.Models;

namespace DoelGewicht.Api.Data;

public interface IDoelGewichtData
{
    Task<List<DoelGewichtDetails>> GetDoelGewicht(string userId);
    Task SetDoelGewicht(double gewicht, string userId);
    Task EditDoelGewicht(int idDoelGewicht, double? gewicht, DateTime? datumBehaald, string userId);
    Task DeleteDoelGewicht(int idDoelGewicht, string userId);
}
