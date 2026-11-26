using DoelGewicht.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DoelGewicht.Api.Data;

public class DoelGewichtData : IDoelGewichtData
{
    private readonly DoelGewichtDbContext _context;

    public DoelGewichtData(DoelGewichtDbContext context)
    {
        _context = context;
    }

    public async Task<List<DoelGewichtDetails>> GetDoelGewicht(string userId)
    {
        return await _context.DoelGewichten
            .Where(dgm => dgm.UserId == userId)
            .Select(dgm => new DoelGewichtDetails(dgm.id, dgm.doelgewicht, dgm.datumToegevoegd, dgm.datumBehaald, dgm.UserId))
            .ToListAsync();
    }

    public async Task SetDoelGewicht(double doelgewicht, string userId)
    {
        var doelGewicht = new DoelGewichtModel
        {
            doelgewicht = doelgewicht,
            datumToegevoegd = DateTime.Now.Date,
            UserId = userId
        };

        _context.DoelGewichten.Add(doelGewicht);
        await _context.SaveChangesAsync();
    }

    public async Task EditDoelGewicht(int idDoelGewicht, double? doelgewicht, DateTime? datumBehaald, string userId)
    {
        var doelGewicht = await _context.DoelGewichten
            .FirstOrDefaultAsync(dgm => dgm.id == idDoelGewicht && dgm.UserId == userId);

        if (doelGewicht != null)
        {
            if (doelgewicht.HasValue)
                doelGewicht.doelgewicht = doelgewicht.Value;

            if (datumBehaald.HasValue)
                doelGewicht.datumBehaald = datumBehaald.Value;

            _context.DoelGewichten.Update(doelGewicht);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteDoelGewicht(int idDoelGewicht, string userId)
    {
        var doelGewicht = await _context.DoelGewichten
            .FirstOrDefaultAsync(dgm => dgm.id == idDoelGewicht && dgm.UserId == userId);

        if (doelGewicht != null)
        {
            _context.DoelGewichten.Remove(doelGewicht);
            await _context.SaveChangesAsync();
        }
    }
}
