using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Point = AdventOfCodeNet10.Extensions.Point;
namespace AdventOfCodeNet10._2023.Day_10
{
  internal class Part_1_2023_Day_10 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/10
    --- Day 10: Pipe Maze ---
    You use the hang glider to ride the hot air from Desert Island all the way up to the floating metal island. This island is surprisingly cold and there definitely aren't any thermals to glide on, so you leave your hang glider behind.

    You wander around for a while, but you don't find any people or animals. However, you do occasionally find signposts labeled "Hot Springs" pointing in a seemingly consistent direction; maybe you can find someone at the hot springs and ask them where the desert-machine parts are made.

    The landscape here is alien; even the flowers and trees are made of metal. As you stop to admire some metal grass, you notice something metallic scurry away in your peripheral vision and jump into a big pipe! It didn't look like any animal you've ever seen; if you want a better look, you'll need to get ahead of it.

    Scanning the area, you discover that the entire field you're standing on is densely packed with pipes; it was hard to tell at first because they're the same metallic silver color as the "ground". You make a quick sketch of all of the surface pipes you can see (your puzzle input).

    The pipes are arranged in a two-dimensional grid of tiles:

    | is a vertical pipe connecting north and south.
    - is a horizontal pipe connecting east and west.
    L is a 90-degree bend connecting north and east.
    J is a 90-degree bend connecting north and west.
    7 is a 90-degree bend connecting south and west.
    F is a 90-degree bend connecting south and east.
    . is ground; there is no pipe in this tile.
    S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
    Based on the acoustics of the animal's scurrying, you're confident the pipe that contains the animal is one large, continuous loop.

    For example, here is a square loop of pipe:

    .....
    .F-7.
    .|.|.
    .L-J.
    .....
    If the animal had entered this loop in the northwest corner, the sketch would instead look like this:

    .....
    .S-7.
    .|.|.
    .L-J.
    .....
    In the above diagram, the S tile is still a 90-degree F bend: you can tell because of how the adjacent pipes connect to it.

    Unfortunately, there are also many pipes that aren't connected to the loop! This sketch shows the same loop as above:

    -L|F7
    7S-7|
    L|7||
    -L-J|
    L|-JF
    In the above diagram, you can still figure out which pipes form the main loop: they're the ones connected to S, pipes those pipes connect to, pipes those pipes connect to, and so on. Every pipe in the main loop connects to its two neighbors (including S, which will have exactly two pipes connecting to it, and which is assumed to connect back to those two pipes).

    Here is a sketch that contains a slightly more complex main loop:

    ..F7.
    .FJ|.
    SJ.L7
    |F--J
    LJ...
    Here's the same example sketch with the extra, non-main-loop pipe tiles also shown:

    7-F7-
    .FJ|7
    SJLL7
    |F--J
    LJ.LJ
    If you want to get out ahead of the animal, you should find the tile in the loop that is farthest from the starting position. Because the animal is in the pipe, it doesn't make sense to measure this by direct distance. Instead, you need to find the tile that would take the longest number of steps along the loop to reach from the starting point - regardless of which way around the loop the animal went.

    In the first example with the square loop:

    .....
    .S-7.
    .|.|.
    .L-J.
    .....
    You can count the distance each tile in the loop is from the starting point like this:

    .....
    .012.
    .1.3.
    .234.
    .....
    In this example, the farthest point from the start is 4 steps away.

    Here's the more complex loop again:

    ..F7.
    .FJ|.
    SJ.L7
    |F--J
    LJ...
    Here are the distances for each tile on that loop:

    ..45.
    .236.
    01.78
    14567
    23...
    Find the single giant loop starting at S. How many steps along the loop does it take to get from the starting position to the point farthest from the starting position?    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 

    public Dictionary<Point, string> Grid = new Dictionary<Point, string>();
    public Point StartPos = new Point(0, 0);
    //get pipe opening constants
    public Dictionary<string, List<string>> PipeOpenings = new Dictionary<string, List<string>>()
    {
      { "|", new List<string> { "N", "S" } },
      { "-", new List<string> { "E", "W" } },
      { "L", new List<string> { "N", "E" } },
      { "J", new List<string> { "N", "W" } },
      { "7", new List<string> { "S", "W" } },
      { "F", new List<string> { "S", "E" } },
    };

    public override string Execute()
    {
      string result = "";
      double totalCount = 0;
      int rowIdx = 0;
      Grid = new Dictionary<Point, string>();
      StartPos = new Point(0, 0);
      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_10.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_10.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        int colIdx = 0;
        foreach (var ch in line)
        {
          Grid.Add(new Point(colIdx, rowIdx), ch.ToString());
          if (ch == 'S')
          {
            StartPos = new Point(colIdx, rowIdx);
          }
          colIdx++;
        }
        rowIdx++;
      }

      var startPipe = GetPipeForStartPosition();

      List<List<Point>> AllPaths = new List<List<Point>>();
      List<Point> currentPath = new List<Point>() { };
      CheckAllPaths(AllPaths, currentPath, StartPos, StartPos, new HashSet<Point>() { });
      
      foreach (var path in AllPaths)
      {
        totalCount = GetFurthestTileDistance(path);
      }

