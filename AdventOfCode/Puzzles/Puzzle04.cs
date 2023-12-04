namespace AdventOfCode.Puzzles;

public class Puzzle04 : Puzzle<Puzzle04.Card, long>
{
    private const int PuzzleId = 04;

    public Puzzle04() : base(PuzzleId) { }

    public Puzzle04(params Card[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var totalPoints = InputEntries.Select(card => card.Points).Sum();
        return totalPoints;
    }

    public override long SolvePart2()
    {
        for (var i = 0; i < InputEntries.Count; i++)
        {
            var card = InputEntries[i];
            var copies = card.NumberOfWinningNumbers + i + 1;

            // For each N winning number on the previous card, each of the N following
            // cards gets an extra copy for every number of the previous card
            for (var j = i + 1; j < copies; j++)
            {
                InputEntries[j].NumberOfCards += card.NumberOfCards;
            }
        }

        var totalNumberOfCards = InputEntries.Select(c => c.NumberOfCards).Sum();
        return totalNumberOfCards;
    }

    protected internal override IEnumerable<Card> ParseInput(string inputItem)
    {
        // Example input:
        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        // Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        // Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        var parts = inputItem.Split(':');
        var id = parts[0].Skip(5).ParseInt();

        var numbers = parts[1].Split('|');
        var winningNumbers = numbers[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.ParseInt())
            .ToList();
        var yourNumbers = numbers[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.ParseInt())
            .ToList();

        yield return new Card(id, winningNumbers, yourNumbers); ;
    }


    public record Card(int Id, List<int> WinningNumbers, List<int> YourNumbers)
    {
        public int NumberOfCards { get; set; } = 1;

        public int NumberOfWinningNumbers { get; } = WinningNumbers.Intersect(YourNumbers).Count();

        public int Points => (int)(double)Math.Pow(2, NumberOfWinningNumbers - 1);
    }
}
