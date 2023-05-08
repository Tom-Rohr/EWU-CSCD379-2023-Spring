namespace Wordle.Api.Data;

public class Player
{
    public int PlayerID { get; set; }
    public required string Name { get; set; }
    public int GameCount { get; set; }
    public double AverageAttempts { get; set; }
    //public int AverageSecondsPerGame { get; set; }

    public static void SeedTestPlayers(AppDbContext db)
    {
        if (!db.Players.Any())
        {
            for(int i = 0; i < 10; i++)
            {
                var player = new Player()
                {
                    Name = $"Player {i}",
                    GameCount = 0,
                    AverageAttempts = 0
                };
                db.Players.Add(player);
            }
            db.SaveChanges();
        }
    }
}
