namespace AdventOfCode.Puzzles;

public class Puzzle10 : Puzzle<string, long>
{
    private const int PuzzleId = 10;

    public Puzzle10() : base(PuzzleId) { }

    public Puzzle10(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    private readonly Dictionary<Point, Point<char>> _tiles = [];

    private enum Direction { N, S, E, W }
    private readonly Direction[] _directions = Enum.GetValues<Direction>().ToArray();

    private Point<char> _start = new Point<char>(-1, -1);

    public override long SolvePart1()
    {
        PopulateTiles();

        _start = _tiles.First(t => t.Value.Value == 'S').Value;
        foreach (var direction in _directions)
        {
            var distance = GetFarthestDistanceFromStart(direction);
            if (distance > 0)
            {
                return distance;
            }
        }

        return -1;
    }

    private long GetFarthestDistanceFromStart(Direction direction)
    {
        var pointN = _start.ToPoint().GetN()!;
        var pointS = _start.ToPoint().GetS()!;
        var pointE = _start.ToPoint().GetE()!;
        var pointW = _start.ToPoint().GetW()!;
        var nextPoint = direction switch
        {
            Direction.N => _tiles[pointN],
            Direction.S => _tiles[pointS],
            Direction.E => _tiles[pointE],
            Direction.W => _tiles[pointW],
            _ => throw new ArgumentException($"Unknown direction [{direction}]")
        };

        var possible = CanGetTo(nextPoint, direction);
        if (!possible)
        {
            return -1;
        }

        var distance = GetLoopDistance(nextPoint, direction);
        return distance < 0 ? distance : distance / 2;
    }

    private long GetLoopDistance(Point<char> firstPointAfterStart, Direction fromDirection)
    {
        var distance = 1L; // Starting at 1, since this is the first step after the start tile
        var nextPoint = firstPointAfterStart;
        var nextDirection = fromDirection;

        while (true)
        {
            var next = GetNextPoint(nextPoint, nextDirection);
            if (next == null)
            {
                return -1;
            }
            (nextPoint, nextDirection) = next.Value;

            distance++;
            if (nextPoint.Value == 'S')
            {
                return distance;
            }
        }
    }

    private (Point<char> NextPoint, Direction NextDirection)? GetNextPoint(Point<char> point, Direction inDirection)
    {
        switch (inDirection)
        {
            case Direction.S:
                if (point.Value == '|') return (_tiles[point.ToPoint().GetS()!], Direction.S);
                else if (point.Value == 'L') return (_tiles[point.ToPoint().GetE()!], Direction.E);
                else if (point.Value == 'J') return (_tiles[point.ToPoint().GetW()!], Direction.W);
                else return null;
            case Direction.N:
                if (point.Value == '|') return (_tiles[point.ToPoint().GetN()!], Direction.N);
                else if (point.Value == '7') return (_tiles[point.ToPoint().GetW()!], Direction.W);
                else if (point.Value == 'F') return (_tiles[point.ToPoint().GetE()!], Direction.E);
                else return null;
            case Direction.W:
                if (point.Value == '-') return (_tiles[point.ToPoint().GetW()!], Direction.W);
                else if (point.Value == 'L') return (_tiles[point.ToPoint().GetN()!], Direction.N);
                else if (point.Value == 'F') return (_tiles[point.ToPoint().GetS()!], Direction.S);
                else return null;
            case Direction.E:
                if (point.Value == '-') return (_tiles[point.ToPoint().GetE()!], Direction.E);
                else if (point.Value == '7') return (_tiles[point.ToPoint().GetS()!], Direction.S);
                else if (point.Value == 'J') return (_tiles[point.ToPoint().GetN()!], Direction.N);
                else return null;
            default:
                return null;
        }
    }

    private static bool CanGetTo(Point<char> point, Direction fromDirection)
    {
        return fromDirection switch
        {
            Direction.S => point.Value is '|' or 'L' or 'J',
            Direction.N => point.Value is '|' or '7' or 'F',
            Direction.W => point.Value is '-' or 'L' or 'F',
            Direction.E => point.Value is '-' or '7' or 'J',
            _ => false,
        };
    }
    // The pipes are arranged in a two-dimensional grid of tiles:
    //     | is a vertical pipe connecting north and south.
    //     - is a horizontal pipe connecting east and west.
    //     L is a 90-degree bend connecting north and east.
    //     J is a 90-degree bend connecting north and west.
    //     7 is a 90-degree bend connecting south and west.
    //     F is a 90-degree bend connecting south and east.
    //     . is ground; there is no pipe in this tile.
    //     S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.

    private void PopulateTiles()
    {
        for (var y = 0; y < InputEntries.Count; y++)
        {
            var line = InputEntries[y];
            for (var x = 0; x < line.Length; x++)
            {
                var point = new Point<char>(x, y)
                {
                    Value = line[x]
                };
                _tiles.Add(point.ToPoint(), point);
            }
        }
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }
}
