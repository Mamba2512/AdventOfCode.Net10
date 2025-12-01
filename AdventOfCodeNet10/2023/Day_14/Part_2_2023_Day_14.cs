using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using Point = AdventOfCodeNet10.Extensions.Point;

namespace AdventOfCodeNet10._2023.Day_14
{
  internal class Part_2_2023_Day_14 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/14
    --- Part Two ---
    The parabolic reflector dish deforms, but not in a way that focuses the beam.
    To do that, you'll need to move the rocks to the edges of the platform.
    Fortunately, a button on the side of the control panel labeled "spin cycle"
    attempts to do just that!
    
    Each cycle tilts the platform four times so that the rounded rocks roll north,
    then west, then south, then east. After each tilt, the rounded rocks roll as
    far as they can before the platform tilts in the next direction. After one
    cycle, the platform will have finished rolling the rounded rocks in those four
    directions in that order.
    
    Here's what happens in the example above after each of the first few cycles:
    
    After 1 cycle:
    .....#....
    ....#...O#
    ...OO##...
    .OO#......
    .....OOO#.
    .O#...O#.#
    ....O#....
    ......OOOO
    #...O###..
    #..OO#....
    
    After 2 cycles:
    .....#....
    ....#...O#
    .....##...
    ..O#......
    .....OOO#.
    .O#...O#.#
    ....O#...O
    .......OOO
    #..OO###..
    #.OOO#...O
    
    After 3 cycles:
    .....#....
    ....#...O#
    .....##...
    ..O#......
    .....OOO#.
    .O#...O#.#
    ....O#...O
    .......OOO
    #...O###.O
    #.OOO#...O
    This process should work if you leave it running long enough, but you're still
    worried about the north support beams. To make sure they'll survive for a
    while, you need to calculate the total load on the north support beams after
    1000000000 cycles.
    
    In the above example, after 1000000000 cycles, the total load on the north
    support beams is 64.
    
