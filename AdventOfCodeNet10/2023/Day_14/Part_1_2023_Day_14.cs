using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using Point = AdventOfCodeNet10.Extensions.Point;

namespace AdventOfCodeNet10._2023.Day_14
{
  internal class Part_1_2023_Day_14 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/14
    --- Day 14: Parabolic Reflector Dish ---
    You reach the place where all of the mirrors were pointing: a massive parabolic
    reflector dish attached to the side of another large mountain.
    
    The dish is made up of many small mirrors, but while the mirrors themselves are
    roughly in the shape of a parabolic reflector dish, each individual mirror
    seems to be pointing in slightly the wrong direction. If the dish is meant to
    focus light, all it's doing right now is sending it in a vague direction.
    
    This system must be what provides the energy for the lava! If you focus the
    reflector dish, maybe you can go where it's pointing and use the light to fix
    the lava production.
    
    Upon closer inspection, the individual mirrors each appear to be connected via
    an elaborate system of ropes and pulleys to a large metal platform below the
    dish. The platform is covered in large rocks of various shapes. Depending on
    their position, the weight of the rocks deforms the platform, and the shape of
    the platform controls which ropes move and ultimately the focus of the dish.
    
    In short: if you move the rocks, you can focus the dish. The platform even has
    a control panel on the side that lets you tilt it in one of four directions!
    The rounded rocks (O) will roll when the platform is tilted, while the
    cube-shaped rocks (#) will stay in place. You note the positions of all of the
    empty spaces (.) and rocks (your puzzle input). For example:
    
    O....#....
    O.OO#....#
    .....##...
    OO.#O....O
    .O.....O#.
    O.#..O.#.#
    ..O..#O..O
    .......O..
    #....###..
    #OO..#....
    Start by tilting the lever so all of the rocks will slide north as far as they
    will go:
    
    OOOO.#.O..
    OO..#....#
    OO..O##..O
    O..#.OO...
    ........#.
    ..#....#.#
    ..O..#.O.O
    ..O.......
    #....###..
    #....#....
    You notice that the support beams along the north side of the platform are
    damaged; to ensure the platform doesn't collapse, you should calculate the
    total load on the north support beams.
    
    The amount of load caused by a single rounded rock (O) is equal to the number
    of rows from the rock to the south edge of the platform, including the row the
    rock is on. (Cube-shaped rocks (#) don't contribute to load.) So, the amount of
    load caused by each rock in each row is as follows:
    
    OOOO.#.O.. 10
    OO..#....#  9
    OO..O##..O  8
    O..#.OO...  7
    ........#.  6
    ..#....#.#  5
    ..O..#.O.O  4
    ..O.......  3
    #....###..  2
    #....#....  1
    The total load is the sum of the load caused by all of the rounded rocks. In
    this example, the total load is 136.
    
    Tilt the platform so that the rounded rocks all roll north. Afterward, what is
    the total load on the north support beams?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
    public Dictionary<Point, char> grid = new Dictionary<Point, char>();
    List<Point> roundRocks = new List<Point>();
    List<Point> cubeRocks = new List<Point>();
    public override string Execute()
    {

      string result = "";
      long totalCount = 0;
      int row = 0;
      int col = 0;
      grid.Clear();
      roundRocks.Clear();
      cubeRocks.Clear();

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

      var orderedRoundedRocks = roundRocks.OrderBy(p => p.Y).ThenBy(p => p.X).ToList();

      foreach (var rock in roundRocks)
      {
        var finalPos = GetFinalPos(rock);
        if(finalPos != rock)
        {
          grid[finalPos] = 'O';
          grid[rock] = '.';
        }
      }

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

      foreach(var kvp in grid)
      {
        if(kvp.Value == 'O')
        {
          totalCount += (row - kvp.Key.Y);
        }
      }


      result = totalCount.ToString();
      return result;
    }

    public Point GetFinalPos(Point currentRock)
    {
      if(currentRock.X == 1 && currentRock.Y == 4)
      {

      }
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
  }
}
