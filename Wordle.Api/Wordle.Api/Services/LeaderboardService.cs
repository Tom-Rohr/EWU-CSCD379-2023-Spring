using Microsoft.EntityFrameworkCore;
using Wordle.Api.Data;

namespace Wordle.Api.Services;

public class LeaderboardService
{
    private readonly AppDbContext _db;
    public LeaderboardService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Player>> GetTopTen(int? count)
    {
        count ??= 10;

        if ( await _db.Players.CountAsync() < count )
        {
            count = await _db.Players.CountAsync();
        }
        var topTen = await _db.Players
            .OrderByDescending(player => player.AverageAttempts)
            .Take(count.Value)
            .ToListAsync();
        return topTen;
    }
}
