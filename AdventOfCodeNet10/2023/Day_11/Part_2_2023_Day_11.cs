namespace AdventOfCodeNet10._2023.Day_11
{
  internal class Part_2_2023_Day_11 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/11
    --- Part Two ---
    The galaxies are much older (and thus much farther apart) than the researcher
    initially estimated.
    
    Now, instead of the expansion you did before, make each empty row or column one
    million times larger. That is, each empty row should be replaced with 1000000
    empty rows, and each empty column should be replaced with 1000000 empty columns.
    
    (In the example above, if each empty row or column were merely 10 times larger,
    the sum of the shortest paths between every pair of galaxies would be 1030. If
    each empty row or column were merely 100 times larger, the sum of the shortest
    paths between every pair of galaxies would be 8410. However, your universe will
    need to expand far beyond these values.)
    
    Starting with the same initial image, expand the universe according to these
    new rules, then find the length of the shortest path between every pair of
    galaxies. What is the sum of these lengths?
    
    
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public Dictionary<Point, char> grid = new();
    List<Point> galaxyPoints = new();
    List<int> emptyRows = new();
    List<int> emptyCols = new();

    public override string Execute()
    {
      grid = new Dictionary<Point, char>();
      galaxyPoints = new List<Point>();
      emptyRows = new List<int>();
      emptyCols = new List<int>();


      string result = "";
      long totalCount = 0;
      int RowLt = 0;
      int ColLt = 0;


      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_11.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_11.txt already stored in "Lines"
      //
      int rowIdx = 0;

      foreach (var line in Lines)
      {
        RowLt = line.Length;
        int coldIdx = 0;

        if (!line.Contains('#'))
        {
          emptyRows.Add(rowIdx);
        }

        foreach (var ch in line)
        {
          grid.Add(new Point(coldIdx, rowIdx), ch);
          if (ch == '#')
          {
            galaxyPoints.Add(new Point(coldIdx, rowIdx));
          }
          coldIdx++;

        }
        rowIdx++;
      }
      ColLt = rowIdx;

      //print the grid
      for (int i = 0; i < ColLt; i++)
      {
        bool isEmptyCol = true;
        for (int j = 0; j < RowLt; j++)
        {
          if (grid[new Point(i, j)] == '#')
          {
            isEmptyCol = false;
            continue;
          }
          //Debug.Write(grid[new Point(j, i)]);
        }
        if (isEmptyCol)
        {
          emptyCols.Add(i);
        }
        //Debug.WriteLine("");
      }

      List<Point> newGalaxyPoints = new();

      foreach (var galaxy in galaxyPoints)
      {
        newGalaxyPoints.Add(GetNewCoordinatesForGalaxy(galaxy));
      }

      for (int i = 0; i < newGalaxyPoints.Count; i++)
      {
        for (int j = i + 1; j < newGalaxyPoints.Count; j++)
        {
          Point galaxyA = newGalaxyPoints[i];
          Point galaxyB = newGalaxyPoints[j];
          int distance = Math.Abs(galaxyA.X - galaxyB.X) + Math.Abs(galaxyA.Y - galaxyB.Y);
          totalCount += distance;
        }
      }
      result = totalCount.ToString();
      return result;
    }

    public Point GetNewCoordinatesForGalaxy(Point Galaxy)
    {
      Point newGalaxyPoint = new Point(Galaxy.X, Galaxy.Y);
      foreach (var emptyRow in emptyRows)
      {
        if (Galaxy.Y > emptyRow)
        {
          newGalaxyPoint = new Point(newGalaxyPoint.X, newGalaxyPoint.Y + 999999);
        }
      }
      foreach (var emptyCol in emptyCols)
      {
        if (Galaxy.X > emptyCol)
        {
          newGalaxyPoint = new Point(newGalaxyPoint.X + 999999, newGalaxyPoint.Y);
        }
      }
      return newGalaxyPoint;
    }
  }
}
