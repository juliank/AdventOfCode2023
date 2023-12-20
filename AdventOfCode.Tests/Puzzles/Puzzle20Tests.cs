namespace AdventOfCode.Tests.Puzzles;

public class Puzzle20Tests
{
    private readonly Puzzle20 puzzle;

    public Puzzle20Tests()
    {
        puzzle = new Puzzle20();
    }

    // [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }
}
