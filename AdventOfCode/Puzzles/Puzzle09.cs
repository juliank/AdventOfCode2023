namespace AdventOfCode.Puzzles;

public class Puzzle09 : Puzzle<List<long>, long>
{
    private const int PuzzleId = 09;

    public Puzzle09() : base(PuzzleId) { }

    public Puzzle09(params List<long>[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var sumOfNextValues = 0L;

        foreach (var inputEntry in InputEntries)
        {
            var differences = CreateDifferences(inputEntry);
            for (var i = 1; i < differences.Count; i++)
            {
                differences[i].Add(differences[i].Last() + differences[i - 1].Last());
            }
            sumOfNextValues += differences.Last().Last();
        }

        return sumOfNextValues;
    }

    public override long SolvePart2()
    {

        var sumOfBeforeValues = 0L;

        foreach (var inputEntry in InputEntries)
        {
            var differences = CreateDifferences(inputEntry);
            for (var i = 1; i < differences.Count; i++)
            {
                differences[i].Insert(0, differences[i].First() - differences[i - 1].First());
            }
            sumOfBeforeValues += differences.Last().First();
        }

        return sumOfBeforeValues;
    }

    private static List<List<long>> CreateDifferences(List<long> inputEntry)
    {
        var differences = new List<List<long>> { inputEntry };
        var diffs = inputEntry.SelectWithPrevious((prev, cur) => cur - prev).ToList();
        differences.Add(diffs);
        while (!diffs.All(d => d == 0))
        {
            diffs = diffs.SelectWithPrevious((prev, cur) => cur - prev).ToList();
            differences.Add(diffs);
        }

        differences.Reverse();
        return differences;
    }

    protected internal override IEnumerable<List<long>> ParseInput(string inputItem)
    {
        yield return inputItem.Split(' ').Select(s => long.Parse(s)).ToList();
    }
}