    Run the spin cycle for 1000000000 cycles. Afterward, what is the total load on
    the north support beams?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public Dictionary<Point, char> grid = new Dictionary<Point, char>();
    List<Point> roundRocks = new List<Point>();
    List<Point> cubeRocks = new List<Point>();
    Dictionary<int, Dictionary<Point, char>> seenStates = new Dictionary<int, Dictionary<Point, char>>();
    bool writeOutput = false;
    public override string Execute()
    {

      //Debug.Write("\f");  // Clear previous output
      string result = "";
      long totalCount = 0;
      int row = 0;
      int col = 0;
      grid.Clear();
      roundRocks.Clear();
      cubeRocks.Clear();
      seenStates.Clear();
      writeOutput = false;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_14.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_14.txt already stored in "Lines"
      //
      if (Lines.Count > 20)
        writeOutput = true;

      foreach (var line in Lines)
      {
        col = 0;
        foreach (var ch in line)
        {
          grid.Add(new Point(col, row), ch);
          if (ch == 'O')
          {
            roundRocks.Add(new Point(col, row));
          }
          else if (ch == '#')
          {
            cubeRocks.Add(new Point(col, row));
          }
          col++;
        }
        row++;
      }
      int currentMove = 0;
      for (long i = 0; i < 250; i++)
      {
        currentMove++;
        roundRocks.Clear();
        roundRocks = GetRoundedRocks();
        var orderedRoundedRocks = roundRocks.OrderBy(p => p.Y).ThenBy(p => p.X).ToList();

        foreach (var rock in orderedRoundedRocks)
        {
          var finalPos = GetFinalPosNorth(rock);
          if (finalPos != rock)
          {
            grid[finalPos] = 'O';
            grid[rock] = '.';
          }
        }
        
        seenStates.Add(currentMove, new Dictionary<Point, char>(grid));

        ////reprint the grid
        //for (int i = 0; i < row; i++)
        //{
        //  for (int j = 0; j < col; j++)
        //  {
        //    var p = new Point(j, i);

        //    Debug.Write(grid[p]);

        //  }
        //  Debug.WriteLine("");
        //}
        //Debug.Write("-----------------------------------------------------------------------------");
        //Debug.WriteLine("");


        //now find new positions of rounded rocks
        currentMove++;
        roundRocks.Clear();
        roundRocks = GetRoundedRocks();
        orderedRoundedRocks.Clear();
        orderedRoundedRocks = roundRocks.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
        foreach (var rock in orderedRoundedRocks)
        {
          var finalPos = GetFinalPosWest(rock);
          if (finalPos != rock)
          {
            grid[finalPos] = 'O';
            grid[rock] = '.';
          }
        }

        seenStates.Add(currentMove, new Dictionary<Point, char>(grid));

        //reprint the grid
        //for (int i = 0; i < row; i++)
        //{
        //  for (int j = 0; j < col; j++)
        //  {
        //    var p = new Point(j, i);

        //    Debug.Write(grid[p]);

        //  }
        //  Debug.WriteLine("");
        //}
        //Debug.Write("-----------------------------------------------------------------------------");
        //Debug.WriteLine("");


        //now find new positions of rounded rocks
        currentMove++;
        roundRocks.Clear();
        roundRocks = GetRoundedRocks();
        orderedRoundedRocks.Clear();
        orderedRoundedRocks = roundRocks.OrderByDescending(p => p.Y).ThenBy(p => p.X).ToList();
        foreach (var rock in orderedRoundedRocks)
        {
          var finalPos = GetFinalPosSouth(rock);
          if (finalPos != rock)
          {
            grid[finalPos] = 'O';
            grid[rock] = '.';
          }
        }
        seenStates.Add(currentMove, new Dictionary<Point, char>(grid));

        ////reprint the grid
        //for (int i = 0; i < row; i++)
        //{
        //  for (int j = 0; j < col; j++)
        //  {
        //    var p = new Point(j, i);

        //    Debug.Write(grid[p]);

        //  }
        //  Debug.WriteLine("");
        //}
        //Debug.Write("-----------------------------------------------------------------------------");
        //Debug.WriteLine("");


        //now find new positions of rounded rocks
        currentMove++;
        roundRocks.Clear();
        roundRocks = GetRoundedRocks();
        orderedRoundedRocks.Clear();
        orderedRoundedRocks = roundRocks.OrderByDescending(p => p.X).ThenBy(p => p.Y).ToList();
        foreach (var rock in orderedRoundedRocks)
        {
          var finalPos = GetFinalPosEast(rock);
          if (finalPos != rock)
          {
            grid[finalPos] = 'O';
            grid[rock] = '.';
          }
        }
        seenStates.Add(currentMove, new Dictionary<Point, char>(grid));

        ////reprint the grid
        //for (int i = 0; i < row; i++)
        //{
        //  for (int j = 0; j < col; j++)
        //  {
        //    var p = new Point(j, i);

        //    Debug.Write(grid[p]);

        //  }
        //  Debug.WriteLine("");
        //}

        //Debug.Write("-----------------------------------------------------------------------------");
        //Debug.WriteLine("");
        //if (i < 20)
        //{
        //  Debug.WriteLine($"After {i + 1} cycles:");
        //  //reprint the grid
        //  for (int j = 0; j < row; j++)
        //  {
        //    for (int k = 0; k < col; k++)
        //    {
        //      var p = new Point(k, j);

        //      Debug.Write(grid[p]);

        //    }
        //    Debug.WriteLine("");
        //  }

        //  Debug.Write("-----------------------------------------------------------------------------");
        //  Debug.WriteLine("");
        //}
      }
      RepeatedState(seenStates); //already saw the repeated cycle from this fnc --> starts from 330 with 168 common difference

      var state = GetState();

      var gridToLookFor = seenStates[472];



      foreach (var kvp in gridToLookFor)
      {
        if (kvp.Value == 'O')
        {
          totalCount += (row - kvp.Key.Y);
        }
      }

      result = totalCount.ToString();
      return result;
    }

