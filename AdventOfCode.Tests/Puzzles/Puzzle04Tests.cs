namespace AdventOfCode.Tests.Puzzles;

public class Puzzle04Tests
{
    private readonly Puzzle04 puzzle;

    public Puzzle04Tests()
    {
        puzzle = new Puzzle04();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(22193);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(5625994);
    }
}
