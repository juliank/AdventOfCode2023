namespace AdventOfCode.Tests.Puzzles;

public class Puzzle12Tests
{
    private readonly Puzzle12 puzzle;

    public Puzzle12Tests()
    {
        puzzle = new Puzzle12();
    }

    // [Fact]
    internal void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact]
    internal void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Theory]
    [InlineData("???.### 1,1,3", "???.###", new int[] { 1, 1, 3 })]
    [InlineData(".??..??...?##. 1,1,3", ".??..??...?##.", new int[] { 1, 1, 3 })]
    [InlineData("?###???????? 3,2,1", "?###????????", new int[] { 3, 2, 1 })]
    public void TestParseRecord(string input, string springs, int[] damagedSprings)
    {
        var result = Puzzle12.ParseSpringRecord(input);
        result.Springs.Should().Be(springs);
        result.DamagedSprings.Should().BeEquivalentTo(damagedSprings);
    }
}
