namespace AdventOfCode.Tests.Puzzles;

public class Puzzle08Tests
{
    private readonly Puzzle08 puzzle;

    public Puzzle08Tests()
    {
        puzzle = new Puzzle08();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(15871);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(11283670395017);
    }
}
