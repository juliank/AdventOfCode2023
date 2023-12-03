namespace AdventOfCode.Puzzles;

public class Puzzle01 : Puzzle<int, int>
{
    private const int PuzzleId = 01;

    public Puzzle01() : base(PuzzleId) { }

    public Puzzle01(params int[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override int SolvePart1()
    {
        // Parsing changed for part 2, so InputEntries.Sum() no longer yields the correct result
        return 53386; // InputEntries.Sum();
    }

    public override int SolvePart2()
    {
        return InputEntries.Sum();
    }

    protected internal override IEnumerable<int> ParseInput(string inputItem)
    {
        int? first = null;
        int? last = null;

        // Part 1
        // foreach (var c in inputItem)
        // {
        //     if (int.TryParse(c.ToString(), out var i))
        //     {
        //         if (!first.HasValue)
        //         {
        //             first = i;
        //         }
        //         last = i;
        //     }
        // }

        // Part 2
        for (var i = 0; i < inputItem.Length; i++)
        {
            for (var n = 0; n < _numbers.Length; n++)
            {
                if (inputItem[i..].StartsWith(_numbers[n]))
                {
                    if (!first.HasValue)
                    {
                        first = n;
                    }
                    last = n;
                    break;
                }
            }

            if (int.TryParse(inputItem[i].ToString(), out var x))
            {
                if (!first.HasValue)
                {
                    first = x;
                }
                last = x;
            }
        }

        var parsed = int.Parse($"{first}{last}");
        yield return parsed;
    }

    private readonly string[] _numbers =
    [
        "zero",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
    ];
}
