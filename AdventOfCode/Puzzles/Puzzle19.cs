namespace AdventOfCode.Puzzles;

public class Puzzle19 : Puzzle<string, long>
{
    private const int PuzzleId = 19;

    public Puzzle19() : base(PuzzleId) { }

    public Puzzle19(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        (var workflows, var parts) = ProcessInput();

        var rejectedParts = new List<Part>();
        var acceptedParts = new List<Part>();

        var initialWorkflow = workflows["in"];
        foreach (var part in parts)
        {
            var currentWorkflow = initialWorkflow;
            var doneWithPart = false;
            while (!doneWithPart)
            {
                foreach (var rule in currentWorkflow.Rules)
                {
                    var nextWorkflow = rule.GetNextWorkflow(part);
                    if (string.IsNullOrEmpty(nextWorkflow))
                    {
                        continue;
                    }
                    if (nextWorkflow == "A")
                    {
                        acceptedParts.Add(part);
                        doneWithPart = true;
                        break;
                    }
                    if (nextWorkflow == "R")
                    {
                        rejectedParts.Add(part);
                        doneWithPart = true;
                        break;
                    }
                    currentWorkflow = workflows[nextWorkflow];
                    break;
                }
            }
        }

        var ratingNumbers = acceptedParts.Select(p => p.X + p.M + p.A + p.S).ToList();
        var allRatings = ratingNumbers.Sum();

        return allRatings;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    private (Dictionary<string, Workflow> workflows, List<Part> parts) ProcessInput()
    {
        var workflows = new List<Workflow>();
        var i = 0;
        for (; i < InputEntries.Count; i++)
        {
            if (string.IsNullOrEmpty(InputEntries[i]))
            {
                i++;
                break;
            }
            var workflow = Workflow.Create(InputEntries[i]);
            workflows.Add(workflow);
        }

        var parts = new List<Part>();
        for (; i < InputEntries.Count; i++)
        {
            // {x=787,m=2655,a=1222,s=2876}
            var values = InputEntries[i][1..^1]
                .Split(',')
                .Select(s => s[2..])
                .Select(s => int.Parse(s))
                .ToArray();

            var part = new Part(values[0], values[1], values[2], values[3]);
            // {
            //     X = values[0],
            //     M = values[1],
            //     A = values[2],
            //     S = values[3],
            // };
            parts.Add(part);
        }

        var workflowsDict = workflows.ToDictionary(wf => wf.Name, wf => wf);
        return (workflowsDict, parts);
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }

    public record Part(int X, int M, int A, int S);

    public record Workflow
    {
        public required string Name { get; init; }
        public List<Rule> Rules { get; } = [];

        public static Workflow Create(string input)
        {
            // px{a<2006:qkq,m>2090:A,rfg}
            var parts = input.Split('{');
            var name = parts.First();
            var steps = parts.Last().TrimEnd('}').Split(',');

            var rules = new List<Rule>();
            foreach (var step in steps)
            {
                string nextWorkflow;
                Func<Part, int>? selector = null;
                Func<int, bool>? ruleLogic = null;

                if (step.Contains(':'))
                {
                    // a<2006:qkq
                    var stepParts = step.Split(':');
                    nextWorkflow = stepParts.Last();

                    var comparison = stepParts.First();
                    var compareField = comparison[0];
                    var compareOperator = comparison[1];
                    var compareValue = int.Parse(comparison[2..]);

                    selector = compareField switch
                    {
                        'x' => (Part p) => p.X,
                        'm' => (Part p) => p.M,
                        'a' => (Part p) => p.A,
                        _ => (Part p) => p.S
                    };

                    ruleLogic = compareOperator switch
                    {
                        '>' => (int i) => i > compareValue,
                        _ => (int i) => i < compareValue
                    };
                }
                else
                {
                    nextWorkflow = step;
                }

                var rule = new Rule(nextWorkflow, selector, ruleLogic);
                rules.Add(rule);
            }

            var workflow = new Workflow { Name = name };
            workflow.Rules.AddRange(rules);
            return workflow;
        }
    }

    public record Rule
    {
        private readonly string _nextWorkflow;
        private readonly Func<Part, int>? _selector;
        private readonly Func<int, bool>? _ruleLogic;

        public Rule(string nextWorkflow, Func<Part, int>? selector, Func<int, bool>? ruleLogic)
        {
            _nextWorkflow = nextWorkflow;
            _selector = selector;
            _ruleLogic = ruleLogic;
        }
        public string? GetNextWorkflow(Part part)
        {
            if (_selector == null || _ruleLogic == null)
            {
                return _nextWorkflow;
            }

            var value = _selector(part);
            var isFulfilled = _ruleLogic(value);
            if (isFulfilled)
            {
                return _nextWorkflow;
            }

            return null;
        }
    }
}
