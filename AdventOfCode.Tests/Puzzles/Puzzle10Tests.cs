namespace AdventOfCode.Tests.Puzzles;

public class Puzzle10Tests
{
    private readonly Puzzle10 puzzle;

    public Puzzle10Tests()
    {
        puzzle = new Puzzle10();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(6875);
    }

    // [Fact]
    internal void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }
}
