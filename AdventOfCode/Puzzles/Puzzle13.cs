namespace AdventOfCode.Puzzles;

public class Puzzle13 : Puzzle<string, long>
{
    private const int PuzzleId = 13;

    public Puzzle13() : base(PuzzleId) { }

    public Puzzle13(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var patterns = ParsePatterns();

        var sumOfVerticalLines = 0;
        var sumOfHorizontalLines = 0;
        foreach (var pattern in patterns)
        {
            var verticalReflection = GetVerticalReflectionLine(pattern);
            sumOfVerticalLines += verticalReflection ?? 0;

            var horizontalReflection = GetHorizontalReflectionLine(pattern);
            sumOfHorizontalLines += horizontalReflection ?? 0;
        }

        var result = sumOfVerticalLines + 100 * sumOfHorizontalLines;
        return result;
    }

    public override long SolvePart2()
    {
        var patterns = ParsePatterns();

        var sumOfVerticalLines = 0;
        var sumOfHorizontalLines = 0;
        foreach (var pattern in patterns)
        {
            var verticalReflection = GetVerticalReflectionLineWithSmudge(pattern);
            var horizontalReflection = GetHorizontalReflectionLineWithSmudge(pattern);
            sumOfVerticalLines += verticalReflection ?? 0;
            sumOfHorizontalLines += horizontalReflection ?? 0;
        }

        var result = sumOfVerticalLines + 100 * sumOfHorizontalLines;
        return result;
        // 40212 is too high
        // 19619 is too low (correct answer for sample input)
        // 19223 is too low (still correct on sample input)
        // 15423 is even lower (but still correct with sample input...)
        // 28177 is not the right answer...
    }

    internal static int? GetVerticalReflectionLineWithSmudge(List<string> pattern)
    {
        var flipped = Enumerable.Range(0, pattern[0].Length)
            .Select(i => pattern.Select(line => line[i]))
            .Select(chars => chars.CreateString())
            .ToList();
        return GetHorizontalReflectionLineWithSmudge(flipped);
    }

    internal static int? GetHorizontalReflectionLineWithSmudge(List<string> pattern)
    {
        var originalHorizontalReflection = GetHorizontalReflectionLine(pattern);
        for (var i = 0; i < pattern.Count; i++)
        {
            var originalLine = pattern[i];
            for (var c = 0; c < originalLine.Length; c++)
            {
                var smudgedLine = new StringBuilder(originalLine);
                smudgedLine[c] = smudgedLine[c] == '#' ? '.' : '#';

                pattern[i] = smudgedLine.ToString(); // Flip single character

                var reflectionLine = GetHorizontalReflectionLine(pattern);
                if (reflectionLine.HasValue && reflectionLine != originalHorizontalReflection)
                {
                    return reflectionLine;
                }
            }
            pattern[i] = originalLine; // Replace with original
        }
        return null;
    }

    internal static int? GetVerticalReflectionLine(List<string> pattern)
    {
        var flipped = Enumerable.Range(0, pattern[0].Length)
            .Select(i => pattern.Select(line => line[i]))
            .Select(chars => chars.CreateString())
            .ToList();
        return GetHorizontalReflectionLine(flipped);
    }

    internal static int? GetHorizontalReflectionLine(List<string> pattern)
    {
        int? reflectionLine = null;

        for (var candidate = 1; candidate < pattern.Count; candidate++)
        {
            var toCheck = Math.Min(candidate, pattern.Count - candidate);
            var areEqual = true;

            for (var i = 0; i < toCheck; i++)
            {
                var above = pattern[candidate - 1 - i];
                var below = pattern[candidate + i];
                areEqual = above == below;
                if (!areEqual)
                {
                    break;
                }
            }

            if (areEqual)
            {
                reflectionLine = candidate;
                break;
            }
        }

        return reflectionLine;
    }

    private List<List<string>> ParsePatterns()
    {
        var patterns = new List<List<string>>();

        List<string> patternLines = [];
        foreach (var line in InputEntries)
        {
            if (string.IsNullOrEmpty(line))
            {
                patterns.Add(patternLines);
                patternLines = [];
            }
            else
            {
                patternLines.Add(line);
            }
        }

        return patterns;
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }
}
