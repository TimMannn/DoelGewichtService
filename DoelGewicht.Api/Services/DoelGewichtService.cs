using System.Security.Claims;
using DoelGewicht.Api.Data;
using DoelGewicht.Api.Models;

namespace DoelGewicht.Api.Services;

public class DoelGewichtService
{
    private readonly IDoelGewichtData _gewichtData;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DoelGewichtService(IDoelGewichtData gewichtData, IHttpContextAccessor httpContextAccessor)
    {
        _gewichtData = gewichtData;
        _httpContextAccessor = httpContextAccessor;
    }

    // Zelfde werkwijze als GewichtService in de hoofdapp: het userId staat in de NameIdentifier-claim.
    private string GetUserIdFromContext()
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new InvalidOperationException("HttpContext is null");

        var user = _httpContextAccessor.HttpContext.User;

        if (user == null || user.Identity?.IsAuthenticated != true)
            throw new InvalidOperationException("User not authenticated");

        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                     ?? user.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new InvalidOperationException("User ID claim not found");

        return userId;
    }

    public async Task<List<DoelGewichtDetails>> GetDoelGewicht()
    {
        var userId = GetUserIdFromContext();
        return await _gewichtData.GetDoelGewicht(userId);
    }

    public async Task SetDoelGewicht(double doelgewicht)
    {
        var userId = GetUserIdFromContext();
        await _gewichtData.SetDoelGewicht(doelgewicht, userId);
    }

    public async Task EditDoelGewicht(int idDoelGewicht, double? doelgewicht, DateTime? datumBehaald)
    {
        var userId = GetUserIdFromContext();
        await _gewichtData.EditDoelGewicht(idDoelGewicht, doelgewicht, datumBehaald, userId);
    }

    public async Task DeleteDoelGewicht(int idDoelGewicht)
    {
        var userId = GetUserIdFromContext();
        await _gewichtData.DeleteDoelGewicht(idDoelGewicht, userId);
    }
}
