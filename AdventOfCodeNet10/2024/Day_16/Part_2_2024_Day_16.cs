using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using Point = AdventOfCodeNet10.Extensions.Point;

namespace AdventOfCodeNet10._2024.Day_16
{
  internal class Part_2_2024_Day_16 : Days
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

    public int FindLowestScoreDijkstraWithQueueDisplay(Point start, Point end)
    {
      var pq = new PriorityQueue<(Point pos, Point dir, int score), int>();
      var visited = new Dictionary<(Point pos, Point dir), int>();

      Point startDir = new Point(1, 0);
      pq.Enqueue((start, startDir, 0), 0);

      Debug.WriteLine("=== DIJKSTRA'S ALGORITHM START ===\n");

      int iteration = 0;

      while (pq.Count > 0)
      {
        iteration++;
        Debug.WriteLine($"═══════════════════════════════════════════════════════════");
        Debug.WriteLine($"ITERATION {iteration} - Queue has {pq.Count} items");
        Debug.WriteLine($"═══════════════════════════════════════════════════════════");

        // Create a temporary copy to show queue contents
        var queueCopy = new List<(Point pos, Point dir, int score)>();
        var tempPq = new PriorityQueue<(Point pos, Point dir, int score), int>();

        // Note: This drains the queue, so we need to rebuild it
        while (pq.Count > 0)
        {
          var item = pq.Dequeue();
          queueCopy.Add(item);
        }

        // Rebuild the queue
        foreach (var item in queueCopy)
        {
          pq.Enqueue(item, item.score);
        }

        Debug.WriteLine("Queue contents (sorted by priority):");
        for (int i = 0; i < Math.Min(queueCopy.Count, 5); i++) // Show top 5
        {
          var item = queueCopy[i];
          Debug.WriteLine($"  {i + 1}. pos=({item.pos.X},{item.pos.Y}), dir={GetDirectionName(item.dir)}, score={item.score}");
        }
        if (queueCopy.Count > 5)
        {
          Debug.WriteLine($"  ... and {queueCopy.Count - 5} more items");
        }
        Debug.WriteLine("");

        var (pos, dir, score) = pq.Dequeue();
        Debug.WriteLine($"→ PROCESSING: pos=({pos.X},{pos.Y}), dir={GetDirectionName(dir)}, score={score}");

        if (pos == end)
        {
          Debug.WriteLine($"\n✓✓✓ REACHED END! Final Score: {score} ✓✓✓");
          return score;
        }

        var state = (pos, dir);
        if (visited.TryGetValue(state, out int prevScore) && prevScore <= score)
        {
          Debug.WriteLine($"  → SKIPPED (visited with score {prevScore})\n");
          continue;
        }

        visited[state] = score;
        Debug.WriteLine($"  → MARKED as visited (states visited: {visited.Count})");
        Debug.WriteLine($"  → Exploring options:");

        // Rest of the code...
        Point nextPos = pos + dir;
        if (Grid.TryGetValue(nextPos, out char cell) && cell != '#')
        {
          pq.Enqueue((nextPos, dir, score + 1), score + 1);
          Debug.WriteLine($"     • Forward to ({nextPos.X},{nextPos.Y}), score={score + 1}");
        }

        Point leftDir = new Point(dir.Y, -dir.X);
        pq.Enqueue((pos, leftDir, score + 1000), score + 1000);
        Debug.WriteLine($"     • Turn left to {GetDirectionName(leftDir)}, score={score + 1000}");

        Point rightDir = new Point(-dir.Y, dir.X);
        pq.Enqueue((pos, rightDir, score + 1000), score + 1000);
        Debug.WriteLine($"     • Turn right to {GetDirectionName(rightDir)}, score={score + 1000}");

        Debug.WriteLine("");
      }

      return -1;
    }

    private string GetDirectionName(Point dir)
    {
      if (dir.X == 1 && dir.Y == 0) return "East→";
      if (dir.X == -1 && dir.Y == 0) return "West←";
      if (dir.X == 0 && dir.Y == 1) return "South↓";
      if (dir.X == 0 && dir.Y == -1) return "North↑";
      return $"({dir.X},{dir.Y})";
    }


