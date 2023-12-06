namespace AdventOfCode.Puzzles;

public class Puzzle06 : Puzzle<long, long>
{
    private const int PuzzleId = 06;

    public Puzzle06() : base(PuzzleId) { }

    public Puzzle06(params long[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        Tuple<int, int>[] races = [Tuple.Create(44, 202), Tuple.Create(82, 1076), Tuple.Create(69, 1138), Tuple.Create(81, 1458)];

        var product = 1L;
        foreach (var race in races)
        {
            (var tMax, var d) = race;
            var alternatives = GetMinMaxDiff(tMax, d);
            product *= alternatives;
        }
        return product;
    }

    public override long SolvePart2()
    {
        var tMax = 44826981L;
        var d = 202107611381458;
        var alternatives = GetMinMaxDiff(tMax, d);
        return alternatives;
    }

    private static long GetMinMaxDiff(long t, long d)
    {
        // d = v * t
        //
        // v = t1
        // d = v * t2
        // t2 = tMax - t1
        //
        // d = t1 * (tMax - t1)
        // t1^2 - tMax*t1 + d = 0
        // t1 = (1/2) * (tMax ± Sqrt(tMax^2 - 4*d))

        var sqrt = Math.Sqrt(t * t - 4 * d);
        var tMin = 0.5 * (t - sqrt);
        var tMax = 0.5 * (t + sqrt);

        var min = (long)tMin;
        var max = (long)tMax;
        if (max == tMax)
        {
            // In case the rounded value is equal to the original square root,
            // which means that we had a perfect square number, we'll have to
            // remove 1 from either max or min since we're looking for the number
            // of elements *between* min and max
            max--;
        }

        return max - min;
    }

    protected internal override IEnumerable<long> ParseInput(string inputItem)
    {
        // No need of parsing the input for this puzzle, as it is so simple
        yield return 0;
        // Puzzle input:
        // Time:        44     82     69     81
        // Distance:   202   1076   1138   1458
    }
}
