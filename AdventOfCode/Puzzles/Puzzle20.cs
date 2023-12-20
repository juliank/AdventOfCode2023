namespace AdventOfCode.Puzzles;

public class Puzzle20 : Puzzle<string, long>
{
    private const int PuzzleId = 20;

    public Puzzle20() : base(PuzzleId) { }

    public Puzzle20(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var modules = CreateModules(InputEntries);
        return -1;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    internal static List<Module> CreateModules(List<string> inputEntries)
    {
        var modules = new List<Module>
        {
            new() { Name = "button", Type = ModuleType.Button, OutputNames = ["broadcaster"] }, // We always start with a button
            new() { Name = "output", Type = ModuleType.Output, OutputNames = [] } // Dummy module as output for non-existing modules
        };

        foreach (var item in inputEntries)
        {
            var module = CreateModule(item);
            modules.Add(module);
        }

        var modulesLookup = modules.ToDictionary(m => m.Name, m => m);
        foreach (var module in modules)
        {
            foreach (var outputName in module.OutputNames)
            {
                if (!modulesLookup.TryGetValue(outputName, out var outputModule))
                {
                    // If a given output doesn't exist, we use the default dummy module "ouput"
                    outputModule = modulesLookup["output"];
                }
                module.Ouputs.Add(outputModule);
                outputModule.Inputs.Add((Module: module, PreviousPulse: 0));
            }
        }

        return modules;
    }

    private static Module CreateModule(string item)
    {
        var parts = item.Split(" -> ");
        var m = parts[0];
        (var name, var type) = m switch
        {
            ['%', .. var n] => (n, ModuleType.FlipFlop),
            ['&', .. var n] => (n, ModuleType.Conjunction),
            _ => (m, ModuleType.Broadcaster)
        };
        var outputs = parts[1].Split(", ").ToArray();

        var module = new Module
        {
            Name = name,
            Type = type,
            OutputNames = outputs
        };
        return module;
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        yield return inputItem;
    }

    public record Module
    {
        public required string Name { get; init; }
        public required ModuleType Type { get; init; }
        public List<(Module Module, int PreviousPulse)> Inputs { get; } = [];
        public List<Module> Ouputs { get; } = [];
        public required string[] OutputNames { get; init; } // Only temporary, for initialization
    }

    public enum ModuleType { FlipFlop, Conjunction, Broadcaster, Button,
        Output
    }
}
