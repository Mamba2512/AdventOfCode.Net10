using System.Diagnostics;
using System.Linq;
using Point = AdventOfCodeNet10.Extensions.Point;

namespace AdventOfCodeNet10._2023.Day_16
{

  internal class Part_1_2023_Day_16 : Days
  {

    /// <summary>
    /*
    https://adventofcode.com/2023/day/16
    --- Day 16: The Floor Will Be Lava ---
    With the beam of light completely focused somewhere, the reindeer leads you
    deeper still into the Lava Production Facility. At some point, you realize that
    the steel facility walls have been replaced with cave, and the doorways are
    just cave, and the floor is cave, and you're pretty sure this is actually just
    a giant cave.
    
    Finally, as you approach what must be the heart of the mountain, you see a
    bright light in a cavern up ahead. There, you discover that the beam of light
    you so carefully focused is emerging from the cavern wall closest to the
    facility and pouring all of its energy into a contraption on the opposite side.
    
    Upon closer inspection, the contraption appears to be a flat, two-dimensional
    square grid containing empty space (.), mirrors (/ and \), and splitters (| and
    -).
    
    The contraption is aligned so that most of the beam bounces around the grid,
    but each tile on the grid converts some of the beam's light into heat to melt
    the rock in the cavern.
    
    You note the layout of the contraption (your puzzle input). For example:
    
    .|...\....
    |.-.\.....
    .....|-...
    ........|.
    ..........
    .........\
    ..../.\\..
    .-.-/..|..
    .|....-|.\
    ..//.|....
    The beam enters in the top-left corner from the left and heading to the right.
    Then, its behavior depends on what it encounters as it moves:
    
    If the beam encounters empty space (.), it continues in the same direction.
    If the beam encounters a mirror (/ or \), the beam is reflected 90 degrees
    depending on the angle of the mirror. For instance, a rightward-moving beam
    that encounters a / mirror would continue upward in the mirror's column, while
    a rightward-moving beam that encounters a \ mirror would continue downward from
    the mirror's column.
    If the beam encounters the pointy end of a splitter (| or -), the beam passes
    through the splitter as if the splitter were empty space. For instance, a
    rightward-moving beam that encounters a - splitter would continue in the same
    direction.
    If the beam encounters the flat side of a splitter (| or -), the beam is split
    into two beams going in each of the two directions the splitter's pointy ends
    are pointing. For instance, a rightward-moving beam that encounters a |
    splitter would split into two beams: one that continues upward from the
    splitter's column and one that continues downward from the splitter's column.
    Beams do not interact with other beams; a tile can have many beams passing
    through it at the same time. A tile is energized if that tile has at least one
    beam pass through it, reflect in it, or split in it.
    
    In the above example, here is how the beam of light bounces around the
    contraption:
    
    >|<<<\....
    |v-.\^....
    .v...|->>>
    .v...v^.|.
    .v...v^...
    .v...v^..\
    .v../2\\..
    <->-/vv|..
    .|<<<2-|.\
    .v//.|.v..
    Beams are only shown on empty tiles; arrows indicate the direction of the
    beams. If a tile contains beams moving in multiple directions, the number of
    distinct directions is shown instead. Here is the same diagram but instead only
    showing whether a tile is energized (#) or not (.):
    
    ######....
    .#...#....
    .#...#####
    .#...##...
    .#...##...
    .#...##...
    .#..####..
    ########..
    .#######..
    .#...#.#..
    Ultimately, in this example, 46 tiles become energized.
    
    The light isn't energizing enough tiles to produce lava; to debug the
    contraption, you need to start by analyzing the current situation. With the
    beam starting in the top-left heading right, how many tiles end up being
    energized?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
    Dictionary<Point, char> Grid = new();
    List<List<(Point pos, Point dir)>> allPaths = new();
    HashSet<Point> energizedTiles = new();
    List<int> numOfEnergizedTiles = new();
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
      numOfEnergizedTiles.Clear();


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

      
      TraverseBeam(new Point(0, 0), new Point(1, 0));

      
      totalCount = energizedTiles.Count;

      result = totalCount.ToString();
      return result;
    }

    private void TraverseBeam(Point pos, Point direction)
    {
      Point currentPoint = pos;
      Point currentDirection = direction;

      if (!Grid.ContainsKey(pos) || Visited.Contains((currentPoint, currentDirection)))
      {
        return;
      }

      Visited.Add((currentPoint, currentDirection));
      energizedTiles.Add(currentPoint);

      var cell = Grid[currentPoint];
      switch (cell)
      {
        case '.':
          //continue in the same direction
          TraverseBeam(currentPoint + direction, currentDirection);
          break;
        case '/':
          //reflect 90 degrees
          var newDirection = new Point(-currentDirection.Y, -currentDirection.X);
          TraverseBeam(currentPoint + newDirection, newDirection);
          break;
        case '\\':
          //reflect 90 degrees
          newDirection = new Point(currentDirection.Y, currentDirection.X);
          TraverseBeam(currentPoint + newDirection, newDirection);
          break;
        case '|':
          //Split or Pass through
          if (currentDirection.X != 0)
          {
            //Split into two beams going up and down
            var dir1 = new Point(0, -1);
            TraverseBeam(currentPoint + dir1, dir1); //up

            var dir2 = new Point(0, 1);
            TraverseBeam(currentPoint + dir2, dir2); //down           
          }
          else
          {
            //Pass through
            TraverseBeam(currentPoint + currentDirection, currentDirection);
          }
          break;
        case '-':
          //Split or Pass through
          if (currentDirection.Y != 0)
          {
            //Split into two beams going left and right
            var dir1 = new Point(-1, 0);
            TraverseBeam(currentPoint + dir1, dir1); //left

            var dir2 = new Point(1, 0);
            TraverseBeam(currentPoint + dir2, dir2); //right
          }
          else
          {
            //Pass through
            TraverseBeam(currentPoint + currentDirection, currentDirection);
          }
          break;
        default:
          throw new InvalidOperationException($"Unknown cell type: {cell}");
      }
    }
  }
}
