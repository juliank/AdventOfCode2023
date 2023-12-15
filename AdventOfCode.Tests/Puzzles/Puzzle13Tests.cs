namespace AdventOfCode.Tests.Puzzles;

public class Puzzle13Tests
{
    private readonly Puzzle13 puzzle;

    public Puzzle13Tests()
    {
        puzzle = new Puzzle13();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(35360);
    }

    // [Fact]
    internal void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Fact]
    public void TestGetVerticalReflectionLine()
    {
        List<string> pattern = [
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#."
        ];

        var reflectionLine = Puzzle13.GetVerticalReflectionLine(pattern);
        reflectionLine.Should().Be(5);
    }

    [Fact]
    public void TestGetVerticalReflectionLine2()
    {
        List<string> pattern = [
            "##....",
            "##..#.",
            "##...#"
        ];

        var reflectionLine = Puzzle13.GetVerticalReflectionLine(pattern);
        reflectionLine.Should().Be(1);
    }

    [Fact]
    public void TestGetVerticalReflectionLine3()
    {
        List<string> pattern = [
            "....##",
            ".#..##",
            "#...##"
        ];

        var reflectionLine = Puzzle13.GetVerticalReflectionLine(pattern);
        reflectionLine.Should().Be(5);
    }

    [Fact]
    public void TestGetHorizontalReflectionLine()
    {
        List<string> pattern = [
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#"
        ];

        var reflectionLine = Puzzle13.GetHorizontalReflectionLine(pattern);
        reflectionLine.Should().Be(4);
    }

    [Fact]
    public void TestGetHorizontalReflectionLine2()
    {
        List<string> pattern = [
            "#...##..#",
            "#...##..#",
            "#........",
            "##......."
        ];

        var reflectionLine = Puzzle13.GetHorizontalReflectionLine(pattern);
        reflectionLine.Should().Be(1);
    }

    [Fact]
    public void TestGetHorizontalReflectionLine3()
    {
        List<string> pattern = [
            "#........",
            "##.......",
            "#...##..#",
            "#...##..#"
        ];

        var reflectionLine = Puzzle13.GetHorizontalReflectionLine(pattern);
        reflectionLine.Should().Be(3);
    }

    [Fact]
    public void TestGetVerticalAndHorizontalReflectionLine()
    {
        List<string> pattern = [
            "###.####.",
            "##...#.#.",
            "..###.###",
            "##..###..",
            "##..###..",
            "..###.###",
            "##...#.##"
        ];

        var verticalLine = Puzzle13.GetVerticalReflectionLine(pattern);
        verticalLine.Should().Be(1);
        var horizontalLine = Puzzle13.GetHorizontalReflectionLine(pattern);
        horizontalLine.Should().BeNull();
    }

    [Fact]
    public void TestGetHorizontalReflectionLineWithSmudge()
    {
        List<string> pattern = [
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#."
        ];

        Puzzle13.GetVerticalReflectionLine(pattern).Should().Be(5);
        Puzzle13.GetVerticalReflectionLineWithSmudge(pattern).Should().BeNull();
        var reflectionLine = Puzzle13.GetHorizontalReflectionLineWithSmudge(pattern);
        reflectionLine.Should().Be(3);
    }

    [Fact]
    public void TestGetHorizontalReflectionLineWithSmudge2()
    {
        List<string> pattern = [
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#"
        ];

        Puzzle13.GetHorizontalReflectionLine(pattern).Should().Be(4);
        Puzzle13.GetVerticalReflectionLineWithSmudge(pattern).Should().BeNull();
        var reflectionLine = Puzzle13.GetHorizontalReflectionLineWithSmudge(pattern);
        reflectionLine.Should().Be(1);
    }
}
