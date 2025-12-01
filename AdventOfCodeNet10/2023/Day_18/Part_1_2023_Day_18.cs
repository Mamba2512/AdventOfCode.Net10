using Point = AdventOfCodeNet10.Extensions.Point;
namespace AdventOfCodeNet10._2023.Day_18
{
    using System.Diagnostics;
    using DigInput = (string dir, int numOfTrenches, string colorCode);
  internal class Part_1_2023_Day_18 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/18
    Thanks to your efforts, the machine parts factory is one of the first factories
    up and running since the lavafall came back. However, to catch up with the
    large backlog of parts requests, the factory will also need a large supply of
    lava for a while; the Elves have already started creating a large lagoon nearby
    for this purpose.
    
    However, they aren't sure the lagoon will be big enough; they've asked you to
    take a look at the dig plan (your puzzle input). For example:
    
    R 6 (#70c710)
    D 5 (#0dc571)
    L 2 (#5713f0)
    D 2 (#d2c081)
    R 2 (#59c680)
    D 2 (#411b91)
    L 5 (#8ceee2)
    U 2 (#caa173)
    L 1 (#1b58a2)
    U 2 (#caa171)
    R 2 (#7807d2)
    U 3 (#a77fa3)
    L 2 (#015232)
    U 2 (#7a21e3)
    The digger starts in a 1 meter cube hole in the ground. They then dig the
    specified number of meters up (U), down (D), left (L), or right (R), clearing
    full 1 meter cubes as they go. The directions are given as seen from above, so
    if "up" were north, then "right" would be east, and so on. Each trench is also
    listed with the color that the edge of the trench should be painted as an RGB
    hexadecimal color code.
    
    When viewed from above, the above example dig plan would result in the
    following loop of trench (#) having been dug out from otherwise ground-level
    terrain (.):
    
    #######
    #.....#
    ###...#
    ..#...#
    ..#...#
    ###.###
    #...#..
    ##..###
    .#....#
    .######
    At this point, the trench could contain 38 cubic meters of lava. However, this
    is just the edge of the lagoon; the next step is to dig out the interior so
    that it is one meter deep as well:
    
    #######
    #######
    #######
    ..#####
    ..#####
    #######
    #####..
    #######
    .######
    .######
    Now, the lagoon can contain a much more respectable 62 cubic meters of lava.
    While the interior is dug out, the edges are also painted according to the
    color codes in the dig plan.
    
    The Elves are concerned the lagoon won't be large enough; if they follow their
    dig plan, how many cubic meters of lava could it hold?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
    Dictionary<Point, char> map = new Dictionary<Point, char>();
    List<DigInput> digInputs = new List<DigInput>();
    public List<Point> CornerPoints = new List<Point>();
    public override string Execute()
    {
      map.Clear();
      digInputs.Clear();
      string result = "";
      long totalCount = 0;
      CornerPoints.Clear(); 

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_18.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_18.txt already stored in "Lines"
      //

      int count = 0;
      foreach (var line in Lines)
      {
        var splitInput = line.Split(' ');
        var part1 = splitInput[2].Split('(')[1];
        var part2 = part1.TrimEnd(')');
        var dir = splitInput[0];
        var numOfTrenches = int.Parse(splitInput[1]);
        var colorCode = part2;
        digInputs.Add((dir, numOfTrenches, colorCode));    
      }

      FillWithTrenches(digInputs, new Point() { X = 0, Y = 0 });

      var lowestX = map.Select(kv => kv.Key.X).Min();
      var highestX = map.Select(kv => kv.Key.X).Max();
      var lowestY = map.Select(kv => kv.Key.Y).Min();
      var highestY = map.Select(kv => kv.Key.Y).Max();

      for (long j = highestY; j >= lowestY; j--)
      {
        for (long i = lowestX; i <= highestX; i++)
        {
          var point = new Point() { X = i, Y = j };
          if (map.ContainsKey(point))
          {
            //if (CornerPoints.Contains(point) == false)
            //{
              //Debug.Write(map[point]);
            //}
            //else
            //{
            //  Debug.Write('C');
            //}
          }
          else
          {
            //if (CornerPoints.Contains(point) == false)
            //{
              //Debug.Write('.');
            //}
            //else
            //{
            //  Debug.Write('N');
            //}
          }
        }
        //Debug.WriteLine("");
      }

      long perimeter = map.Count;

      //shoelace formula : uses corner points
      long shoelaceSum = 0;
      for(int i = 0; i < CornerPoints.Count; i++)
      {
        if(i == CornerPoints.Count - 1)
        {
          var currentPoint1 = CornerPoints[i];
          var nextPoint1 = CornerPoints[0];
          shoelaceSum += (currentPoint1.X * nextPoint1.Y) - (nextPoint1.X * currentPoint1.Y);
          break;
        }
        var currentPoint = CornerPoints[i];
        var nextPoint = CornerPoints[(i + 1)];
        shoelaceSum += (currentPoint.X * nextPoint.Y) - (nextPoint.X * currentPoint.Y);
      }

      long interiorArea = Math.Abs(shoelaceSum) / 2;

      totalCount = interiorArea + perimeter/2 + 1;
      Debug.WriteLine($"Perimeter (boundary points): {perimeter}");
      Debug.WriteLine($"Corner points count: {CornerPoints.Count}");
      Debug.WriteLine($"Shoelace sum: {shoelaceSum}");
      Debug.WriteLine($"Interior area: {interiorArea}");
      Debug.WriteLine($"Total area: {totalCount}");

      result = totalCount.ToString();
      return result;
    }
    
    private void FillWithTrenches(List<DigInput> digInputs, Point startPoint)
    {
      
      foreach (var digInput in digInputs)
      {
        CornerPoints.Add(startPoint);
        startPoint = FillMapWithTrenches(digInput, startPoint);
      }
    }
  


    public Point FillMapWithTrenches(DigInput digInput, Point currentPoint)
    {
      var dir = digInput.dir;

      for (int i = 0; i < digInput.numOfTrenches; i++)
      {
        switch (dir)
        {
          case "R":
            {
              var newPoint = new Point() { X = currentPoint.X + 1, Y = currentPoint.Y };
              map[newPoint] = '#';
              currentPoint = newPoint;
              break;
            }
          case "L":
            {
              var newPoint = new Point() { X = currentPoint.X - 1, Y = currentPoint.Y };
              map[newPoint] = '#';
              currentPoint = newPoint;
              break;
            }
          case "U":
            {
              var newPoint = new Point() { X = currentPoint.X, Y = currentPoint.Y + 1 };
              map[newPoint] = '#';
              currentPoint = newPoint;
              break;
            }
          case "D":
            {
              var newPoint = new Point() { X = currentPoint.X, Y = currentPoint.Y - 1 };
              map[newPoint] = '#';
              currentPoint = newPoint;
              break;
            }
        }      
      }
      return currentPoint;
    }
  }
}
