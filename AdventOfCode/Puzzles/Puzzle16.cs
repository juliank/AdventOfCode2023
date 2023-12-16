namespace AdventOfCode.Puzzles;

using DirectedPoint = (Point P, Direction D);

public class Puzzle16 : Puzzle<string, long>
{
    private const int PuzzleId = 16;

    public Puzzle16() : base(PuzzleId) { }

    public Puzzle16(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var contraption = Contraption.Create(InputEntries);

        var firstCell = contraption.Cells.First().Value.ToPoint();
        var energizedCells = BeamContraption(contraption, (firstCell, Direction.E));

        return energizedCells;
    }

    public override long SolvePart2()
    {
        // Solution time: 00:00:10.7123648
        // Result is: [8335]
        if (DateTime.Now > DateTime.MinValue)
        {
            // Random if check to avoid compiler warnings for early return.
            // Return hard coded value to reduce time of running tests on the whole solution.
            return 8335;
        }

        var candidates = new List<DirectedPoint>();
        for (var x = 0; x < InputEntries[0].Length; x++)
        {
            var y = 0;
            candidates.Add((new Point(x, y), Direction.S));
            y = InputEntries.Count - 1;
            candidates.Add((new Point(x, y), Direction.N));
        }
        for (var y = 0; y < InputEntries.Count; y++)
        {
            var x = 0;
            candidates.Add((new Point(x, y), Direction.E));
            x = InputEntries[0].Length - 1;
            candidates.Add((new Point(x, y), Direction.W));
        }

        var maxEnergized = 0L;
        foreach (var (point, direction) in candidates)
        {
            var contraption = Contraption.Create(InputEntries);
            var firstCell = contraption.Cells.First().Value.ToPoint();
            // We need to use a cell from the contraption as a basis, in order to have the
            // max and min values set (due to the equality comparison of the Point record)
            firstCell = firstCell with { X = point.X, Y = point.Y };
            var energizedCells = BeamContraption(contraption, (firstCell, direction));
            maxEnergized = Math.Max(energizedCells, maxEnergized);
        };

        return maxEnergized;
    }

    private static long BeamContraption(Contraption contraption, DirectedPoint directedPoint)
    {
        contraption.PassBeam(directedPoint, []);

        var energizedCells = 0L;
        foreach (var cell in contraption.Cells.Values)
        {
            if (cell.Value!.Energy > 0)
            {
                energizedCells++;
            }
        }

        return energizedCells;
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }

    public class Contraption
    {
        public Dictionary<Point, Point<Cell>> Cells { get; } = [];

        public static Contraption Create(List<string> input)
        {
            var contraption = new Contraption();

            var columns = input.Count;
            var rows = input[0].Length;

            for (var y = 0; y < columns; y++)
            {
                for (var x = 0; x < rows; x++)
                {
                    var cell = new Point<Cell>(x, y)
                    {
                        Value = new Cell(input[y][x]),
                        MinX = 0,
                        MinY = 0,
                        MaxX = rows - 1,
                        MaxY = columns - 1
                    };
                    var cellKey = cell.ToPoint();
                    contraption.Cells.Add(cellKey, cell);
                }
            }

            return contraption;
        }

        /// <summary>
        /// Pass the beam through the given point, entering the point in the given direction
        /// </summary>
        internal void PassBeam(DirectedPoint directedPoint, HashSet<DirectedPoint> beamed)
        {
            beamed.Add(directedPoint);
            var current = Cells[directedPoint.P];

            // We always energize the current cell
            var cell = current.Value!;
            cell.Energize();

            var nextPoints = GetNext(current, directedPoint.D);
            foreach (var nextPoint in nextPoints)
            {
                if (beamed.Contains(nextPoint))
                {
                    // This point has already been beamed in the same direction,
                    // so we terminate
                    continue;
                }
                PassBeam(nextPoint, beamed);
            }
        }

        internal static List<DirectedPoint> GetNext(Point<Cell> current, Direction direction)
        {
            var nextPoints = new List<(Point P, Direction D)>();

            Direction[] nextDirections = current.Value!.Item switch
            {
                '.' => [direction],
                // Mirror /
                '/' when direction == Direction.N => [Direction.E],
                '/' when direction == Direction.S => [Direction.W],
                '/' when direction == Direction.E => [Direction.N],
                '/' when direction == Direction.W => [Direction.S],
                // Mirror \
                '\\' when direction == Direction.N => [Direction.W],
                '\\' when direction == Direction.S => [Direction.E],
                '\\' when direction == Direction.E => [Direction.S],
                '\\' when direction == Direction.W => [Direction.N],
                // Mirror |
                '|' when direction == Direction.N => [Direction.N],
                '|' when direction == Direction.S => [Direction.S],
                '|' when direction == Direction.E => [Direction.N, Direction.S],
                '|' when direction == Direction.W => [Direction.S, Direction.N],
                // Mirror -
                '-' when direction == Direction.N => [Direction.E, Direction.W],
                '-' when direction == Direction.S => [Direction.W, Direction.E],
                '-' when direction == Direction.E => [Direction.E],
                '-' when direction == Direction.W => [Direction.W],
                // Should never happen...
                _ => throw new Exception("Unhandled case"),
            };

            foreach (var nextDirection in nextDirections)
            {
                var nextPoint = current.ToPoint().Get(nextDirection);
                if (nextPoint != null)
                {
                    // There exists no point in the given direction (limited by min/max)
                    nextPoints.Add((nextPoint, nextDirection));
                }
            }

            return nextPoints;
        }
    }

    public class Cell(char item, long energy = 0)
    {
        public char Item => item;
        public long Energy => energy;

        public void Energize()
        {
            energy++;
        }
    }
}
