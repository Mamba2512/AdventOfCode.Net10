using System.Diagnostics;

namespace AdventOfCodeNet10._2023.Day_11
{
  internal class Part_1_2023_Day_11 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/11
    --- Day 11: Cosmic Expansion ---
    You continue following signs for "Hot Springs" and eventually come across an
    observatory. The Elf within turns out to be a researcher studying cosmic
    expansion using the giant telescope here.
    
    He doesn't know anything about the missing machine parts; he's only visiting
    for this research project. However, he confirms that the hot springs are the
    next-closest area likely to have people; he'll even take you straight there
    once he's done with today's observation analysis.
    
    Maybe you can help him with the analysis to speed things up?
    
    The researcher has collected a bunch of data and compiled the data into a
    single giant image (your puzzle input). The image includes empty space (.) and
    galaxies (#). For example:
    
    ...#......
    .......#..
    #.........
    ..........
    ......#...
    .#........
    .........#
    ..........
    .......#..
    #...#.....
    The researcher is trying to figure out the sum of the lengths of the shortest
    path between every pair of galaxies. However, there's a catch: the universe
    expanded in the time it took the light from those galaxies to reach the
    observatory.
    
    Due to something involving gravitational effects, only some space expands. In
    fact, the result is that any rows or columns that contain no galaxies should
    all actually be twice as big.
    
    In the above example, three columns and two rows contain no galaxies:
    
    v  v  v
    ...#......
    .......#..
    #.........
    >..........<
    ......#...
    .#........
    .........#
    >..........<
    .......#..
    #...#.....
    ^  ^  ^
    These rows and columns need to be twice as big; the result of cosmic expansion
    therefore looks like this:
    
    ....#........
    .........#...
    #............
    .............
    .............
    ........#....
    .#...........
    ............#
    .............
    .............
    .........#...
    #....#.......
    Equipped with this expanded universe, the shortest path between every pair of
    galaxies can be found. It can help to assign every galaxy a unique number:
    
    ....1........
    .........2...
    3............
    .............
    .............
    ........4....
    .5...........
    ............6
    .............
    .............
    .........7...
    8....9.......
    In these 9 galaxies, there are 36 pairs. Only count each pair once; order
    within the pair doesn't matter. For each pair, find any shortest path between
    the two galaxies using only steps that move up, down, left, or right exactly
    one . or # at a time. (The shortest path between two galaxies is allowed to
    pass through another galaxy.)
    
    For example, here is one of the shortest paths between galaxies 5 and 9:
    
    ....1........
    .........2...
    3............
    .............
    .............
    ........4....
    .5...........
    .##.........6
    ..##.........
    ...##........
    ....##...7...
    8....9.......
    This path has length 9 because it takes a minimum of nine steps to get from
    galaxy 5 to galaxy 9 (the eight locations marked # plus the step onto galaxy 9
    itself). Here are some other example shortest path lengths:
    
    Between galaxy 1 and galaxy 7: 15
    Between galaxy 3 and galaxy 6: 17
    Between galaxy 8 and galaxy 9: 5
    In this example, after expanding the universe, the sum of the shortest path
    between all 36 pairs of galaxies is 374.
    
    Expand the universe, then find the length of the shortest path between every
    pair of galaxies. What is the sum of these lengths?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 

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
        newGalaxyPoints.Add(GetNewCoordinatesForGalaxy(galaxy)) ;
      }

      for(int i = 0; i < newGalaxyPoints.Count; i++)
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
          newGalaxyPoint = new Point(newGalaxyPoint.X, newGalaxyPoint.Y + 1);
        }
      }
      foreach (var emptyCol in emptyCols)
      {
        if (Galaxy.X > emptyCol)
        {
          newGalaxyPoint = new Point(newGalaxyPoint.X + 1, newGalaxyPoint.Y);
        }
      }

      return newGalaxyPoint;
    }
  }
}
