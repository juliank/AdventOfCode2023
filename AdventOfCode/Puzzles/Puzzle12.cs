namespace AdventOfCode.Puzzles;

public class Puzzle12 : Puzzle<string, long>
{
    private const int PuzzleId = 12;

    public Puzzle12() : base(PuzzleId) { }

    public Puzzle12(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        return -1;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    internal static long PossibleArrangements(string record)
    {
        (var springs, var damagedSprings) = ParseSpringRecord(record);
        return PossibleArrangements(springs, damagedSprings);
    }

    private static long PossibleArrangements(string springs, int[] damagedSprings)
    {
        // ???.### 1,1,3 =>
        if (string.IsNullOrEmpty(springs))
        {
            return 0;
        }
        if (damagedSprings.Length == 0 && !springs.Any(s => s == '#'))
        {
            return 0;
        }
        if (springs.Take(damagedSprings[0] + 1).Any(s => s == '.'))
        {
            return 0;
        }

        var arrangements = 1L;

        var remainingSprings = springs[damagedSprings[0]..];
        var remainingDamagedSprings = damagedSprings[1..];
        var possibleRemaining = PossibleArrangements(remainingSprings, remainingDamagedSprings);
        arrangements += possibleRemaining;

        var alternateSprings = springs[1..];
        var possibleAlternatives = PossibleArrangements(alternateSprings, damagedSprings);
        arrangements += possibleAlternatives;

        return arrangements;
    }

    internal static (string Springs, int[] DamagedSprings) ParseSpringRecord(string str)
    {
        // operational (.)
        // damaged (#)
        // unknown (?)
        var parts = str.Split(' ');
        var springs = parts[0];
        var damagedSprings = parts[1].Split(',').Select(c => int.Parse(c.ToString())).ToArray();
        return (springs, damagedSprings);
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }
}
