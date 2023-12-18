namespace AdventOfCode.Puzzles;

public class Puzzle18 : Puzzle<string, long>
{
    private const int PuzzleId = 18;

    public Puzzle18() : base(PuzzleId) { }

    public Puzzle18(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var digPlan = CreateDigPlan();
        return -1;
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
