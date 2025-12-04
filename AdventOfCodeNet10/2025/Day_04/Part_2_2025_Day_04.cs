using AdventOfCodeNet10.Extensions;
using Point = AdventOfCodeNet10.Extensions.Point;
namespace AdventOfCodeNet10._2025.Day_04
{
  internal class Part_2_2025_Day_04 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/4
    Now, the Elves just need help accessing as much of the paper as they can.
    
    Once a roll of paper can be accessed by a forklift, it can be removed. Once a
    roll of paper is removed, the forklifts might be able to access more rolls of
    paper, which they might also be able to remove. How many total rolls of paper
    could the Elves remove if they keep repeating this process?
    
    Starting with the same example as above, here is one way you could remove as
    many rolls of paper as possible, using highlighted @ to indicate that a roll of
    paper is about to be removed, and using x to indicate that a roll of paper was
    just removed:
    
    Initial state:
    ..@@.@@@@.
    @@@.@.@.@@
    @@@@@.@.@@
    @.@@@@..@.
    @@.@@@@.@@
    .@@@@@@@.@
    .@.@.@.@@@
    @.@@@.@@@@
    .@@@@@@@@.
    @.@.@@@.@.
    
    Remove 13 rolls of paper:
    ..xx.xx@x.
    x@@.@.@.@@
    @@@@@.x.@@
    @.@@@@..@.
    x@.@@@@.@x
    .@@@@@@@.@
    .@.@.@.@@@
    x.@@@.@@@@
    .@@@@@@@@.
    x.x.@@@.x.
    
    Remove 12 rolls of paper:
    .......x..
    .@@.x.x.@x
    x@@@@...@@
    x.@@@@..x.
    .@.@@@@.x.
    .x@@@@@@.x
    .x.@.@.@@@
    ..@@@.@@@@
    .x@@@@@@@.
    ....@@@...
    
    Remove 7 rolls of paper:
    ..........
    .x@.....x.
    .@@@@...xx
    ..@@@@....
    .x.@@@@...
    ..@@@@@@..
    ...@.@.@@x
    ..@@@.@@@@
    ..x@@@@@@.
    ....@@@...
    
    Remove 5 rolls of paper:
    ..........
    ..x.......
    .x@@@.....
    ..@@@@....
    ...@@@@...
    ..x@@@@@..
    ...@.@.@@.
    ..x@@.@@@x
    ...@@@@@@.
    ....@@@...
    
    Remove 2 rolls of paper:
    ..........
    ..........
    ..x@@.....
    ..@@@@....
    ...@@@@...
    ...@@@@@..
    ...@.@.@@.
    ...@@.@@@.
    ...@@@@@x.
    ....@@@...
    
    Remove 1 roll of paper:
    ..........
    ..........
    ...@@.....
    ..x@@@....
    ...@@@@...
    ...@@@@@..
    ...@.@.@@.
    ...@@.@@@.
    ...@@@@@..
    ....@@@...
    
    Remove 1 roll of paper:
    ..........
    ..........
    ...x@.....
    ...@@@....
    ...@@@@...
    ...@@@@@..
    ...@.@.@@.
    ...@@.@@@.
    ...@@@@@..
    ....@@@...
    
    Remove 1 roll of paper:
    ..........
    ..........
    ....x.....
    ...@@@....
    ...@@@@...
    ...@@@@@..
    ...@.@.@@.
    ...@@.@@@.
    ...@@@@@..
    ....@@@...
    
    Remove 1 roll of paper:
    ..........
    ..........
    ..........
    ...x@@....
    ...@@@@...
    ...@@@@@..
    ...@.@.@@.
    ...@@.@@@.
    ...@@@@@..
    ....@@@...
    Stop once no more rolls of paper are accessible by a forklift. In this example,
    a total of 43 rolls of paper can be removed.
    
    Start with your original diagram. How many rolls of paper in total can be
    removed by the Elves and their forklifts?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public Dictionary<Point, char> Grid = new();
    const int TOTAL_ADJACENT_PAPER_ROLLS_ALLOWED = 4;

    public override string Execute()
    {
      Grid.Clear();
      string result = "";
      long totalCount = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2025_Day_04.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2025_Day_04.txt already stored in "Lines"
      //

      int rowIdx = 0;
      foreach (var line in Lines)
      {
        int colIdx = 0;
        foreach (var ch in line)
        {
          Grid[new Point(colIdx, rowIdx)] = ch;
          colIdx++;
        }
        rowIdx++;
      }

      bool IsAnyPaperRollRemovable = true;
      while (IsAnyPaperRollRemovable)
      {
        bool IsAtLeastOneRemovedThisIteration = false;
        foreach (var elt in Grid)
        {
          if (elt.Value == '@')
          {
            if (GetNumOfAdjacentPaperRolls(elt.Key))
            {
              Grid[elt.Key] = '.';
              IsAtLeastOneRemovedThisIteration = true;
              totalCount++;
            }
          }
        }
        if (!IsAtLeastOneRemovedThisIteration)
        {
          IsAnyPaperRollRemovable = false;
        }
      }
 
      result = totalCount.ToString();
      return result;
    }

    private bool GetNumOfAdjacentPaperRolls(Point currentPoint)
    {
      int totalAdjacentPaperRolls = 0;
      foreach (var dir in Directions.WithDiagonals)
      {
        var newPos = currentPoint + dir;
        if (Grid.ContainsKey(newPos) && Grid[newPos] == '@')
        {
          totalAdjacentPaperRolls++;
        }
        if (totalAdjacentPaperRolls >= TOTAL_ADJACENT_PAPER_ROLLS_ALLOWED)
        {
          return false;
        }
      }
      return true;
    }
  }
}