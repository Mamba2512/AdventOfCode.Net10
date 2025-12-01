using System.Diagnostics;
using System.Linq;
using Point = AdventOfCodeNet10.Extensions.Point;

namespace AdventOfCodeNet10._2023.Day_16
{
  internal class Part_2_2023_Day_16 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/16
    As you try to work out what might be wrong, the reindeer tugs on your shirt and
    leads you to a nearby control panel. There, a collection of buttons lets you
    align the contraption so that the beam enters from any edge tile and heading
    away from that edge. (You can choose either of two directions for the beam if
    it starts on a corner; for instance, if the beam starts in the bottom-right
    corner, it can start heading either left or upward.)
    
    So, the beam could start on any tile in the top row (heading downward), any
    tile in the bottom row (heading upward), any tile in the leftmost column
    (heading right), or any tile in the rightmost column (heading left). To produce
    lava, you need to find the configuration that energizes as many tiles as
    possible.
    
    In the above example, this can be achieved by starting the beam in the fourth
    tile from the left in the top row:
    
    .|<2<\....
    |v-v\^....
    .v.v.|->>>
    .v.v.v^.|.
    .v.v.v^...
    .v.v.v^..\
    .v.v/2\\..
    <-2-/vv|..
    .|<<<2-|.\
    .v//.|.v..
    Using this configuration, 51 tiles are energized:
    
    .#####....
    .#.#.#....
    .#.#.#####
    .#.#.##...
    .#.#.##...
    .#.#.##...
    .#.#####..
    ########..
    .#######..
    .#...#.#..
    Find the initial beam configuration that energizes the largest number of tiles;
    how many tiles are energized in that configuration?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
    Dictionary<Point, char> Grid = new();
    List<List<(Point pos, Point dir)>> allPaths = new();
    HashSet<Point> energizedTiles = new();
    long maxEnergizedTiles = 0;
    HashSet<(Point pos, Point dir)> Visited = new();
    int Rows = 0;
    int Cols = 0;
    public override string Execute()
    {
      Grid.Clear();
      string result = "";
      long totalCount = 0;
      allPaths.Clear();
      energizedTiles.Clear();
      Visited.Clear();
      maxEnergizedTiles = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_16.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_16.txt already stored in "Lines"
      //

      int rowIdx = 0;
      foreach (var line in Lines)
      {
        Cols = line.Length;
        int colIdx = 0;
        foreach (var ch in line)
        {
          Grid.Add(new Point(colIdx, rowIdx), ch);

          colIdx++;
        }
        rowIdx++;
      }
      Rows = rowIdx;

      //perform beam traversal
      foreach (var kvp in Grid)
      {
        if (kvp.Key.X == 0)
        {
          if (kvp.Key.Y == 0)
          {
            //top-left corner, beam can go down or right
            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(1, 0)); //right
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);

            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(0, 1)); //down
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);

          }
          else if (kvp.Key.Y == Rows - 1)
          {
            //bottom-left corner, beam can go up or right
            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(1, 0)); //right
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);

            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(0, -1)); //up
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);
          }
          else
          {
            //left edge, beam goes right
            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(1, 0));
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);
          }
        }

        else if (kvp.Key.X == Cols - 1)
        {
          if (kvp.Key.Y == 0)
          {
            //top-right corner, beam can go down or left
            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(-1, 0)); //left
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);

            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(0, 1)); //down
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);
          }
          else if (kvp.Key.Y == Rows - 1)
          {
            //bottom-right corner, beam can go up or left
            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(-1, 0)); //left
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);

            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(0, -1)); //up
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);
          }
          else
          {
            //right edge, beam goes left
            Visited.Clear();
            energizedTiles.Clear();
            TraverseBeam(kvp.Key, new Point(-1, 0));
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);
          }
        }

        else if (kvp.Key.Y == 0 && 0 < kvp.Key.X && kvp.Key.X < Cols - 1)
        {
          //top edge, beam goes down
          Visited.Clear();
          energizedTiles.Clear();
          TraverseBeam(kvp.Key, new Point(0, 1));
          maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);
        }
        else if (kvp.Key.Y == Rows - 1 && 0 < kvp.Key.X && kvp.Key.X < Cols - 1)
        {
          //bottom edge, beam goes up
          Visited.Clear();
          energizedTiles.Clear();
          TraverseBeam(kvp.Key, new Point(0, -1));
          maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles.Count);
        }
      }

      totalCount = maxEnergizedTiles;

      result = totalCount.ToString();
      return result;
    }

    private void TraverseBeam(Point startPos, Point startDirection)
    {
      var stack = new Stack<(Point, Point)>();
      stack.Push((startPos, startDirection));

      while (stack.Count > 0)
      {
        var (pos, direction) = stack.Pop();
        if(!Grid.ContainsKey(pos) || Visited.Contains((pos, direction)))
        {
          continue;
        }

        Visited.Add((pos, direction));
        energizedTiles.Add(pos);

        var cell = Grid[pos];
        switch (cell)
        {
          case '.':
            //continue in the same direction
            stack.Push((pos + direction, direction));
            break;
          case '/':
            //reflect 90 degrees
            var newDirection = new Point(-direction.Y, -direction.X);
            stack.Push((pos + newDirection, newDirection));
            break;
          case '\\':
            //reflect 90 degrees
            newDirection = new Point(direction.Y, direction.X);
            stack.Push((pos + newDirection, newDirection));
            break;
          case '|':
            //Split or Pass through
            if (direction.X != 0)
            {
              //Split into two beams going up and down
              var dir1 = new Point(0, -1);
              stack.Push((pos + dir1, dir1)); //up

              var dir2 = new Point(0, 1);
              stack.Push((pos + dir2, dir2)); //down           
            }
            else
            {
              //Pass through
              stack.Push((pos + direction, direction));
            }
            break;
          case '-':
            //Split or Pass through
            if (direction.Y != 0)
            {
              //Split into two beams going left and right
              var dir1 = new Point(-1, 0);
              stack.Push((pos + dir1, dir1)); //left

              var dir2 = new Point(1, 0);
              stack.Push((pos + dir2, dir2)); //right
            }
            else
            {
              //Pass through
              stack.Push((pos + direction, direction));
            }
            break;
        }
      }
    }
  }
}
