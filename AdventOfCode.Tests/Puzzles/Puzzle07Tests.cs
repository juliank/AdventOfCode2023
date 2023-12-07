namespace AdventOfCode.Tests.Puzzles;

public class Puzzle07Tests
{
    private readonly Puzzle07 puzzle;

    public Puzzle07Tests()
    {
        puzzle = new Puzzle07();
    }

    // [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Theory]
    [InlineData("32T3K", 1)] // 1 pair
    [InlineData("KK677", 2)] // 2 pairs
    [InlineData("KTJJT", 2)] // 2 pairs
    [InlineData("T55J5", 3)] // 3 of a kind
    [InlineData("QQQJA", 3)] // 3 of a kind
    public void TestHandScoring(string input, int expectedType)
    {
        var cards = input.Select(Puzzle07.CreateCard).ToList();
        var hand = new Puzzle07.Hand(cards, 0);

        hand.Type.Should().Be(expectedType);
    }

    [Theory]
    [InlineData("KTJJT", "KK677")]
    [InlineData("T55J5", "QQQJA")]
    public void TestHandStrength(string weaker, string stronger)
    {
        var weakerCards = weaker.Select(Puzzle07.CreateCard).ToList();
        var weakerHand = new Puzzle07.Hand(weakerCards, 0);
        var strongerCards = stronger.Select(Puzzle07.CreateCard).ToList();
        var strongerHand = new Puzzle07.Hand(strongerCards, 0);

        strongerHand.GlobalRank.Should().BeGreaterThan(weakerHand.GlobalRank);
    }
}
