namespace AdventOfCode.Puzzles;

public interface IPuzzle
{
    object Solve();
    object SolvePart1();
    object SolvePart2();
    bool SkipPart1WhenSolveAll { get; }
    bool SkipPart2WhenSolveAll { get; }
}
