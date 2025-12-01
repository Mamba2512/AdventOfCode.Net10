using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using Point = AdventOfCodeNet10.Extensions.Point;

namespace AdventOfCodeNet10._2024.Day_16
{
  internal class Part_1_2024_Day_16 : Days
  {
    public Dictionary<Point, char> Grid = new();
    public int RowSize = 0;
    public int ColumnSize = 0;
    public Point StartPos = new(0, 0);
    public Point EndPos = new(0, 0);

    // Store all scores found
    private List<int> allScores = new();
    private int callDepth = 0;

    public override string Execute()
    {
      string result = "";

      int rowIdx = 0;

      foreach (var line in Lines)
      {
        int colIdx = 0;
        foreach (var entry in line)
        {
          // Store as Point(column, row) = Point(X, Y)
          Grid.Add(new Point(colIdx, rowIdx), entry);
          if (entry == 'S')
          {
            StartPos = new Point(colIdx, rowIdx);
          }
          else if (entry == 'E')
          {
            EndPos = new Point(colIdx, rowIdx);
          }
          colIdx++;
        }
        rowIdx++;
        ColumnSize = colIdx;
        RowSize = rowIdx;
      }

      // Find minimum score using recursive backtracking
      //int minScore = FindLowestScoreBacktracking(StartPos, EndPos);

      int minScore = FindLowestScoreDijkstra(StartPos, EndPos);
      result = minScore.ToString();
      return result;
    }

    public int FindLowestScoreDijkstra(Point start, Point end)
    {
      // Priority queue: always processes LOWEST score first
      var pq = new PriorityQueue<(Point pos, Point dir, int score), int>();

      // Track best score for each (position, direction) state
      var visited = new Dictionary<(Point pos, Point dir), int>();

      // Start facing East
      Point startDir = new Point(1, 0);
      pq.Enqueue((start, startDir, 0), 0);

      while (pq.Count > 0)
      {
        var (pos, dir, score) = pq.Dequeue();

        // Found the end! First time we reach it = optimal score
        if (pos == end)
        {
          return score;
        }

        var state = (pos, dir);

        // Skip if we've seen this state with a better score
        if (visited.TryGetValue(state, out int prevScore) && prevScore <= score)
        {
          continue;
        }
        visited[state] = score;

        // Option 1: Move forward
        Point nextPos = pos + dir;
        if (Grid.TryGetValue(nextPos, out char cell) && cell != '#')
        {
          pq.Enqueue((nextPos, dir, score + 1), score + 1);
        }

        // Option 2: Turn left
        Point leftDir = new Point(dir.Y, -dir.X);
        pq.Enqueue((pos, leftDir, score + 1000), score + 1000);

        // Option 3: Turn right
        Point rightDir = new Point(-dir.Y, dir.X);
        pq.Enqueue((pos, rightDir, score + 1000), score + 1000);
      }

      return -1; // No path found
    }


    /*
    public int FindLowestScoreBacktracking(Point start, Point end)
    {
      allScores.Clear();
      callDepth = 0;

      // East direction: X+1, Y+0 (moving right)
      Point startDir = new Point(1, 0);

      // Explore all paths
      RecursiveBacktrack(start, startDir, end, 0, new HashSet<(Point, Point)>());

      foreach (var score in allScores.OrderBy(s => s))
      {
        Debug.WriteLine($"  Score: {score}");
      }

      return allScores.Count > 0 ? allScores.Min() : -1;
    }

    
    private void RecursiveBacktrack(
      Point pos,
      Point dir,
      Point end,
      int currentScore,
      HashSet<(Point, Point)> visited)
    {
      callDepth++;
      string indent = new string(' ', callDepth * 2);

      string dirName = GetDirectionName(dir);
      Debug.WriteLine($"{indent}→ pos=({pos.X},{pos.Y}), dir={dirName}, score={currentScore}");

      // BASE CASE: Reached the end!
      if (pos == end)
      {
        allScores.Add(currentScore);
        Debug.WriteLine($"{indent}  ✓✓✓ REACHED END! Score={currentScore} ✓✓✓");
        callDepth--;
        return;
      }

      var state = (pos, dir);

      // Check if already visited (prevent cycles)
      if (visited.Contains(state))
      {
        Debug.WriteLine($"{indent}  ✗ Already visited, skip");
        callDepth--;
        return;
      }

      // Mark as visited
      visited.Add(state);
      Debug.WriteLine($"{indent}  Marked as visited");

      // OPTION 1: Move forward in current direction
      Point nextPos = new Point(pos.X + dir.X, pos.Y + dir.Y);
      if (Grid.TryGetValue(nextPos, out char cell) && cell != '#')
      {
        Debug.WriteLine($"{indent}  Option 1: Move forward to ({nextPos.X},{nextPos.Y}) (score +1)");
        RecursiveBacktrack(nextPos, dir, end, currentScore + 1, visited);
      }
      else
      {
        Debug.WriteLine($"{indent}  Option 1: Forward BLOCKED");
      }

      // OPTION 2: Turn left 90° (counterclockwise)
      // East(1,0) → North(0,-1), North(0,-1) → West(-1,0), etc.
      Point leftDir = new Point(-dir.Y, dir.X);
      string leftDirName = GetDirectionName(leftDir);
      Debug.WriteLine($"{indent}  Option 2: Turn left to {leftDirName} (score +1000)");
      RecursiveBacktrack(pos, leftDir, end, currentScore + 1000, visited);

      // OPTION 3: Turn right 90° (clockwise)
      // East(1,0) → South(0,1), South(0,1) → West(-1,0), etc.
      Point rightDir = new Point(dir.Y, -dir.X);
      string rightDirName = GetDirectionName(rightDir);
      Debug.WriteLine($"{indent}  Option 3: Turn right to {rightDirName} (score +1000)");
      RecursiveBacktrack(pos, rightDir, end, currentScore + 1000, visited);

      // BACKTRACK: Unmark as visited to allow other paths
      visited.Remove(state);
      Debug.WriteLine($"{indent}  ← Backtrack (unmarked) {state.pos} : {state.dir}");

      callDepth--;
    }

    private string GetDirectionName(Point dir)
    {
      if (dir.X == 1 && dir.Y == 0) return "East→(1,0)";
      if (dir.X == -1 && dir.Y == 0) return "West←(-1,0)";
      if (dir.X == 0 && dir.Y == 1) return "South↓(0,1)";
      if (dir.X == 0 && dir.Y == -1) return "North↑(0,-1)";
      return $"({dir.X},{dir.Y})";
    }
    */


  }
}