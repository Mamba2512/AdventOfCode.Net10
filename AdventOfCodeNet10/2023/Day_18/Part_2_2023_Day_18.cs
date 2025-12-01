using Point = AdventOfCodeNet10.Extensions.Point;
namespace AdventOfCodeNet10._2023.Day_18
{
  using System.Diagnostics;
  using DigInput = (string dir, int numOfTrenches, string colorCode);
  internal class Part_2_2023_Day_18 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/18
    The Elves were right to be concerned; the planned lagoon would be much too
    small.
    
    After a few minutes, someone realizes what happened; someone swapped the color
    and instruction parameters when producing the dig plan. They don't have time to
    fix the bug; one of them asks if you can extract the correct instructions from
    the hexadecimal codes.
    
    Each hexadecimal code is six hexadecimal digits long. The first five
    hexadecimal digits encode the distance in meters as a five-digit hexadecimal
    number. The last hexadecimal digit encodes the direction to dig: 0 means R, 1
    means D, 2 means L, and 3 means U.
    
    So, in the above example, the hexadecimal codes can be converted into the true
    instructions:
    
    #70c710 = R 461937
    #0dc571 = D 56407
    #5713f0 = R 356671
    #d2c081 = D 863240
    #59c680 = R 367720
    #411b91 = D 266681
    #8ceee2 = L 577262
    #caa173 = U 829975
    #1b58a2 = L 112010
    #caa171 = D 829975
    #7807d2 = L 491645
    #a77fa3 = U 686074
    #015232 = L 5411
    #7a21e3 = U 500254
    Digging out this loop and its interior produces a lagoon that can hold an
    impressive 952408144115 cubic meters of lava.
    
    Convert the hexadecimal color codes into the correct instructions; if the Elves
    follow this new dig plan, how many cubic meters of lava could the lagoon hold?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
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

      foreach (var line in Lines)
      {
        var splitInput = line.Split(' ');
        var part1 = splitInput[2].Split('(')[1];
        var part2 = part1.TrimEnd(')');
        var dir = splitInput[0];
        var numOfTrenches = int.Parse(splitInput[1]);
        var colorCode = part2;

        CodeInterpretor(part2);
      }

      var currentPoint = new Point(0, 0);
      long perimeter = 0;

      foreach(var digInput in digInputs)
      {
        CornerPoints.Add(currentPoint);
        perimeter += digInput.numOfTrenches;

        currentPoint = digInput.dir switch
        {
          "R" => new Point(currentPoint.X + digInput.numOfTrenches, currentPoint.Y),
          "L" => new Point(currentPoint.X - digInput.numOfTrenches, currentPoint.Y),
          "U" => new Point(currentPoint.X, currentPoint.Y + digInput.numOfTrenches),
          "D" => new Point(currentPoint.X, currentPoint.Y - digInput.numOfTrenches),
          _ => throw new Exception("Invalid direction"),
        };
      }

      //shoelace formula : uses corner points
      long shoelaceSum = 0;
      for (int i = 0; i < CornerPoints.Count; i++)
      {
        if (i == CornerPoints.Count - 1)
        {
          var currentPoint1 = CornerPoints[i];
          var nextPoint1 = CornerPoints[0];
          shoelaceSum += (currentPoint1.X * nextPoint1.Y) - (nextPoint1.X * currentPoint1.Y);
          break;
        }
        var currPoint = CornerPoints[i];
        var nextPoint = CornerPoints[(i + 1)];
        shoelaceSum += (currPoint.X * nextPoint.Y) - (nextPoint.X * currPoint.Y);
      }

      long interiorArea = Math.Abs(shoelaceSum) / 2;

      totalCount = interiorArea + perimeter / 2 + 1;
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

    private void CodeInterpretor(string colorCode)
    {
      string codeForDir = colorCode.Substring(colorCode.Length - 1, 1);
      string codeForNumOfTrenches = colorCode.Substring(1, colorCode.Length - 2);
      int numOfTrenches = int.Parse(codeForNumOfTrenches, System.Globalization.NumberStyles.HexNumber);
      string dir = codeForDir switch
      {
        "0" => "R",
        "1" => "D",
        "2" => "L",
        "3" => "U",
        _ => throw new Exception("Invalid direction code"),
      };

      digInputs.Add((dir, numOfTrenches, colorCode));
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
