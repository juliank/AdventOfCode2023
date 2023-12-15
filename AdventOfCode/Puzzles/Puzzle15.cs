namespace AdventOfCode.Puzzles;

public class Puzzle15 : Puzzle<string, long>
{
    private const int PuzzleId = 15;

    public Puzzle15() : base(PuzzleId) { }

    public Puzzle15(params string[] inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var hashSum = 0L;
        foreach (var item in InputEntries)
        {
            var hash = AsciiHash(item);
            hashSum += hash;
        }
        return hashSum;
    }

    public override long SolvePart2()
    {
        var steps = InputEntries.Select(ie => new Step(ie)).ToList();
        var boxes = Enumerable.Range(0, 256).Select(_ => new List<Step>()).ToArray();

        foreach (var step in steps)
        {
            var labelHash = step.Hash();
            var existingStep = boxes[labelHash].FirstOrDefault(s => s.Label == step.Label);
            int? existingIndex = existingStep == null ? null : boxes[labelHash].IndexOf(existingStep!);

            if (step.Operator == '-')
            {
                if (existingIndex.HasValue)
                {
                    boxes[labelHash].RemoveAt(existingIndex.Value);
                }
            }
            else // Operator == '='
            {
                if (existingIndex.HasValue)
                {
                    boxes[labelHash][existingIndex.Value] = step;
                }
                else
                {
                    boxes[labelHash].Add(step);
                }
            }
        }

        var totalFocusingPower = 0L;
        for (var boxNumber = 0; boxNumber < boxes.Length; boxNumber++)
        {
            totalFocusingPower += FocusingPower(boxes[boxNumber], boxNumber);
        }
        return totalFocusingPower;
    }

    protected internal override IEnumerable<string> ParseInput(string inputItem)
    {
        return inputItem.Split(',');
    }

    internal static long FocusingPower(List<Step> box, int boxNumber)
    {
        var focusingPower = 0L;
        for (var lensSlot = 1; lensSlot <= box.Count; lensSlot++)
        {
            focusingPower += FocusingPower(box[lensSlot - 1], boxNumber, lensSlot);
        }
        return focusingPower;
    }

    internal static long FocusingPower(Step lens, int boxNumber, int lensSlot)
    {
        var power = 1 + boxNumber;
        power *= lensSlot * lens.Value;
        return power;
    }

    internal static int AsciiHash(string str)
    {
        var currentValue = 0;
        foreach (var c in str)
        {
            currentValue += c;
            currentValue *= 17;
            var remainder = currentValue % 256;
            currentValue = remainder;
        }
        return currentValue;
    }

    public record Step(string Label, char Operator, int Value)
    {
        public int Hash()
        {
            return AsciiHash(Label);
        }

        public Step(string stepDescription) : this("", ' ', -1)
        {
            if (stepDescription[^1] == '-')
            {
                // cm-
                Operator = '-';
                Label = stepDescription[0..^1];
            }
            else
            {
                // rn=1
                Operator = '=';
                var parts = stepDescription.Split('=');
                Label = parts[0];
                Value = int.Parse(parts[1]);
            }
        }
    }
}
