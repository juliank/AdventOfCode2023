namespace AdventOfCode.Tests.Puzzles;

public class Puzzle09Tests
{
    private readonly Puzzle09 puzzle;

    public Puzzle09Tests()
    {
        puzzle = new Puzzle09();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(1853145119);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(923);
    }
}