      result = totalCount.ToString();
      return result;
    }

    //public int FindFurthestPoint()
    //{
    //  string startPipe = GetPipeForStartPosition();

    //  //get first connected neighbour from S




    //  //


    //}

    //public Point FindFirstConnectedNeighbour(Point start)
    //{

    //}






    //below ones are not working for the test input so need to use above fnc

    public void CheckAllPaths(List<List<Point>> allPaths, List<Point> currentPath, Point StartPos, Point currentPos, HashSet<Point> visitedPositions)
    {
      //Debug.WriteLine("Visiting: " + currentPos + " : " + Grid[currentPos] + " Path so far: " + string.Join(" -> ", currentPath));
      if (currentPos == StartPos && currentPath.Count > 2)
      {
        // Found a loop
        //Debug.WriteLine("Found a loop: " + string.Join(" -> ", currentPath));
        allPaths.Add(currentPath);
        return;
      }

      foreach (var dir in Directions.WithoutDiagonals)
      {
        //Debug.WriteLine(" ---> Checking direction: " + dir);
        var nextPos = currentPos + dir; //incorrect


        if (Grid.ContainsKey(nextPos) && !visitedPositions.Contains(nextPos) && Grid[nextPos] != ".")
        {
          if (CanConnect(currentPos, nextPos))
          {
            ////Debug.WriteLine(" ---> Can connect from " + currentPos + " : " + Grid[currentPos] + " to " + nextPos + " : " + Grid[nextPos]);
            // found a new point to visit
            visitedPositions.Add(nextPos);
            currentPath.Add(nextPos);
            CheckAllPaths(allPaths, currentPath, StartPos, nextPos, visitedPositions);
          }
          else
          {
            //Debug.WriteLine(" ---> Cannot connect from " + currentPos + " : " + Grid[currentPos] + " to " + nextPos + " : " + Grid[nextPos]);
          }
        }
        else
        {
          //Debug.WriteLine(" ---> Cannot visit " + nextPos + "Grid.ContainsKey(nextPos)" + Grid.ContainsKey(nextPos) + "visitedPositions.Contains(nextPos)" + visitedPositions.Contains(nextPos));
        }
      }
    }
    public bool CanConnect(Point pipeFrom, Point pipeTo)
    {
      // check if pipes horizontal or vertical
      bool isHorizontal = pipeFrom.Y == pipeTo.Y;
      //get pipe types
      string pipeFromType = Grid[pipeFrom];
      string pipeToType = Grid[pipeTo];

      if (isHorizontal)
      {
        //check if we going left or if we going right
        bool isGoingRight = pipeFrom.X < pipeTo.X;
        if (isGoingRight)
        {
          //check if pipeFrom has east opening and pipeTo has west opening
          if (!PipeOpenings[pipeFromType].Contains("E") || !PipeOpenings[pipeToType].Contains("W"))
          {
            return false;
          }
        }
        else
        {
          //check if pipeFrom has west opening and pipeTo has east opening
          if (!PipeOpenings[pipeFromType].Contains("W") || !PipeOpenings[pipeToType].Contains("E"))
          {
            return false;
          }
        }
      }
      else
      {
        //check if we going up or if we going down
        bool isGoingDown = pipeFrom.Y < pipeTo.Y;
        if (isGoingDown)
        {
          //check if pipeFrom has south opening and pipeTo has north opening
          if (!PipeOpenings[pipeFromType].Contains("S") || !PipeOpenings[pipeToType].Contains("N"))
          {
            return false;
          }
        }
        else
        {
          //check if pipeFrom has north opening and pipeTo has south opening
          if (!PipeOpenings[pipeFromType].Contains("N") || !PipeOpenings[pipeToType].Contains("S"))
          {
            return false;
          }
        }
      }
      return true; // Placeholder
    }

    public string GetPipeForStartPosition()
    {
      var startPos = StartPos;
      List<List<string>> possiblePipes = new List<List<string>>();

      foreach (var dir in Directions.WithoutDiagonals)
      {
        var nextPos = startPos + dir;
        if (!Grid.ContainsKey(nextPos) || Grid[nextPos] == ".")
        {
          //found a pipe next to start position
          continue;
        }
        else
        {
          //found a pipe next to start position
          string pipeType = Grid[nextPos];
          //determine pipe type based on direction
          if (dir == Directions.ToUp)
          {
            if (PipeOpenings[pipeType].Contains("S")) //only pipes facing South can connect to S
            {
              //StatPipe can be any one which have North Opening
              possiblePipes.Add(PipeOpenings.Where(p => p.Value.Contains("N")).Select(p => p.Key).ToList());
            }
          }
          else if (dir == Directions.ToDown)
          {
            if (PipeOpenings[pipeType].Contains("N"))
            {
              //StatPipe can be any one which have South Opening
              possiblePipes.Add(PipeOpenings.Where(p => p.Value.Contains("S")).Select(p => p.Key).ToList());
            }
          }
          else if (dir == Directions.ToLeft)
          {
            if (PipeOpenings[pipeType].Contains("E"))
            {
              //StatPipe can be any one which have West Opening
              possiblePipes.Add(PipeOpenings.Where(p => p.Value.Contains("W")).Select(p => p.Key).ToList());
            }
          }
          else if (dir == Directions.ToRight)
          {
            if (PipeOpenings[pipeType].Contains("W"))
            {
              //StatPipe can be any one which have East Opening
              possiblePipes.Add(PipeOpenings.Where(p => p.Value.Contains("E")).Select(p => p.Key).ToList());
            }
          }
        }
      }

      //now the entry in each list having most num of occurences of a partucular string is my startpos pipe
      var pipeCounts = new Dictionary<string, int>();
      foreach (var pipe in possiblePipes)
      {
        foreach (var p in pipe)
        {
          if (!pipeCounts.ContainsKey(p))
          {
            pipeCounts[p] = 1;
          }
          else
          {
            pipeCounts[p]++;
          }

        }
      }

      //find the pipe with max count
      var startPipe = pipeCounts.OrderByDescending(p => p.Value).First().Key;
      Grid[StartPos] = startPipe;
      return startPipe;
    }

    public double GetFurthestTileDistance(List<Point> path)
    {
      double maxDistance = 0;

      maxDistance = Math.Ceiling(path.Count / 2.0);


      return maxDistance;
    }
  }
}
