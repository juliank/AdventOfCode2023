namespace AdventOfCode.Puzzles;

public class Puzzle08 : Puzzle<string, long>
{
    private const int PuzzleId = 08;

    public Puzzle08() : base(PuzzleId) { }

    public Puzzle08(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        (var directions, var nodes) = ParseInput();

        var nextNode = "AAA";
        char direction;
        var steps = 0;
        while (nextNode != "ZZZ")
        {
            direction = directions[steps % directions.Length];
            nextNode = direction == 'L' ? nodes[nextNode].L : nodes[nextNode].R;
            steps++;
        }

        return steps;
    }

    public override long SolvePart2()
    {
        if (DateTime.Now > DateTime.MinValue)
        {
            // The current brute force solution doesn't work for part two.
            // After two days in, we're still not getting any answer...
            // The solution is probably mathematical (least common denominator?).
            throw new NotImplementedException();
        }

        (var directions, var nodes) = ParseInput();

        var nextNodes = nodes.Keys.Where(k => k.EndsWith('A')).ToList();
        char direction;
        var steps = 0L;

        var sw = Stopwatch.StartNew();
        while (nextNodes.Any(n => n[2] != 'Z'))
        {
            // Must cast to int for array index (this is safe because we've done % directions.Length)
            var directionIndex = (int)(steps % directions.Length);
            direction = directions[directionIndex];
            nextNodes = direction == 'L'
                ? nextNodes.Select(n => nodes[n].L).ToList()
                : nextNodes.Select(n => nodes[n].R).ToList();
            steps++;

            if (steps % 10_000_000 == 0)
            {
                // Console.WriteLine($"{DateTime.Now.TimeOfDay}: Processed {steps:#,0} steps in {(sw.ElapsedMilliseconds / 1000):#,0} seconds");
                Console.WriteLine($"{DateTime.Now.TimeOfDay}: Processed {steps:#,0} steps in {sw.Elapsed}");
            }
        }

        return steps;
    }

    private (string Directions, Dictionary<string, (string L, string R)> Nodes) ParseInput()
    {
        var directions = InputEntries[0];
        var nodes = InputEntries.Skip(2)
            .Select(ParseNode)
            .ToDictionary(n => n.X, n => (n.L, n.R));
        return (directions, nodes);
    }

    private (string X, string L, string R) ParseNode(string str)
    {
        var parts = str.Split(" = ");
        var x = parts[0];
        var rl = parts[1][1..^1].Split(", ");
        return (x, rl[0], rl[1]);
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }
}
