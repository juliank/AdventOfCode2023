namespace AdventOfCode.Puzzles;

public class Puzzle18 : Puzzle<string, long>
{
    private const int PuzzleId = 18;

    public Puzzle18() : base(PuzzleId) { }

    public Puzzle18(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var digPlan = CreateDigPlan();

        var minX = digPlan.Select(p => p.X).Min();
        var maxX = digPlan.Select(p => p.X).Max();
        var minY = digPlan.Select(p => p.Y).Min();
        var maxY = digPlan.Select(p => p.Y).Max();
        var edges = digPlan.Select(p => p.ToPoint()).ToHashSet();

        var trenchCount = 0;
        // Run ray tracing
        for (var y = minY; y <= maxY; y++)
        {
            var line = string.Empty;

            var isInTrench = false;
            for (var x = minX; x <= maxX; x++)
            {
                var exitPoint = false;
                if (edges.Contains(new Point<string>(x, y)))
                {
                    if (!isInTrench)
                    {
                        // We're entering the trench
                        isInTrench = true;
                    }
                }
                else
                {
                    if (isInTrench)
                    {
                        // Remember to also increase the count at the point
                        // where we exit the trench.
                        // trenchCount++;
                        exitPoint = true;
                        isInTrench = false;
                    }
                }

                if (isInTrench || exitPoint)
                {
                    line += "#";
                    trenchCount++;
                }
                else
                {
                    line += ".";
                }
            }

            Console.WriteLine(line);
        }

        return trenchCount;
        // 67137 is too high
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    private List<Point<string>> CreateDigPlan()
    {
        var digPlan = new List<Point<string>>();

        var currentPoint = new Point(0, 0);
        foreach (var item in InputEntries)
        {
            var parts = item.Split(' ');

            var direction = parts[0] switch
            {
                "U" => Direction.N,
                "D" => Direction.S,
                "R" => Direction.E,
                "L" => Direction.W,
                _ => throw new ArgumentException($"Unknown direction {parts[0]}")
            };
            var steps = int.Parse(parts[1]);
            var color = parts[2][1..^1];

            for (var i = 0; i < steps; i++)
            {
                currentPoint = currentPoint.Get(direction)!; // No max/min, so we'll always get a next point
                var digPoint = new Point<string>(currentPoint.X, currentPoint.Y) { Value = color };
                digPlan.Add(digPoint);
            }
        }
        return digPlan;
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }
}
