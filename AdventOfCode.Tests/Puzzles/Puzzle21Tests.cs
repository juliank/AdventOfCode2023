namespace AdventOfCode.Tests.Puzzles;

public class Puzzle21Tests
{
    private readonly Puzzle21 puzzle;

    public Puzzle21Tests()
    {
        puzzle = new Puzzle21();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(3722);
    }

    // [Fact]
    internal void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }
}
