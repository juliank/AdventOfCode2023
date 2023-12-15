namespace AdventOfCode.Tests.Puzzles;

public class Puzzle15Tests
{
    private readonly Puzzle15 puzzle;

    public Puzzle15Tests()
    {
        puzzle = new Puzzle15();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(497373);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(259356);
    }

    [Theory]
    [InlineData("rn=1", 30)]
    [InlineData("rn", 0)]
    [InlineData("cm-", 253)]
    [InlineData("qp=3", 97)]
    public void TestAsciiHash(string str, int expectedHash)
    {
        Puzzle15.AsciiHash(str).Should().Be(expectedHash);
    }

    [Theory]
    [InlineData("rn=1", "rn", '=', 1)]
    [InlineData("cm-", "cm", '-', -1)]
    public void TestStepConstruction(string stepDescription, string label, char op, int val)
    {
        var step = new Puzzle15.Step(stepDescription);
        step.Label.Should().Be(label);
        step.Operator.Should().Be(op);
        step.Value.Should().Be(val);
    }
}
