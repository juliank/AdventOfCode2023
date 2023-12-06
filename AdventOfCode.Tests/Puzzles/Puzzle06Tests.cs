namespace AdventOfCode.Tests.Puzzles;

public class Puzzle06Tests
{
    private readonly Puzzle06 puzzle;

    public Puzzle06Tests()
    {
        puzzle = new Puzzle06();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(588588);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(34655848);
    }
}
