namespace AdventOfCode.Tests.Puzzles;

public class Puzzle11Tests
{
    private readonly Puzzle11 puzzle;

    public Puzzle11Tests()
    {
        puzzle = new Puzzle11();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(9605127);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(458191688761);
    }
}
