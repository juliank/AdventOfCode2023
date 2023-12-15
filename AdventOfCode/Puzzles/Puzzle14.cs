namespace AdventOfCode.Puzzles;

public class Puzzle14 : Puzzle<string, long>
{
    private const int PuzzleId = 14;

    public Puzzle14() : base(PuzzleId) { }

    public Puzzle14(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var platform = CreatePlatform();

        platform = TiltNorth(platform);

        var totalLoad = CalculateLoad(platform);
        return totalLoad;
    }

    public override long SolvePart2()
    {
        var platform = CreatePlatform();
        // Console.WriteLine("Initial platform:");
        // OutputPlatform(platform);
        // Console.WriteLine();

        // for (int i = 0; i < 4; i++)
        // {
        //     platform = Rotate90DegreeRight(platform);
        //     Console.WriteLine($"Rotated platform {i}:");
        //     OutputPlatform(platform);
        //     Console.WriteLine();
        // }

        // if (DateTime.Now > DateTime.MinValue)
        // {
        //     return -1;
        // }

        var sw = Stopwatch.StartNew();
        var cycleCount = 1_000_000_000;
        var lastCycle = false;
        for (var i = 1; i <= cycleCount; i++)
        {
            (platform, var foundInCache) = TiltCycle(platform, i);
            var cacheKey = platform.SelectMany(line => line).CreateString();
            if (i <= 2)
            {
                // Console.WriteLine($"After cycle {i}:");
                // OutputPlatform(platform);
                // Console.WriteLine();
            }
            if (foundInCache && !lastCycle)
            {
                // Console.WriteLine($"Found in cache at {i}:");
                // OutputPlatform(platform);
                // Console.WriteLine();

                // Console.WriteLine();
                // Console.WriteLine($"Got the first cache hit at i = {i} - key: {cacheKey}");
                // var cacheKey = platform.SelectMany(line => line).CreateString();
                var cacheHit = CycleMap[cacheKey];
                var cycleLength = i - cacheHit - 1;
                var cyclesToSkip = (cycleCount / cycleLength) - 1;
                i += cyclesToSkip * cycleLength;
                lastCycle = true;
                // Console.WriteLine($"Cycle length is {cycleLength} (starting at i = {cacheHit})");
                // Console.WriteLine($"Skipping {cyclesToSkip} cycles, continuing at i = {i}");
                // Console.WriteLine();

            }
            if (i % 1_000_000 == 0 || i <= 20 || i >= cycleCount - 20)
            {
                var currentLoad = CalculateLoad(platform);
                // Console.WriteLine($"{DateTime.Now}: Tilted {i:#,0} cycles in {sw.Elapsed} - current load is {currentLoad} - cache key: [{cacheKey}]");
                // Console.WriteLine($"After {i:#,0} cycles ({sw.Elapsed}) - current load: {currentLoad}");
                // OutputPlatform(platform);
                // Console.WriteLine();
            }
        }

        // Console.WriteLine();
        // Console.WriteLine("Cycle cache:");
        // foreach (var cachedKey in CycleMap)
        // {
        //     Console.WriteLine($"{cachedKey.Value}: {cachedKey.Key}");
        // }
        var totalLoad = CalculateLoad(platform);
        return totalLoad;
        // 89805 is too low (first attempt with optimized solution)
        // 89824 is too low (after trying to take cache cycle length into account)
    }

    private static void OutputPlatform(char[][] platform)
    {
        foreach (var line in platform)
        {
            Console.WriteLine(line.CreateString());
        }
    }

    private static readonly Dictionary<string, char[][]> TiltCache = [];
    private static readonly Dictionary<string, int> CycleMap = [];

    private static char[][]? GetFromCache(string cacheKey)
    {
        var cached = TiltCache.TryGetValue(cacheKey, out var cachedPlatform);
        if (!cached)
        {
            return null;
        }

        var copy = CopyPlatform(cachedPlatform!);
        return copy;
    }

    private static char[][] CopyPlatform(char[][] platform)
    {
        var copy = new List<char[]>();
        foreach (var line in platform)
        {
            copy.Add([.. line]);
        }
        return copy.ToArray();
    }

    private static (char[][] TiltCycle, bool FoundInCache) TiltCycle(char[][] platform, int cycle)
    {
        var cacheKey = platform.SelectMany(line => line).CreateString();
        // var cached = TiltCache.TryGetValue(cacheKey, out var cachedPlatform);
        var cachedPlatform = GetFromCache(cacheKey);
        if (cachedPlatform != null)
        {
            return (cachedPlatform!, true);
        }

        platform = TiltNorth(platform);

        platform = Rotate90DegreeRight(platform);
        platform = TiltNorth(platform); // West is now north

        platform = Rotate90DegreeRight(platform);
        platform = TiltNorth(platform); // South is now north

        platform = Rotate90DegreeRight(platform);
        platform = TiltNorth(platform); // East is now north

        platform = Rotate90DegreeRight(platform); // North is now north again

        // if (cached)
        // {
        //     Console.WriteLine("Cached platform:");
        //     OutputPlatform(cachedPlatform!);
        //     Console.WriteLine("Tilted platform:");
        //     OutputPlatform(platform);
        // }
        // else
        // {
        //     TiltCache.Add(cacheKey, platform);
        // }
        TiltCache.Add(cacheKey, platform);
        CycleMap.Add(cacheKey, cycle);

        return (platform, false);
    }

    private static char[][] Rotate90DegreeRight(char[][] platform)
    {
        var rotatedPlatform = new List<char[]>();
        for (var col = 0; col < platform[0].Length; col++)
        {
            var rotatedColumn = platform
                .Select(row => row[col])
                .Reverse()
                .ToArray();
            rotatedPlatform.Add(rotatedColumn);
        }

        return rotatedPlatform.ToArray();
    }

    private static char[][] TiltNorth(char[][] platform)
    {
        var tiltedPlatform = CopyPlatform(platform);

        var row = 0;
        while (row < tiltedPlatform.Length - 1)
        {
            var changes = SlideNorth(tiltedPlatform, row);
            if (!changes)
            {
                row++; // We're done with this row
            }
        }

        return tiltedPlatform;
    }

    private static bool SlideNorth(char[][] platform, int row)
    {
        var changes = false;
        var thisRow = platform[row];
        var nextRow = platform[row + 1];

        for (var r = 0; r < thisRow.Length; r++)
        {
            if (thisRow[r] == '.' && nextRow[r] == 'O')
            {
                thisRow[r] = 'O';
                nextRow[r] = '.';
                changes = true;
            }
        }

        var next = row + 1;
        if (next < platform.Length - 1)
        {
            changes = SlideNorth(platform, next) || changes;
        }

        return changes;
    }

    private static long CalculateLoad(char[][] platform)
    {
        var totalLoad = 0L;

        for (var r = 0; r < platform.Length; r++)
        {
            var rowFactor = platform.Length - r;
            var roundedRocks = platform[r].Count(c => c == 'O');
            var rowScore = rowFactor * roundedRocks;
            totalLoad += rowScore;
        }

        return totalLoad;
    }

    private char[][] CreatePlatform()
    {
        var columns = InputEntries[0].Length;
        var rows = InputEntries.Count;

        var platform = InputEntries.Select(row => row.ToCharArray()).ToArray();

        return platform;
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }
}
