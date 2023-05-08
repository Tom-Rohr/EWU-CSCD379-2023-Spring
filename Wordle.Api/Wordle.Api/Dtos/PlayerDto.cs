namespace Wordle.Api.Dtos;

public class PlayerDto
{
    public string? Name { get; set; } = null!;
    public double AverageGuesses { get; set; }
    public int GameCount { get; set; }
}
