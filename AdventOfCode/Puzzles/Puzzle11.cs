namespace AdventOfCode.Puzzles;

public class Puzzle11 : Puzzle<List<char>, long>
{
    private const int PuzzleId = 11;

    public Puzzle11() : base(PuzzleId) { }

    public Puzzle11(params List<char>[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var (xExpansions, yExpansions) = ExpandUniverse(1);
        var galaxies = CreateGalaxies(xExpansions, yExpansions);
        return CalculateSumOfPaths(galaxies);
    }

    public override long SolvePart2()
    {
        var (xExpansions, yExpansions) = ExpandUniverse(999999);
        var galaxies = CreateGalaxies(xExpansions, yExpansions);
        return CalculateSumOfPaths(galaxies);
    }

    private (List<int> XExpansions, List<int> YExpansions) ExpandUniverse(int expansionCount)
    {
        var rows = InputEntries.Count;

        var yExpansions = new List<int>();
        var currentExpansion = 0;
        for (var y = 0; y < rows; y++)
        {
            if (InputEntries[y].All(ieY => ieY == '.'))
            {
                currentExpansion += expansionCount;
            }
            yExpansions.Add(currentExpansion);
        }

        var columns = InputEntries[0].Count;
        var xExpansions = new List<int>();
        currentExpansion = 0;
        for (var x = 0; x < columns; x++)
        {
            if (InputEntries.Select(ie => ie[x]).All(ieX => ieX == '.'))
            {
                currentExpansion += expansionCount;
            }
            xExpansions.Add(currentExpansion);
        }

        return (xExpansions, yExpansions);
    }

    private List<Point> CreateGalaxies(List<int> xExpansions, List<int> yExpansions)
    {
        var galaxies = new List<Point>();

        for (var y = 0; y < InputEntries.Count; y++)
        {
            for (var x = 0; x < InputEntries[0].Count; x++)
            {
                if (InputEntries[y][x] == '#')
                {
                    var xPosition = x + xExpansions[x];
                    var yPosition = y + yExpansions[y];
                    galaxies.Add(new Point(xPosition, yPosition));
                }
            }
        }

        return galaxies;
    }

    private long CalculateSumOfPaths(List<Point> galaxies)
    {
        var shortestPathSum = 0L;
        for (var g1 = 0; g1 < galaxies.Count - 1; g1++)
        {
            var first = galaxies[g1];
            for (var g2 = g1 + 1; g2 < galaxies.Count; g2++)
            {
                var second = galaxies[g2];
                var distance = first.ManhattanDistanceTo(second);
                shortestPathSum += distance;
            }
        }

        return shortestPathSum;
    }

    protected internal override IEnumerable<List<char>> ParseInput(string inputItem)
    {
        yield return inputItem.ToList();
    }
}
