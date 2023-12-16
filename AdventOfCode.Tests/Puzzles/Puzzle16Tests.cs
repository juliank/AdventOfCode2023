namespace AdventOfCode.Tests.Puzzles;

public class Puzzle16Tests
{
    private readonly Puzzle16 puzzle;

    public Puzzle16Tests()
    {
        puzzle = new Puzzle16();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(8098);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(8335);
    }
}
