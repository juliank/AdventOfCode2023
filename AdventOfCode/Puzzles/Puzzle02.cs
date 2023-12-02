namespace AdventOfCode.Puzzles;

public class Puzzle02 : Puzzle<Puzzle02.Game, long>
{
    private const int PuzzleId = 02;

    public Puzzle02() : base(PuzzleId) { }

    public Puzzle02(params Game[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        // Max 12 red cubes, 13 green cubes, and 14 blue cubes
        var possibleGames = InputEntries.Where(game => game.IsPossible(12, 13, 14));

        return possibleGames.Sum(g => g.Id); // 2207
    }

    public override long SolvePart2()
    {
        var sumOfPowers = 0L;

        foreach (var game in InputEntries)
        {
            var (red, blue, green) = game.MinimumRequired();
            var power = red * blue * green;
            sumOfPowers += power;
        }

        return sumOfPowers; // 62241
    }

    protected internal override IEnumerable<Game> ParseInput(string inputItem)
    {
        // Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        // Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
        // Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red

        var game = inputItem.Split(':'); // Game 1
        var gameId = int.Parse(game[0].Split(' ')[1]);

        var sets = game[1] // 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            .Split(';') // 3 blue, 4 red
            .Select(s =>
            {
                var cubes = s
                    .Split(',') // 3 blue
                    .Select(c =>
                    {
                        var cube = c.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var color = cube[1].Trim();
                        var count = int.Parse(cube[0].Trim());
                        return new Cube(color, count);
                    })
                    .ToList();
                return cubes;
            })
            .ToList();

        yield return new Game(gameId, sets); ;
    }

    public record Cube(string Color, int Count);

    public record Game(int Id, List<List<Cube>> Sets)
    {
        public bool IsPossible(int red, int green, int blue)
        {
            if (Sets.Any(s => s.Any(c => c.Color == "red" && c.Count > red)))
            {
                return false;
            }
            if (Sets.Any(s => s.Any(c => c.Color == "blue" && c.Count > blue)))
            {
                return false;
            }
            if (Sets.Any(s => s.Any(c => c.Color == "green" && c.Count > green)))
            {
                return false;
            }
            return true;
        }

        public (int red, int blue, int green) MinimumRequired()
        {
            var maxRed = Sets.SelectMany(s => s.Where(c => c.Color == "red")).Max(c => c.Count);
            var maxBlue = Sets.SelectMany(s => s.Where(c => c.Color == "blue")).Max(c => c.Count);
            var maxGreen = Sets.SelectMany(s => s.Where(c => c.Color == "green")).Max(c => c.Count);

            return (maxRed, maxBlue, maxGreen);
        }
    }
}
