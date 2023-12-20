namespace AdventOfCode.Puzzles;

public class Puzzle20 : Puzzle<string, long>
{
    private const int PuzzleId = 20;

    public Puzzle20() : base(PuzzleId) { }

    public Puzzle20(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        throw new NotImplementedException();
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }

    public class Module
    {
        public required string Name { get; init; }
    }

    public enum ModuleType { FlipFlop, Conjunction, Broadcast, Button }
}
