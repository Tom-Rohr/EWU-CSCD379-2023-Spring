using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Data;
using Wordle.Api.Dtos;
using Wordle.Api.Services;

namespace Wordle.Api.Controllers;

/*Routing by Convention. if controller is named SomethingController,
  like we have below with WordController, then the route will be /Something. */
[Route("[controller]")]
[ApiController]
public class LeaderboardController : ControllerBase
{
    private readonly LeaderboardService _leaderboardService;

    public LeaderboardController(LeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }
    [HttpGet("GetPlayer")]
    public async Task<Player> GetPlayer(string name)
    {
        return await _leaderboardService.GetPlayer(name);
    }

    [HttpGet("GetTopTen")]
    public async Task<IEnumerable<Player>> GetTopTen(int? count)
    {
        return await _leaderboardService.GetTopTen(count);
    }

    [HttpPost]
    public async Task<Player> AddPlayer(string name, int gameCount, double averageAttempts)
    {
        return await _leaderboardService.AddPlayer(name, gameCount, averageAttempts);
    }

    [HttpPost("AddPlayerFromBody")]
    public async Task<Player> AddPlayerFromBody([FromBody]PlayerDto player)
    {
        return await _leaderboardService.AddPlayer(player.Name, player.GameCount, player.AverageAttempts);
    }
}
