namespace AdventOfCode.Puzzles;

public class Puzzle03 : Puzzle<List<Puzzle03.Element>, long>
{
    private const int PuzzleId = 03;

    public Puzzle03() : base(PuzzleId) { }

    public Puzzle03(params List<Element>[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var elements = InputEntries.SelectMany(ie => ie).ToList();

        // All elements with value <0 are symbols
        var symbols = elements.Where(e => e.Value < 0).ToList();
        // All elements with value >= 0 (or in reality >0) are possible engine parts
        var engineCandidates = elements.Where(e => e.Value >= 0).ToList();

        var engineParts = new List<Element>();
        foreach (var candidate in engineCandidates)
        {
            var candidatePosition = candidate.Position;

            // Since a number (engine part) can cover an arbitrary number of positions (<=3),
            // we have to check if _any_ of these positions can be adjacent to a symbol.
            for (var x = 0; x < candidate.Size; x++)
            {
                if (symbols.Any(s => s.Position.IsAdjacentTo(candidatePosition)))
                {
                    engineParts.Add(candidate);
                    break; // Break to avoid adding the same engine part multiple times
                }
                candidatePosition = candidatePosition.GetE()!;
            }
        }

        var sum = engineParts.Select(ep => ep.Value).Sum();
        return sum;
    }

    public override long SolvePart2()
    {
        var elements = InputEntries.SelectMany(ie => ie).ToList();

        var gearCandidates = elements.Where(e => e.Value == -2).ToList();
        var engineCandidates = elements.Where(e => e.Value >= 0).ToList();

        var gearRatios = new List<long>();
        foreach (var gearCandidate in gearCandidates)
        {
            var adjacentElements = new List<Element>();

            foreach (var engineCandidate in engineCandidates)
            {
                var candidatePosition = engineCandidate.Position;
                for (var x = 0; x < engineCandidate.Size; x++)
                {
                    if (gearCandidate.Position.IsAdjacentTo(candidatePosition))
                    {
                        adjacentElements.Add(engineCandidate);
                        break;
                    }
                    candidatePosition = candidatePosition.GetE()!;
                }
            }

            if (adjacentElements.Count == 2)
            {
                var gearRatio = adjacentElements[0].Value * adjacentElements[1].Value;
                gearRatios.Add(gearRatio);
            }
        }

        var sum = gearRatios.Sum();
        return sum;
    }

    private static int _yCounter = 0;

    protected internal override IEnumerable<List<Element>> ParseInput(string inputItem)
    {
        // Example input
        // 467..114..
        // ...*......
        // ..35..633.
        // ......#...
        // 617*......
        // .....+.58.
        // ..592.....
        // ......755.
        // ...$.*....
        // .664.598..

        var elements = new List<Element>();

        bool AddToElementsIfNumber(int position, int digits)
        {
            if (inputItem[position..].Take(digits).TryParse(out int number))
            {
                var element = new Element(number, new Point(position, _yCounter), digits);
                elements.Add(element);
                return true;
            }
            return false;
        }

        // We've added 2 periods to the end of the input entries, for simplicity
        for (var x = 0; x < inputItem.Length - 3; x++)
        {
            if (inputItem[x] == '.')
            {
                continue;
            }

            if (inputItem[x] == '*')
            {
                // If the symbol is a *, this is a special cymbol indicating a cog.
                // We give this a value of -2, for easier filtration later on.
                var element = new Element(-2, new Point(x, _yCounter), 1);
                elements.Add(element);
            }
            else if (inputItem[x] is '+' or '-')
            {
                // + or - at the start of a number will be parsed as a valid integer,
                // so we have to check these two symbols explicitly first
                var element = new Element(-1, new Point(x, _yCounter), 1);
                elements.Add(element);
            }
            else if (AddToElementsIfNumber(x, 3))
            {
                x += 2; // Additional increment to x, to skipe the whole number we just found
            }
            else if (AddToElementsIfNumber(x, 2))
            {
                x += 1;
            }
            else if (!AddToElementsIfNumber(x, 1))
            {
                // Any other symbol is represented with the value -1
                var element = new Element(-1, new Point(x, _yCounter), 1);
                elements.Add(element);
            }
        }

        _yCounter++;

        yield return elements;
    }

    /// <summary>
    /// If the element has a value <0 it is a symbol, otherwise it is a number
    /// </summary>
    public record Element(int Value, Point Position, int Size);

    private static bool TryParse(IEnumerable<char> chars, out int result)
    {
        var s = new string(chars.ToArray());
        if (int.TryParse(s, out var i))
        {
            result = i;
            return true;
        }

        result = 0;
        return false;
    }
}
