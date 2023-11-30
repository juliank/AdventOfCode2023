namespace AdventOfCode.Models;

public record Point<T> : Point where T : notnull
{
    public Point(int X, int Y, int Z = 0) : base(X, Y, Z)
    {
        // TODO: Is it possible to set/null out Value here
        // to avoid having the value copied when constructing
        // from another record using 'with'?
    }

    public T? Value { get; set; }

    public override string ToString()
    {
        if (Value == null) return base.ToString();

        return $"{X},{Y} ({Value})";
    }
}

/// <summary>
/// A two or three dimentional point, with X and Y (and Z) coordinates.
///
/// - X is east (+1) and west (-1).
/// - Y is south (+1) and north (-1)
/// - Z is up (+1) and down (-1)
///
/// Y is perhaps the oposite of what one might expect, but this is due to most
/// of the puzzles in AoC _increases_ the Y value when moving one row _down_
/// in a grid/matrix.
///
/// It is also possible to create a point with boundary values for X, Y and Z,
/// so that one can check whether getting the next point N/S/E/W/U/P of the
/// current point will succeed or not. By default a point has no boundaries,
/// and thus getting the next will always succeed.
/// </summary>
public record Point(int X, int Y, int Z = 0)
{
    public int? MaxX { get; init; } = default!;
    public int? MaxY { get; init; } = default!;
    public int? MinX { get; init; } = default!;
    public int? MinY { get; init; } = default!;
    public int? MinZ { get; init; } = default!;
    public int? MaxZ { get; init; } = default!;

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }

    public bool HasN() { return MinY == null || Y - 1 >= MinY; }
    public Point? GetN()
    {
        if (!HasN()) return null;
        return this with { Y = Y - 1 };
    }

    public bool HasS() { return MaxY == null || Y + 1 <= MaxY; }
    public Point? GetS()
    {
        if (!HasS()) return null;
        return this with { Y = Y + 1 };
    }

    public bool HasE() { return MaxX == null || X + 1 <= MaxX; }
    public Point? GetE()
    {
        if (!HasE()) return null;
        return this with { X = X + 1 };
    }

    public bool HasW() { return MinX == null || X - 1 >= MinX; }
    public Point? GetW()
    {
        if (!HasW()) return null;
        return this with { X = X - 1 };
    }

    public bool HasUp() { return MaxZ == null || Z + 1 <= MaxZ; }
    public Point? GetUp()
    {
        if (!HasUp()) return null;
        return this with { Z = Z + 1 };
    }

    public bool HasDown() { return MinZ == null || Z - 1 >= MinZ; }
    public Point? GetDown()
    {
        if (!HasDown()) return null;
        return this with { Z = Z - 1 };
    }

    public bool IsAdjacentTo(Point point)
    {
        if (this == point)
        {
            return false;
        }

        return Math.Abs(X - point.X) <= 1
            && Math.Abs(Y - point.Y) <= 1
            && Math.Abs(Z - point.Z) <= 1;
    }

    public int ManhattanDistanceTo(Point point)
    {
        return Math.Abs(X - point.X) + Math.Abs(Y - point.Y) + Math.Abs(Z - point.Z);
    }
}
