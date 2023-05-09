namespace Wordle.Api.Dtos;

public class PlayerDto
{
    public string Name { get; set; } = null!;
    public double AverageAttempts { get; set; }
    public int GameCount { get; set; }
}
