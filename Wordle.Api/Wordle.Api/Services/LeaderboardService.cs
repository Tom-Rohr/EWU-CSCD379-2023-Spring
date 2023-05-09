using Microsoft.EntityFrameworkCore;
using Wordle.Api.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Wordle.Api.Services;

public class LeaderboardService
{
    private readonly AppDbContext _db;
    public LeaderboardService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Player> AddPlayer(string newName, int gameCount, double averageAttempts)
    {
        if (newName is null || newName.Length == 0)
        {
            throw new ArgumentException("You must enter a name after the game is finished");
        }
        //Check if the name is already in the database.  If not, create and add them.
        var player = await _db.Players.FirstOrDefaultAsync(w => w.Name == newName);
        //If existing player, update them.
        if (player is not null)
        {
            if (player.GameCount == 1){
                player.AverageAttempts = averageAttempts;
            }
            else
            {
                player.AverageAttempts = ((player.AverageAttempts * (player.GameCount - 1)) + averageAttempts) / player.GameCount;
            }
        }
        else
        {
            player = new()
            {
                Name = newName,
                GameCount = gameCount,
                AverageAttempts = averageAttempts
            };
            _db.Players.Add(player);
        }
        await _db.SaveChangesAsync();
        return player;
    }

    public async Task<Player> GetPlayer(string name)
    {
        var player = await _db.Players.FirstOrDefaultAsync(w => w.Name == name);
        return player!;
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
