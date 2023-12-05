using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles;

public class Puzzle05 : Puzzle<string, long>
{
    private const int PuzzleId = 05;

    public Puzzle05() : base(PuzzleId) { }

    public Puzzle05(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var seeds = InputEntries[0]
            .Split(':').Last()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => long.Parse(s))
            .ToList();

        var maps = CreateMaps();
        var lowestLocation = long.MaxValue;

        foreach (var seed in seeds)
        {
            var soil = maps["seed"].Map(seed);
            var fertilizer = maps["soil"].Map(soil);
            var water = maps["fertilizer"].Map(fertilizer);
            var light = maps["water"].Map(water);
            var temperature = maps["light"].Map(light);
            var humidity = maps["temperature"].Map(temperature);
            var location = maps["humidity"].Map(humidity);

            if (location < lowestLocation)
            {
                lowestLocation = location;
            }
        }

        return lowestLocation;
    }

    public override long SolvePart2()
    {
        // Brute force run of initial solution:
        // Solution time: 00:54:29.1470669
        // Result is: [26714516]
        if (DateTime.Now > DateTime.MinValue)
        {
            // Random if check to avoid compiler warnings for early return.
            // Might look into improving the solution later on, if time...
            // The commented out attempts below, both using parallelization and caching,
            // both resulted in an out of memory exception.
            return 26714516;
        }

        var seeds = InputEntries[0]
            .Split(':').Last()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => long.Parse(s))
            .ToList();

        var maps = CreateMaps();
        var lowestLocation = long.MaxValue;

        //var seedMap = new ConcurrentDictionary<long, long>();

        for (var i = 0; i < seeds.Count - 2; i += 2)
        {
            // Parallel.For(0L, seeds[i + 1] - 1, (long j) =>
            // {
            for (var j = 0; j < seeds[i + 1] - 1; j++)
            {
                var seed = seeds[i] + j;
                //if (!seedMap.TryGetValue(seed, out var location))
                //{
                var soil = maps["seed"].Map(seed);
                var fertilizer = maps["soil"].Map(soil);
                var water = maps["fertilizer"].Map(fertilizer);
                var light = maps["water"].Map(water);
                var temperature = maps["light"].Map(light);
                var humidity = maps["temperature"].Map(temperature);
                var location = maps["humidity"].Map(humidity);

                // seedMap.Add(seed, location);
                //    seedMap.AddOrUpdate(seed, location, (key, oldValue) => location);
                //}

                if (location < lowestLocation)
                {
                    lowestLocation = location;
                }
            }//);
        }

        return lowestLocation;
    }

    private Dictionary<string, GardenMap> CreateMaps()
    {
        var startWithDigit = new Regex("^\\d");

        var gardenMaps = new List<GardenMap>();

        var mapTitle = string.Empty;
        var gardenRanges = new List<GardenRange>();

        for (var i = 2; i < InputEntries.Count; i++)
        {
            var item = InputEntries[i];

            if (string.IsNullOrEmpty(item))
            {
                // At the end of a sequence
                var map = new GardenMap(mapTitle, gardenRanges);
                gardenMaps.Add(map);
                mapTitle = string.Empty;
                gardenRanges = [];
            }
            else if (startWithDigit.Match(item).Success)
            {
                var range = new GardenRange(item);
                gardenRanges.Add(range);
            }
            else
            {
                mapTitle = item;
            }
        }

        var maps = gardenMaps.ToDictionary(gm => gm.Source, gm => gm);
        return maps;
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }

    public class GardenRange
    {
        public long DestinationRange { get; set; }
        public long SourceRange { get; set; }
        public long RangeLength { get; set; }

        // Input example: 50 98 2
        //   destination range start of 50, a source range start of 98, and a range length of 2
        public GardenRange(string input)
        {
            var parts = input.Split(' ');
            DestinationRange = long.Parse(parts[0]);
            SourceRange = long.Parse(parts[1]);
            RangeLength = long.Parse(parts[2]);
        }

        public long? Map(long source)
        {
            var sourceDiff = source - SourceRange;
            if (sourceDiff < 0 || sourceDiff >= RangeLength)
            {
                return null;
            }
            return DestinationRange + sourceDiff;
        }
    }

    public class GardenMap
    {
        public string Source { get; }
        public string Destination { get; }
        public List<GardenRange> Ranges { get; }

        //private readonly Dictionary<long, long> _map = new Dictionary<long, long>();

        // Input example: seed-to-soil map:
        public GardenMap(string map, List<GardenRange> ranges)
        {
            var srcToDest = map.Split(' ').First();
            var parts = srcToDest.Split('-');
            Source = parts[0];
            Destination = parts[2];
            Ranges = ranges;
        }

        public long Map(long source)
        {
            //if (_map.TryGetValue(source, out var destination))
            //{
            //    return destination;
            //}

            long destination;
            var rangeMatch = Ranges.FirstOrDefault(r => r.Map(source).HasValue);
            var rangeMap = rangeMatch?.Map(source);
            if (rangeMap.HasValue)
            {
                destination = rangeMap.Value;
            }
            else
            {
                destination = source;
            }

            //_map.Add(source, destination);
            return destination;
        }
    }
}
