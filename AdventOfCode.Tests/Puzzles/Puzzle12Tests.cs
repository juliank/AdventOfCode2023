namespace AdventOfCode.Tests.Puzzles;

public class Puzzle12Tests
{
    private readonly Puzzle12 puzzle;

    public Puzzle12Tests()
    {
        puzzle = new Puzzle12();
    }

    // [Fact]
    internal void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact]
    internal void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }
}
