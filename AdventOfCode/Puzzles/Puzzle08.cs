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
        (var directions, var nodes) = ParseInput();

        var stepsToEnd = new List<long>();
        var startNodes = nodes.Keys.Where(n => n.EndsWith('A'));
        foreach (var startNode in startNodes)
        {
            var nextNode = startNode;
            char direction;
            var steps = 0L;
            while (!nextNode.EndsWith('Z'))
            {
                direction = directions[(int)(steps % directions.Length)];
                nextNode = direction == 'L' ? nodes[nextNode].L : nodes[nextNode].R;
                steps++;
            }
            stepsToEnd.Add(steps);
        }
        var lcm = MathX.LeastCommonMultiple(stepsToEnd.ToArray());
        return lcm;
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
