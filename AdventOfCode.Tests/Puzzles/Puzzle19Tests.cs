namespace AdventOfCode.Tests.Puzzles;

public class Puzzle19Tests
{
    private readonly Puzzle19 puzzle;

    public Puzzle19Tests()
    {
        puzzle = new Puzzle19();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(476889);
    }

    // [Fact]
    internal void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }
}
