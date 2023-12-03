namespace AdventOfCode.Tests.Puzzles;

public class Puzzle03Tests
{
    private readonly Puzzle03 puzzle;

    public Puzzle03Tests()
    {
        puzzle = new Puzzle03();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(531561);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(83279367);
    }
}