    public int CountTilesOnOptimalPaths(Point start, Point end)
    {
      // Step 1: Find minimum score
      int minScore = FindLowestScoreDijkstra(start, end);
      Debug.WriteLine($"Minimum score: {minScore}");

      // Step 2: Get distances from Start to all states
      var distFromStart = GetAllDistances(start, new Point(1, 0)); // Start facing East

      // Step 3: Get distances from End to all states (backward)
      var distToEnd = new Dictionary<(Point, Point), int>();

      // Try all possible ending directions
      foreach (var endDir in new[] {
        new Point(1, 0),   // East
        new Point(-1, 0),  // West
        new Point(0, 1),   // South
        new Point(0, -1)   // North
      })
      {
        var distances = GetAllDistancesReverse(end, endDir);
        foreach (var kvp in distances)
        {
          if (!distToEnd.ContainsKey(kvp.Key) || kvp.Value < distToEnd[kvp.Key])
          {
            distToEnd[kvp.Key] = kvp.Value;
          }
        }
      }

      // Step 4: Find all tiles where distFromStart + distToEnd = minScore
      var tilesOnPath = new HashSet<Point>();

      foreach (var tile in Grid.Keys)
      {
        if (Grid[tile] == '#') continue; // Skip walls

        // Try all 4 directions for this tile
        foreach (var dir in new[] {
          new Point(1, 0), new Point(-1, 0),
          new Point(0, 1), new Point(0, -1)
        })
        {
          var state = (tile, dir);

          if (distFromStart.TryGetValue(state, out int fromStart) &&
              distToEnd.TryGetValue(state, out int toEnd))
          {
            if (fromStart + toEnd == minScore)
            {
              tilesOnPath.Add(tile);
              break; // No need to check other directions for this tile
            }
          }
        }
      }

      // Visualize
      Debug.WriteLine("\n=== Tiles on Optimal Paths ===");
      for (int y = 0; y < RowSize; y++)
      {
        for (int x = 0; x < ColumnSize; x++)
        {
          Point p = new Point(x, y);
          if (tilesOnPath.Contains(p))
          {
            Debug.Write('O');
          }
          else
          {
            Debug.Write(Grid[p]);
          }
        }
        Debug.WriteLine("");
      }

      return tilesOnPath.Count;
    }

    // Get distances from a starting point to all reachable states
    private Dictionary<(Point pos, Point dir), int> GetAllDistances(Point start, Point startDir)
    {
      var distances = new Dictionary<(Point pos, Point dir), int>();
      var pq = new PriorityQueue<(Point pos, Point dir, int score), int>();

      pq.Enqueue((start, startDir, 0), 0);
      distances[(start, startDir)] = 0;

      while (pq.Count > 0)
      {
        var (pos, dir, score) = pq.Dequeue();
        var state = (pos, dir);

        if (distances.TryGetValue(state, out int best) && score > best)
        {
          continue;
        }

        // Move forward
        Point nextPos = pos + dir;
        if (Grid.TryGetValue(nextPos, out char cell) && cell != '#')
        {
          var nextState = (nextPos, dir);
          int newScore = score + 1;

          if (!distances.TryGetValue(nextState, out int existing) || newScore < existing)
          {
            distances[nextState] = newScore;
            pq.Enqueue((nextPos, dir, newScore), newScore);
          }
        }

        // Turn left
        Point leftDir = new Point(dir.Y, -dir.X);
        var leftState = (pos, leftDir);
        int leftScore = score + 1000;

        if (!distances.TryGetValue(leftState, out int existingLeft) || leftScore < existingLeft)
        {
          distances[leftState] = leftScore;
          pq.Enqueue((pos, leftDir, leftScore), leftScore);
        }

        // Turn right
        Point rightDir = new Point(-dir.Y, dir.X);
        var rightState = (pos, rightDir);
        int rightScore = score + 1000;

        if (!distances.TryGetValue(rightState, out int existingRight) || rightScore < existingRight)
        {
          distances[rightState] = rightScore;
          pq.Enqueue((pos, rightDir, rightScore), rightScore);
        }
      }

      return distances;
    }

    // Get distances BACKWARD from end (reverse direction movement)
    private Dictionary<(Point pos, Point dir), int> GetAllDistancesReverse(Point start, Point startDir)
    {
      var distances = new Dictionary<(Point pos, Point dir), int>();
      var pq = new PriorityQueue<(Point pos, Point dir, int score), int>();

      pq.Enqueue((start, startDir, 0), 0);
      distances[(start, startDir)] = 0;

      while (pq.Count > 0)
      {
        var (pos, dir, score) = pq.Dequeue();
        var state = (pos, dir);

        if (distances.TryGetValue(state, out int best) && score > best)
        {
          continue;
        }

        // Move BACKWARD (opposite of direction)
        Point prevPos = new Point(pos.X - dir.X, pos.Y - dir.Y);
        if (Grid.TryGetValue(prevPos, out char cell) && cell != '#')
        {
          var prevState = (prevPos, dir);
          int newScore = score + 1;

          if (!distances.TryGetValue(prevState, out int existing) || newScore < existing)
          {
            distances[prevState] = newScore;
            pq.Enqueue((prevPos, dir, newScore), newScore);
          }
        }

        // Turn left
        Point leftDir = new Point(dir.Y, -dir.X);
        var leftState = (pos, leftDir);
        int leftScore = score + 1000;

        if (!distances.TryGetValue(leftState, out int existingLeft) || leftScore < existingLeft)
        {
          distances[leftState] = leftScore;
          pq.Enqueue((pos, leftDir, leftScore), leftScore);
        }

        // Turn right
        Point rightDir = new Point(-dir.Y, dir.X);
        var rightState = (pos, rightDir);
        int rightScore = score + 1000;

        if (!distances.TryGetValue(rightState, out int existingRight) || rightScore < existingRight)
        {
          distances[rightState] = rightScore;
          pq.Enqueue((pos, rightDir, rightScore), rightScore);
        }
      }

      return distances;
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
