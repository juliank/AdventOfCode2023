# Solving a new puzzle

It all starts at https://adventofcode.com/

1. Copy `Puzzle00.cs` and rename it according to the puzzle number
   - Also remember to rename the class inside the file
   - Also copy `Puzzle00Tests.cs` and rename it accordingly (for making sure we don't break old puzzles if refactoring)
1. Save the input for today's puzzle in the paralell `...Input` repository (with the name `NN.txt`)
   - [It is recommended](https://www.reddit.com/r/adventofcode/wiki/faqs/copyright/inputs/) to _not_ share the puzzle input in a public directory
   - The repository for the puzzle inputs should have the same name as this repository, with the added _Input_ postfix
1. Implement `ParseInput` in the newly created puzzle class
1. Implement `SolvePart1` (followed by `SolvePart2`)
1. Run puzzle and submit the result :-)

# Running the code

To avoid having to specify the project path every time, it is easiest running the puzzles from within the `AdventOfCode` project directory:

- Run the latest puzzle:  
  `dotnet run`
- Run a specific puzzle:  
  `dotnet run 3`
- Run all tests:  
  `dotnet test ..`
