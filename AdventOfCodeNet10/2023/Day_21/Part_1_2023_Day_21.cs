using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using Point = AdventOfCodeNet10.Extensions.Point;
namespace AdventOfCodeNet10._2023.Day_21
{
  internal class Part_1_2023_Day_21 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/21
    You manage to catch the airship right as it's dropping someone else off on
    their all-expenses-paid trip to Desert Island! It even helpfully drops you off
    near the gardener and his massive farm.
    
    "You got the sand flowing again! Great work! Now we just need to wait until we
    have enough sand to filter the water for Snow Island and we'll have snow again
    in no time."
    
    While you wait, one of the Elves that works with the gardener heard how good
    you are at solving problems and would like your help. He needs to get his steps
    in for the day, and so he'd like to know which garden plots he can reach with
    exactly his remaining 64 steps.
    
    He gives you an up-to-date map (your puzzle input) of his starting position
    (S), garden plots (.), and rocks (#). For example:
    
    ...........
    .....###.#.
    .###.##..#.
    ..#.#...#..
    ....#.#....
    .##..S####.
    .##..#...#.
    .......##..
    .##.#.####.
    .##..##.##.
    ...........
    The Elf starts at the starting position (S) which also counts as a garden plot.
    Then, he can take one step north, south, east, or west, but only onto tiles
    that are garden plots. This would allow him to reach any of the tiles marked O:
    
    ...........
    .....###.#.
    .###.##..#.
    ..#.#...#..
    ....#O#....
    .##.OS####.
    .##..#...#.
    .......##..
    .##.#.####.
    .##..##.##.
    ...........
    Then, he takes a second step. Since at this point he could be at either tile
    marked O, his second step would allow him to reach any garden plot that is one
    step north, south, east, or west of any tile that he could have reached after
    the first step:
    
    ...........
    .....###.#.
    .###.##..#.
    ..#.#O..#..
    ....#.#....
    .##O.O####.
    .##.O#...#.
    .......##..
    .##.#.####.
    .##..##.##.
    ...........
    After two steps, he could be at any of the tiles marked O above, including the
    starting position (either by going north-then-south or by going west-then-east).
    
    A single third step leads to even more possibilities:
    
    ...........
    .....###.#.
    .###.##..#.
    ..#.#.O.#..
    ...O#O#....
    .##.OS####.
    .##O.#...#.
    ....O..##..
    .##.#.####.
    .##..##.##.
    ...........
    He will continue like this until his steps for the day have been exhausted.
    After a total of 6 steps, he could reach any of the garden plots marked O:
    
    ...........
    .....###.#.
    .###.##.O#.
    .O#O#O.O#..
    O.O.#.#.O..
    .##O.O####.
    .##.O#O..#.
    .O.O.O.##..
    .##.#.####.
    .##O.##.##.
    ...........
    In this example, if the Elf's goal was to get exactly 6 more steps today, he
    could use them to reach any of 16 garden plots.
    
    However, the Elf actually needs to get 64 steps today, and the map he's handed
    you is much larger than the example map.
    
    Starting from the garden plot marked S on your map, how many garden plots could
    the Elf reach in exactly 64 steps?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
    public Dictionary<Point, char> Grid = new();
    int Rows = 0;
    int Cols = 0;
    Point StartPos = (0, 0);
    public HashSet<Point> visited = new();
    public override string Execute()
    {
      Grid.Clear();
      visited.Clear();
      string result = "";
      long totalCount = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_21.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_21.txt already stored in "Lines"
      //

      int rowIdx = 0;
      foreach (var line in Lines)
      {
        Rows = line.Length;
        int colIdx = 0;
        foreach (var ch in line)
        {
          Grid[new Point(colIdx, rowIdx)] = ch;

          if (ch == 'S')
            StartPos = (colIdx, rowIdx);

          colIdx++;
        }
        rowIdx++;
      }
      Cols = rowIdx;
      //PrintGrid();
      totalCount = TraverseGrid();
      foreach (var val in visited)
      {
        Grid[val] = '0';
      }
      //Debug.WriteLine("--------------------------------------------------------------------------------------------------------------");
      //PrintGrid();
      result = totalCount.ToString();
      return result;
    }

    private void PrintGrid()
    {
      //print the grid
      for (int i = 0; i < Cols; i++)
      {
        for (int j = 0; j < Rows; j++)
        {
          Debug.Write(Grid[(j, i)]);
        }
        Debug.WriteLine("");
      }
    }

    private int TraverseGrid()
    {
      var queue = new Queue<(Point position, int steps)>();
      visited = new HashSet<Point>();
      queue.Enqueue((StartPos, 0));

      while (queue.Count > 0)
      {
        var (currentPos, stepsTaken) = queue.Dequeue();
        //Debug.WriteLine($"Exploring [{currentPos.X},{currentPos.Y}] : alreadyNumOfStepsTaken: {stepsTaken}");
        if (Grid.TryGetValue(currentPos, out var value) && Grid[currentPos] != '#')
        {
          //Debug.WriteLine($"    New Point Added : [{currentPos.X},{currentPos.Y}]");
          if(stepsTaken == 64)
          {
            visited.Add(currentPos);
          }

          foreach (var dir in Directions.WithoutDiagonals)
          {
            var newPos = currentPos + dir;
            var updatedStep = stepsTaken + 1;
            if (updatedStep <= 64)
            {
              queue.Enqueue((newPos, updatedStep));
            }
          }

        }
      }
      return visited.Count;
    }
  }
}
