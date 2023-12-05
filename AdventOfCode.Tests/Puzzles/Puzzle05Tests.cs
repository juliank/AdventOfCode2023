namespace AdventOfCode.Tests.Puzzles;

public class Puzzle05Tests
{
    private readonly Puzzle05 puzzle;

    public Puzzle05Tests()
    {
        puzzle = new Puzzle05();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(84470622);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(26714516);
    }
}
