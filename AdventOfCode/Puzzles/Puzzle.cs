[assembly: InternalsVisibleTo("AdventOfCode.Tests")]
namespace AdventOfCode.Puzzles;

public abstract class Puzzle<TInput, TResult> : IPuzzle
    where TInput : notnull
    where TResult : notnull
{
    public int Id { get; }

    private List<TInput>? inputEntries;

    public List<TInput> InputEntries
    {
        get
        {
            if (inputEntries == null)
            {
                inputEntries = LoadInput();
            }
            return inputEntries;
        }
        protected set
        {
            inputEntries = value;
        }
    }

    public Puzzle(int id)
    {
        Id = id;
    }

    // Constructor to allow creating the puzzle with a limited amount of entries (for easier testing)
    protected Puzzle(int id, params TInput[] inputEntries) : this(id)
    {
        this.inputEntries = new List<TInput>(inputEntries);
    }

    object IPuzzle.Solve() => Solve();

    public virtual bool SkipPart1WhenSolveAll => false;

    object IPuzzle.SolvePart1() => SolvePart1();

    public virtual bool SkipPart2WhenSolveAll => false;

    object IPuzzle.SolvePart2() => SolvePart2();

    public TResult Solve()
    {
        TResult result;

        Stopwatch sw;
        try
        {
            sw = Stopwatch.StartNew();
            result = SolvePart2();
        }
        catch (NotImplementedException)
        {
            Console.WriteLine("Solution to part 2 is not implemented yet, solving part 1 instead");
            sw = Stopwatch.StartNew();
            result = SolvePart1();
        }

        sw.Stop();
        var elapsed = sw.Elapsed < TimeSpan.FromSeconds(1) ? $"{sw.ElapsedMilliseconds} ms" : $"{sw.Elapsed}";
        Console.WriteLine($"Solution time: {elapsed}");

        return result;
    }

    public abstract TResult SolvePart1();

    public abstract TResult SolvePart2();

    protected internal abstract IEnumerable<TInput> ParseInput(string inputItem);

    private List<TInput> LoadInput()
    {
        var fileName = $"{Id:D2}.txt";
        var path = FileHelper.GetInputFilePath(fileName);

        var entries = new List<TInput>();
        foreach (var line in FileHelper.ReadFile(path))
        {
            entries.AddRange(ParseInput(line));
        }

        return entries;
    }
}