    public long GetState()
    {
      for (long i = 1; i < 4000000000; i++)
      {
        var currentRemainder = (4000000000 - i) % 168;
        if (currentRemainder == 0)
        {
          return i;
        }
      }
      return -1;
    }

    public List<Point> GetRoundedRocks()
    {
      var rocks = new List<Point>();
      foreach (var kvp in grid)
      {
        if (kvp.Value == 'O')
        {
          rocks.Add(kvp.Key);
        }
      }
      return rocks;
    }

    public void RepeatedState(Dictionary<int, Dictionary<Point, char>> inputStates)
    {
      Dictionary<int, List<int>> repeatedStates = new Dictionary<int, List<int>>();

      foreach (var state1 in inputStates)
      {
        foreach (var state2 in inputStates)
        {
          if(state1.Key != state2.Key)
          {
            if(CompareStates(state1.Value, state2.Value))
            {
              if(writeOutput)
              { 
                //Debug.WriteLine($"Repeated state found between moves {state1.Key} and {state2.Key}"); 
              }
            }
          }
        }
      }
    }

    public bool CompareStates(Dictionary<Point, char> state1, Dictionary<Point, char> state2)
    {
      foreach (var kvp in state1)
      {
        if (!state2.ContainsKey(kvp.Key) || state2[kvp.Key] != kvp.Value)
        {
          return false;
        }
      }
      return true;
    }



    public Point GetFinalPosNorth(Point currentRock)
    {
      var dir = Directions.ToUp;
      var pos = currentRock;
      //Debug.WriteLine($"*********Rock initial {pos}*****************");

      var newPos = pos + dir;
      while (grid.ContainsKey(newPos) && grid[newPos] == '.')
      {
        pos = newPos;
        //Debug.WriteLine($"  moves to {pos}");
        newPos = pos + dir;
        //Debug.WriteLine($"    checking {newPos}");
      }

      //Debug.WriteLine($"**********Rock final pos {pos}***************");
      //Debug.WriteLine("##############################################################");
      return pos;
    }

    public Point GetFinalPosWest(Point currentRock)
    {
      var dir = Directions.ToLeft;
      var pos = currentRock;
      //Debug.WriteLine($"*********Rock initial {pos}*****************");

      var newPos = pos + dir;
      while (grid.ContainsKey(newPos) && grid[newPos] == '.')
      {
        pos = newPos;
        //Debug.WriteLine($"  moves to {pos}");
        newPos = pos + dir;
        //Debug.WriteLine($"    checking {newPos}");
      }

      //Debug.WriteLine($"**********Rock final pos {pos}***************");
      //Debug.WriteLine("##############################################################");
      return pos;
    }

    public Point GetFinalPosSouth(Point currentRock)
    {
      var dir = Directions.ToDown;
      var pos = currentRock;
      //Debug.WriteLine($"*********Rock initial {pos}*****************");

      var newPos = pos + dir;
      while (grid.ContainsKey(newPos) && grid[newPos] == '.')
      {
        pos = newPos;
        //Debug.WriteLine($"  moves to {pos}");
        newPos = pos + dir;
        //Debug.WriteLine($"    checking {newPos}");
      }

      //Debug.WriteLine($"**********Rock final pos {pos}***************");
      //Debug.WriteLine("##############################################################");
      return pos;
    }

    public Point GetFinalPosEast(Point currentRock)
    {
      var dir = Directions.ToRight;
      var pos = currentRock;
      //Debug.WriteLine($"*********Rock initial {pos}*****************");

      var newPos = pos + dir;
      while (grid.ContainsKey(newPos) && grid[newPos] == '.')
      {
        pos = newPos;
        //Debug.WriteLine($"  moves to {pos}");
        newPos = pos + dir;
        //Debug.WriteLine($"    checking {newPos}");
      }

      //Debug.WriteLine($"**********Rock final pos {pos}***************");
      //Debug.WriteLine("##############################################################");
      return pos;
    }
  }
}
