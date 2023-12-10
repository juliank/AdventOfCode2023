namespace AdventOfCode.Tests.Puzzles;

public class Puzzle07Tests
{
    private readonly Puzzle07 puzzle;

    public Puzzle07Tests()
    {
        puzzle = new Puzzle07();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(251806792);
    }

    // [Fact]
    internal void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Theory]
    // Part one examples
    // [InlineData("32T3K", 1)] // 1 pair
    // [InlineData("KK677", 2)] // 2 pairs
    // [InlineData("KTJJT", 2)] // 2 pairs
    // [InlineData("T55J5", 3)] // 3 of a kind
    // [InlineData("QQQJA", 3)] // 3 of a kind
    // Part two examples
    [InlineData("QJJQ2", 5)] // 4 of a kind
    [InlineData("JKKK2", 5)] // 4 of a kind
    [InlineData("QQQQ2", 5)] // 4 of a kind
    public void TestHandScoring(string input, int expectedType)
    {
        var cards = input.Select(Puzzle07.CreateCard).ToList();
        var hand = new Puzzle07.Hand(cards, 0);

        hand.Type.Should().Be(expectedType);
    }

    [Theory]
    [InlineData("KTJJT", "KK677")]
    [InlineData("T55J5", "QQQJA")]
    [InlineData("JKKK2", "QQQQ2")] // Part 2 example (both 4 of a kind): JKKK2 is weaker than QQQQ2 because J is weaker than Q
    public void TestHandStrength(string weaker, string stronger)
    {
        var weakerCards = weaker.Select(Puzzle07.CreateCard).ToList();
        var weakerHand = new Puzzle07.Hand(weakerCards, 0);
        var strongerCards = stronger.Select(Puzzle07.CreateCard).ToList();
        var strongerHand = new Puzzle07.Hand(strongerCards, 0);

        strongerHand.GlobalRank.Should().BeGreaterThan(weakerHand.GlobalRank);
    }
}
