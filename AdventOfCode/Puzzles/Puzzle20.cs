namespace AdventOfCode.Puzzles;

public class Puzzle20 : Puzzle<string, long>
{
    private const int PuzzleId = 20;

    public Puzzle20() : base(PuzzleId) { }

    public Puzzle20(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var modules = CreateModules(InputEntries);
        var button = modules[0];
        var output = modules[1];

        var sentPulses = button.SendPulse();
        while (sentPulses.Count > 0)
        {
            var newSenders = new List<Module>();
            var newPulses = new List<(Module sender, Module receiver, int pulse)>();

            foreach (var (sender, receiver, pulse) in sentPulses)
            {
                receiver.ReceivePulse(sender, pulse);
                newSenders.Add(receiver);
            }

            foreach (var sender in newSenders)
            {
                var pulses = sender.SendPulse();
                newPulses.AddRange(pulses);
            }

            sentPulses = newPulses;
        }

        var lowPulsesSent = (long)modules.Sum(m => m.LowPulsesSent);
        var highPulsesSent = modules.Sum(m => m.HighPulsesSent);
        var product = lowPulsesSent * highPulsesSent;
        return product;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    internal static List<Module> CreateModules(List<string> inputEntries)
    {
        // We always start with a button
        var button = new Module()
        {
            Name = "button",
            Type = ModuleType.Button,
            OutputNames = ["broadcaster"],
            PulseToSend = 0
        };
        // Dummy module as output for non-existing modules
        var output = new Module() { Name = "output", Type = ModuleType.Output, OutputNames = [] };
        var modules = new List<Module> { button, output };

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
                module.Outputs.Add(outputModule);
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
        public int State { get; private set; } = 0;
        public int? PulseToSend = null;
        public List<(Module Module, int PreviousPulse)> Inputs { get; } = [];
        public List<Module> Outputs { get; } = [];
        public required string[] OutputNames { get; init; } // Only temporary, for initialization
        public int HighPulsesSent { get; private set; }
        public int LowPulsesSent { get; private set; }

        public void ReceivePulse(Module sender, int pulse)
        {
            if (Type == ModuleType.Broadcaster)
            {
                // Send the same pulse to all destination modules
                State = pulse;
                PulseToSend = pulse;
            }
            else if (Type == ModuleType.FlipFlop)
            {
                // If input is a low pulse...
                if (pulse == 0)
                {
                    // Flip the state and send the new state
                    State = State == 0 ? 1 : 0;
                    if (State == 0)
                    {
                        State = 1;
                        PulseToSend = 1;
                    }
                    else
                    {
                        State = 0;
                        PulseToSend = 0;
                    }
                }
                else
                {
                    PulseToSend = null;
                }
            }
            else if (Type == ModuleType.Conjunction)
            {
                var inputState = Inputs.Single(i => i.Module.Name == sender.Name);
                inputState.PreviousPulse = pulse;
                // If all previous inputs are high, send a low pulse - otherwise a high pulse
                State = Inputs.All(i => i.PreviousPulse == 1) ? 0 : 1;
            }
            // TODO: Should we count the number of received (high/low) pulses?
            //       Perhaps also a method for _sending_ pulses (and counting high/low pulses sent)?
        }

        public List<(Module sender, Module receiver, int pulse)> SendPulse()
        {
            var pulses = new List<(Module, Module, int)>();
            if (PulseToSend.HasValue)
            {
                if (PulseToSend == 1)
                {
                    HighPulsesSent += Outputs.Count();
                }
                else
                {
                    LowPulsesSent += Outputs.Count();
                }

                foreach (var module in Outputs)
                {
                    pulses.Add((this, module, State));
                }
            }
            return pulses;
        }
    }

    public enum ModuleType { FlipFlop, Conjunction, Broadcaster, Button, Output }
}
