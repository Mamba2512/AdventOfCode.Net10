
using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Point = AdventOfCodeNet10.Extensions.Point;

namespace AdventOfCodeNet10._2024.Day_20
{
  internal class Part_1_2024_Day_20 : Days
  {
    
    public Dictionary<Point, char> Grid = new();
    public int RowSize = 0;
    public int ColumnSize = 0;
    public Point StartPos = new(0, 0);
    public Point EndPos = new(0, 0);
    /// <summary>
    /*
    https://adventofcode.com/2024/day/20
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

      Debug.WriteLine("Grid:");
      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2024_Day_20.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2024_Day_20.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        int colIdx = 0;
        foreach (var entry in line)
        {
          Grid.Add(new Point(rowIdx, colIdx), entry);
          if (entry == 'S')
          {
            StartPos = new Point(rowIdx, colIdx);
          }
          else if (entry == 'E')
          {
            EndPos = new Point(rowIdx, colIdx);
          }
          colIdx++;
        }
        rowIdx++;
        ColumnSize = colIdx;
      }
      RowSize = rowIdx;

      for (int i = 0; i < RowSize; i++)
      {
        for (int j = 0; j < ColumnSize; j++)
        {
          Debug.Write(Grid[(i, j)]);
        }
        Debug.WriteLine("");
      }

      var allPaths = FindAllPaths(StartPos, EndPos);
      totalCount = allPaths.Count;

      PrintAllPaths(allPaths);



      result = totalCount.ToString();
      return result;
    }

    private void PrintAllPaths(List<List<Point>> allPaths)
    {
      int pathNumber = 1;
      foreach (var path in allPaths)
      {
        Debug.WriteLine($"Path {pathNumber}:");
        var pathSet = new HashSet<Point>(path);
        for (int i = 0; i < RowSize; i++)
        {
          for (int j = 0; j < ColumnSize; j++)
          {
            var currentPoint = new Point(i, j);
            if (pathSet.Contains(currentPoint))
            {
              Debug.Write('*');
            }
            else
            {
              Debug.Write(Grid[(i, j)]);
            }
          }
          Debug.WriteLine("");
        }
        Debug.WriteLine("");
        pathNumber++;
      }

    }

    public List<List<Point>> FindAllPaths(Point start, Point end)
    {
      var allPaths = new List<List<Point>>();
      var currentPath = new List<Point>();
      var visited = new HashSet<Point>();
      var allPathsWithCheats = new Dictionary<(Point, Point), List<List<Point>>>(); 
      //var cheats = new List<(Point, Point)>();

      FindPathsRecursive(start, end, currentPath, visited, allPaths);

      return allPaths;

    }

    public void FindPathsRecursive(Point pos, Point end, List<Point> currentPath, HashSet<Point> visited, List<List<Point>> allPaths)
    {
      if (!Grid.TryGetValue(pos, out char value) || value == '#')
        return;
      if (visited.Contains(pos))
        return;
      currentPath.Add(pos);
      visited.Add(pos);

      if (pos == end)
      {
        allPaths.Add(new List<Point>(currentPath));
      }
      else
      {
        foreach (var dir in Directions.WithoutDiagonals)
        {
          FindPathsRecursive(pos + dir, end, currentPath, visited, allPaths);
        }
      }

      currentPath.RemoveAt(currentPath.Count - 1);
      visited.Remove(pos);
    }

    //public void FindPathsRecursive(Point pos, Point end, List<Point> currentPath, HashSet<Point> visited, Dictionary<(Point, Point), List<List<Point>>> allPaths, bool isCheatActive)
    //{
    //  if (!Grid.TryGetValue(pos, out char value) || value == '#')
    //  {
    //    if(isCheatActive)
    //    {
    //      return;
    //    }
    //    else
    //    {
    //      return;
    //    }
    //  }       
    //  if (visited.Contains(pos))
    //    return;
    //  currentPath.Add(pos);
    //  visited.Add(pos);

    //  if (pos == end)
    //  {
    //    allPaths.Add(new List<Point>(currentPath));
    //  }
    //  else
    //  {
    //    foreach (var dir in Directions.WithoutDiagonals)
    //    {
    //      FindPathsRecursive(pos + dir, end, currentPath, visited, allPaths);
    //    }
    //  }

    //  currentPath.RemoveAt(currentPath.Count - 1);
    //  visited.Remove(pos);
    //}



  }
}
