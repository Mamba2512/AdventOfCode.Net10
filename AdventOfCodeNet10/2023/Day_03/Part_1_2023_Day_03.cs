using Point = AdventOfCodeNet10.Extensions.Point;
using AdventOfCodeNet10.Extensions;

namespace AdventOfCodeNet10._2023.Day_03
{
  internal class Part_1_2023_Day_03 : Days
  {
    public Dictionary<Point, string> Grid = new Dictionary<Point, string>();
    public List<Point> SymbolPoints = new List<Point>();
    public Dictionary<Point, int> NumberPoints = new Dictionary<Point, int>();
    public Dictionary<Point, int> CorrectedNumberPoints = new Dictionary<Point, int>();

    /// <summary>
    /*
    https://adventofcode.com/2023/day/3
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      int rowIdx = 0;
      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_03.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_03.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        int colIdx = 0;
        foreach (var elt in line)
        {
          Grid.Add(new Point(colIdx, rowIdx), elt.ToString());
          if (elt.ToString() != "." && !Int32.TryParse(elt.ToString(), out int val))
          {
            SymbolPoints.Add(new Point(colIdx, rowIdx));
          }
          if (Int32.TryParse(elt.ToString(), out int val2))
          {
            NumberPoints.Add(new Point(colIdx, rowIdx), val2);
          }
          colIdx++;
        }
        totalCount++;
        rowIdx++;
      }

      // DEBUG: Print all symbols found
      System.Diagnostics.Debug.WriteLine($"Found {SymbolPoints.Count} symbols:");
      foreach (var sp in SymbolPoints)
      {
        System.Diagnostics.Debug.WriteLine($"  Symbol at ({sp.X}, {sp.Y}): '{Grid[sp]}'");
      }

      HashSet<Point> adjacentPoints = new HashSet<Point>();

      foreach (var sp in SymbolPoints)
      {
        CheckAdjacentPoints(sp, adjacentPoints);
      }

      // DEBUG: Print adjacent points
      System.Diagnostics.Debug.WriteLine($"\nFound {adjacentPoints.Count} adjacent number points:");
      foreach (var ap in adjacentPoints.OrderBy(p => p.Y).ThenBy(p => p.X))
      {
        System.Diagnostics.Debug.WriteLine($"  Point ({ap.X}, {ap.Y}): digit {NumberPoints[ap]}");
      }

      //toDO: figure out complete numbers from adjacentPoints

      // After getting adjacentPoints, group them into complete numbers
      Dictionary<Point, int> completeNumbers = GroupPointsIntoNumbers(adjacentPoints);

      // DEBUG: Print complete numbers
      System.Diagnostics.Debug.WriteLine($"\nGrouped into {completeNumbers.Count} complete numbers:");
      foreach (var cn in completeNumbers.OrderBy(kvp => kvp.Key.Y).ThenBy(kvp => kvp.Key.X))
      {
        System.Diagnostics.Debug.WriteLine($"  Number starting at ({cn.Key.X}, {cn.Key.Y}): {cn.Value}");
      }

    

      // Sum all the complete numbers
      totalCount = completeNumbers.Values.Sum();

      System.Diagnostics.Debug.WriteLine($"\nTotal sum: {totalCount}");
      System.Diagnostics.Debug.WriteLine($"Expected: 4361");


      result = totalCount.ToString();
      return result;
    }

    public Dictionary<Point, int> GroupPointsIntoNumbers(HashSet<Point> points)
    {
      var result = new Dictionary<Point, int>();
      var processed = new HashSet<Point>();

      foreach (var point in points)
      {
        if (processed.Contains(point) || !NumberPoints.ContainsKey(point))
          continue;

        // Find all consecutive points on the same row (same Y, consecutive X)
        var current = point;

        // Go left to find the start of the number
        while (true)
        {
          var left = new Point(current.X - 1, current.Y);
          if (NumberPoints.ContainsKey(left) && !processed.Contains(left))
            current = left;
          else
            break;
        }

        // Now go right from the leftmost point to build the complete number
        var start = current;
        var numberDigits = new List<int>();
        
        while (NumberPoints.ContainsKey(current) && !processed.Contains(current))
        {
          numberDigits.Add(NumberPoints[current]);
          processed.Add(current);
          current = new Point(current.X + 1, current.Y);
        }

        // Build the actual number from the digits
        int completeNumber = 0;
        foreach (var digit in numberDigits)
        {
          completeNumber = completeNumber * 10 + digit;
        }

        // Store using the leftmost point as the key
        result[start] = completeNumber;
      }

      return result;
    }

    public void CheckAdjacentPoints(Point currentPos, HashSet<Point> adjacentPoints)
    {
      HashSet<Point> result = new HashSet<Point>();


      foreach (var direction in Directions.WithDiagonals)
      {
        var nextPos = currentPos + direction;
        if (NumberPoints.ContainsKey(nextPos))
        {
          adjacentPoints.Add((nextPos));
        }
      }

    }


  }
}
