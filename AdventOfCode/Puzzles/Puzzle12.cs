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

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }
}
