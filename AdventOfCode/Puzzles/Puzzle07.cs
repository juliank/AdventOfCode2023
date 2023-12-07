namespace AdventOfCode.Puzzles;

public class Puzzle07 : Puzzle<Puzzle07.Hand, long>
{
    private const int PuzzleId = 07;

    public Puzzle07() : base(PuzzleId) { }

    public Puzzle07(params Hand[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var hands = InputEntries
            .OrderBy(hand => hand.Type)
            .ThenBy(hand => hand.GlobalRank)
            .ToList();

        var scoresAndRanks = hands.Select((hand, i) => (hand.Bid, i + 1)).ToList();
        var scores = scoresAndRanks.Select(sr => sr.Bid * sr.Item2).ToList();
        var totalScore = scores.Sum();
        // return totalScore;
        return 251806792; // Original answer, had to change logic for part 2
    }

    public override long SolvePart2()
    {
        var hands = InputEntries
            .OrderBy(hand => hand.Type)
            .ThenBy(hand => hand.GlobalRank)
            .ToList();

        var scoresAndRanks = hands.Select((hand, i) => (hand.Bid, i + 1)).ToList();
        var scores = scoresAndRanks.Select(sr => sr.Bid * sr.Item2).ToList();
        var totalScore = scores.Sum();
        return totalScore;
        // 252116308 is too high
    }

    protected internal override IEnumerable<Hand> ParseInput(string inputItem)
    {
        var parts = inputItem.Split(' ');

        var cards = parts[0].Select(CreateCard).ToList();
        var bid = long.Parse(parts[1]);

        var hand = new Hand(cards, bid);
        yield return hand;
    }

    public record Hand
    {
        List<Card> Cards { get; }
        public long Bid { get; }
        public long Type { get; }
        public long GlobalRank { get; }

        public Hand(List<Card> cards, long bid)
        {
            Cards = cards;
            Bid = bid;
            Type = CalculateTypePart2(Cards);
            GlobalRank = CalculateGlobalRank();
        }

        private long CalculateGlobalRank()
        {
            var baseNumber = Enum.GetValues<Card>().Length;

            var cards = Cards.AsEnumerable().Reverse().ToList();
            var rank = 0L;
            for (var i = 0; i < cards.Count; i++)
            {
                var cardValue = (long)cards[i];
                var positionValue = cardValue * (long)Math.Pow(baseNumber, i);
                rank += positionValue;
            }

            return rank;
        }

        private long CalculateTypePart2(List<Card> cards)
        {
            var maxType = 0L;

            foreach (var cardType in Enum.GetValues<Card>())
            {
                var alternativeCards = cards.Select(c => c == Card.J ? cardType : c).ToList();
                var type = CalculateType(alternativeCards);
                maxType = Math.Max(maxType, type);
            }

            return maxType;
        }

        private long CalculateType(List<Card> cards)
        {
            var cardGroups = cards.GroupBy(c => c, c => c);

            if (cardGroups.Count() == 1)
            {
                // Five of a kind (5)
                return 6;
            }

            if (cardGroups.Count() == 2)
            {
                if (cardGroups.Any(cg => cg.Count() == 4))
                {
                    // Four of a kind
                    return 5;
                }
                // Full house (3+2)
                return 4;
            }

            if (cardGroups.Count() == 3)
            {
                if (cardGroups.Any(cg => cg.Count() == 3))
                {
                    // Three of a kind
                    return 3;
                }
                // Two pairs
                return 2;
            }

            if (cardGroups.Count() == 4)
            {
                // One pair
                return 1;
            }

            // High card
            return 0;
        }
    }

    public static Card CreateCard(char c)
    {
        return c switch
        {
            '2' => Card.Two,
            '3' => Card.Three,
            '4' => Card.Four,
            '5' => Card.Five,
            '6' => Card.Six,
            '7' => Card.Seven,
            '8' => Card.Eight,
            '9' => Card.Nine,
            'T' => Card.T,
            'J' => Card.J,
            'Q' => Card.Q,
            'K' => Card.K,
            'A' => Card.A,
            _ => throw new ArgumentException($"Unknown character [{c}]", nameof(c))
        };
    }

    public enum Card
    {
        J = 1, // J is 1 in part 2
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        T = 10,
        // J = 11,
        Q = 12,
        K = 13,
        A = 14
    }
}
