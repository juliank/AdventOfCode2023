namespace AdventOfCode.Tests.Puzzles;

public class Puzzle14Tests
{
    private readonly Puzzle14 puzzle;

    public Puzzle14Tests()
    {
        puzzle = new Puzzle14();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(110565);
    }

    // [Fact]
    internal void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }
}
