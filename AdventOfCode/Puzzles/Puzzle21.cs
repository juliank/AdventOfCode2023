namespace AdventOfCode.Puzzles;

public class Puzzle21 : Puzzle<string, long>
{
    private const int PuzzleId = 21;

    public Puzzle21() : base(PuzzleId) { }

    public Puzzle21(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var columns = InputEntries[0].Length;
        var rows = InputEntries.Count;
        var points = ProcessInput();
        var startingPoint = points.First(p => p.Value.Value == 'S').Value;
        var visitedPoints = new Dictionary<Point, int>();

        var targetSteps = 64;
        var candidates = new List<Point> { startingPoint };
        for (var step = 0; step <= targetSteps; step++)
        {
            var nextCandidates = new List<Point>();
            foreach (var candidate in candidates)
            {
                visitedPoints[candidate.ToHashKey()] = step;
            }

            foreach (var candidate in candidates)
            {
                var next = GetCandidates(candidate, points, visitedPoints, columns, rows);
                nextCandidates.AddRange(next);
            }
            candidates = nextCandidates.Distinct().ToList();
        }

        // Any step reached in an even number of steps can also be reached in 64 steps
        var evenlyReachedPoints = visitedPoints.Where(p => p.Value % 2 == 0).ToList();
        return evenlyReachedPoints.Count;
    }

    public override long SolvePart2()
    {
        if (DateTime.Now > DateTime.MinValue)
        {
            // Random if check to avoid compiler warnings for early return.
            // The solution to part two _works_. But for the sample input
            // the runtime is > 2 minutes for 5000 steps, so we won't reach
            // a solution for the proper input (with 26501365) anytime soon...
            throw new NotImplementedException();
        }

        var columns = InputEntries[0].Length;
        var rows = InputEntries.Count;
        var points = ProcessInput(withMaxMin: false);
        var startingPoint = points.First(p => p.Value.Value == 'S').Value;
        var visitedPoints = new Dictionary<Point, int>();

        var targetSteps = 5000;
        var candidates = new List<Point> { startingPoint };
        for (var step = 0; step <= targetSteps; step++)
        {
            var nextCandidates = new List<Point>();
            foreach (var candidate in candidates)
            {
                visitedPoints[candidate.ToHashKey()] = step;
            }

            foreach (var candidate in candidates)
            {
                var next = GetCandidates(candidate, points, visitedPoints, columns, rows);
                nextCandidates.AddRange(next);
            }
            candidates = nextCandidates.Distinct().ToList();
        }

        // Any step reached in an even number of steps can also be reached in 64 steps
        var evenlyReachedPoints = visitedPoints.Where(p => p.Value % 2 == 0).ToList();
        return evenlyReachedPoints.Count;
    }

    private static List<Point> GetCandidates(Point fromPoint, Dictionary<Point, Point<char>> points, Dictionary<Point, int> visitedPoints, int columns, int rows)
    {
        var candidates = new List<Point>();
        foreach (var direction in Point.Directions2D)
        {
            var point = fromPoint.Get(direction);
            if (point != null)
            {
                var notVisited = !visitedPoints.ContainsKey(point.ToHashKey());

                // For part 2: calculate position on original map
                //             for point outside of max/min values
                var gardenMapHashKey = point.ToHashKey();
                if (gardenMapHashKey.X > columns - 1)
                {
                    var overflowX = gardenMapHashKey.X;
                    var newX = overflowX % columns;
                    gardenMapHashKey = gardenMapHashKey with { X = newX };
                }
                else if (gardenMapHashKey.X < 0)
                {
                    var overflowX = gardenMapHashKey.X;
                    var newX = (columns + overflowX % columns) % columns;
                    gardenMapHashKey = gardenMapHashKey with { X = newX };
                }
                if (gardenMapHashKey.Y > rows - 1)
                {
                    var overflowY = gardenMapHashKey.Y;
                    var newY = overflowY % rows;
                    gardenMapHashKey = gardenMapHashKey with { Y = newY };
                }
                else if (gardenMapHashKey.Y < 0)
                {
                    var overflowY = gardenMapHashKey.Y;
                    var newY = (rows + overflowY % rows) % rows;
                    gardenMapHashKey = gardenMapHashKey with { Y = newY };
                }

                var notRock = points[gardenMapHashKey].Value != '#';
                if (notVisited && notRock)
                {
                    candidates.Add(point);
                }
            }
        }
        return candidates;
    }

    private Dictionary<Point, Point<char>> ProcessInput(bool withMaxMin = true)
    {
        var minX = 0;
        var minY = 0;
        var maxX = InputEntries[0].Length - 1;
        var maxY = InputEntries.Count - 1;

        var points = new Dictionary<Point, Point<char>>();
        for (var y = minY; y < InputEntries.Count; y++)
        {
            for (var x = minX; x < InputEntries[0].Length; x++)
            {
                Point<char> point;
                if (withMaxMin)
                {
                    point = new Point<char>(x, y)
                    {
                        Value = InputEntries[y][x],
                        MinX = minX,
                        MaxX = maxX,
                        MinY = minY,
                        MaxY = maxY
                    };
                }
                else
                {
                    point = new Point<char>(x, y)
                    {
                        Value = InputEntries[y][x]
                    };
                }
                points.Add(point.ToHashKey(), point);
            }
        }

        return points;
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }
}
