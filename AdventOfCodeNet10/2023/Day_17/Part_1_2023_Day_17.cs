using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using Point = AdventOfCodeNet10.Extensions.Point;
namespace AdventOfCodeNet10._2023.Day_17
{
  using state = (Point position, Point direction, int stepCount);
  internal class Part_1_2023_Day_17 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/17
    The lava starts flowing rapidly once the Lava Production Facility is
    operational. As you leave, the reindeer offers you a parachute, allowing you to
    quickly reach Gear Island.
    
    As you descend, your bird's-eye view of Gear Island reveals why you had trouble
    finding anyone on your way up: half of Gear Island is empty, but the half below
    you is a giant factory city!
    
    You land near the gradually-filling pool of lava at the base of your new
    lavafall. Lavaducts will eventually carry the lava throughout the city, but to
    make use of it immediately, Elves are loading it into large crucibles on wheels.
    
    The crucibles are top-heavy and pushed by hand. Unfortunately, the crucibles
    become very difficult to steer at high speeds, and so it can be hard to go in a
    straight line for very long.
    
    To get Desert Island the machine parts it needs as soon as possible, you'll
    need to find the best way to get the crucible from the lava pool to the machine
    parts factory. To do this, you need to minimize heat loss while choosing a
    route that doesn't require the crucible to go in a straight line for too long.
    
    Fortunately, the Elves here have a map (your puzzle input) that uses traffic
    patterns, ambient temperature, and hundreds of other parameters to calculate
    exactly how much heat loss can be expected for a crucible entering any
    particular city block.
    
    For example:
    
    2413432311323
    3215453535623
    3255245654254
    3446585845452
    4546657867536
    1438598798454
    4457876987766
    3637877979653
    4654967986887
    4564679986453
    1224686865563
    2546548887735
    4322674655533
    Each city block is marked by a single digit that represents the amount of heat
    loss if the crucible enters that block. The starting point, the lava pool, is
    the top-left city block; the destination, the machine parts factory, is the
    bottom-right city block. (Because you already start in the top-left block, you
    don't incur that block's heat loss unless you leave that block and then return
    to it.)
    
    Because it is difficult to keep the top-heavy crucible going in a straight line
    for very long, it can move at most three blocks in a single direction before it
    must turn 90 degrees left or right. The crucible also can't reverse direction;
    after entering each city block, it may only turn left, continue straight, or
    turn right.
    
    One way to minimize heat loss is this path:
    
    2>>34^>>>1323
    32v>>>35v5623
    32552456v>>54
    3446585845v52
    4546657867v>6
    14385987984v4
    44578769877v6
    36378779796v>
    465496798688v
    456467998645v
    12246868655<v
    25465488877v5
    43226746555v>
    This path never moves more than three consecutive blocks in the same direction
    and incurs a heat loss of only 102.
    
    Directing the crucible from the lava pool to the machine parts factory, but not
    moving more than three consecutive blocks in the same direction, what is the
    least heat loss it can incur?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
    Dictionary<Point, char> Grid = new();
    HashSet<int> energyLoss = new();
    int Rows = 0;
    int Cols = 0;
    Point EndPos = new();
    Point StartPos = (0,0);
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      Grid.Clear();
      energyLoss.Clear();
      int rowIdx = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_17.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_17.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        Cols = line.Length;
        int colIdx = 0;
        foreach (var ch in line)
        {
          Grid[new Point(colIdx, rowIdx)] = ch;
          colIdx++;
        }
        rowIdx++;
      }
      Rows = rowIdx;
      EndPos = (Cols - 1, Rows - 1);

      ExplorePaths(new Point(0, 0), new Point(1, 0)); // Start going right
      ExplorePaths(new Point(0, 0), new Point(0, 1)); // Start going down

      totalCount = energyLoss.Min();
      result = totalCount.ToString();
      return result;
    }


    private void ExplorePaths(Point startPos, Point currentDirection)
    {
      var pq = new PriorityQueue<state, int>();
      var visited = new HashSet<state>();

      pq.Enqueue((startPos, currentDirection, 0), 0);

      while(pq.Count > 0)
      {
        pq.TryDequeue(out var currentState, out var currentEnergyloss);

        if(!Grid.ContainsKey(currentState.position))
        {
          continue;
        }
        if(visited.Contains(currentState))
        {
          continue;
        }
        visited.Add(currentState);

        //calc cell cost
        var currentCellCost = Grid[currentState.position] - '0';
        if(currentState.position == StartPos && currentState.stepCount == 0)
        {
          currentCellCost = 0;
        }
        var newEnergyLoss = currentEnergyloss + currentCellCost;

        if (currentState.position == EndPos)
        {
          //found the minimum value so return this value
          energyLoss.Add(newEnergyLoss);
          return;
        }

        //Option 1 : go straight
        if(currentState.stepCount < 3)
        {
          var newPos = currentState.position + currentState.direction;
          var newStepCount = currentState.stepCount + 1;
          var newState = (newPos, currentState.direction, newStepCount);
          pq.Enqueue(newState, newEnergyLoss);
        }

        //Option 2: go left
        var leftDir = new Point(currentState.direction.Y, -currentState.direction.X);
        var leftPos = currentState.position + leftDir;
        var leftNewState = (leftPos, leftDir, 1);
        pq.Enqueue(leftNewState, newEnergyLoss);

        //Option 3: go right
        var rightDir = new Point(-currentState.direction.Y, currentState.direction.X);
        var rightPos = currentState.position + rightDir;
        var rightNewState = (rightPos, rightDir, 1);
        pq.Enqueue(rightNewState, newEnergyLoss);
      }

    }
  }
}
